using HeroQuest.Common.Enums;
using HeroQuest.Library.Enums;
using HeroQuest.Library.Interfaces;
using HeroQuest.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HeroQuest.Library.BehavioralMovement
{
    public class RandomBehavior: IRandomBehavior
    {
        public BehaviorTypeEnum BehaviorTypeName { get; set; }
        private Random random = new Random();
        private int lowerLimitConsecutiveMovements = 10;
        private int upperLimitConsecutiveMovements = 25;
        private List<BehaviorEnemyInfoModel> behaviorEnemies = new List<BehaviorEnemyInfoModel>();

        IMovementDirectionManager _movementDirectionManager;
        public RandomBehavior(IMovementDirectionManager movementDirectionManager)
        {
            _movementDirectionManager = movementDirectionManager;
            BehaviorTypeName = BehaviorTypeEnum.Random;
        }

        public MovementDirectionEnum ApplyBehavior(BehaviorPropertiesModel behaviorPropertiesModel)
        {
            var enemyFound = behaviorEnemies.Where(a => a.EnemyName == behaviorPropertiesModel.EnemyName).FirstOrDefault();
            if(enemyFound == null)
            {
                var enemyNewInfo = new BehaviorEnemyInfoModel()
                {
                    EnemyName = behaviorPropertiesModel.EnemyName,
                    ConsecutiveMovements = GetNonRepeatingRandomConsecutiveMovements(),
                    IterationCounter = 0,
                };
               
                behaviorEnemies.Add(enemyNewInfo);
            }
            MovementDirectionEnum movementDirection = MovementDirectionEnum.NotMoving;
            foreach (var singleEnemy in behaviorEnemies)
            {
                
                if (singleEnemy.EnemyName == behaviorPropertiesModel.EnemyName)
                {
                    singleEnemy.IterationCounter++;
                    movementDirection = (behaviorPropertiesModel.MovementDirection.HasValue)
                                                      ? (MovementDirectionEnum)behaviorPropertiesModel.MovementDirection
                                                      : MovementDirectionEnum.NotMoving;

                    if (singleEnemy.ConsecutiveMovements== singleEnemy.IterationCounter)
                    {
                        movementDirection = _movementDirectionManager.GetNonRepeatingRandomDirection(movementDirection);
                        singleEnemy.ConsecutiveMovements = GetNonRepeatingRandomConsecutiveMovements();
                        singleEnemy.IterationCounter = 0;
                    }
                }
            }
            return movementDirection;
        }

        private int GetNonRepeatingRandomConsecutiveMovements()
        {
            var numberIsRepeating = true;
            var randomNumber = 0;
            while (numberIsRepeating)
            {
                var countRepetitions = 0;
                randomNumber = random.Next(lowerLimitConsecutiveMovements, upperLimitConsecutiveMovements);
                foreach (var enemy in behaviorEnemies)
                {
                    if (enemy.ConsecutiveMovements == randomNumber)
                    {
                        countRepetitions++;
                    }
                }
                if (countRepetitions == 0)
                {
                    numberIsRepeating = false;
                }
            }
             return randomNumber;
        } 
    }
}
