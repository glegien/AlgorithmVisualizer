using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using Algorithm;
using Charts;
using IOManager;
using Monitor;

namespace AlgorithmVisualizer
{
    class Program
    {
        static void Main(string[] args)
        {
            var aggregateCatalog = new AggregateCatalog();
            aggregateCatalog.Catalogs.Add(new DirectoryCatalog("."));
            var container = new CompositionContainer(aggregateCatalog);
            container.ComposeParts();

            var algorithm = container.GetExportedValue<IAlgorithm>();

            var charts = container.GetExportedValue<ICharts>();
            var ioManager = container.GetExportedValue<IIoManager>();
            var monitor = container.GetExportedValue<IMonitor>();

            algorithm.Run();
        }
    }
}
