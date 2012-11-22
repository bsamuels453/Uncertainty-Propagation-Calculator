#region

using System;
using System.Collections.Generic;
using System.Linq;

#endregion

namespace Uncertainty_Propagation_Calculator{
    /// <summary>
    ///   helper class for the manipulation of equations stored in strings
    /// </summary>
    internal static class ExpressionManip{
        #region SplitType enum

        /// <summary>
        ///   Used to specify how to split an equation, with respect to order of operations. Since exponents have a slightly different order of operations, they are separate from normal splitting operations. For the curious, this manifests in the expression 5*6^2. If one wanted to get the expression on the left side of exponent using the normal LeftSide rule, the split method owuld return "5*6". Using the ExponentLeft splitting type, only "6" is returned.
        /// </summary>
        public enum SplitType{
            RightSide,
            LeftSide,
            ExponentRight,
            ExponentLeft
        }

        #endregion

        public static EquationSegment SplitEquation(string equation, int splitIndex, SplitType sideToGet){
            var retStruct = new EquationSegment();
            char beginningBracket;
            char endingBracket;
            int increment;
            int limitValue;

            if (sideToGet == SplitType.LeftSide || sideToGet == SplitType.ExponentLeft){
                beginningBracket = ')';
                endingBracket = '(';
                increment = -1;
                limitValue = -1;
            }
            else{
                beginningBracket = '(';
                endingBracket = ')';
                increment = 1;
                limitValue = equation.Count();
            }


            //the left side is enclosed with brackets, so the only way to terminate the left side is with an opening bracket
            if (equation[splitIndex + increment] == beginningBracket){
                int totalBrackets = 1;
                int index = splitIndex + increment;
                while (totalBrackets != 0){
                    index += increment;
                    if (index == limitValue)
                        throw new Exception("formatting error, brackets not set up properly");

                    if (equation[index] == beginningBracket){
                        totalBrackets++;
                    }
                    if (equation[index] == endingBracket){
                        totalBrackets--;
                    }
                }


                if (sideToGet == SplitType.LeftSide || sideToGet == SplitType.ExponentLeft){
                    retStruct.EndIndex = splitIndex;
                    retStruct.StartIndex = index;
                    retStruct.Segment = equation.Substring(retStruct.StartIndex, retStruct.EndIndex - retStruct.StartIndex);
                }
                else{
                    retStruct.EndIndex = index + 1;
                    retStruct.StartIndex = splitIndex + 1;
                    retStruct.Segment = equation.Substring(retStruct.StartIndex, retStruct.EndIndex - retStruct.StartIndex);
                }
                return retStruct;
            }
            else{
                //for this case, order of operations must be respected because of lack of brackets
                //there are two ways for a termination to occur, either one of the terminating characters is reached
                //or an unexpected endingbracket is reached, such as in the case (x/y)

                int index = splitIndex + increment;

                char[] terminatingChars = new char[0];
                bool ignoreCharacterTermination = false;
                bool ignoreBracketTermination = false;
                if (sideToGet == SplitType.LeftSide){
                    terminatingChars = new[]{'=', '+', '-'};
                }
                if (sideToGet == SplitType.RightSide){
                    terminatingChars = new[]{'=', '+', '-', '*', '/'};
                }
                if (sideToGet == SplitType.ExponentRight){
                    terminatingChars = new[]{'=', '+', '-', '*', '/'};
                    if (equation[index] == beginningBracket){ //this expression may be always false
                        ignoreCharacterTermination = true;
                    }
                    else{
                        index++; //we want to completely ignore the first character because of negatives
                        ignoreBracketTermination = true;
                    }
                }
                if (sideToGet == SplitType.ExponentLeft){
                    terminatingChars = new[]{'=', '+', '-', '*', '/'};
                }

                int totalBrackets = 0;
                while (true){
                    //first try character based termination
                    if (terminatingChars.Contains(equation[index])){
                        if (!ignoreCharacterTermination){
                            break;
                        }
                    }

                    //now try bracket based termination
                    if (equation[index] == beginningBracket){
                        if (!ignoreBracketTermination){
                            totalBrackets++;
                        }
                    }

                    if (equation[index] == endingBracket){
                        if (!ignoreBracketTermination){
                            totalBrackets--;
                        }
                    }

                    if (totalBrackets < 0){
                        //index -= increment;
                        break;
                    }

                    //nothing interesting, loop again
                    if (index == limitValue - increment){
                        index += increment;
                        break;
                    }
                    //firstCharacterException = false;
                    index += increment;
                }
                if (sideToGet == SplitType.LeftSide || sideToGet == SplitType.ExponentLeft){
                    retStruct.EndIndex = splitIndex;
                    retStruct.StartIndex = index + 1;
                    retStruct.Segment = equation.Substring(retStruct.StartIndex, retStruct.EndIndex - retStruct.StartIndex);
                }
                else{
                    retStruct.EndIndex = index;
                    retStruct.StartIndex = splitIndex + 1;
                    retStruct.Segment = equation.Substring(retStruct.StartIndex, retStruct.EndIndex - retStruct.StartIndex);
                }
                return retStruct;
            }
        }

        /// <summary>
        ///   Takes an equation and converts any integer-constants to floating constants. For example, converts "y=2*x+5" to "y=2.0*x+5.0"
        /// </summary>
        /// <param name="equation"> </param>
        /// <returns> </returns>
        public static string ConvertEquationIntsToFloats(string equation){
            bool hasDecimal = false;
            for (int si = 1; si < equation.Count(); si++){
                if (char.IsDigit(equation[si]))
                    continue;

                if (equation[si - 1] == ')' ||
                    equation[si - 1] == '(' ||
                    equation[si - 1] == '*' ||
                    equation[si - 1] == '+' ||
                    equation[si - 1] == '-' ||
                    equation[si - 1] == '/' ||
                    char.IsLetter(equation[si - 1]) ||
                    equation[si - 1] == '^'
                    )
                    continue;

                if (equation[si] == '.'){
                    hasDecimal = true;
                    continue;
                }

                if (hasDecimal){
                    hasDecimal = false;
                    continue;
                }

                equation = equation.Insert(si, ".0");
                hasDecimal = true;
            }
            if (char.IsDigit(equation.Last()) && !hasDecimal){
                equation = equation.Insert(equation.Count(), ".0");
            }
            return equation;
        }

        /// <summary>
        ///   Converts a set of symbols (variables) into the provided aliases. Symbol and alias names are provided through Dictionary(string,string), where the key represents the symbol and the value represents its alias.
        /// </summary>
        /// <param name="symbolAliases"> </param>
        /// <param name="equation"> </param>
        /// <returns> </returns>
        public static string ConvertSymbolsIntoAliases(Dictionary<string, string> symbolAliases, string equation){
            //generate the equation with all of our new variable names
            var aliasEquation = (string) equation.Clone();

            //todo: get rid of 3 character limitation on symbol names
            for (int i = 0; i < aliasEquation.Count(); i++){
                int splitLen = 3;
                if (i + 3 > aliasEquation.Count()){
                    splitLen = aliasEquation.Count() - i;
                }
                string split = aliasEquation.Substring(i, splitLen);

                foreach (var reference in symbolAliases){
                    if (split.Contains(reference.Key)){
                        split = split.Replace(reference.Key, reference.Value);
                        aliasEquation = aliasEquation.Remove(i, splitLen);
                        aliasEquation = aliasEquation.Insert(i, split);
                        break;
                    }
                }
            }

            //sanity check
            foreach (var reference in symbolAliases){
                if (reference.Value.Count() != 1){
                    throw new Exception("one of the aliases were longer than one character");
                }
                if (reference.Key == "l" || reference.Key == "o" || reference.Key == "g" || reference.Key == "n"){
                    throw new Exception("cannot use letters l, o, g, or n");
                }
            }
            return aliasEquation;
        }

        /// <summary>
        ///   does the opposite of the method above this one
        /// </summary>
        /// <param name="symbolAliases"> </param>
        /// <param name="equation"> </param>
        /// <returns> </returns>
        public static string ConvertAliasesIntoSymbols(Dictionary<string, string> symbolAliases, string equation){
            string str = (string) equation.Clone();

            //xxx this method is bound by the 1 character alias limit
            for (int chr = 0; chr < str.Count(); chr++){
                foreach (var reference in symbolAliases){
                    if (chr >= str.Count())
                        break;
                    if (str[chr] == reference.Value[0]){
                        str = str.Remove(chr, 1);
                        str = str.Insert(chr, reference.Key);
                        chr += reference.Key.Count();
                    }
                }
            }
            return str;
        }

        /// <summary>
        ///   This method parses all the symbols in an equation, and associates a single-letter alias to each one. Useful for formatting wolfram alpha input since it will shit itself if you give it variables that are multi-char.
        /// </summary>
        /// <param name="equation"> </param>
        /// <returns> </returns>
        public static Dictionary<string, string> GenerateEquationAliases(string equation){
            var splittableEquation = (String) equation.Clone();

            splittableEquation = splittableEquation.Replace('+', ' ');
            splittableEquation = splittableEquation.Replace('-', ' ');
            splittableEquation = splittableEquation.Replace('*', ' ');
            splittableEquation = splittableEquation.Replace('/', ' ');
            splittableEquation = splittableEquation.Replace("ln", " ");
            splittableEquation = splittableEquation.Replace("log", " ");
            splittableEquation = splittableEquation.Replace("e^", " ");
            splittableEquation = splittableEquation.Replace('^', ' ');
            splittableEquation = splittableEquation.Replace('(', ' ');
            splittableEquation = splittableEquation.Replace(')', ' ');
            //splittableEquation = splittableEquation.Replace("cos", " "); 
            //fat chance im gonna do this for all the trig functions

            var rawSymbols = splittableEquation.Split(' ');

            //parse out the extra empty spaces
            String[] symbolList = (
                                      from constant in rawSymbols
                                      where constant != ""
                                      where Char.IsDigit(constant.ToCharArray()[0]) == false
                                      where constant.Contains(".") == false
                                      select constant
                                  ).ToArray();

            //make sure that none of the symbol names contain substrings of other symbol names because 
            //that will shit up EVERYTHING when it comes time to convert between symbols and aliases
            foreach (var symbol in symbolList){
                foreach (var sconstant in symbolList){
                    if (symbol.Contains(sconstant) && symbol != sconstant){
                        throw new Exception("one of the variables substrings that can be confused as other variables");
                    }
                }
            }


            //since many variables may have partial latex used in them 
            //(like x_bot for subscript), every variable is redefined with
            //single letter variable names to prevent wolfram from shitting itself

            //the key is the original varaible name, the value is its new name
            var symbolAliases = new Dictionary<string, string>();

            //blacklist:
            //ln, log, e
            const string alphabet = "abcdfhijkmpqrstuvwxyz";

            //todo: get rid of 3 character limitation on symbol names
            for (int i = 0; i < symbolList.Count(); i++){
                if (symbolList[i].Count() > 3){
                    throw new Exception("variable names may not be longer than 3 characters");
                }
                try{
                    symbolAliases.Add(symbolList[i], alphabet[i].ToString());
                }
                catch (ArgumentException){
                }
            }
            return symbolAliases;
        }

        #region Nested type: EquationSegment

        public struct EquationSegment{
            public int EndIndex;
            public string Segment;
            public int StartIndex;
        }

        #endregion
    }
}