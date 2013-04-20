using System.ComponentModel.Composition;
using Algorithm;

namespace Charts
{
    public class Charts : ICharts
    {
        #region Private Member Variables

        private readonly IAlgorithm _algorithm;

        #endregion

        #region Constructors

        [ImportingConstructor]
        public Charts(IAlgorithm algorithm)
        {
            _algorithm = algorithm;
        }

        #endregion
    }
}
