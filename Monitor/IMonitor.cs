using System.Collections.Generic;
using System.ComponentModel.Composition;
using Algorithm;

namespace Monitor
{
    [InheritedExport]
    public interface IMonitor
    {
        /// <summary>
        /// Starts the Monitor module
        /// </summary>
        void Start();

        /// <summary>
        /// Calculation accuracy that will trigger algorithm stop
        /// </summary>
        double AlgorithmStoppingThreshold { get; set; }

        /// <summary>
        /// All calculation results from the Algorihtm
        /// </summary>
        IEnumerable<ICalculationResult> CalculationResults { get; }
    }
}
