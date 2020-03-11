using HeroQuest.Library.Enums;
using HeroQuest.Library.Interfaces;
using System.Windows;

namespace HeroQuest.Library.GameLevelsTemplate
{
    public class Level_1 : LevelTemplate, ILevel_1
    {
        public override GameLevelsEnum GameLevelNumber { get { return GameLevelsEnum.Level1; } }

        public override Visibility BluePotionVisibility { get { return Visibility.Visible; } }
        public override Visibility RedPotionVisibility { get { return Visibility.Hidden; } }

        public override Visibility SwordVisibility { get { return Visibility.Visible; } }
        public override Visibility MaceVisibility { get { return Visibility.Visible; } }
        public override Visibility BowAndArrowVisibility { get { return Visibility.Hidden; } }

        public override HealthStatesEnum HeroPlayerHealthPoints { get { return HealthStatesEnum.Strong; } }
        public override MovementStrategyEnum HeroPlayerMovementStrategy { get { return MovementStrategyEnum.LinearMovement; } }
        public override bool HeroPlayerTimer { get { return true; } }

        public override bool ArrowTimer { get { return false; } }
        public override bool ShootingWeaponsVisibilityTimer { get { return true; } }
        public override bool EnemyFireballTimer { get { return false; } }

        public override Visibility BatVisibility { get { return Visibility.Visible; } }
        public override HealthStatesEnum BatHealthPoints { get { return HealthStatesEnum.Normal; } }
        public override MovementStrategyEnum BatMovementStrategy { get { return MovementStrategyEnum.ZigZagMovement; } }
        public override bool BatTimer { get { return true; } }

        public override Visibility WizardVisibility { get { return Visibility.Hidden; } }
        public override HealthStatesEnum WizardHealthpoints { get { return HealthStatesEnum.Dead; ; } }
        public override MovementStrategyEnum WizardMovementStrategy { get { return MovementStrategyEnum.LinearMovement; } }
        public override bool WizardTimer { get { return false; } }
    }
}
