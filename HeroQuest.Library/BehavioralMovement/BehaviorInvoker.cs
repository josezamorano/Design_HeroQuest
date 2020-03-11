using HeroQuest.Library.DependencyInjection;
using HeroQuest.Library.Enums;
using HeroQuest.Library.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace HeroQuest.Library.BehavioralMovement
{
    public class BehaviorInvoker : IBehaviorInvoker
    {
        List<IBehavior> AllBehaviors = new List<IBehavior>();
        IRandomBehavior randomBehavior = null;
        ITargetedBehavior targetedBehavior = null;

        public BehaviorInvoker()
        {
            CreateBehaviors();
            AllBehaviors.Add(randomBehavior);
            AllBehaviors.Add(targetedBehavior);
        }
        public IBehavior GetSelectedBehavior(BehaviorTypeEnum behaviorType)
        {
             var selectedBehavior = AllBehaviors.Where(a => a.BehaviorTypeName == behaviorType).FirstOrDefault();
             return selectedBehavior;
        }

        private void CreateBehaviors()
        {
            randomBehavior = (randomBehavior == null) ? ContainerConfig.GetInstance<IRandomBehavior>() : randomBehavior;
            targetedBehavior = (targetedBehavior == null) ? ContainerConfig.GetInstance<ITargetedBehavior>() : targetedBehavior;
        }
    }
}
