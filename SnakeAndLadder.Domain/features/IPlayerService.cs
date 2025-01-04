using SnakeAndLadder.Database.Models;
using SnakeAndLadder.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeAndLadder.Domain.features
{
    public interface IPlayerService
    {
        Task<Result<PlayerResponseModel>> CreatePlayer(PlayerRequestModel newPlayer);
        Task<Result<PlayerResponseModel>> GetPlayer(int playerId);
    }
}
