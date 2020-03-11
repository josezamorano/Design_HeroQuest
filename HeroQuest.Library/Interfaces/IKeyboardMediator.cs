using HeroQuest.Library.Models;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Input;

namespace HeroQuest.Library.Interfaces
{
    public interface IKeyboardMediator
    {
        KeyboardActionProcessorModel SelectCommandKey(KeyEventArgs e, IPlayer player, List<Image> weaponImages, List<Image> potionImages);
    }
}
