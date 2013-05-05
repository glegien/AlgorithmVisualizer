using System.Collections.Generic;
using System.Linq;
using Algorithm;
using IOManager;
using NUnit.Framework;
using Rhino.Mocks;

namespace MonitorTests
{
    [TestFixture]
    public class CalculationAccuracyThresholdTests : MonitorTestsBase
    {
        #region Private Constants

        private const double AccuracyBelowThreshold = 0.7;
        private const double AccuracyOverThreshold = 0.9;
        private const double AccuracyThreshold = 0.8;

        #endregion

        #region Test Cases

        [Test]
        public void ShouldStopAlgorithmWhenCalculationAccuracyIsOverThreshold()
        {
            // Arrange
            var calculationResultOverThreshold = StubCalculationResult(AccuracyOverThreshold);
            var algorithmStub = StubAlgorithm(new List<ICalculationResult>(),
                                              new List<ICalculationResult> {calculationResultOverThreshold});

            var ioManagerMock = MockRepository.GenerateMock<IIoManager>();

            var monitor = CreateMonitor(algorithmStub, ioManagerMock);

            // Act
            monitor.AlgorithmStoppingThreshold = AccuracyThreshold;
            monitor.Start();

            // Assert
            ioManagerMock.AssertWasCalled(mock => mock.StopAlgorithm(), options => options.Repeat.Once());
        }

        [Test]
        public void ShouldNotStopAlgorithmWhenCalculationAccuracyInNotOverThreshold()
        {
            // Arrange
            var calculationResultBelowThreshold = StubCalculationResult(AccuracyBelowThreshold);
            var algorithmStub = StubAlgorithm(new List<ICalculationResult>(),
                                              new List<ICalculationResult> { calculationResultBelowThreshold });

            var ioManagerMock = MockRepository.GenerateMock<IIoManager>();

            var monitor = CreateMonitor(algorithmStub, ioManagerMock);

            // Act
            monitor.AlgorithmStoppingThreshold = AccuracyThreshold;
            monitor.Start();

            // Assert
            ioManagerMock.AssertWasNotCalled(mock => mock.StopAlgorithm());
        }
        
        [Test]
        public void ShouldNotStopAlgorithmWhenThresholdAccuracyIsNotSet()
        {
            // Arrange
            var calculationResultStubCollection = StubCalculationResultCollection(0.9, 9, 0.01);
            var algorithmStub = StubAlgorithm(new List<ICalculationResult>(),
                                              calculationResultStubCollection);
            var ioManagerMock = MockRepository.GenerateMock<IIoManager>();

            var monitor = CreateMonitor(algorithmStub, ioManagerMock);

            // Act
            monitor.Start();

            // Assert
            ioManagerMock.AssertWasNotCalled(mock => mock.StopAlgorithm());
        }

        #endregion
    }
}
