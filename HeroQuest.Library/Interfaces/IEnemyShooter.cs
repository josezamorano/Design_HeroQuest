using HeroQuest.Common.Enums;
using System.Collections.Generic;

namespace HeroQuest.Library.Interfaces
{
    public interface IEnemyShooter
    {
        MovementDirectionEnum ShootingDirection_Enemy { get; set; }
        ITarget Target { get; set; }
        System.Windows.Controls.Image ShooterImage { get; set; }
        List<System.Windows.Controls.Image> AllFireballLeftShootingImages { get; set; }
        List<System.Windows.Controls.Image> AllFireballRightShootingImages { get; set; }
        int EnemyIncreasePointForWeaponStrikingTarget(int weaponStrikePoints);
        int EnemyIncreasePoints_ForStrikingTarget();
    }
}
