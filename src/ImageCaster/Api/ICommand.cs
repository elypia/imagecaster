using System.CommandLine;

namespace ImageCaster.Api
{
    public interface ICommand
    {
        Command Configure();
        int Execute();
    }
}