using HeroQuest.Library.Enums;
using HeroQuest.Library.Interfaces;
using System.Windows;

namespace HeroQuest.Library.GameLevelsTemplate
{
    public abstract class LevelTemplate
    {
        public abstract GameLevelsEnum GameLevelNumber { get; }

        public abstract Visibility BluePotionVisibility { get; }
        public abstract Visibility  RedPotionVisibility { get; }

        public abstract Visibility SwordVisibility { get; }
        public abstract Visibility MaceVisibility { get; }
        public abstract Visibility BowAndArrowVisibility { get; }

        public abstract HealthStatesEnum HeroPlayerHealthPoints { get; }
        public abstract MovementStrategyEnum HeroPlayerMovementStrategy { get; }
        public abstract bool HeroPlayerTimer { get; }

        public abstract Visibility BatVisibility { get; }
        public abstract HealthStatesEnum BatHealthPoints { get; }
        public abstract MovementStrategyEnum BatMovementStrategy { get; }
        public abstract bool BatTimer { get; }

        public abstract Visibility WizardVisibility { get; }
        public abstract HealthStatesEnum WizardHealthpoints { get; }
        public abstract MovementStrategyEnum WizardMovementStrategy { get; }
        public abstract bool WizardTimer { get; }

        public abstract bool ArrowTimer { get; }
        public abstract bool ShootingWeaponsVisibilityTimer { get; }
        public abstract bool EnemyFireballTimer { get; }
    }
}
