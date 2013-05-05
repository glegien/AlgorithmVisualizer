namespace Algorithm
{
    /// <summary>
    /// Register this class to algorithm so as to receive calculation results
    /// </summary>
    public interface ICalculationResultListener
    {
        /// <summary>
        /// This method is invoked every time the algorithm calculates new result
        /// </summary>
        /// <param name="calculationResult">Result of latest calculation</param>
        void NewCalculationResult(ICalculationResult calculationResult);
    }
}
