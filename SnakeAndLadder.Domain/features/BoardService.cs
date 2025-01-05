using Microsoft.EntityFrameworkCore;
using SnakeAndLadder.Database.Models;
using SnakeAndLadder.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeAndLadder.Domain.features
{
    public class BoardService : IBoardService
    {
        private readonly AppDbContext _db;

        public BoardService(AppDbContext db)
        {
            _db = db;
        }

        public async Task<Result<BoardResponseModel>> CreateBoard(BoardRequestModel newBoard)
        {
            var board = new TblBoard
            {
                Type = newBoard.Type,
                Destination = newBoard.Destination,
            };
            await _db.TblBoards.AddAsync(board);
            await _db.SaveChangesAsync();

            var response = new BoardResponseModel
            {
                BoardId = board.BoardId,
                Type = board.Type,
                Destination = board.Destination
            };
            return Result<BoardResponseModel>.Success(response, "Board created successfully.");
        }

        public async Task<Result<BoardResponseModel>> GetBoardById(int boardId)
        {
            Result<BoardResponseModel> model = new Result<BoardResponseModel>();

            var board = await _db.TblBoards.AsNoTracking().FirstOrDefaultAsync(x => x.BoardId == boardId);
            if (board is null) 
            {
                model = Result<BoardResponseModel>.NotFound("Board not found");
                goto Result;
            }

            var response = new BoardResponseModel 
            { 
                BoardId = board.BoardId,
                Type = board.Type, 
                Destination = board.Destination 
            }; 
            model = Result<BoardResponseModel>.Success(response, "Board found");
        Result: 
            return model;
        }

        public async Task<Result<BoardResponseModel>> GetBoards()
        {
            var boards = await _db.TblBoards.AsNoTracking().ToListAsync();
            var response = new BoardResponseModel 
            { 
                Boards = boards.Select(board => new BoardResponseModel 
                { 
                    BoardId = board.BoardId, 
                    Type = board.Type,
                    Destination = board.Destination
                }).ToList()
            };

            return Result<BoardResponseModel>.Success(response, "Boards retrieved successfully.");
        }

        public async Task<Result<BoardResponseModel>> UpdateBoard(int boardId, BoardRequestModel updatedBoard) 
        {
            Result<BoardResponseModel> model = new Result<BoardResponseModel>();
            var board = await _db.TblBoards.FirstOrDefaultAsync(x => x.BoardId == boardId);
            if (board is null) 
            {
                model = Result<BoardResponseModel>.NotFound("Board not found");
                goto Result;
            } 

            board.Type = updatedBoard.Type;
            board.Destination = updatedBoard.Destination;
            await _db.SaveChangesAsync();

            var response = new BoardResponseModel
            { 
                BoardId = board.BoardId, 
                Type = board.Type,
                Destination = board.Destination 
            }; 

            model = Result<BoardResponseModel>.Success(response, "Board updated successfully.");
        Result:
            return model;
        }

        public async Task<Result<TblBoard>> DeleteBoard(int boardId)
        {
            var board = await _db.TblBoards.FirstOrDefaultAsync(x => x.BoardId == boardId);
            if(board is null)
            {
                return Result<TblBoard>.NotFound("Board not found.");
            }

            _db.TblBoards.Remove(board);
            await _db.SaveChangesAsync();

            return Result<TblBoard>.Success(board, "Board deleted successfully.");
        }
    }
}
