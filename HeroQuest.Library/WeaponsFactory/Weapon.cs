using HeroQuest.Common.Interfaces;
using HeroQuest.Library.Enums;
using HeroQuest.Library.Interfaces;
using System;
using System.Collections.Generic;
using System.Windows.Controls;

namespace HeroQuest.Library.WeaponsFactory
{
    public class Weapon : IWeapon
    {
        public WeaponsListEnum WeaponName { get; set; }
        public bool IsHeldByPlayer { get; set; }
        public int DamagePower { get; set; }
        public Image ImageWeapon { get; set ; }
        public bool IsWeaponReadyForStrike { get; set; }

        protected IPlayer _player;
        protected IShooter _shooter;
        protected List<IEnemy> _allTargets;
        protected int DisplaceWeaponToHeroHandX { get; set; }
        protected int DisplaceWeaponToHeroHandY { get; set; }

        protected IImageManager _imageManager;
        protected IStrikeStrategyInvoker _strikeStrategyInvoker;
        public Weapon(IImageManager imageManager, IStrikeStrategyInvoker strikeStrategyInvoker)
        {
            _imageManager = imageManager;
            _strikeStrategyInvoker = strikeStrategyInvoker;
        }
        
        //IMPLEMENTATION done in the Children
        public virtual bool Strike(IPlayer shooter)
        {
            throw new NotImplementedException();
        }

        //IMPLEMENTATION Done in the Children
        public virtual void SetNotificationTimer(NotificationTimerEnum notificationTimer)
        {
            throw new NotImplementedException();
        }

        public virtual void SetWeaponMovementWhenIsCarriedByPlayer_GetNotification(System.Windows.Controls.Image PlayerImage)
        {
            System.Drawing.Point playerLocationPoint = new System.Drawing.Point(DisplaceWeaponToHeroHandX, DisplaceWeaponToHeroHandY);
            _imageManager.SetNewLocationForParentAndChildImage(PlayerImage, playerLocationPoint, ImageWeapon);
        }
    }
}
