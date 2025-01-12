using Azure.Core;
using SnakeAndLadder.Database.Models;
using SnakeAndLadder.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;


namespace SnakeAndLadder.Domain.features
{
    public class GameplayService : IGameplayService
    {
        private readonly AppDbContext _db;
        private readonly string[] allowedColors = { "Red", "Blue", "Green", "Black" };

        public GameplayService(AppDbContext db)
        {
            _db = db;
        }

        public async Task<Result<GameplayResponseModel>> AddPlayer(GameplayRequestModel gameRequest)
        {
            Result<GameplayResponseModel> model = new Result<GameplayResponseModel>();

            if (!allowedColors.Contains(gameRequest.PlayerColor, StringComparer.OrdinalIgnoreCase))
            {
                model = Result<GameplayResponseModel>.ValidationError("Invalid color selected. Please choose from Red, Blue, Green, or Black.");
                goto Result;
            }

            List<TblGameplay> gamePlayers;
            try
            { 
                gamePlayers = await _db.TblGameplays.Where(g => g.GameCode == gameRequest.GameCode).ToListAsync();
            }
            catch (Exception ex) 
            { 
                model = Result<GameplayResponseModel>.SystemError("Error accessing the database: " + ex.Message); 
                goto Result;
            }

            if (gamePlayers.Count >= 4) 
            { 
                model = Result<GameplayResponseModel>.ValidationError("The game already has 4 players.");
                goto Result;
            }

            if (gamePlayers.Any(p => p.PlayerColor.Equals(gameRequest.PlayerColor, StringComparison.OrdinalIgnoreCase))) 
            {
                model = Result<GameplayResponseModel>.ValidationError("This color is already taken."); 
                goto Result;
            }

            var player = new TblGameplay 
            { 
                GameCode = gameRequest.GameCode,
                PlayerCode = gameRequest.PlayerCode,
                PlayerColor = gameRequest.PlayerColor, 
                TurnOrder = gamePlayers.Count + 1 
            }; 
            await _db.TblGameplays.AddAsync(player);
            await _db.SaveChangesAsync();
            if (gamePlayers.Count == 3)
            { 
                StartGame(gameRequest.GameCode); 
            } 
            var response = new GameplayResponseModel 
            {
                PlayerCode = gameRequest.PlayerCode,
                PlayerColor = gameRequest.PlayerColor 
            };

            model = Result<GameplayResponseModel>.Success(response, "Player added successfully.");
            goto Result; 
        Result: 
            return model;
        }

        public async Task<Result<GameplayResponseModel>> RollDice(GameplayRequestModel request)
        {
            Result<GameplayResponseModel> model = new Result<GameplayResponseModel>();

            var game = await _db.TblGameplays.FirstOrDefaultAsync(g => g.GameCode == request.GameCode && g.PlayerCode == request.PlayerCode);
            if (game == null)
            {
                model = Result<GameplayResponseModel>.NotFound("Game or Player not found");
                goto Result;
            }

            var diceRoll = new Random().Next(1, 7);
            var newMovePosition = game.MovePosition + diceRoll;

            newMovePosition = await CheckForSnakesAndLadders(newMovePosition);

            game.DiceRoll = diceRoll;
            game.MovePosition = newMovePosition;
            _db.TblGameplays.Update(game);
            await _db.SaveChangesAsync();

            var response = new GameplayResponseModel
            {
                PlayerCode = game.PlayerCode,
                DiceRoll = diceRoll,
                MovePosition = newMovePosition,
                PlayerColor = game.PlayerColor,
                TurnOrder = game.TurnOrder
            };

            model = Result<GameplayResponseModel>.Success(response, "Dice rolled successfully.");
            goto Result;

        Result:
            return model;
        }

        public async Task<Result<GameplayResponseModel>> UpdateWinner(GameplayRequestModel request)
        {
            Result<GameplayResponseModel> model = new Result<GameplayResponseModel>();

            var winner = new TblWinnerPlayer
            {
                GameCode = request.GameCode,
                WinnerId = request.WinnerId,
                SecondPlaceId = request.SecondPlaceId,
                ThirdPlaceId = request.ThirdPlaceId
            };

            await _db.TblWinnerPlayers.AddAsync(winner);
            await _db.SaveChangesAsync();

            var response = new GameplayResponseModel
            {
                WinnerId = request.WinnerId,
                SecondPlaceId = request.SecondPlaceId,
                ThirdPlaceId = request.ThirdPlaceId
            };

            model = Result<GameplayResponseModel>.Success(response, "Winner updated successfully.");
            goto Result;

        Result:
            return model;
        }

        private async Task<int> CheckForSnakesAndLadders(int position)
        { 
            var board = await _db.TblBoards.FirstOrDefaultAsync(b => b.BoardId == position);

            if (board != null && (board.Type.Equals("Snake", StringComparison.OrdinalIgnoreCase) || board.Type.Equals("Ladder", StringComparison.OrdinalIgnoreCase))) 
            {
                int destination; if (int.TryParse(board.Destination, out destination)) 
                { 
                    return destination;
                } 
            }
            return position;
        }

        private void StartGame(string gameCode)
        { 
            Console.WriteLine($"Game {gameCode} has started with 4 players."); 
        }
    }
}
