using DHS.Imaging;
using DHS.Mathematics;
using System;
using System.Text.RegularExpressions;

namespace AgroVision.Utils
{
    public class RasterUtil
    {

        public static bool IsOmittingZeros(string equation)
        {
            return Regex.IsMatch(equation, "([^0-9]|^)\\.[0-9]+") || Regex.IsMatch(equation, "([^0-9]|^),[0-9]+");
        }

        public static bool IsValidEquation(string equation)
        {
            try
            {
                Compute(equation, new BitmapColor(0, 0, 0));
            }
            catch
            {
                return false;
            }

            return true;
        }

        public static double Compute(string equation, BitmapColor color)
        {
            if (equation != null)
                try
                {
                    equation = equation.ToUpper();
                    equation = Regex.Replace(equation, @"\r?\n|\r|\s+", "");

                    equation = equation.Replace("R", color.R.ToString());
                    equation = equation.Replace("G", color.G.ToString());
                    equation = equation.Replace("B", color.B.ToString());

                    return ExpressionParser.Eval(equation);
                }
                catch
                {
                    throw new Exception("Equação inválida.");
                }

            return 0;
        }

    }
}