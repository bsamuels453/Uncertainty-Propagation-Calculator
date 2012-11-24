using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using NCalc;

namespace Uncertainty_Propagation_Calculator{
    internal static class UncertaintyCalculator{

        public static UncertCalcResults Calculate(UncertCalcInput input){
            UncertCalcResults output = new UncertCalcResults();

            string equation = input.Equation;

            //split equation into two sides
            var sides = equation.Split('=');
            var leftSide = sides[0] + "=";
            var rightSide = sides[1];

            var symbolAliases = ExpressionManip.GenerateEquationAliases(rightSide);

            string aliasEquation = ExpressionManip.ConvertSymbolsIntoAliases(symbolAliases, rightSide);

            var solver = new WolframEvaluator(input.ApiKey);

            string[] partialDerivs = new string[symbolAliases.Count];
            var dependentVariables = new char[symbolAliases.Count];

            int si = 0;
            foreach (var reference in symbolAliases) {
                partialDerivs[si] = solver.CalculatePartialDeriv(aliasEquation, reference.Value[0]);
                dependentVariables[si] = reference.Value[0];
                si++;
            }

            //wolfram alpha does this mindblowingly retarded thing where it will change scientific notation from *10^n to x10^n
            for (int i = 0; i < partialDerivs.Length; i++) {
                for (int letter = 0; letter < partialDerivs[i].Length - 2; letter++) {
                    if (Char.IsDigit(partialDerivs[i][letter]) && Char.IsDigit(partialDerivs[i][letter + 2]) && partialDerivs[i][letter + 1] == 'x') {
                        partialDerivs[i] = partialDerivs[i].Remove(letter + 1, 1);
                        partialDerivs[i] = partialDerivs[i].Insert(letter + 1, "*");
                    }
                }
            }

            for (int i = 0; i < partialDerivs.Length; i++) {
                partialDerivs[i] = "(∂/∂" + dependentVariables[i] + ")=" + partialDerivs[i];
                partialDerivs[i] = ExpressionManip.ConvertAliasesIntoSymbols(symbolAliases, partialDerivs[i]);
            }

            string[] librePartialDerivs = new string[symbolAliases.Count];

            for (int i = 0; i < symbolAliases.Count; i++) {
                librePartialDerivs[i] = LibreMathConverter.EquationToLibre(partialDerivs[i]);
            }

            output.PartialDerivs = librePartialDerivs;

            var symbolValues = new Dictionary<string, string>();
            for (int i = 0; i < input.VariableValues.Count; i++){
                symbolValues.Add(input.VariableNames[i], input.VariableValues[i].ToString());
            }

            string[] partialSolutions;

            PlugIntoEquationAndSolve(partialDerivs, symbolValues, out output.PluggedPartialDerivs, out partialSolutions);

            for (int i = 0; i < partialDerivs.Length; i++) {
                output.PluggedPartialDerivs[i] = LibreMathConverter.EquationToLibre(output.PluggedPartialDerivs[i]);
            }

            string propogationStr = "δ" + leftSide.Substring(0, leftSide.Count() - 1);
            propogationStr += "=sqrt{";

            si = 0;
            foreach (var data in input.VariableUncertainties) {
                propogationStr += "(" + partialSolutions[si] + "*" + data + ")^2+";
                si++;
            }
            propogationStr = propogationStr.Substring(0, propogationStr.Count() - 1);
            propogationStr += "}";

            var dResults = new double[partialSolutions.Count()];
            var dConstants = new double[partialSolutions.Count()];


            for (int i = 0; i < partialSolutions.Count(); i++) {
                dResults[i] = double.Parse(partialSolutions[i]);
            }


            for (int i = 0; i < partialSolutions.Count(); i++){
                dConstants[i] = double.Parse(input.VariableUncertainties[i].ToString());
            }

            /*            si = 0;
            foreach (var data in constantData) {
                dConstants[si] = double.Parse(data.Value);
                si++;
            }
             * */


            double uncertainty = 0;

            for (int i = 0; i < partialSolutions.Count(); i++) {
                uncertainty += Math.Pow(dResults[i] * dConstants[i], 2);
            }
            uncertainty = Math.Sqrt(uncertainty);

            string sUncertain = uncertainty.ToString();

            output.PropEquation ="size 16{" + propogationStr + "=" + sUncertain + "}";
            output.Result = sUncertain;
            return output;
        }

        static void PlugIntoEquationAndSolve(string[] equations, Dictionary<string,string> variableValues, out string[] pluggedEquations, out string[] solutions){
            solutions = new string[equations.Length];
            var pluggedEqs = new string[equations.Length];

            //now gonna plug values into the equations to get the results
            for (int i = 0; i < equations.Length; i++) {
                var toEval = (string)equations[i].Clone();
                int epPos = toEval.IndexOf('=');
                string leftside = toEval.Substring(0, epPos + 1);

                toEval = toEval.Substring(epPos + 1, toEval.Count() - epPos - 1);

                foreach (var pair in variableValues) {
                    toEval = toEval.Replace(pair.Key, pair.Value);
                }

                var eqToDisplay = (string)toEval.Clone();

                //go through the equation and convert everything to floating point if it isnt already
                toEval = ExpressionManip.ConvertEquationIntsToFloats(toEval);

                //now we need to convert power function (x^n) to Pow(x,n)
                //toEval = PowerConversion(toEval);
                int pos;
                while ((pos = toEval.IndexOf('^')) != -1) {
                    var right = ExpressionManip.SplitEquation(toEval, pos, ExpressionManip.SplitType.ExponentRight);
                    var left = ExpressionManip.SplitEquation(toEval, pos, ExpressionManip.SplitType.ExponentLeft);

                    toEval = toEval.Remove(left.StartIndex, right.EndIndex - left.StartIndex);
                    toEval = toEval.Insert(left.StartIndex, "Pow(" + left.Segment + "," + right.Segment + ")");
                }
                toEval = toEval.Replace("log", "&"); //placeholder character
                while ((pos = toEval.IndexOf('&')) != -1) {
                    var right = ExpressionManip.SplitEquation(toEval, pos, ExpressionManip.SplitType.RightSide);
                    var left = ExpressionManip.SplitEquation(toEval, pos, ExpressionManip.SplitType.LeftSide);

                    toEval = toEval.Remove(left.StartIndex, right.EndIndex - left.StartIndex);
                    toEval = toEval.Insert(left.StartIndex, "Log(" + left.Segment + "," + Math.E + ")");
                }


                //toEval = toEval.Replace("log", "Log10");//xxx FIX THIS SHIT FOR LOGS

                var e = new Expression(toEval, EvaluateOptions.None);
                solutions[i] = e.Evaluate().ToString();

                //FIX THIS TO USE SIGFIGS
                /*
                //seriously dont want 10 digits of mantissa
                if (results[i][results[i].Count()-1] == '.'){
                    results[i] = results[i].Substring(0, results[i].Count()-4);
                }

                //now round it
                double val = double.Parse(results[i]);
                val = Math.Round(val, 7);
                results[i] = val.ToString();
                 * */
                pluggedEqs[i] = leftside + eqToDisplay;

            }
            pluggedEquations = pluggedEqs;
        }

        public struct UncertCalcInput{
            public string ApiKey;
            public string Equation;
            public List<string> VariableNames;
            public List<double> VariableValues;
            public List<double> VariableUncertainties; 
        }

        public struct UncertCalcResults{
            public string[] PartialDerivs;
            public string[] PluggedPartialDerivs;
            public string PropEquation;
            public string Result;
        }
    }
}