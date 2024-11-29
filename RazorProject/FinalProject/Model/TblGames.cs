using FinalProject.Model;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace FinalProject.Model
{
    public class TblGames
    {
        [Key]
        public int GameID { get; set; } = 0;

        public DateTime? StartDate { get; set; } = DateTime.Now;
        public DateTime? EndDate { get; set; }
        public int? GameDuration { get; set; }
        public int? Moves { get; set; } = 0;


        //[NotMapped]
        //public ChessBoard? chessBoard { get; set; }


        public int? PlayerID {  get; set; }




        public List<TblMoves> MovesWeb { get; set; } = new List<TblMoves>();

        [ForeignKey("TblUsersId")]
        public int? TblUsersId { get; set; }
        public TblUsers? User { get; set; }

        [Column(TypeName = "nvarchar(50)")] 
        public string? Result { get; set; }


        
    }

   


}