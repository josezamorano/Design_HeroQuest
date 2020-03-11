using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using HeroQuest.Common.Enums;
using HeroQuest.Common.Interfaces;
using HeroQuest.Library.Enums;
using HeroQuest.Library.Interfaces;
using HeroQuest.Library.Models;

namespace HeroQuest.Library.EnemiesFactory
{
    public class Wizard : IWizard
    {
        public Image EnemyImage { get; set; }
        public Image ShooterImage { get; set; }
        public ITarget Target { get; set; }
        public List<Image> AllFireballLeftShootingImages { get; set; }
        public List<Image> AllFireballRightShootingImages { get; set; }
        public EnemyNameEnum EnemyName { get; set; }
        public MovementDirectionEnum MovementDirection { get ; set ; }
        public int StrikePoints { get; set; }
        public MovementDirectionEnum ShootingDirection_Enemy { get; set; }
        

        private IBehavior _selectedBehavior;
        private IMovementStrategy _movementStrategy;
        private int _totalAccummulatedStrikePoints;
        
        private int _healthReservePoints;
        private int _speedMovement;
        private System.Windows.Point _playerLocationPoint;

        IBehaviorInvoker _behaviorInvoker;
        ICastle _castle;
        IHealthInvoker _healthInvoker;
        IImageManager _imageManager;
        IMovementDirectionManager _movementDirectionManager;
        IMovementStrategyInvoker _movementStrategyInvoker;
        
        public Wizard(
            IBehaviorInvoker behaviorInvoker,
            ICastle castle ,
            IHealthInvoker healthInvoker, 
            IImageManager imageManager , 
            IMovementDirectionManager movementDirectionManager, 
            IMovementStrategyInvoker movementStrategyInvoker
            )
        {
            _behaviorInvoker = behaviorInvoker;
            _castle = castle;
            _healthInvoker = healthInvoker;
            _imageManager  = imageManager;
            _movementDirectionManager = movementDirectionManager;
            _movementStrategyInvoker = movementStrategyInvoker;
            
            EnemyName = EnemyNameEnum.Wizard;
            _speedMovement = 8;
            StrikePoints = 5;
            SetDefaultMovementStrategy();
            SetBehaviorType(BehaviorTypeEnum.Random);
            MovementDirection = movementDirectionManager.GetRandomDirection();
        }

        public void SetDefaultMovementStrategy()
        {
            var movementStrategy = _movementStrategyInvoker.GetMovementStrategy(MovementStrategyEnum.LinearMovement);
            _movementStrategy = movementStrategy;
        }
       
        public void SetHealth(HealthStatesEnum healthState)
        {
            var selectedHealthState = _healthInvoker.GetHealthState((int)healthState);
            _healthReservePoints = (selectedHealthState.HealthPoints);
        }

        public void SetBehaviorType(BehaviorTypeEnum behaviorType)
        {
            _selectedBehavior = _behaviorInvoker.GetSelectedBehavior(behaviorType);
        }
        
        public void SetMovementStrategy(MovementStrategyEnum movementStrategy)
        {
            _movementStrategy = _movementStrategyInvoker.GetMovementStrategy(movementStrategy);
        }

        public int EnemyIncreasePointForWeaponStrikingTarget(int weaponStrikePoints)
        {
            _totalAccummulatedStrikePoints += weaponStrikePoints;
            return weaponStrikePoints;
        }
       
        public int EnemyIncreasePoints_ForStrikingTarget()
        {
            _totalAccummulatedStrikePoints += StrikePoints;
            return StrikePoints;
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
        
        public void SetPlayerLocation_ReceiveNotification(Point locationPoint)
        {
            _playerLocationPoint = locationPoint;
        }

        public void Move(MovementDirectionEnum movementDirection)
        {
            //WIZARD
            //Define Behavior
            System.Windows.Point wizardLocation = _imageManager.GetImageLocation(EnemyImage);
            BehaviorPropertiesModel behaviorPropertiesModel = new BehaviorPropertiesModel()
            {
                EnemyName = EnemyName,
                MovementDirection = movementDirection,
                Target = _playerLocationPoint,
                Chaser = wizardLocation,
            };
            var behavioralDirection = _selectedBehavior.ApplyBehavior(behaviorPropertiesModel);
            //Confirm Castle Wall Boundaries reached
            var _imageObjectShapeModel = _castle.GetCanvasLocationAndDimensionsOuterLimits();
            var castleSideWallBoundaryReached = _imageManager.ImageIsBeyondBoundaries_Adaptor(_imageObjectShapeModel, EnemyImage);

            var selectedMovementDirection = (castleSideWallBoundaryReached == MovementDirectionEnum.MovementClear_NoBoundariesReached)
                                            ? behavioralDirection
                                            : _movementDirectionManager.GetOppositeDirection(castleSideWallBoundaryReached);

            //Set Movement 
            MovementDirection = selectedMovementDirection;
            ShootingDirection_Enemy =(MovementDirection == MovementDirectionEnum.MovingLeft || MovementDirection == MovementDirectionEnum.MovingRight)? MovementDirection : MovementDirectionEnum.NotMoving;
            //Process Movement 
            System.Drawing.Point newLocationPoint = _movementStrategy.GetMovement(MovementDirection);
            System.Drawing.Point newSpeedLocationPoint = _imageManager.ModifyLocationPointValues(newLocationPoint, _speedMovement);
            _imageManager.SetNewLocationImage(EnemyImage, newSpeedLocationPoint);
        }
    }
}
