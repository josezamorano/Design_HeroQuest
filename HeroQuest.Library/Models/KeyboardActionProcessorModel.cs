using HeroQuest.Library.Enums;
using System;
using System.Collections.Generic;
using System.Windows.Controls;

namespace HeroQuest.Library.Models
{
    public class KeyboardActionProcessorModel
    {
        public PlayerRelatedActionsEnum PlayerRelatedAction { get; set; }
        public List<Enum> ActionExecuted { get; set; }
        public List<Image> ImagesCollected { get; set; }
    }
}
