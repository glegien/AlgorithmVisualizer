namespace Algorithm
{
    /// <summary>
    /// Single calculation result
    /// </summary>
    public interface ICalculationResult
    {
        /// <summary>
        /// Value ranging from 0.0 to 1.0 showing the accuracy of current calculation
        /// </summary>
        double Accuracy { get; }
    }
}
