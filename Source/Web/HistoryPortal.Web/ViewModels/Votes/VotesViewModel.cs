namespace HistoryPortal.Web.ViewModels.Votes
{
    using HistoryPortal.Data.Models;
    using HistoryPortal.Web.Infrastructure.Mapping;

    public class VotesViewModel
    {
        public int Ups { get; set; }

        public int Downs { get; set; }
    }
}