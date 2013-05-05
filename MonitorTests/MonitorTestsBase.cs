using System;
using System.Collections.Generic;
using Algorithm;
using IOManager;
using Monitor;
using Rhino.Mocks;

namespace MonitorTests
{
    public class MonitorTestsBase
    {
        #region Protected Helper Methods

        protected static IMonitor CreateMonitor(IAlgorithm algorithm, IIoManager ioManager)
        {
            var monitor = new Monitor.Monitor(algorithm, ioManager);
            return monitor;
        }

        protected static ICalculationResult StubCalculationResult(double accuracy)
        {
            var calculationResultStub = MockRepository.GenerateStub<ICalculationResult>();
            calculationResultStub.Stub(s => s.Accuracy).Return(accuracy);

            return calculationResultStub;
        }

        protected static IAlgorithm StubAlgorithm(IEnumerable<ICalculationResult> initialCalculationResultCollection,
                                                  IEnumerable<ICalculationResult> activeCalculationResultCollection)
        {
            var algorithmStub = MockRepository.GenerateStub<IAlgorithm>();

            algorithmStub.Stub(stub => stub.CalculationResults).Return(initialCalculationResultCollection);
            algorithmStub.Stub(
                stub => stub.RegisterCalculationResultListener(Arg<ICalculationResultListener>.Is.Anything))
                         .Do((Action<ICalculationResultListener>)(calculationResultListener =>
                         {
                             foreach (var calculationResult in activeCalculationResultCollection)
                             {
                                 calculationResultListener.NewCalculationResult(calculationResult);
                             }
                         }));

            return algorithmStub;
        }

        protected IEnumerable<ICalculationResult> StubCalculationResultCollection(double startingAccuracy, int count, double step)
        {
            var calculationResultCollection = new List<ICalculationResult>();
            var calculationAccuracy = startingAccuracy;
            while (count > 0)
            {
                var calculationResult = StubCalculationResult(calculationAccuracy);
                calculationResultCollection.Add(calculationResult);
                calculationAccuracy += step;
                count--;
            }
            return calculationResultCollection;
        }

        #endregion
    }
}
