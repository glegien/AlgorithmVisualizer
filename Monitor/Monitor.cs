using System.ComponentModel.Composition;
using Algorithm;

namespace Monitor
{
    public class Monitor : IMonitor
    {
        #region Private Member Variables

        private readonly IAlgorithm _algorithm;

        #endregion

        #region Constructors

        [ImportingConstructor]
        public Monitor(IAlgorithm algorithm)
        {
            _algorithm = algorithm;
        }

        #endregion
    }
}
