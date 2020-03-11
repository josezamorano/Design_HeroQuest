using HeroQuest.Library.Enums;
using HeroQuest.Library.Models;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Input;

namespace HeroQuest.Library.Interfaces
{
    public interface ICommand
    {
        CommandsEnum ActionCommand { get; set; }
        KeyboardActionProcessorModel Execute(KeyEventArgs e = null, IPlayer player = null, List<Image> weaponImages= null, List<Image> potionImages=null);
    }
}
