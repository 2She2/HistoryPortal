namespace HistoryPortal.Web.Controllers
{
    using System.Linq;
    using System.Web.Mvc;

    using AutoMapper.QueryableExtensions;

    using HistoryPortal.Data;
    using HistoryPortal.Web.ViewModels.Home;
    using HistoryPortal.Web.Infrastructure;


    public class HomeController : BaseController
    {
        public HomeController(IHistoryPortalData data, ISanitizer sanitizer)
            : base(data, sanitizer)
        {
        }

        public ActionResult Index()
        {
            var articles = new IndexViewModel();

            articles.NewestArticles = this.Data
                .Articles
                .All()
                .OrderByDescending(a => a.CreatedOn)
                .Take(4)
                .Project().To<IndexArticleViewModel>();

            articles.PopularArticles = this.Data
                .Articles
                .All()
                .OrderByDescending(a => a.Votes.Count)
                .Take(4)
                .Project().To<IndexArticleViewModel>();

            return View(articles);
        }

        [ChildActionOnly]
        [OutputCache(Duration = 60 * 60)]
        public ActionResult NewestArticles()
        {
            var articles = this.Data
               .Articles
               .All()
               .OrderByDescending(a => a.Votes.Count)
               .Take(4)
               .Project().To<IndexArticleViewModel>();

            return PartialView("_IndexArticlePartial", articles);
        }

        [ChildActionOnly]
        [OutputCache(Duration = 60 * 60)]
        public ActionResult MostCommentedTickets()
        {
            var articles = this.Data
               .Articles
               .All()
               .OrderByDescending(a => a.Votes.Count)
               .Take(4)
               .Project().To<IndexArticleViewModel>();

            return PartialView("_IndexArticlePartial", articles);
        }
    }
}