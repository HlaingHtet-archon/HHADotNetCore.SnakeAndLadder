using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeAndLadder.Domain.Models
{
    public class BoardResponseModel
    {
        public int BoardId { get; set; }
        public string Type { get; set; } = null!;
        public string Destination { get; set; } = null!;
        public List<BoardResponseModel> Boards { get; set; } = new List<BoardResponseModel>();
    }
}
