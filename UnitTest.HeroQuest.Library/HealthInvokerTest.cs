using System;
using FluentAssertions;
using HeroQuest.Library.Enums;
using HeroQuest.Library.Interfaces;
using HeroQuest.Library.PlayerStates;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTest.HeroQuest.Library
{
    [TestClass]
    public class HealthInvokerTest
    {
        [TestMethod]
        public void GetHealthState_Test()
        {
            HealthInvoker healthInvoker = new HealthInvoker();
            var pointsSuperStrong = 39;
            IHealthState health = healthInvoker.GetHealthState(pointsSuperStrong);
            health.HealthStateName.Should().BeEquivalentTo(HealthStatesEnum.SuperStrong);

            var pointsSuperStrong1 = 25;
            health = healthInvoker.GetHealthState(pointsSuperStrong1);
            health.HealthStateName.Should().BeEquivalentTo(HealthStatesEnum.SuperStrong);
          
            var pointsStrong = 22;
            health = healthInvoker.GetHealthState(pointsStrong);
            health.HealthStateName.Should().BeEquivalentTo(HealthStatesEnum.Strong);

            var pointsNormal = 17;
            health = healthInvoker.GetHealthState(pointsNormal);
            health.HealthStateName.Should().BeEquivalentTo(HealthStatesEnum.Normal);

            var pointsWeak = 11;
            health = healthInvoker.GetHealthState(pointsWeak);
            health.HealthStateName.Should().BeEquivalentTo(HealthStatesEnum.Weak);

            var pointsIll = 4;
            health = healthInvoker.GetHealthState(pointsIll);
            health.HealthStateName.Should().BeEquivalentTo(HealthStatesEnum.Injured);
        
            var pointsDead = 0;
            health = healthInvoker.GetHealthState(pointsDead);
            health.HealthStateName.Should().BeEquivalentTo(HealthStatesEnum.Dead);

        }
    }
}
