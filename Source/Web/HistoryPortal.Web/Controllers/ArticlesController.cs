    namespace HistoryPortal.Web.Controllers
{
    using System.Linq;
    using System.Web.Mvc;

    using Microsoft.AspNet.Identity;
    using AutoMapper.QueryableExtensions;

    using HistoryPortal.Data;
    using HistoryPortal.Web.ViewModels.Articles;
    using HistoryPortal.Web.InputModels.Articles;
    using HistoryPortal.Data.Models;
    using HistoryPortal.Web.Infrastructure;
    using AutoMapper;
    using System;
    using System.Collections;

    public class ArticlesController : BaseController
    {
        private const int PageSize = 5;

        public ArticlesController(IHistoryPortalData data, ISanitizer sanitizer)
            : base(data, sanitizer)
        {
        }

        // GET: Article
        public ActionResult All(int? id)
        {
            int pageNumber = id.GetValueOrDefault(1);

            var data = this.Data
                .Articles
                .All()
                .OrderByDescending(x => x.CreatedOn)
                .Skip((pageNumber - 1) * PageSize)
                .Take(PageSize)
                .Project()
                .To<ArticlesViewModel>()
                .ToList();

            ViewBag.Pages = Math.Ceiling((double)this.Data.Articles.All().Count() / PageSize);
            ViewBag.CurrPage = pageNumber;

            for (int i = 0; i < data.Count; i++)
            {
                data[i].Content = this.Sanitizer.Sanitize(data[i].Content);
            }

            return View(data);
        }

        public ActionResult Details(int Id = 1)
        {
            // Add data to "CreateArticleInputModel" for the View

            Mapper.CreateMap<Article, ArticlesViewModel>()
                .ForMember(a => a.Ups, opt => opt.MapFrom(u => u.Votes.Where(v => v.State == VoteState.Up).Count()));

            Mapper.CreateMap<Article, ArticlesViewModel>()
                .ForMember(a => a.Downs, opt => opt.MapFrom(u => u.Votes.Where(v => v.State == VoteState.Down).Count()));

            var article = this.Data
                .Articles
                .All()
                .Where(a => a.Id == Id)
                .Project().To<ArticlesViewModel>()
                .FirstOrDefault();

            var categories = this.Data
                .Categories
                .All()
                .Select(x => x.Name).ToList();

            return View(article);
        }

        [HttpGet]
        [Authorize]
        public ActionResult Create()
        {
            var model = new ArticleInputModel();
            return this.View(model);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ArticleInputModel input)
        {
            if (ModelState.IsValid)
            {
                var userId = this.User.Identity.GetUserId();

                var article = new Article
                {
                    Title = input.Title,
                    Content = this.Sanitizer.Sanitize(input.Content),
                    Category = this.Data.Categories.All().FirstOrDefault(x => x.Name == input.Category),
                    AuthorId = userId,
                    State = ArticleState.Pending
                };

                this.Data.Articles.Add(article);
                this.Data.SaveChanges();
                return this.RedirectToAction("details", new { article.Id });
            }

            return this.View(input);
        }
    }
}