namespace HistoryPortal.Web.Areas.Administration.Controllers
{
    using AutoMapper.QueryableExtensions;
    using HistoryPortal.Data;
    using HistoryPortal.Data.Models;
    using HistoryPortal.Web.Areas.Administration.Controllers.Base;
    using HistoryPortal.Web.Areas.Administration.ViewModels.ArticlesAdmin;
    using HistoryPortal.Web.Infrastructure;
    using Kendo.Mvc.UI;
    using System;
    using System.Collections;
    using System.Data.Entity;
    using System.Globalization;
    using System.Linq;
    using System.Net;
    using System.Threading;
    using System.Web.Mvc;
    using Model = HistoryPortal.Data.Models.Article;
    using ViewModel = HistoryPortal.Web.Areas.Administration.ViewModels.ArticlesAdmin.ArticlesAdminViewModel;

    public class ArticlesAdminController : KendoGridAdministrationController
    {
        public ArticlesAdminController(IHistoryPortalData data, ISanitizer sanitizer)
            : base(data, sanitizer)
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        }

        public ActionResult Index()
        {
            return View();
        }

        protected override IEnumerable GetData()
        {
            var articles = this.Data
                .Articles
                .All()
                .Project<Article>().To<ArticlesAdminViewModel>();

            return articles;
        }

        protected override T GetById<T>(object id)
        {
            return this.Data.Articles.GetById(id) as T;
        }

        [HttpPost]
        public ActionResult Create([DataSourceRequest]DataSourceRequest request, ViewModel model)
        {
            var dbModel = base.Create<Model>(model);
            if (dbModel != null) model.Id = dbModel.Id;
            return this.GridOperation(model, request);
        }

        [HttpPost]
        public ActionResult Update([DataSourceRequest]DataSourceRequest request, ViewModel model)
        {
            if (model != null && ModelState.IsValid)
            {
                var dbModel = this.GetById<Article>(model.Id);
                var newCategory = this.Data.Categories.All().FirstOrDefault(c => c.Name == model.Category.Name);

                if (newCategory == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Invalid Category Name!"); 
                }

                int statusCode = Convert.ToInt32(model.State);
                if (-1 > statusCode || statusCode > 2)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Invalid Status Code!");
                }

                dbModel.Title = model.Title;
                //dbModel.CategoryId = newCategory.Id;
                dbModel.State = model.State;
                
                this.ChangeEntityStateAndSave(dbModel, EntityState.Modified);
                model.ModifiedOn = dbModel.ModifiedOn;
            }

            return this.GridOperation(model, request);
        }

        [HttpPost]
        public ActionResult Destroy([DataSourceRequest]DataSourceRequest request, ViewModel model)
        {
            if (model != null && ModelState.IsValid)
            {
                this.Data.Articles.Delete(model.Id);
                this.Data.SaveChanges();
            }

            return this.GridOperation(model, request);
        }
    }
}