using System.CommandLine;

namespace ImageCaster.Api
{
    public interface ICliCommand
    {
        Command Configure();
        int Execute();
    }
}