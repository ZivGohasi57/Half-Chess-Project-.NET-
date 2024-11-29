using FinalProject.Model;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinalProject.Model
{
    public class TblQueryResults
    {
        [Key]
        public int QueryId { get; set; } = 0;
        public DateTime? QueryDate { get; set; } = DateTime.Now;


        [ForeignKey("User")]
        public int UserId { get; set; }


        public List<TblGames> Games { get; set; }

        public TblUsers User { get; set; }

        



    }
}