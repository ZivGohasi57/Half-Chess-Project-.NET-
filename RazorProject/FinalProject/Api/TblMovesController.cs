using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FinalProject.Data;
using FinalProject.Model;

namespace FinalProject.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class TblMovesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public TblMovesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/TblMoves
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TblMoves>>> GetMoves()
        {
          if (_context.Moves == null)
          {
              return NotFound();
          }
            return await _context.Moves.ToListAsync();
        }

        // GET: api/TblMoves/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TblMoves>> GetTblMoves(int id)
        {
          if (_context.Moves == null)
          {
              return NotFound();
          }
            var tblMoves = await _context.Moves.FindAsync(id);

            if (tblMoves == null)
            {
                return NotFound();
            }

            return tblMoves;
        }


        // PUT: api/TblMoves/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTblMoves(int id, TblMoves tblMoves)
        {
            if (id != tblMoves.MoveId)
            {
                return BadRequest();
            }

            _context.Entry(tblMoves).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TblMovesExists(id))
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

        // POST: api/TblMoves
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TblMoves>> PostTblMoves([FromBody] TblMoves move)
        {
            if (move == null)
            {
                return BadRequest("The move object is null.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); 
            }

            if (_context.Moves == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Moves' is null.");
            }

            try
            {
                
                var newMove = new TblMoves
                {
                    GameId = move.GameId,
                    PieceType = move.PieceType,
                    PlayerId = move.PlayerId,
                    TblGamesGameID = move.TblGamesGameID,
                    FromPosition = move.FromPosition,
                    ToPosition = move.ToPosition,
                    TimeTaken = move.TimeTaken
                };

                _context.Moves.Add(newMove);
                await _context.SaveChangesAsync();

                
                return CreatedAtAction("GetTblMoves", new { id = newMove.MoveId }, newMove);
            }
            catch (Exception ex)
            {
                
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }




        // DELETE: api/TblMoves/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTblMoves(int id)
        {
            if (_context.Moves == null)
            {
                return NotFound();
            }
            var tblMoves = await _context.Moves.FindAsync(id);
            if (tblMoves == null)
            {
                return NotFound();
            }

            _context.Moves.Remove(tblMoves);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TblMovesExists(int id)
        {
            return (_context.Moves?.Any(e => e.MoveId == id)).GetValueOrDefault();
        }
    }
}
