using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FinalProject.Data;
using FinalProject.Model;
using FinalProject.ChessPieces;
using System.Drawing;
using Newtonsoft.Json;

namespace FinalProject.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class TblGamesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public TblGamesController(ApplicationDbContext context)
        {
            _context = context;
        }




        // GET: api/TblGames
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TblGames>>> GetGames()
        {
          if (_context.Games == null)
          {
              return NotFound();
          }
            return await _context.Games.ToListAsync();
        }

        // GET: api/TblGames/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TblGames>> GetTblGames(int id)
        {
          if (_context.Games == null)
          {
              return NotFound();
          }
            var tblGames = await _context.Games.FindAsync(id);

            if (tblGames == null)
            {
                return NotFound();
            }

            return tblGames;
        }

        // PUT: api/TblGames/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTblGames(int id, TblGames tblGames)
        {
            if (id != tblGames.GameID)
            {
                return BadRequest();
            }

            _context.Entry(tblGames).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TblGamesExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }



        [HttpPost]
        public async Task<ActionResult<TblGames>> PostGame([FromBody] TblGames newGame)
        {
            if (newGame == null)
            {
                return BadRequest("Game data is null.");
            }

            newGame.PlayerID = LogInHelper.userID;
            if (newGame.EndDate.HasValue && newGame.StartDate.HasValue)
            {
                
                newGame.GameDuration = (int)(newGame.EndDate.Value - newGame.StartDate.Value).TotalMinutes;
            }
            else
            {
                newGame.GameDuration = 0; 
            }
            newGame.TblUsersId = LogInHelper.userID;

            _context.Games.Add(newGame);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return BadRequest($"Error saving the game: {ex.Message}");
            }

            return CreatedAtAction("GetGame", new { id = newGame.GameID }, newGame);
        }





        [HttpGet("{id}")]
        public async Task<ActionResult<TblGames>> GetGame(int id)
        {
            var game = await _context.Games.FindAsync(id);

            if (game == null)
            {
                return NotFound();
            }

            return game;
        }



        //// POST: api/TblGames
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPost]
        //public async Task<ActionResult<TblGames>> PostTblGames([FromBody] TblGames game)
        //{
        //    if (game == null)
        //    {
        //        return BadRequest("Game data is null.");
        //    }

        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState); // שליחת הודעות שגיאה של ModelState אם הקלט לא תקין
        //    }

        //    if (_context.Games == null)
        //    {
        //        return Problem("Entity set 'ApplicationDbContext.Moves' is null.");
        //    }
        //    try
        //    {
        //        _context.Games.Add(game);
        //        await _context.SaveChangesAsync();
        //        return CreatedAtAction("GetTblGames", new { id = game.GameID }, game);
        //    }
        //    catch (DbUpdateException dbEx)
        //    {
        //        var innerMessage = dbEx.InnerException != null ? dbEx.InnerException.Message : dbEx.Message;
        //        return StatusCode(500, new { Error = "Database error", Details = innerMessage });
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, new { Error = "Server error", Details = ex.Message });
        //    }


        //try
        //{
        //    var newGame = new TblGames
        //    {

        //        StartDate = game.StartDate,
        //        EndDate = game.EndDate,
        //        GameDuration = game.GameDuration,
        //        Moves = game.Moves,
        //        PlayerID = game.PlayerID,
        //        TblUsersId = game.TblUsersId,
        //        Result = game.Result

        //    };
        //    _context.Games.Add(newGame);
        //    await _context.SaveChangesAsync();

        //    // החזרת תשובה עם ה-ID של המשחק החדש
        //    return CreatedAtAction("GetTblGames", new { id = newGame.GameID }, newGame);
        //}
        //catch (Exception ex)
        //{
        //    return StatusCode(500, $"Internal server error: {ex.Message}");
        //}
        //}


        // DELETE: api/TblGames/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTblGames(int id)
        {
            if (_context.Games == null)
                return NotFound("The Games table is not initialized in the database.");

            var game = await _context.Games.FindAsync(id);
            if (game == null)
                return NotFound($"Game with ID {id} was not found.");

            _context.Games.Remove(game);
            await _context.SaveChangesAsync();

            return Ok($"Game with ID {id} was successfully deleted.");
        }

        private bool TblGamesExists(int id)
        {
            return _context.Games?.Any(e => e.GameID == id) ?? false;
        }



        
    }
}

