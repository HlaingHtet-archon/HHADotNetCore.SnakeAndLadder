using SnakeAndLadder.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeAndLadder.Domain.features
{
    public interface IGameplayService
    {
        Task<Result<GameplayResponseModel>> AddPlayer(GameplayRequestModel request);
        Task<Result<GameplayResponseModel>> RollDice(GameplayRequestModel request);
        Task<Result<GameplayResponseModel>> UpdateWinner(GameplayRequestModel request);
    }

}
