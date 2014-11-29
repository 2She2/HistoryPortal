namespace HistoryPortal.Web.InputModels.Votes
{
    using System.ComponentModel.DataAnnotations;

    public class VoteInputModel
    {
        [Required]
        public int Id { get; set; }
    }
}