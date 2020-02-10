using System.CommandLine;

namespace ImageCasterCli.Api
{
    public interface ICliCommand
    {
        Command Configure();
    }
}
