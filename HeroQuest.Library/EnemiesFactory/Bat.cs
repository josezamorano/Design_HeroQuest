using HeroQuest.Common.Enums;
using HeroQuest.Common.Interfaces;
using HeroQuest.Library.Enums;
using HeroQuest.Library.Interfaces;
using HeroQuest.Library.Models;
using System.Collections.Generic;
using System.Windows.Controls;

namespace HeroQuest.Library.EnemiesFactory
{
    public class Bat : IBat
    {
        public EnemyNameEnum EnemyName { get; set; }
        public Image EnemyImage { get; set; }

        public MovementDirectionEnum MovementDirection { get; set; }
        private int _speedMovement;
        private int _strikePoints;
        private System.Windows.Point _playerLocationPoint;

        /**** Required when the Enemy shoots an weapon ****/
       
        public ITarget Target { get; set; }
        public Image ShooterImage { get; set; }
        public MovementDirectionEnum ShootingDirection_Enemy { get; set; }
        public List<Image> AllFireballLeftShootingImages { get;set; }
        public List<Image> AllFireballRightShootingImages { get; set; }
        public int EnemyIncreasePointForWeaponStrikingTarget(int weaponStrikePoints)
        {
            _totalAccummulatedStrikePoints += weaponStrikePoints;
            return weaponStrikePoints;
        }
        /*==================================================*/

        private IBehavior _selectedBehavior;
        private IMovementStrategy _movementStrategy;
        private int _totalAccummulatedStrikePoints;
        private int _healthReservePoints;
       
        IBehaviorInvoker _behaviorInvoker;
        ICastle _castle;
        IHealthInvoker _healthInvoker;
        IImageManager _imageManager;
        IMovementDirectionManager _movementDirectionManager;
        IMovementStrategyInvoker _movementStrategyInvoker;
        public Bat(
            IBehaviorInvoker behaviorInvoker, 
            ICastle castle,
            IHealthInvoker healthInvoker, 
            IImageManager imageManager, 
            IMovementDirectionManager movementDirectionManager, 
            IMovementStrategyInvoker movementStrategyInvoker
            )
        {
            _behaviorInvoker = behaviorInvoker;
            _castle = castle;
            _healthInvoker = healthInvoker;
            _imageManager = imageManager;
            _movementDirectionManager = movementDirectionManager;
            _movementStrategyInvoker = movementStrategyInvoker;
            
            EnemyName = EnemyNameEnum.Bat;
            _speedMovement = 8;
            _strikePoints = 5;
            SetBehaviorType(BehaviorTypeEnum.Random);
            MovementDirection = movementDirectionManager.GetRandomDirection();
        }

        public void SetBehaviorType(BehaviorTypeEnum behaviorType)
        {
            _selectedBehavior = _behaviorInvoker.GetSelectedBehavior(behaviorType);
        }

        public void SetHealth(HealthStatesEnum healthState)
        {
            IHealthState selectedHealthState = _healthInvoker.GetHealthState((int)healthState);
            _healthReservePoints = (selectedHealthState.HealthPoints);
        }

        public void SetMovementStrategy(MovementStrategyEnum movementStrategy)
        {
            _movementStrategy = _movementStrategyInvoker.GetMovementStrategy(movementStrategy);
        }
        
        public int EnemyIncreasePoints_ForStrikingTarget()
        {
            _totalAccummulatedStrikePoints += _strikePoints;
            return _strikePoints;
        }
        
        public int GetTotalAccummulatedStrikePoints()
        {
            return _totalAccummulatedStrikePoints;
        }
        
        public void IncreaseHealth(int pointsStrength)
        {
            _healthReservePoints += pointsStrength;
        }

        public void ReduceHealth(int damagePoints)
        {
            _healthReservePoints -= damagePoints;
        }
        
        public int GetHealthPoints()
        {
            return _healthReservePoints;
        }
        
        public void SetPlayerLocation_ReceiveNotification(System.Windows.Point locationPoint)
        {
            _playerLocationPoint = locationPoint;
        }
        
        public void Move(MovementDirectionEnum movementDirection)
        {   //BAT
            //Define Behavior
            System.Windows.Point batLocation = _imageManager.GetImageLocation(EnemyImage);
            BehaviorPropertiesModel behaviorPropertiesModel = new BehaviorPropertiesModel()
            {
                EnemyName = EnemyName,
                MovementDirection = movementDirection,
                Target = _playerLocationPoint,
                Chaser = batLocation,
            };

            var behavioralDirection = _selectedBehavior.ApplyBehavior(behaviorPropertiesModel);
            //Confirm Castle Wall Boundaries reached
            var _imageObjectShapeModel = _castle.GetCanvasLocationAndDimensionsOuterLimits();
            var castleSideWallBoundaryReached = _imageManager.ImageIsBeyondBoundaries_Adaptor(_imageObjectShapeModel, EnemyImage);

            var selectedMovementDirection = (castleSideWallBoundaryReached == MovementDirectionEnum.MovementClear_NoBoundariesReached) 
                                            ? behavioralDirection
                                            :_movementDirectionManager.GetOppositeDirection(castleSideWallBoundaryReached);
                                       
            //Set Movement 
            MovementDirection = selectedMovementDirection; 
            //Process Movement 
            System.Drawing.Point newLocationPoint = _movementStrategy.GetMovement(MovementDirection);
            System.Drawing.Point newSpeedLocationPoint = _imageManager.ModifyLocationPointValues(newLocationPoint, _speedMovement);
            _imageManager.SetNewLocationImage(EnemyImage, newSpeedLocationPoint);
        }
    }
}
