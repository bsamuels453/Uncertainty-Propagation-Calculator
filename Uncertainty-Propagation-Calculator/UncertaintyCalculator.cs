using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using NCalc;

namespace Uncertainty_Propagation_Calculator{
    internal class UncertaintyCalculator{
        public delegate void CalculationStatusUpdate(string str, bool isError);
        public delegate void OnCalculationCompletion(UncertCalcResults output);

        readonly CalculationStatusUpdate _updateEvent;
        readonly OnCalculationCompletion _onComplete;
        readonly WolframEvaluator _wolframEval;

        public UncertaintyCalculator(WolframEvaluator wolframEval, CalculationStatusUpdate callback = null, OnCalculationCompletion onCompleteCallback = null){
            _updateEvent = callback;
            _onComplete = onCompleteCallback;
            _wolframEval = wolframEval;
        }

        public void Calculate(Object stateInfo) {
            var input = (UncertCalcInput)stateInfo;
            UncertCalcResults output = new UncertCalcResults();

            string equation = input.Equation;

            //split equation into two sides
            var sides = equation.Split('=');
            var leftSide = sides[0] + "=";
            var rightSide = sides[1];

            var symbolAliases = ExpressionManip.GenerateEquationAliases(rightSide);

            string aliasEquation = ExpressionManip.ConvertSymbolsIntoAliases(symbolAliases, rightSide);

            string[] partialDerivs = new string[symbolAliases.Count];
            var dependentVariables = new char[symbolAliases.Count];

            //need to change log to log(10,x)
            //convert log to log base 10
            string wolframEquation;
            wolframEquation = aliasEquation.Replace("log", "&"); //placeholder character
            int pos;
            while ((pos = wolframEquation.IndexOf('&')) != -1){
                var right = ExpressionManip.SplitEquation(wolframEquation, pos, ExpressionManip.SplitType.RightSide);
                var left = ExpressionManip.SplitEquation(wolframEquation, pos, ExpressionManip.SplitType.LeftSide);

                wolframEquation = wolframEquation.Remove(left.StartIndex, right.EndIndex - left.StartIndex);
                wolframEquation = wolframEquation.Insert(left.StartIndex, "Log(10," + right.Segment + ")");
            }




            StatusUpdate("Beginning wolfram alpha queries");
            int si = 0;
            foreach (var reference in symbolAliases) {
                StatusUpdate("Sending query " + (si+1) + " of " + symbolAliases.Count+"...");
                partialDerivs[si] = _wolframEval.CalculatePartialDeriv(wolframEquation, reference.Value[0]);
                dependentVariables[si] = reference.Value[0];
                si++;
                StatusUpdate("Response recieved");
            }
            StatusUpdate("Finalizing calculations");

            //wolfram alpha does this mindblowingly retarded thing where it will change scientific notation from *10^n to x10^n
            for (int i = 0; i < partialDerivs.Length; i++) {
                for (int letter = 0; letter < partialDerivs[i].Length - 2; letter++) {
                    if (Char.IsDigit(partialDerivs[i][letter]) && Char.IsDigit(partialDerivs[i][letter + 2]) && partialDerivs[i][letter + 1] == 'x') {
                        partialDerivs[i] = partialDerivs[i].Remove(letter + 1, 1);
                        partialDerivs[i] = partialDerivs[i].Insert(letter + 1, "*");
                    }
                }
            }

            //when wolfram alpha returns "log", it actually means "ln"
            for (int i = 0; i < partialDerivs.Length; i++) { 
                partialDerivs[i] = partialDerivs[i].Replace("log", "ln");
            }

            for (int i = 0; i < partialDerivs.Length; i++) {
                partialDerivs[i] = "(∂/∂" + dependentVariables[i] + ")=" + partialDerivs[i];
                partialDerivs[i] = ExpressionManip.ConvertAliasesIntoSymbols(symbolAliases, partialDerivs[i]);
            }

            string[] librePartialDerivs = new string[symbolAliases.Count];

            for (int i = 0; i < symbolAliases.Count; i++) {
                if (input.UseLibreConverter) {
                    librePartialDerivs[i] = LibreMathConverter.EquationToLibre(partialDerivs[i]);
                }
                else{
                    librePartialDerivs[i] = LatexConverter.ToLatex(partialDerivs[i]);
                }
            }

            output.PartialDerivs = librePartialDerivs;

            var symbolValues = new Dictionary<string, string>();
            for (int i = 0; i < input.VariableValues.Count; i++){
                symbolValues.Add(input.VariableNames[i], input.VariableValues[i]);
            }

            string[] partialSolutions;

            PlugIntoEquationAndSolve(partialDerivs, symbolValues, out output.PluggedPartialDerivs, out partialSolutions);

            for (int i = 0; i < partialDerivs.Length; i++) {
                
                if (input.UseLibreConverter) {
                    output.PluggedPartialDerivs[i] = LibreMathConverter.EquationToLibre(output.PluggedPartialDerivs[i]);
                }
                else {
                    output.PluggedPartialDerivs[i] = LatexConverter.ToLatex(output.PluggedPartialDerivs[i]);
                }
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
                dConstants[i] = double.Parse(input.VariableUncertainties[i]);
            }

            double uncertainty = 0;

            for (int i = 0; i < partialSolutions.Count(); i++) {
                uncertainty += Math.Pow(dResults[i] * dConstants[i], 2);
            }
            uncertainty = Math.Sqrt(uncertainty);

            string sUncertain = uncertainty.ToString();

            if (input.UseLibreConverter) {
                output.PropEquation = "size 16{" + propogationStr + "=" + sUncertain + "}";
            }
            else{
                output.PropEquation = "{" + propogationStr + "=" + sUncertain + "}";
                output.PropEquation = output.PropEquation.Replace("δ", @"\delta ");
                output.PropEquation = output.PropEquation.Replace("∂", @"\partial ");
            }
            output.Result = sUncertain;

            for (int i = 0; i < partialSolutions.Count(); i++){
                if (input.UseLibreConverter) {
                    output.PartialDerivs[i] = "size 16{" + output.PartialDerivs[i] + "}";
                    output.PluggedPartialDerivs[i] = "size 16{" + output.PluggedPartialDerivs[i] + "}";
                }
                else {
                    output.PartialDerivs[i] = "{" + output.PartialDerivs[i] + "}";
                    output.PluggedPartialDerivs[i] = "{" + output.PluggedPartialDerivs[i] + "}";
                    output.PartialDerivs[i] = output.PartialDerivs[i].Replace("δ", @"\delta ");
                    output.PluggedPartialDerivs[i] = output.PluggedPartialDerivs[i].Replace("δ", @"\delta ");
                    output.PartialDerivs[i] = output.PartialDerivs[i].Replace("∂", @"\partial ");
                    output.PluggedPartialDerivs[i] = output.PluggedPartialDerivs[i].Replace("∂", @"\partial ");
                }

            }
            StatusUpdate("Calculation complete");
            if (_onComplete != null){
                _onComplete.Invoke(output);
            }
        }

        void PlugIntoEquationAndSolve(string[] equations, Dictionary<string,string> variableValues, out string[] pluggedEquations, out string[] solutions){
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
                //toEval = ExpressionManip.ConvertEquationIntsToFloats(toEval);

                //convert power function (^) to Pow
                int pos;
                while ((pos = toEval.IndexOf('^')) != -1) {
                    var right = ExpressionManip.SplitEquation(toEval, pos, ExpressionManip.SplitType.ExponentRight);
                    var left = ExpressionManip.SplitEquation(toEval, pos, ExpressionManip.SplitType.ExponentLeft);

                    toEval = toEval.Remove(left.StartIndex, right.EndIndex - left.StartIndex);
                    toEval = toEval.Insert(left.StartIndex, "Pow(" + left.Segment + "," + right.Segment + ")");
                }

                //convert natural log to Log base e
                toEval = toEval.Replace("ln", "&"); //placeholder character
                while ((pos = toEval.IndexOf('&')) != -1) {
                    var right = ExpressionManip.SplitEquation(toEval, pos, ExpressionManip.SplitType.RightSide);
                    var left = ExpressionManip.SplitEquation(toEval, pos, ExpressionManip.SplitType.LeftSide);

                    toEval = toEval.Remove(left.EndIndex, right.EndIndex - left.EndIndex);
                    toEval = toEval.Insert(left.EndIndex, "Log(" + right.Segment + "," + Math.E + ")");
                }

                //convert log to log base 10
                toEval = toEval.Replace("log", "&"); //placeholder character
                while ((pos = toEval.IndexOf('&')) != -1) {
                    var right = ExpressionManip.SplitEquation(toEval, pos, ExpressionManip.SplitType.RightSide);
                    var left = ExpressionManip.SplitEquation(toEval, pos, ExpressionManip.SplitType.LeftSide);

                    toEval = toEval.Remove(left.EndIndex, right.EndIndex - left.EndIndex);
                    toEval = toEval.Insert(left.EndIndex, "Log(" + right.Segment + "," + 10 + ")");
                }


                if (toEval != "") {
                    var e = new Expression(toEval, EvaluateOptions.None);
                    solutions[i] = e.Evaluate().ToString();
                }
                else{
                    solutions[i] = "0";
                }

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

        void StatusUpdate(string str, bool isError=false){
            if (_updateEvent != null){
                _updateEvent.Invoke(str, isError);
            }
        }

        public struct UncertCalcInput{
            public bool UseLibreConverter;
            public string Equation;
            public List<string> VariableNames;
            public List<string> VariableValues;
            public List<string> VariableUncertainties; 
        }

        public struct UncertCalcResults{
            public string[] PartialDerivs;
            public string[] PluggedPartialDerivs;
            public string PropEquation;
            public string Result;
        }
    }
}