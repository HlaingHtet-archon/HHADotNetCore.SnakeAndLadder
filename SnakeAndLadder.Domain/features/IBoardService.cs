using SnakeAndLadder.Database.Models;
using SnakeAndLadder.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeAndLadder.Domain.features
{
    public interface IBoardService
    {
        Task<Result<BoardResponseModel>> CreateBoard(BoardRequestModel newBoard);
        Task<Result<BoardResponseModel>> GetBoardById(int boardId);
        Task<Result<BoardResponseModel>> GetBoards();
        Task<Result<BoardResponseModel>> UpdateBoard(int boardId, BoardRequestModel updatedBoard);
        Task<Result<TblBoard>> DeleteBoard(int boardId);
    }
}
