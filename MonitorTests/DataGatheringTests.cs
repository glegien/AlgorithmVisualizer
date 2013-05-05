using System.Collections.Generic;
using System.Linq;
using Algorithm;
using IOManager;
using NUnit.Framework;
using Rhino.Mocks;

namespace MonitorTests
{
    [TestFixture]
    public class DataGatheringTests : MonitorTestsBase
    {
        protected IIoManager StubIoManager()
        {
            var ioManager = MockRepository.GenerateStub<IIoManager>();
            return ioManager;
        }

        #region Test Cases

        [Test]
        public void ShouldExposePreviousCalculationResults()
        {
            // Arrange
            var initialCalculationResultCollection = StubCalculationResultCollection(0.1, 7, 0.1).ToList();
            var algorithmStub = StubAlgorithm(initialCalculationResultCollection, new List<ICalculationResult>());
            var ioManagerStub = StubIoManager();

            var monitor = CreateMonitor(algorithmStub, ioManagerStub);

            // Act
            monitor.Start();

            // Assert
            CollectionAssert.AreEquivalent(monitor.CalculationResults, initialCalculationResultCollection);
        }

        [Test]
        public void ShouldAppendNewResultToCalculationResultsCollection()
        {
            // Arrange
            var initialCalculationResultCollection = StubCalculationResultCollection(0.1, 7, 0.1).ToList();
            var activeCalculationResultCollection = StubCalculationResultCollection(0.8, 2, 0.1).ToList();

            var algorithmStub = StubAlgorithm(initialCalculationResultCollection, activeCalculationResultCollection);
            var ioManagerStub = StubIoManager();

            var monitor = CreateMonitor(algorithmStub, ioManagerStub);

            // Act
            monitor.Start();

            // Assert
            CollectionAssert.AreEquivalent(monitor.CalculationResults,
                                           initialCalculationResultCollection.Union(activeCalculationResultCollection));
        }

        #endregion
    }
}
