namespace HistoryPortal.Web.Controllers
{
    using System.Linq;
    using System.Web.Mvc;

    using Microsoft.AspNet.Identity;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;

    using HistoryPortal.Data;
    using HistoryPortal.Web.Infrastructure;
    using HistoryPortal.Data.Models;
    using HistoryPortal.Web.ViewModels.Votes;
    using HistoryPortal.Web.ViewModels.Articles;

    public class VotesController : BaseController
    {
        public VotesController(IHistoryPortalData data, ISanitizer sanitizer)
            : base(data, sanitizer)
        {
        }

        [HttpPost]
        [Authorize]
        public ActionResult Up(int Id)
        {
            var model = this.Vote(Id, VoteState.Up);
            if (model == null)
            {
                return this.PartialView();
            }

            return this.PartialView("_VotesPartial", model);
        }

        [HttpPost]
        [Authorize]
        public ActionResult Down(int Id)
        {
            var model = this.Vote(Id, VoteState.Down);
            if (model == null)
            {
                return this.PartialView();
            }

            return this.PartialView("_VotesPartial", model);
        }

        private ArticlesViewModel Vote(int Id, VoteState newVote)
        {
            var currArticle = this.Data
                .Articles
                .All()
                .FirstOrDefault(a => a.Id == Id);

            var vote = this.GetUserVote(currArticle);

            if (vote != null && vote.State == newVote)
            {
                 return this.Data
                    .Articles
                    .All()
                    .Where(a => a.Id == Id)
                    .Project().To<ArticlesViewModel>()
                    .FirstOrDefault();
            }

            if (vote != null && vote.State != newVote)
            {
                currArticle.Votes.Remove(vote);
            }

            var voteModel = this.AddVote(currArticle, newVote);
            return voteModel;
        }

        private Vote GetUserVote(Article article)
        {
            var vote = article
                .Votes
                .FirstOrDefault(x => x.UserId == this.User.Identity.GetUserId());

            return vote;
        }

        private ArticlesViewModel AddVote(Article currArticle, VoteState state)
        {
            if (currArticle == null)
            {
                ModelState.AddModelError("ArticleId", "Article not founed!");
            }

            var uId = this.User.Identity.GetUserId();

            var vote = new Vote()
            {
                State = state,
                UserId = this.User.Identity.GetUserId()
            };

            currArticle.Votes.Add(vote);
            this.Data.SaveChanges();

            var upsCount = currArticle.Votes.Where(v => v.State == VoteState.Up).Count();
            var downsCount = currArticle.Votes.Where(v => v.State == VoteState.Down).Count();

            var newVoteModel = new VotesViewModel()
            {
                Ups = upsCount,
                Downs = downsCount
            };

            //Mapper.CreateMap<Article, ArticlesViewModel>()
            //    .ForMember(a => a.Ups, opt => opt.MapFrom(u => u.Votes.Where(v => v.State == VoteState.Up).Count()));

            //Mapper.CreateMap<Article, ArticlesViewModel>()
            //    .ForMember(a => a.Downs, opt => opt.MapFrom(u => u.Votes.Where(v => v.State == VoteState.Down).Count()));


            var article = this.Data
                .Articles
                .All()
                .Where(a => a.Id == currArticle.Id)
                .Project().To<ArticlesViewModel>()
                .FirstOrDefault();

            return article;
        }
    }

}