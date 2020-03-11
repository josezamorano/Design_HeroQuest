using HeroQuest.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeroQuest.Common.Models
{
    public class MovementDiagonalModel
    {
        public ShapeEdgesEnum CornerDirection { get; set; }
        public List<MovementDirectionEnum> DiagonalMoveCombination { get; set; }
    }
}
