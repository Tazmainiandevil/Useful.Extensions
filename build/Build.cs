using Nuke.Common;
using Nuke.Common.CI.GitHubActions;
using Nuke.Common.IO;
using Nuke.Common.ProjectModel;
using Nuke.Common.Tools.Docker;
using Nuke.Common.Tools.DotNet;
using Nuke.Common.Tools.PowerShell;

[GitHubActions(
    "continuous",
    GitHubActionsImage.UbuntuLatest,
    On = new[] { GitHubActionsTrigger.Push },
    ImportSecrets = new[] { nameof(SnykToken), nameof(SnykSeverityThreshold), nameof(SnykCodeSeverityThreshold) },
    InvokedTargets = new[] { nameof(BuildTestCode), nameof(SnykTest), nameof(SnykCodeTest), nameof(GenerateSbom) })]
class Build : NukeBuild
{
    public static int Main() => Execute<Build>(x => x.BuildTestCode, x => x.SnykTest, x => x.SnykCodeTest, x => x.GenerateSbom);

    [Parameter("Configuration to build - Default is 'Debug' (local) or 'Release' (server)")]
    readonly Configuration Configuration = IsLocalBuild ? Configuration.Debug : Configuration.Release;

    [Parameter("Snyk Token to interact with the API")][Secret] readonly string SnykToken;
    [Parameter("Snyk Severity Threshold (critical, high, medium or low)")] readonly string SnykSeverityThreshold;
    [Parameter("Snyk Code Severity Threshold (high, medium or low)")] readonly string SnykCodeSeverityThreshold;

    [Solution(GenerateProjects = true)] readonly Solution Solution;

    AbsolutePath SourceDirectory => RootDirectory / "src";
    AbsolutePath TestsDirectory => RootDirectory / "tests";
    AbsolutePath OutputDirectory => RootDirectory / "outputs";

    Target Clean => _ => _
        .Executes(() =>
        {
            SourceDirectory.GlobDirectories("*/bin", "*/obj").DeleteDirectories();
        });

    Target BuildTestCode => _ => _
        .DependsOn(Clean)
        .Executes(() =>
        {
            DotNetTasks.DotNetRestore(_ => _
                .SetProjectFile(Solution)
            );
            DotNetTasks.DotNetBuild(_ => _
                .EnableNoRestore()
                .SetProjectFile(Solution)
                .SetConfiguration(Configuration)
                .SetProperty("SourceLinkCreate", true)
            );
            DotNetTasks.DotNetTest(_ => _
                .EnableNoRestore()
                .EnableNoBuild()
                .SetConfiguration(Configuration)
                .SetTestAdapterPath(TestsDirectory / "*.Tests")
            );
        });
    Target SnykAuth => _ => _
        .DependsOn(BuildTestCode)
        .Executes(() =>
        {
            PowerShellTasks.PowerShell(_ => _
                .SetCommand("npm install snyk@latest -g")
            );
            PowerShellTasks.PowerShell(_ => _
                .SetCommand($"snyk auth {SnykToken}")
            );
        });
    Target SnykTest => _ => _
        .DependsOn(SnykAuth)
        .Requires(() => SnykSeverityThreshold)
        .Executes(() =>
        {
            PowerShellTasks.PowerShell(_ => _
                .SetCommand($"snyk test --all-projects --exclude=build --severity-threshold={SnykSeverityThreshold.ToLowerInvariant()}")
            );
        });
    Target SnykCodeTest => _ => _
        .DependsOn(SnykAuth)
        .Requires(() => SnykCodeSeverityThreshold)
        .Executes(() =>
        {
            PowerShellTasks.PowerShell(_ => _
                .SetCommand($"snyk code test --all-projects --exclude=build --severity-threshold={SnykCodeSeverityThreshold.ToLowerInvariant()}")
            );
        });
    Target GenerateSbom => _ => _
        .DependsOn(SnykAuth)
        .Produces(OutputDirectory / "*.json")
        .Executes(() =>
        {
            OutputDirectory.CreateOrCleanDirectory();
            PowerShellTasks.PowerShell(_ => _
                .SetCommand($"snyk sbom --all-projects --format spdx2.3+json --json-file-output={OutputDirectory / "sbom.json"}")
            );
        });
    //Target SnykTest => _ => _
    //    .DependsOn(BuildTestCode)
    //    .Requires(() => SnykToken, () => SnykSeverityThreshold)
    //    .Executes(() =>
    //    {
    //        // Snyk Test
    //        DockerTasks.DockerRun(_ => _
    //            .EnableRm()
    //            .SetVolume($"{RootDirectory}:/app")
    //            .SetEnv($"SNYK_TOKEN={SnykToken}")
    //            .SetImage("snyk/snyk:dotnet")
    //            .SetCommand($"snyk test --all-projects --exclude=build --severity-threshold={SnykSeverityThreshold.ToLowerInvariant()}")
    //        );
    //    });
    //Target SnykCodeTest => _ => _
    //    .DependsOn(BuildTestCode)
    //    .Requires(() => SnykToken, () => SnykCodeSeverityThreshold)
    //    .Executes(() =>
    //    {
    //        DockerTasks.DockerRun(_ => _
    //            .EnableRm()
    //            .SetVolume($"{RootDirectory}:/app")
    //            .SetEnv($"SNYK_TOKEN={SnykToken}")
    //            .SetImage("snyk/snyk:dotnet")
    //            .SetCommand($"snyk code test --all-projects --exclude=build --severity-threshold={SnykCodeSeverityThreshold.ToLowerInvariant()}")
    //        );
    //    });
    //Target GenerateSbom => _ => _
    //    .DependsOn(BuildTestCode)
    //    .Produces(OutputDirectory / "*.json")
    //    .Requires(() => SnykToken)
    //    .Executes(() =>
    //    {
    //        OutputDirectory.CreateOrCleanDirectory();
    //        DockerTasks.DockerRun(_ => _
    //            .EnableRm()
    //            .SetVolume($"{RootDirectory}:/app")
    //            .SetEnv($"SNYK_TOKEN={SnykToken}")
    //            .SetImage("snyk/snyk:dotnet")
    //            .SetCommand($"snyk sbom --all-projects --format spdx2.3+json --json-file-output={OutputDirectory.Name}/sbom.json")
    //        );
    //    });
}