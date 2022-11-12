using ADotNet.Clients;
using ADotNet.Models.Pipelines.GithubPipelines.DotNets;
using ADotNet.Models.Pipelines.GithubPipelines.DotNets.Tasks;
using ADotNet.Models.Pipelines.GithubPipelines.DotNets.Tasks.SetupDotNetTaskV1s;

var adotNetClient = new ADotNetClient();

var githubPipeline = new GithubPipeline
{
    Name = ".Net",

    OnEvents = new Events
    {
        Push = new PushEvent
        {
            Branches = new string[] { "develop" }
        },

        PullRequest = new PullRequestEvent
        {
            Branches = new string[] { "develop" }
        }
    },

    Jobs = new Jobs
    {
        Build = new BuildJob
        {
            RunsOn = BuildMachines.Windows2019,

            Steps = new List<GithubTask>
            {
                new CheckoutTaskV2
                {
                    Name = "Check Out"
                },

                new SetupDotNetTaskV1
                {
                    Name = "Setup Dot Net Version",

                    TargetDotNetVersion = new TargetDotNetVersion
                    {
                        DotNetVersion = "6.0.x",
                        IncludePrerelease = true
                    }
                },

                new RestoreTask
                {
                    Name = "Restore",
                    Run = "dotnet restore ./FarmFresh/FarmFresh.sln"
                },

                new DotNetBuildTask
                {
                    Name = "Build",
                    Run = "dotnet build ./FarmFresh/FarmFresh.sln --no-restore"
                },

                new TestTask
                {
                    Name = "Test",
                    Run = "dotnet test ./FarmFresh/FarmFresh.sln --no-build --verbosity normal"
                }
            }
        }
    }
};

adotNetClient.SerializeAndWriteToFile(githubPipeline, "../../../../../.github/workflows/dotnet.yml");