using HeroQuest.Common.Enums;
using HeroQuest.Library.Enums;
using HeroQuest.Library.Interfaces;
using HeroQuest.Library.Models;
using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Input;

namespace HeroQuest.Library.KeyboardCommands
{
    public class CommandArrowKeysMovement : ICommandArrowKeysMovement
    {
        public CommandsEnum ActionCommand { get { return CommandsEnum.ArrowKeys_Movement; } set { } }

        public KeyboardActionProcessorModel Execute(KeyEventArgs e, IPlayer player = null, List<Image> weaponImages = null, List<Image> potionImages = null)
        {
            MovementDirectionEnum selectedMovement = MovementDirectionEnum.NotMoving;
            switch (e.Key)
            {
                case Key.Left:
                    selectedMovement = MovementDirectionEnum.MovingLeft;
                    break;

                case Key.Right:
                    selectedMovement = MovementDirectionEnum.MovingRight;
                    break;

                case Key.Up:
                    selectedMovement = MovementDirectionEnum.MovingUp;
                    break;

                case Key.Down:
                    selectedMovement = MovementDirectionEnum.MovingDown;
                    break;
            }

            return new KeyboardActionProcessorModel
            {
                PlayerRelatedAction = PlayerRelatedActionsEnum.Movement,
                ActionExecuted = new List<Enum>() { selectedMovement } ,
            };
        }
    }
}
