using HeroQuest.Common.Interfaces;
using HeroQuest.Common.Managers;
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
    public class CommandKeyPickupItem : ICommandKeyPickupItem
    {
       
        IImageManager _imageManager;
       
        public CommandKeyPickupItem(IImageManager imageManager)
        {
            _imageManager = imageManager;
        }
        
        public CommandsEnum ActionCommand { get { return CommandsEnum.Key_PickupItem; } set { } }
        public KeyboardActionProcessorModel Execute(KeyEventArgs e, IPlayer player = null, List<Image> weaponImages = null, List<Image> potionImages = null )
        {
            //Here we compare if two objects INTERSECT 
            Image playerPickedUpWeapon = GetSelectedItem(player.ShooterImage, weaponImages);
            Image playerPickedUpPotion = GetSelectedItem(player.ShooterImage, potionImages);

            KeyboardActionProcessorModel keyboardActionProcessor = new KeyboardActionProcessorModel();
            keyboardActionProcessor.ImagesCollected = new List<Image>();
            keyboardActionProcessor.ActionExecuted = new List<Enum>();
            keyboardActionProcessor.PlayerRelatedAction = PlayerRelatedActionsEnum.CollectItem;
            if (playerPickedUpWeapon !=null)
            {
                keyboardActionProcessor.ImagesCollected.Add(playerPickedUpWeapon);
                keyboardActionProcessor.ActionExecuted.Add(WeaponActionsEnum.PickupWeapon);
            }

            if(playerPickedUpPotion != null)
            {
                keyboardActionProcessor.ImagesCollected.Add(playerPickedUpPotion);
                keyboardActionProcessor.ActionExecuted.Add(PotionActionsEnum.PickupPotion);
            }

            return keyboardActionProcessor;
        }

        private Image GetSelectedItem(Image item1, List<Image> itemsList )
        {
            foreach (var item in itemsList)
            {
                bool itemsIntersect = _imageManager.ImagesCollisionDetected_Adaptor(item1, item);
                if (itemsIntersect)
                    return item;
            }
            return null;
        }
    }
}
