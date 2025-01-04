using SnakeAndLadder.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeAndLadder.Domain.Models
{
    public class PlayerResponseModel
    {
        public TblPlayer TblPlayer {  get; set; }

        public int Id { get; set; }
        public string PlayerCode { get; set; } = null!;
    }
}
