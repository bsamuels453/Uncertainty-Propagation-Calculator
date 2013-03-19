using System;

namespace Uncertainty_Propagation_Calculator{
    internal class LatexConverter{
        public static string ToLatex(string expression, string fontSize = "Normal") {
            //convert fractions
            int pos;
            while ((pos = expression.IndexOf('/')) != -1) {
                var right = ExpressionManip.SplitEquation(expression, pos, ExpressionManip.SplitType.RightSide);
                var left = ExpressionManip.SplitEquation(expression, pos, ExpressionManip.SplitType.LeftSide);

                expression = expression.Remove(left.StartIndex, right.EndIndex - left.StartIndex);
                expression = expression.Insert(left.StartIndex, "\\frac{" + left.Segment + "}{" + right.Segment + "}");
            }

            //convert math symbols
            expression = expression.Replace("ρ", @"\rho ");
            expression = expression.Replace("π", @"\pi ");
            expression = expression.Replace("Ω", @"\Omega ");
            expression = expression.Replace("δ", @"\delta ");
            expression = expression.Replace("Δ", @"\Delta ");
            expression = expression.Replace("α", @"\alpha ");
            expression = expression.Replace("μ", @"\mu ");
            expression = expression.Replace("ε", @"\epsilon ");
            expression = expression.Replace("θ", @"\theta ");
            expression = expression.Replace("κ", @"\kappa ");
            expression = expression.Replace("σ", @"\sigma ");
            expression = expression.Replace("β", @"\beta ");
            expression = expression.Replace("γ", @"\gamma ");
            expression = expression.Replace("η", @"\eta ");
            expression = expression.Replace("λ", @"\lambda ");
            expression = expression.Replace("ν", @"\nu ");
            expression = expression.Replace("ξ", @"\xi ");
            expression = expression.Replace("τ", @"\tau ");
            expression = expression.Replace("υ", @"\upsilon ");
            expression = expression.Replace("φ", @"\phi ");
            expression = expression.Replace("ψ", @"\psi ");
            expression = expression.Replace("ω", @"\omega ");

            string prefix;
            const string suffix = "}$";

            //todo: enum this
            switch (fontSize) {
                case "Tiny":
                    prefix = @"\tiny ${ ";
                    break;
                case "Script Size":
                    prefix = @"\scriptsize ${ ";
                    break;
                case "Footnote Size":
                    prefix = @"\footnotesize ${ ";
                    break;

                case "Small":
                    prefix = @"\small ${ ";
                    break;

                case "Normal":
                    prefix = @"\normalsize ${ ";
                    break;
                case "large":
                    prefix = @"\large ${ ";
                    break;

                case "Large":
                    prefix = @"\Large ${ ";
                    break;

                case "LARGE":
                    prefix = @"\LARGE ${ ";
                    break;
                case "huge":
                    prefix = @"\huge ${ ";
                    break;
                case "Huge":
                    prefix = @"\Huge ${ ";
                    break;
                default:
                    prefix = "${";
                    break;
            }
            return prefix + expression + suffix;
        }
    }
}