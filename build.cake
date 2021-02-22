var target = Argument("target", "ExecuteBuild");
var solutionFolder = "./";
var outputDirectory = "./artifacts";
var configuration = Argument("configuration", "Release");
var myLibraryFolder = "./CalculatorLibrary";
var myLibraryOutputFolder = "./MyLibraryArtifacts";
Task("Clean")
    .Does(() =>
    {
        CleanDirectory(outputDirectory);
        CleanDirectory(myLibraryOutputFolder);
    });
Task("Restore")
    .Does(() =>
    {
        DotNetCoreRestore("");
    });
Task("Build")
    .IsDependentOn("Restore")
    .IsDependentOn("Clean")
    .Does(() =>
    {
        DotNetCoreBuild(solutionFolder, new DotNetCoreBuildSettings
        {
            NoRestore = true,
            Configuration = configuration
        });
    });

Task("Publish")
    .IsDependentOn("Build")
    .Does(() =>
    {
        DotNetCorePublish(solutionFolder, new DotNetCorePublishSettings
        {
            NoRestore = true,
            Configuration = configuration,
            NoBuild = true,
            OutputDirectory = outputDirectory
        });
        
    });
Task("PublishLibrary")
    .IsDependentOn("Build")
    .Does(() =>
    {
        DotNetCorePublish(myLibraryFolder, new DotNetCorePublishSettings
        {
            NoRestore = true,
            Configuration = configuration,
            NoBuild = true,
            OutputDirectory = myLibraryOutputFolder
        });
    });
Task("ExecuteBuild")
    .IsDependentOn("Publish")
    .IsDependentOn("PublishLibrary");
RunTarget(target);