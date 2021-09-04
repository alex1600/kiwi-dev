namespace Nostrum
{
    public static class MathUtils
    {
        /// <summary>
        /// Maps a 0-1 value to a 0-360 angle. The result can be scaled by using the multiplier argument.
        /// </summary>
        /// <param name="value">the 0-1 input value</param>
        /// <param name="multiplier">the multiplier</param>
        /// <returns>the mapped angle value in degrees</returns>
        public static double FactorToAngle(double value, double multiplier = 1)
        {
            return value * (359.9 / multiplier);
        }

        /// <summary>
        /// Calculates a 0-1 value by dividing the input value for the max value. Result is capped at 1.
        /// </summary>
        /// <param name="input">the input value</param>
        /// <param name="max">the max value</param>
        /// <returns>input/max if max is greater than 0; 1 if the result if greater than 1</returns>
        public static double FactorCalc(double input, double max)
        {
            return max > 0
                ? input / max > 1
                    ? 1
                    : input / max
                : 1;
        }

        /// <summary>
        /// The standard sigmoid function.
        /// </summary>
        /// <param name="xValue">The input value.</param>
        /// <returns>The calculated output.</returns>
        public static double SigmoidFunction(double xValue)
        {
            if (xValue > 10) return 1.0;
            else if (xValue < -10) return 0.0;
            else return 1.0 / (1.0 + Math.Exp(-xValue));
        }

        /// <summary>
        /// The standard TanH function.
        /// </summary>
        /// <param name="xValue">The input value.</param>
        /// <returns>The calculated output.</returns>
        public static double TanHFunction(double xValue)
        {
            if (xValue > 10) return 1.0;
            else if (xValue < -10) return -1.0;
            else return Math.Tanh(xValue);
        }

        /// <summary>
        /// The SoftSign function as proposed by Xavier Glorot and Yoshua Bengio (2010): 
        /// "Understanding the difficulty of training deep feedforward neural networks".
        /// </summary>
        /// <param name="xValue">The input value.</param>
        /// <returns>The calculated output.</returns>
        public static double SoftSignFunction(double xValue)
        {
            return xValue / (1 + Math.Abs(xValue));
    }   
    }
}