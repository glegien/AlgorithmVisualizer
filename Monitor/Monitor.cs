using System.Collections.Generic;
using System.ComponentModel.Composition;
using Algorithm;
using IOManager;

namespace Monitor
{
    public class Monitor : IMonitor, ICalculationResultListener
    {
        #region Private Constants

        private const double StartingMaximumCalculationAccuracy = 1.0;

        #endregion

        #region Private Member Variables

        private readonly IList<ICalculationResult> _calculationResults; 
        private readonly IAlgorithm _algorithm;
        private readonly IIoManager _ioManager;

        #endregion

        #region Private Helper Methods

        private void FillCalculationResultsWithAlgorithmsInitiallCollection()
        {
            foreach (var calculationResult in _algorithm.CalculationResults)
            {
                _calculationResults.Add(calculationResult);
            }
        }

        #endregion

        #region Constructors

        [ImportingConstructor]
        public Monitor(IAlgorithm algorithm, IIoManager ioManager)
        {
            AlgorithmStoppingThreshold = StartingMaximumCalculationAccuracy;

            _calculationResults = new List<ICalculationResult>();

            _algorithm = algorithm;
            _ioManager = ioManager;
        }

        #endregion

        #region IMonitor Implementation

        public void Start()
        {
            _algorithm.RegisterCalculationResultListener(this);
            FillCalculationResultsWithAlgorithmsInitiallCollection();
        }

        public double AlgorithmStoppingThreshold { get; set; }

        public IEnumerable<ICalculationResult> CalculationResults { get { return _calculationResults; } }

        #endregion

        #region ICalculationResultListener Implementation

        public void NewCalculationResult(ICalculationResult calculationResult)
        {
            if (calculationResult.Accuracy > AlgorithmStoppingThreshold)
            {
                _ioManager.StopAlgorithm();
            }

            _calculationResults.Add(calculationResult);
        }

        #endregion
    }
}
