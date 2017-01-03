#addin nuget:?package=Cake.Git
#addin "Cake.WebDeploy"

//////////////////////////////////////////////////////////////////////
// ARGUMENTS
//////////////////////////////////////////////////////////////////////

var target = Argument("target", "Default");
var configuration = Argument("configuration", "Release");

//////////////////////////////////////////////////////////////////////
// PREPARATION
//////////////////////////////////////////////////////////////////////
string solutionName = "FrannHammer.Master.sln";
string buildArtifactsDirectory = "./buildArtifacts";

//////////////////////////////////////////////////////////////////////
// TASKS
//////////////////////////////////////////////////////////////////////

Task("Clean")
    .Does(() =>
{
    //CleanDirectory(".");
});

Task("Restore-NuGet-Packages")
    .IsDependentOn("Clean")
    .Does(() =>
{
    NuGetRestore(solutionName);
});

Task("Build")
    .IsDependentOn("Restore-NuGet-Packages")
    .IsDependentOn("Assign-Assembly-Info")
    .Does(() =>
{
    if(IsRunningOnWindows())
    {
      // Use MSBuild
      MSBuild(solutionName, settings =>
        settings.SetConfiguration(configuration));
    }
    else
    {
      // Use XBuild
      XBuild(solutionName, settings =>
        settings.SetConfiguration(configuration));
    }
});

Task("Run-Unit-Tests")
    .IsDependentOn("Build")
    .Does(() =>
{
    string currentBranchName = GitBranchCurrent(".").FriendlyName;

    if(currentBranchName.Equals("(no branch)"))
    {
        currentBranchName = EnvironmentVariable("APPVEYOR_REPO_BRANCH");
    }

    string reportUnitDirectory = buildArtifactsDirectory + @"/" + currentBranchName + @"/reportUnit";
    string reportUnitOutputFile = reportUnitDirectory + @"/unit.html";
    string unitTestResultsOutputDirectory = buildArtifactsDirectory + @"/" + currentBranchName + @"/unittestresults";
    string openCoverOutputDirectory = buildArtifactsDirectory + @"/" + currentBranchName + @"/opencover";
    string openCoverResultsFile = openCoverOutputDirectory + @"/opencoverresults.xml";
    string unitTestResultsOutputFile = unitTestResultsOutputDirectory + @"/testresults.xml";

    EnsureDirectoryExists(unitTestResultsOutputDirectory);
    EnsureDirectoryExists(reportUnitDirectory);
    EnsureDirectoryExists(openCoverOutputDirectory);

    OpenCover(tool => 
        tool.NUnit3("./**/bin/" + configuration + "/*.Tests.dll", new NUnit3Settings { 
                NoResults = false,
                Results = unitTestResultsOutputFile,
                Where = "cat!=LONGRUNNING"
            }),
            openCoverResultsFile,
        new OpenCoverSettings()
            .WithFilter("+[*]*")
            .WithFilter("-[*.Tests]*"));

    var reportSettings = new ReportGeneratorSettings();
    reportSettings.ReportTypes = new List<ReportGeneratorReportType>{
        ReportGeneratorReportType.Badges,
        ReportGeneratorReportType.Html
    };

    ReportGenerator(openCoverResultsFile, Directory(openCoverOutputDirectory), reportSettings);
    ReportUnit(new FilePath(unitTestResultsOutputFile), new FilePath(reportUnitOutputFile));
});

Task("Assign-Assembly-Info")
    .Description("Create Assembly Information.")
    .Does(() =>{

        CreateAssemblyInfo(
            "./SolutionInfo.cs",
            new AssemblyInfoSettings{
                Version = "0.4.*",
                Company = "FrannDotExe"
        });
});

//////////////////////////////////////////////////////////////////////
// TASK TARGETS
//////////////////////////////////////////////////////////////////////

Task("Default")
    .IsDependentOn("Run-Unit-Tests");

//////////////////////////////////////////////////////////////////////
// EXECUTION
//////////////////////////////////////////////////////////////////////

RunTarget(target);
