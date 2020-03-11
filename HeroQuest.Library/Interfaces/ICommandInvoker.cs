using HeroQuest.Library.Enums;

namespace HeroQuest.Library.Interfaces
{
    public interface ICommandInvoker
    {
        ICommand GetCommand(CommandsEnum actionCommand);
    }
}
