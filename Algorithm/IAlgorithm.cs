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
        /// Starts algorithm 
        /// </summary>
        /// <param name="inputData">Input data for algorithm</param>
        void Start(string inputData);

        /// <summary>
        /// Ends algorithm
        /// </summary>
        void Stop();

        /// <summary>
        /// Pause algotithm in its current state
        /// </summary>
        void Pause();

        /// <summary>
        /// Resumes algorithm operations from paused state 
        /// </summary>
        void Continue();

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
