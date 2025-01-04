using System;
using System.Collections.Generic;

namespace SnakeAndLadder.Database.Models;

public partial class TblWinnerPlayer
{
    public int Id { get; set; }

    public string GameCode { get; set; } = null!;

    public string WinnerId { get; set; } = null!;

    public string SecondPlaceId { get; set; } = null!;

    public string ThirdPlaceId { get; set; } = null!;
}
