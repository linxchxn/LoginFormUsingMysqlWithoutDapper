using System.ComponentModel.DataAnnotations;

namespace LoginFormForGameInUnity.Models
{
    public class UserScore
    {
        [Key]
        public int UserID { get; set; }
        public int Score { get; set; }
        public int TotalScore { get; set; }
        public DateTime RecordPlayDate { get; set; }
    }
}
