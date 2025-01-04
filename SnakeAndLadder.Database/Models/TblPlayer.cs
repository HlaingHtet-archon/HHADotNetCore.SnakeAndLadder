using System;
using System.Collections.Generic;

namespace SnakeAndLadder.Database.Models;

public partial class TblPlayer
{
    public int Id { get; set; }

    public string PlayerCode { get; set; } = null!;
}
