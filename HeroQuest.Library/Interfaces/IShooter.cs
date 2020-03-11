using System.Collections.Generic;
using System.Windows.Input;

namespace HeroQuest.Library.Interfaces
{ 
    public interface IShooter
    {
        System.Windows.Controls.Image ShooterImage { get; set; }
        Key ShootingDirection_Player { get; set; }
        void IncreaseHealth(int potionStrength);
        List<IEnemy> GetAllEnemies();
        void IncreasePointForStrikingTarget(int weaponStrikePoints);
        List<System.Windows.Controls.Image> AllSwordSwingImages { get; set; }
        List<System.Windows.Controls.Image> AllMaceSwingImages { get; set; }
        List<System.Windows.Controls.Image> AllArrowShootingLeftImages { get; set; }
        List<System.Windows.Controls.Image> AllArrowShootingRightImages { get; set; }
    }
}
