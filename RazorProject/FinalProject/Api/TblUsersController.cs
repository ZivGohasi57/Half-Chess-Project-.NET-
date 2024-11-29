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
    public class TblUsersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public TblUsersController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("GetUserName")]
        public IActionResult GetUserName()
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == LogInHelper.userID);
            if (user == null)
            {
                return NotFound("User not found.");
            }
            return Ok(user.Name);
        }


        [HttpGet("GetUser")]
        public async Task<ActionResult<TblUsers>> GetUser()
        {
            var user = await _context.Users.FindAsync(LogInHelper.userID);
            if (user == null)
            {
                return NotFound("User not found.");
            }
            return Ok(user);
        }



        [HttpPut("UpdateGameCount")]
        public IActionResult UpdateGameCount()
        {
           
            var user = _context.Users.FirstOrDefault(u => u.Id == LogInHelper.userID);
            if (user == null)
            {
                return NotFound("User not found.");
            }

            //  GameCount ++
            user.GamesPlayed += 1;
            _context.SaveChanges();

            return Ok($"GameCount updated to {user.GamesPlayed}");
        }







//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        // GET: api/TblUsers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TblUsers>>> GetUsers()
        {
          if (_context.Users == null)
          {
              return NotFound();
          }
            return await _context.Users.ToListAsync();
        }

        // GET: api/TblUsers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TblUsers>> GetTblUsers(int id)
        {
          if (_context.Users == null)
          {
              return NotFound();
          }
            var tblUsers = await _context.Users.FindAsync(id);

            if (tblUsers == null)
            {
                return NotFound();
            }

            return tblUsers;
        }

        // PUT: api/TblUsers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTblUsers(int id, TblUsers tblUsers)
        {
            if (id != tblUsers.Id)
            {
                return BadRequest();
            }

            _context.Entry(tblUsers).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TblUsersExists(id))
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

        // POST: api/TblUsers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TblUsers>> PostTblUsers(TblUsers tblUsers)
        {
          if (_context.Users == null)
          {
              return Problem("Entity set 'ApplicationDbContext.Users'  is null.");
          }
            _context.Users.Add(tblUsers);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTblUsers", new { id = tblUsers.Id }, tblUsers);
        }

        // DELETE: api/TblUsers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTblUsers(int id)
        {
            if (_context.Users == null)
            {
                return NotFound();
            }
            var tblUsers = await _context.Users.FindAsync(id);
            if (tblUsers == null)
            {
                return NotFound();
            }

            _context.Users.Remove(tblUsers);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TblUsersExists(int id)
        {
            return (_context.Users?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
