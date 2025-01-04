using System;
using System.Collections.Generic;

namespace SnakeAndLadder.Database.Models;

public partial class TblBoard
{
    public int BoardId { get; set; }

    public string Type { get; set; } = null!;

    public string Destination { get; set; } = null!;
}
