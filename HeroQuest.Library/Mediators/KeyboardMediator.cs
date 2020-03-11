using HeroQuest.Library.Interfaces;
using HeroQuest.Library.Models;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Input;

namespace HeroQuest.Library.Mediators
{
    public class KeyboardMediator: IKeyboardMediator
    {
        ICommandInvoker _commandInvoker;
        public KeyboardMediator(ICommandInvoker commandInvoker)
        {
            _commandInvoker  = commandInvoker;
        }

        public KeyboardActionProcessorModel SelectCommandKey(KeyEventArgs e, IPlayer player, List<Image> weaponImages, List<Image> potionImages)
        {
            KeyboardActionProcessorModel KeyboardActionProcessor = null;
            if(e.Key == Key.Space)
            {
                // WEAPON Strike
                var selectedCommand = _commandInvoker.GetCommand(Enums.CommandsEnum.Key_StrikeEnemy);
                KeyboardActionProcessor = selectedCommand.Execute(e, player, weaponImages, potionImages);
                return KeyboardActionProcessor;
            }
            if(e.Key == Key.LeftShift)
            {
                // ITEM Pickup
                var selectedCommand = _commandInvoker.GetCommand(Enums.CommandsEnum.Key_PickupItem);
                KeyboardActionProcessor = selectedCommand.Execute(e,player, weaponImages, potionImages);
                return KeyboardActionProcessor;
            }
            if(e.Key == Key.LeftCtrl)
            {
                // ITEM Drop
                var selectedCommand = _commandInvoker.GetCommand(Enums.CommandsEnum.Key_DropItem);
                KeyboardActionProcessor = selectedCommand.Execute(e, player, weaponImages, potionImages);
                //NO MORE code required
                return null;
            }
            if(e.Key == Key.Up || e.Key == Key.Down || e.Key == Key.Left || e.Key == Key.Right)
            {
                if(e.Key == Key.Left || e.Key == Key.Right)
                {
                    player.ShootingDirection_Player = e.Key;
                }
                
                //MOVEMENT Arrow Keys
                var selectedCommand = _commandInvoker.GetCommand(Enums.CommandsEnum.ArrowKeys_Movement);
                KeyboardActionProcessor = selectedCommand.Execute(e);
                return KeyboardActionProcessor;
            }
            return null;
        }
    }
}
