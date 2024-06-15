using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BenchmarkDotNet.Exporters;
using BenchmarkDotNet.Loggers;
using BenchmarkDotNet.Reports;
using Perfolizer.Horology;

namespace SmartVectorBenchmarks;

internal class SimpleCsvExporter : ExporterBase
{
    protected override string FileExtension => "csv";
    protected override string FileNameSuffix { get; }

    public SimpleCsvExporter(string fileNameSuffix) 
    {
        FileNameSuffix = fileNameSuffix;
    }

    public override void ExportToLog(Summary summary, ILogger logger)
    {
        var columnNames = new[] { "Method", "Mean", "Error", "StdDev" };
        var columns = summary
            .GetColumns()
            .Where(x => columnNames.Contains(x.ColumnName))
            .ToDictionary(x => x.ColumnName);
        var stats = summary.BenchmarksCases.Select(x => summary[x]!.ResultStatistics!).ToArray();
        var meanUnit = TimeUnit.GetBestTimeUnit(stats.Select(x => x.Mean).ToArray());
        var stdErrUnit = TimeUnit.GetBestTimeUnit(stats.Select(x => x.StandardError).ToArray());
        var stdDevUnit = TimeUnit.GetBestTimeUnit(stats.Select(x => x.StandardDeviation).ToArray());

        logger.WriteLine($"Method,Mean [{meanUnit.Name}],Error [{stdErrUnit.Name}],StdDev [{stdDevUnit.Name}]");
        foreach(var @case in summary.BenchmarksCases)
        {
            var report = summary[@case]!;
            var stat = report.ResultStatistics!;
            logger.Write($"{columns["Method"].GetValue(summary, @case)},");
            logger.Write($"{stat.Mean / meanUnit.NanosecondAmount:0.000},");
            logger.Write($"{stat.StandardError / stdErrUnit.NanosecondAmount:0.000},");
            logger.Write($"{stat.StandardDeviation / stdDevUnit.NanosecondAmount:0.000}");
            logger.WriteLine();
        }
    }
}
