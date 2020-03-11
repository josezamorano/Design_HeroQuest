using HeroQuest.Library.Enums;
using HeroQuest.Library.Interfaces;
using HeroQuest.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace HeroQuest.Library.KeyboardCommands
{
    public class CommandKeyDropItem : ICommandKeyDropItem
    {
        public CommandsEnum ActionCommand { get { return CommandsEnum.Key_DropItem; } set { } }

        public KeyboardActionProcessorModel Execute(KeyEventArgs e, IPlayer player = null, List<Image> weaponImages = null, List<Image> potionImages = null)
        {
            var result = player.DropWeapon_RemoveObserver();
            var keyboardActionProcessor = new KeyboardActionProcessorModel()
            {
                PlayerRelatedAction = PlayerRelatedActionsEnum.Weapon,
                ActionExecuted = new List<Enum>() { WeaponActionsEnum.DropWeapon },
            };

            return null;
        }
    }
}
