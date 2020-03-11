using HeroQuest.Library.Enums;
using HeroQuest.Library.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HeroQuest.Library.GameLevelsTemplate
{
    public class Level_2 : LevelTemplate, ILevel_2
    {
        public override GameLevelsEnum GameLevelNumber { get { return GameLevelsEnum.Level2; } }

        public override Visibility BluePotionVisibility { get { return Visibility.Visible; } }
        public override Visibility RedPotionVisibility { get { return Visibility.Visible; } }

        public override Visibility SwordVisibility { get { return Visibility.Visible; } }
        public override Visibility MaceVisibility { get { return Visibility.Visible; } }
        public override Visibility BowAndArrowVisibility { get { return Visibility.Visible; } }

        public override HealthStatesEnum HeroPlayerHealthPoints { get { return HealthStatesEnum.Strong; } }
        public override MovementStrategyEnum HeroPlayerMovementStrategy { get { return MovementStrategyEnum.LinearMovement; } }
        public override bool HeroPlayerTimer { get { return true; } }
        
        public override bool ArrowTimer { get { return true; } }
        public override bool ShootingWeaponsVisibilityTimer { get { return true; } }
        public override bool EnemyFireballTimer { get { return true; } }

        public override Visibility BatVisibility { get { return Visibility.Visible; } }
        public override HealthStatesEnum BatHealthPoints { get { return HealthStatesEnum.Normal; } }
        public override MovementStrategyEnum BatMovementStrategy { get { return MovementStrategyEnum.ZigZagMovement; } }
        public override bool BatTimer { get { return true; } }

        public override Visibility WizardVisibility { get { return Visibility.Visible; } }
        public override HealthStatesEnum WizardHealthpoints { get { return HealthStatesEnum.Normal; ; } }
        public override MovementStrategyEnum WizardMovementStrategy { get { return MovementStrategyEnum.LinearMovement; } }
        public override bool WizardTimer { get { return true; } }
    }
}
