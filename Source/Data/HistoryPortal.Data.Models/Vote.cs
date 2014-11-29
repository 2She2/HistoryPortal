namespace HistoryPortal.Data.Models
{
    using HistoryPortal.Data.Common.Models;

    public class Vote : DeletableEntity
    {
        public int Id { get; set; }

        public VoteState State { get; set; }

        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }
    }
}