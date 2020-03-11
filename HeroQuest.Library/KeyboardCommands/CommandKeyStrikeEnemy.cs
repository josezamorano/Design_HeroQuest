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
    public class CommandKeyStrikeEnemy : ICommandKeyStrikeEnemy
    {
        public CommandsEnum ActionCommand { get { return CommandsEnum.Key_StrikeEnemy; } set { } }

        public KeyboardActionProcessorModel Execute(KeyEventArgs e = null, IPlayer player = null, List<Image> weaponImages = null, List<Image> potionImages = null)
        {
            var weapons = player.GetWeapons();
            foreach (var weapon in weapons)
            {
                weapon.Strike(player);
            }
            //No further code required. 
            return null;
        }
    }
}
