using System;
using System.Collections.Generic;

namespace SnakeAndLadder.Database.Models;

public partial class TblGameplay
{
    public int Id { get; set; }

    public string GameCode { get; set; } = null!;

    public string PlayerCode { get; set; } = null!;

    public int DiceRoll { get; set; }

    public int MovePosition { get; set; }

    public string PlayerColor { get; set; } = null!;

    public int TurnOrder { get; set; }
}
