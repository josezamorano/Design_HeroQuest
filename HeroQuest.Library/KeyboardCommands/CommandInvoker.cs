using HeroQuest.Library.DependencyInjection;
using HeroQuest.Library.Enums;
using HeroQuest.Library.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace HeroQuest.Library.KeyboardCommands
{
    public class CommandInvoker : ICommandInvoker
    {
        private List<ICommand> commands = new List<ICommand>();
        public CommandInvoker()
        {
            commands.Add(ContainerConfig.GetInstance<ICommandArrowKeysMovement>());
            commands.Add(ContainerConfig.GetInstance<ICommandKeyDropItem>());
            commands.Add(ContainerConfig.GetInstance<ICommandKeyPickupItem>());
            commands.Add(ContainerConfig.GetInstance<ICommandKeyStrikeEnemy>());
        }

        public ICommand GetCommand(CommandsEnum actionCommand)
        {
           var command = commands.Where(a => a.ActionCommand == actionCommand).FirstOrDefault();
           return command;
        }
    }
}
