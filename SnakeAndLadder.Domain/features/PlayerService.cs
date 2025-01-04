using Microsoft.EntityFrameworkCore;
using SnakeAndLadder.Database.Models;
using SnakeAndLadder.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace SnakeAndLadder.Domain.features
{
    public class PlayerService : IPlayerService
    {
        private readonly AppDbContext _db;

        public PlayerService(AppDbContext db)
        {
            _db = db;
        }

        public async Task<Result<PlayerResponseModel>> CreatePlayer(PlayerRequestModel newPlayer)
        {
            var playerCode = Ulid.NewUlid().ToString().ToUpper().Substring(0, 4) 
                + "-" + Ulid.NewUlid().ToString().ToUpper().Substring(5, 4);
            var tblPlayer = new TblPlayer
            {
                Id = newPlayer.Id,
                PlayerCode = playerCode
            }; 
            await _db.TblPlayers.AddAsync(tblPlayer); 
            await _db.SaveChangesAsync();

            var response = new PlayerResponseModel
            {
                TblPlayer = tblPlayer
            };
            return Result<PlayerResponseModel>.Success(response, "Player created successfully.");
        }

        public async Task<Result<PlayerResponseModel>> GetPlayer(int playerId)
        {
            Result<PlayerResponseModel> model = new Result<PlayerResponseModel>();

            var player = await _db.TblPlayers.AsNoTracking().FirstOrDefaultAsync(x  => x.Id == playerId);
            if (player is null)
            {
                model = Result<PlayerResponseModel>.NotFound("Player Not found");
                goto Result;
            }

            var response = new PlayerResponseModel
            {
                Id = player.Id,
                PlayerCode = player.PlayerCode
            };

            model = Result<PlayerResponseModel>.Success(response, "Player existed");
        Result:
            return model;
        }
    }
}
