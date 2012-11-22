#region

using System.Linq;

#endregion

namespace Uncertainty_Propagation_Calculator{
    internal class LibreMathConverter{
        //xxxtodo: replace the internals of this method with stuff that calls the ExpressionManip helper class
        public static string EquationToLibre(string equation){
            int divPos;
            while ((divPos = equation.IndexOf('/')) != -1){
                bool leftSideEnclosed = equation[divPos - 1] == ')';
                bool rightSideEnclosed = equation[divPos + 1] == '(';
                bool terminatedByBracket = false;
                int bracketReq;

                bracketReq = 0;
                int openingBrackets = 0;
                int closingBracketPos = -1;
                for (int i = divPos + 1; i < equation.Count(); i++){
                    switch (equation[i]){
                        case '(':
                            openingBrackets++;
                            break;
                        case ')':
                            openingBrackets--;
                            if (openingBrackets < bracketReq){
                                closingBracketPos = i;
                                terminatedByBracket = true;
                            }
                            break;
                        case '=':
                            closingBracketPos = i + 1;
                            break;
                        default:
                            if (!rightSideEnclosed){
                                if (equation[i] == '+' ||
                                    equation[i] == '-' ||
                                    equation[i] == '*' ||
                                    equation[i] == '/'
                                    ){
                                    closingBracketPos = i;
                                    break;
                                }
                            }
                            break;
                    }

                    if (closingBracketPos != -1){
                        break;
                    }

                    if (i == equation.Count() - 1){
                        closingBracketPos = i + 1; //xxx
                    }
                }

                if (terminatedByBracket){
                    equation = equation.Remove(closingBracketPos, 1);
                }
                equation = equation.Insert(closingBracketPos, "}"); //okay


                if (rightSideEnclosed){
                    equation = equation.Remove(divPos + 1, 1);
                }
                equation = equation.Insert(divPos + 1, "{");

                equation = equation.Remove(divPos, 1); //okay
                equation = equation.Insert(divPos, " over "); //okay

                if (leftSideEnclosed){
                    equation = equation.Remove(divPos - 1, 1);
                    divPos--;
                }

                equation = equation.Insert(divPos, "}"); //okay


                terminatedByBracket = false;
                bracketReq = 0;
                int closingBrackets = 0;
                int openingBracketPos = -1;
                for (int i = divPos - 1; i >= 0; i--){
                    switch (equation[i]){
                        case '(':
                            closingBrackets--;
                            if (closingBrackets < bracketReq){ //xxx?
                                openingBracketPos = i;
                                terminatedByBracket = true;
                            }
                            break;
                        case ')':
                            closingBrackets++;
                            break;
                        case '=':
                            openingBracketPos = i + 1;
                            break;
                    }

                    if (openingBracketPos != -1){
                        break;
                    }
                    if (i == 0){
                        openingBracketPos = i;
                    }
                }
                if (terminatedByBracket){
                    equation = equation.Remove(openingBracketPos, 1);
                }
                equation = equation.Insert(openingBracketPos, "{");
            }
            return equation;
        }
    }
}