using SnakeAndLadder.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeAndLadder.Domain.Models
{
    public class GameplayRequestModel
    {
        public string GameCode { get; set; } = null!;
        public string PlayerCode { get; set; } = null!; 
        public string PlayerColor { get; set; } = null!;
        public string WinnerId { get; set; } = null!;
        public string SecondPlaceId { get; set; } = null!; 
        public string ThirdPlaceId { get; set; } = null!;
    }
}
