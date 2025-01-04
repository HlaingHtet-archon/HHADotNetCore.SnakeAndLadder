using SnakeAndLadder.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeAndLadder.Domain.Models
{
    public class BoardRequestModel {
        public string Type { get; set; } = null!;
        public string Destination { get; set; } = null!; 
    }
}
