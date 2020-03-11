using HeroQuest.Library.Enums;
using HeroQuest.Library.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using HeroQuest.Common.Interfaces;
using System.Windows.Input;
using HeroQuest.Common.Enums;

namespace HeroQuest.Library
{
    public class Player : IPlayer
    {
       
        public System.Windows.Controls.Image ShooterImage { get; set; }
        public System.Windows.Controls.Image TargetImage { get; set; }
        public Key ShootingDirection_Player { get; set; }
        public List<System.Windows.Controls.Image> AllSwordSwingImages { get; set; }
        public List<System.Windows.Controls.Image> AllMaceSwingImages { get; set; }
        public List<System.Windows.Controls.Image> AllArrowShootingLeftImages { get; set; }
        public List<System.Windows.Controls.Image> AllArrowShootingRightImages { get; set; }
        

        private List<IWeapon> weaponsCarried_Observers;
        private List<IEnemy> allEnemies_Observers;
        private List<ITarget> allTargets;
        private BehaviorTypeEnum behaviorType;
        private int _TotalAccummulatedEnemyStrikePoints;
        private int _healthReservePoints;
        private int _speedMovement;
        private IMovementStrategy _movementStrategy;
        private System.Windows.Controls.Image _mainWindow;

        ICastle _castle;
        IHealthInvoker _healthInvoker;
        IImageManager _imageManager;
        IMovementDirectionManager _movementDirectionManager;
        IMovementStrategyInvoker _movementStrategyInvoker;
        IAllWeaponsFactory _weaponsFactory;
        public Player(
            ICastle castle,
            IHealthInvoker healthInvoker,
            IImageManager imageManager,
            IMovementDirectionManager movementDirectionManager,
            IMovementStrategyInvoker movementStrategyInvoker,
            IAllWeaponsFactory weaponsFactory
            )
        {
            _castle = castle;
            _healthInvoker = healthInvoker;
            _imageManager = imageManager;
            _movementDirectionManager = movementDirectionManager;
            _movementStrategyInvoker = movementStrategyInvoker;
            _weaponsFactory = weaponsFactory;

            behaviorType = BehaviorTypeEnum.Targeted;

            AllSwordSwingImages = new List<System.Windows.Controls.Image>();
            AllMaceSwingImages = new List<System.Windows.Controls.Image>();
            AllArrowShootingLeftImages = new List<System.Windows.Controls.Image>();
            AllArrowShootingRightImages = new List<System.Windows.Controls.Image>();
            weaponsCarried_Observers = new List<IWeapon>();
            allEnemies_Observers = new List<IEnemy>();
            allTargets = new List<ITarget>();

            ShootingDirection_Player = Key.None;
            _speedMovement = 5;
        }

        public void RegisterWPFMainWindow(System.Windows.Controls.Image mainWindow)
        {
            _mainWindow = mainWindow;
            _imageManager.RegisterWPFMainWindow(_mainWindow);
        }
        
        public void SetHealth(HealthStatesEnum healthState)
        {
            var selectedHealthState = _healthInvoker.GetHealthState((int)healthState);
            _healthReservePoints = selectedHealthState.HealthPoints;
            SetSpeedMovementByHealthState(selectedHealthState);
        }
        
        public void SetMovementStrategy(MovementStrategyEnum movementStrategy)
        {
            _movementStrategy = _movementStrategyInvoker.GetMovementStrategy(movementStrategy);
        }

        public void IncreasePointForStrikingTarget(int weaponStrikePoints)
        {
            _TotalAccummulatedEnemyStrikePoints += weaponStrikePoints;
        }

        public int GetTotalAccummulatedStrikePoints()
        {
            return _TotalAccummulatedEnemyStrikePoints;
        }

        public void IncreaseHealth(int pointsStrength)
        {
            _healthReservePoints += pointsStrength;
        }

        public void ReduceHealth(int enemyDamagePoints)
        {
            _healthReservePoints -= enemyDamagePoints;
        }

        public int GetHealthPoints()
        {
            return _healthReservePoints;
        }

        public void SetSpeedMovementByHealthState(IHealthState healthState)
        {
            _speedMovement = healthState.MovementSpeed;
        }

        public void PlayerMove(MovementDirectionEnum movementDirection)
        {
            //HERO
            //Define Behavior

            //Confirm Castle Wall Boundaries reached
            var castleInnerLimits = _castle.GetCanvasLocationAndDimensionsInnerLimits();
            MovementDirectionEnum castleWallReachedByPlayer = _imageManager.ImageIsBeyondBoundaries_Adaptor(castleInnerLimits, ShooterImage);
            //Process Movement 
            var selectedMovement = (castleWallReachedByPlayer != MovementDirectionEnum.MovementClear_NoBoundariesReached)
                                   ? _movementDirectionManager.GetOppositeDirection(castleWallReachedByPlayer)
                                   : movementDirection;

            ProcessPlayerMovement(selectedMovement);
        }

        public void PickupItem(System.Windows.Controls.Image image)
        {
            if (image.Name.ToLower().Contains(PlayerRelatedActionsEnum.Potion.ToString().ToLower()))
            {
                var allPotions = Enum.GetValues(typeof(PotionsListEnum)).Cast<PotionsListEnum>().ToList();
                foreach (var potionItem in allPotions)
                {
                    if (image.Name.Contains(potionItem.ToString()))
                    {
                        ProcessHealthIncrease((int)potionItem);
                        image.Visibility = System.Windows.Visibility.Hidden;
                    }
                }
            }

            if (image.Name.ToLower().Contains(PlayerRelatedActionsEnum.Weapon.ToString().ToLower()))
            {
                var isPreviousWeaponReleased = DropWeapon_RemoveObserver();
                if (isPreviousWeaponReleased)
                {
                    var weaponsEnum = Enum.GetValues(typeof(WeaponsListEnum)).Cast<WeaponsListEnum>().ToList();
                    foreach (var weaponItem in weaponsEnum)
                    {
                        if (image.Name.Contains(weaponItem.ToString()))
                        {
                            AddWeapon_SubscribeObserver(weaponItem, image);
                        }
                    }
                }
            }
        }

        public bool DropWeapon_RemoveObserver()
        {
            weaponsCarried_Observers.Clear();
            return (weaponsCarried_Observers.Count == 0) ? true : false;
        }

        public List<IWeapon> GetWeapons()
        {
            return weaponsCarried_Observers;
        }

        public List<IEnemy> GetAllEnemies()
        {
            return allEnemies_Observers;
        }

        public void AddEnemy_SubscribeObserver(IEnemy enemy)
        {
            allEnemies_Observers.Add(enemy);
        }

        public void RemoveEnemy_UnsubscribeObserver(IEnemy enemy)
        {
            allEnemies_Observers.Remove(enemy);
        }

        public void RemoveEnemy_UnsubscribeAllObservers()
        {
            allEnemies_Observers.Clear();
        }


        public void PublishPlayerLocationPoint_NotifyObservers()
        {
            var playerImageLocationPoint = _imageManager.GetImageLocation(ShooterImage);
            foreach(IEnemy enemy in allEnemies_Observers)
            {
                enemy.SetPlayerLocation_ReceiveNotification(playerImageLocationPoint);
            }
        }

        #region Helper Methods

        private void AddWeapon_SubscribeObserver(WeaponsListEnum weaponItem, System.Windows.Controls.Image imageWeapon)
        {
            var newWeapon = _weaponsFactory.GetWeapon(weaponItem);
            if (newWeapon != null)
            {
                newWeapon.IsHeldByPlayer = true;
                newWeapon.ImageWeapon = imageWeapon;
                newWeapon.ImageWeapon.Name = imageWeapon.Name;
                weaponsCarried_Observers.Add(newWeapon);
            }
        }

        private void ProcessHealthIncrease(int healthPoints)
        {
            IncreaseHealth(healthPoints);
            var newHealthPoints = GetHealthPoints();
            var newHealthState = _healthInvoker.GetHealthState(newHealthPoints);
            if(newHealthState != null)
            {
                SetSpeedMovementByHealthState(newHealthState);
            }
        }
        
        private void ProcessPlayerMovement(MovementDirectionEnum movementDirection)
        {
            Point movement = _movementStrategy.GetMovement(movementDirection);
            Point playerNewLocationPoint = _imageManager.ModifyLocationPointValues(movement,_speedMovement);
            _imageManager.SetNewLocationImage(ShooterImage, playerNewLocationPoint);
            var weaponsCarriedByPlayer = GetWeapons();
            if(weaponsCarriedByPlayer.Count>0)
            {
                PlayerCarriesAWeapon_SetWeaponMovement_NotifyObservers(playerNewLocationPoint, weaponsCarriedByPlayer);
            }
        }

        private void PlayerCarriesAWeapon_SetWeaponMovement_NotifyObservers(Point playerNewPointLocation, List<IWeapon> weapons_Observers)
        {
            foreach (IWeapon weaponObserver in weapons_Observers)
            {
                if (weaponObserver.IsHeldByPlayer)
                    weaponObserver.SetWeaponMovementWhenIsCarriedByPlayer_GetNotification(ShooterImage);
            }
        }
        #endregion
    }
}
