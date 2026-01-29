using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Running;
using System;

namespace Arborist.Benchmarks;

public class Program
{
  public static void Main(string[] args)
  {
    // Detect if running in CI
    var isCI = !string.IsNullOrEmpty(Environment.GetEnvironmentVariable("CI"))
            || !string.IsNullOrEmpty(Environment.GetEnvironmentVariable("GITHUB_ACTIONS"));

    var config = GetConfig(isCI);

    // Use BenchmarkSwitcher for automatic discovery of all benchmark classes
    // This replaces the manual list and supports filtering via command line
    BenchmarkSwitcher
      .FromAssembly(typeof(Program).Assembly)
      .Run(args, config);
  }

  private static IConfig GetConfig(bool isCI)
  {
    var config = ManualConfig
      .Create(DefaultConfig.Instance)
      .WithOptions(ConfigOptions.JoinSummary);

    if (isCI)
    {
      // CI: Accurate, longer runs
      // Remove ShortRunJob - use default for accurate measurements
      config = config
        .AddJob(Job.Default)
        .WithOptions(ConfigOptions.DisableLogFile);
    }
    else
    {
      // Local: Fast iterations for development
      config = config
        .AddJob(Job.ShortRun)
        .WithOptions(ConfigOptions.DisableLogFile);
    }

    return config;
  }
}
