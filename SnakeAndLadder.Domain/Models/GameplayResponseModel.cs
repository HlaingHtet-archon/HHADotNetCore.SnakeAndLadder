using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeAndLadder.Domain.Models
{
    public class GameplayResponseModel
    {
        public string PlayerCode { get; set; } = null!;
        public string PlayerColor { get; set;} = null!;

        public int DiceRoll { get; set; }
        public int MovePosition { get; set; }
        public int TurnOrder { get; set; }

        public string WinnerId { get; set; } = null!;
        public string SecondPlaceId { get; set; } = null!;
        public string ThirdPlaceId { get; set; } = null!;
    }
}
