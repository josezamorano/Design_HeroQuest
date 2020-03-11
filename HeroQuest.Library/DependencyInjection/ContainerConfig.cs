using Autofac;
using HeroQuest.Library.KeyboardCommands;
using HeroQuest.Library.Interfaces;
using HeroQuest.Library.MovementStrategy;
using HeroQuest.Library.PlayerStates;
using HeroQuest.Common.Interfaces;
using HeroQuest.Common.Managers;
using HeroQuest.Library.WeaponsFactory;
using HeroQuest.Library.WeaponStrikeStrategy;
using HeroQuest.Library.Mediators;
using HeroQuest.Library.BehavioralMovement;
using HeroQuest.Library.EnemiesFactory;
using HeroQuest.Library.Helpers;
using HeroQuest.Library.EnemyWeaponsFactory;
using HeroQuest.Library.GameLevelsTemplate;

namespace HeroQuest.Library.DependencyInjection
{
    public class ContainerConfig
    {
        private static IContainer container=null;
        public static void Configure()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<Bat>().As<IBat>();
            builder.RegisterType<AllEnemiesFactory>().As<IAllEnemiesFactory>();
            builder.RegisterType<AllWeaponsFactory>().As<IAllWeaponsFactory>();
            builder.RegisterType<AllEnemyWeaponsFactory>().As<IAllEnemyWeaponsFactory>();
            builder.RegisterType<BehaviorInvoker>().As<IBehaviorInvoker>();
            builder.RegisterType<BowAndArrow>().As<IBowAndArrow>();
            builder.RegisterType<Castle>().As<ICastle>();
            builder.RegisterType<CommandArrowKeysMovement>().As<ICommandArrowKeysMovement>();
            builder.RegisterType<CommandInvoker>().As<ICommandInvoker>();
            builder.RegisterType<CommandKeyDropItem>().As<ICommandKeyDropItem>();
            builder.RegisterType<CommandKeyPickupItem>().As<ICommandKeyPickupItem>();
            builder.RegisterType<CommandKeyStrikeEnemy>().As<ICommandKeyStrikeEnemy>();
            builder.RegisterType<DeadState>().As<IDeadState>();
            builder.RegisterType<EnemyFireball>().As<IEnemyFireball>();
            builder.RegisterType<GameDashboard>().As<IGameDashboard>();
            builder.RegisterType<GameLevelInvoker>().As<IGameLevelInvoker>();
            builder.RegisterType<HealthInvoker>().As<IHealthInvoker>();
            builder.RegisterType<InjuredState>().As<IInjuredState>();
            builder.RegisterType<ImageManager>().As<IImageManager>();
            builder.RegisterType<KeyboardMediator>().As<IKeyboardMediator>();
            builder.RegisterType<Level_0_Stop>().As<ILevel_0_Stop>();
            builder.RegisterType<Level_1>().As<ILevel_1>();
            builder.RegisterType<Level_2>().As<ILevel_2>();
            builder.RegisterType<LinearStrategy>().As<ILinearStrategy>();
            builder.RegisterType<Mace>().As<IMace>();
            builder.RegisterType<MovementStrategyInvoker>().As<IMovementStrategyInvoker>();
            builder.RegisterType<MovementDirectionManager>().As<IMovementDirectionManager>();
            builder.RegisterType<NormalState>().As<INormalState>();
            builder.RegisterType<Player>().As<IPlayer>();
            builder.RegisterType<RandomBehavior>().As<IRandomBehavior>().SingleInstance();
            builder.RegisterType<StraightShootStrikeStrategy>().As<IStraightShootStrikeStrategy>();
            builder.RegisterType<StrikeStrategyInvoker>().As<IStrikeStrategyInvoker>();
            builder.RegisterType<StrongState>().As<IStrongState>();
            builder.RegisterType<SwingStrikeStrategy>().As<ISwingStrikeStrategy>();
            builder.RegisterType<Sword>().As<ISword>();
            builder.RegisterType<SuperStrongState>().As<ISuperStrongState>();
            builder.RegisterType<TargetedBehavior>().As<ITargetedBehavior>();
            builder.RegisterType<WeakState>().As<IWeakState>();
            builder.RegisterType<Weapon>().As<IWeapon>();
            builder.RegisterType<WeaponHelper>().As<IWeaponHelper>();
            builder.RegisterType<Wizard>().As<IWizard>();
            builder.RegisterType<ZigZagStrategy>().As<IZigZagStrategy>();

            builder.RegisterType<UIMediator>().As<IUIMediator>();

            container = builder.Build();
        }

        public static T GetInstance<T>()
        {
            if(container == null)
            {
                Configure();
            }
            return container.Resolve<T>();
        }
    }
}
