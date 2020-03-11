using HeroQuest.Common.Enums;
using HeroQuest.Common.Interfaces;
using HeroQuest.Common.Models;
using HeroQuest.Library.Enums;
using HeroQuest.Library.Interfaces;
using HeroQuest.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;

namespace HeroQuest.Library.Mediators
{
    public class UIMediator : IUIMediator
    {
        private Random random = new Random();
        private IEnemyFireball _enemyFireball;
        IKeyboardMediator _KeyboardMediator;
        IImageManager _imageManager;
        public UIMediator( IKeyboardMediator KeyboardMediator,IImageManager imageManager)
        {
            _KeyboardMediator = KeyboardMediator;
            _imageManager = imageManager;
        }
        
        
        public void RegisterNotificationDispatcherTimer_HeroPlayer(IPlayer player, NotificationTimerEnum notificationTimer)
        {
            player.PublishPlayerLocationPoint_NotifyObservers();

            //Remove Enemies_Unsubscribe_Observers
            var allEnemiesCount = player.GetAllEnemies().Count;
            if (allEnemiesCount > 0)
            {
                foreach (IEnemy enemy in player.GetAllEnemies())
                {
                    if (enemy.GetHealthPoints() <= 0)
                    {
                        player.RemoveEnemy_UnsubscribeObserver(enemy);
                        break;
                    }
                }
            }
        }

        public void RegisterNotificationDispatcherTimer_Arrow(IPlayer player, NotificationTimerEnum notificationTimer)
        {
            IWeapon player_ArrowAndBow = (player.GetWeapons()).Where(a => a.WeaponName == WeaponsListEnum.Bow).FirstOrDefault();

            if (player_ArrowAndBow != null)
            {
                player_ArrowAndBow.SetNotificationTimer(notificationTimer);
            }
        }
        
        public void RegisterNotificationDispatcherTimer_Bat(IPlayer player,IBat bat,NotificationTimerEnum notificationTimer)
        {
            Image batImage = bat.EnemyImage;
            if (batImage != null)
            {
                if (notificationTimer == NotificationTimerEnum.TimerON)
                {
                    //_bat.EnemyImage = batImage;
                    bat.Move(bat.MovementDirection);

                    EnemyStrikeHero(bat, player);
                }
            }
        }

        public void RegisterNotificationDispatcherTimer_Wizard(IPlayer player,IWizard wizard,NotificationTimerEnum notificationTimer)
        {
            Image wizardImage = wizard.EnemyImage;
            if (wizardImage != null)
            {
                if (notificationTimer == NotificationTimerEnum.TimerON)
                {
                    wizard.Move(wizard.MovementDirection);
                    EnemyStrikeHero(wizard, player);
                    SetWeaponNextStrike(wizard);
                }
            }
        }

        public void RegisterNotificationDispatcherTimer_EnemyFireball(IEnemyFireball enemyFireball, NotificationTimerEnum notificationTimer)
        {
            _enemyFireball = enemyFireball;
            enemyFireball.SetNotificationTimer(notificationTimer);
        }

        public void Form1_KeyDown_Event(KeyEventArgs e, IPlayer player, List<Image> allWeaponsImages, List<Image> allPotionsImages)
        {
            KeyboardActionProcessorModel keyboardActionProcessor = _KeyboardMediator.SelectCommandKey(e, player, allWeaponsImages, allPotionsImages);
            if (keyboardActionProcessor != null)
            {
                //Player Pickup Items 
                if (keyboardActionProcessor.PlayerRelatedAction == PlayerRelatedActionsEnum.CollectItem)
                {
                    if (keyboardActionProcessor.ActionExecuted.Count > 0)
                    {
                        foreach (var actionExecuted in keyboardActionProcessor.ActionExecuted)
                        {
                            if (actionExecuted.ToString() == PotionActionsEnum.PickupPotion.ToString())
                                CollectItem(player,allPotionsImages, keyboardActionProcessor.ImagesCollected);

                            if (actionExecuted.ToString() == WeaponActionsEnum.PickupWeapon.ToString())
                                CollectItem(player,allWeaponsImages, keyboardActionProcessor.ImagesCollected);
                        }
                    }
                }

                //Player moves around 
                if (keyboardActionProcessor.PlayerRelatedAction == PlayerRelatedActionsEnum.Movement)
                {
                    foreach (MovementDirectionEnum movement in keyboardActionProcessor.ActionExecuted)
                    {
                        player.PlayerMove(movement);
                    }
                }
            }
        }

        public void Form1_KeyUp_Event(IPlayer player)
        {
           player.PlayerMove(MovementDirectionEnum.NotMoving);
        }

        #region Private Methods
        private void CollectItem(IPlayer player, List<Image> itemsImagesToBeCollected, List<Image> itemsImagesPickedUp)
        {
            IEnumerable<Image> pickedUpItemsImages = itemsImagesToBeCollected.Intersect(itemsImagesPickedUp);
            if (pickedUpItemsImages.Count() > 0)
            {
                foreach(var itemImage in pickedUpItemsImages)
                {
                    player.PickupItem(itemImage);
                }
            }
        }

        int counter = 0, newStrike = 0;
        int lowerLevelConsecutiveMovements = 10;
        int upperLevelConsecutiveMovements = 35;
        private void SetWeaponNextStrike(IEnemy shooter)
        {
            if (counter == 0)
            {
                newStrike = random.Next(lowerLevelConsecutiveMovements, upperLevelConsecutiveMovements);
            }

            if (counter == newStrike)
            {
                _enemyFireball.Strike(shooter);
                counter = 0;
                newStrike = 0;
            }
            else
            {
                counter++;
            }
        }

        private void EnemyStrikeHero(IEnemy enemy, IPlayer player)
        {
            bool enemyStruckHero = _imageManager.ImagesCollisionDetected_Adaptor(player.ShooterImage, enemy.EnemyImage);

            if (enemyStruckHero)
            {
                int enemyStrikePoints = enemy.EnemyIncreasePoints_ForStrikingTarget();
                player.ReduceHealth(enemyStrikePoints);
            }
        }
        #endregion
    }
}
