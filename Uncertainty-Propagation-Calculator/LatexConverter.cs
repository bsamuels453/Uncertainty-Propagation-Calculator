using System;

namespace Uncertainty_Propagation_Calculator{
    internal class LatexConverter{


        public static string ToLatex(string expression){
            //  / greek
            int pos;
            while ((pos = expression.IndexOf('/')) != -1) {
                var right = ExpressionManip.SplitEquation(expression, pos, ExpressionManip.SplitType.RightSide);
                var left = ExpressionManip.SplitEquation(expression, pos, ExpressionManip.SplitType.LeftSide);

                expression = expression.Remove(left.StartIndex, right.EndIndex - left.StartIndex);
                expression = expression.Insert(left.StartIndex, "\\frac{" + left.Segment + "}{" + right.Segment + "}");
            }
            return expression;
        }
    }
}