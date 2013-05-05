using System;
using System.ComponentModel.Composition;
using System.Collections.Generic;

namespace Algorithm
{
    /// <summary>
    /// Use this interface to interact with algorithm
    /// </summary>
    [InheritedExport]
    public interface IAlgorithm
    {
        /// <summary>
        /// Example method for setting up projects purposes
        /// Feel free to remove / change when necessary
        /// </summary>
        void Run();

        /// <summary>
        /// Registeres a ICalculationResultListener that will be notified each time new result is calculated
        /// </summary>
        /// <exception cref="ArgumentNullException">When calculationResultListener is null</exception>
        /// <param name="calculationResultListener">CalculationResultListener to register</param>
        void RegisterCalculationResultListener(ICalculationResultListener calculationResultListener);

        /// <summary>
        /// Collection of all calculation results
        /// </summary>
        IEnumerable<ICalculationResult> CalculationResults { get; }
    }
}
