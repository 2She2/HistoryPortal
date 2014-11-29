using AutoMapper.QueryableExtensions;
using HistoryPortal.Data;
using HistoryPortal.Data.Models;
using HistoryPortal.Web.Areas.Administration.Controllers.Base;
using HistoryPortal.Web.Areas.Administration.ViewModels.CategoriesAdminViewModel;
using HistoryPortal.Web.Infrastructure;
using Kendo.Mvc.UI;
using System.Collections;
using System.Data.Entity;
using System.Globalization;
using System.Threading;
using System.Web.Mvc;
using Model = HistoryPortal.Data.Models.Category;
using ViewModel = HistoryPortal.Web.Areas.Administration.ViewModels.CategoriesAdminViewModel.CategoryViewModel;

namespace HistoryPortal.Web.Areas.Administration.Controllers
{
    public class CategoriesAdminController : KendoGridAdministrationController
    {
        public CategoriesAdminController(IHistoryPortalData data, ISanitizer sanitizer)
            : base(data, sanitizer)
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        }

        // GET: Administration/CategoriesAdmin
        public ActionResult Index()
        {
            return View();
        }

        protected override IEnumerable GetData()
        {
            var categories = this.Data
                .Categories
                .All()
                .Project<Category>().To<CategoryViewModel>();

            return categories;
        }

        protected override T GetById<T>(object id)
        {
            return this.Data.Categories.GetById(id) as T;
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
                var dbModel = this.GetById<Category>(model.Id);

                dbModel.Name = model.Name;

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
                this.Data.Categories.Delete(model.Id);
                this.Data.SaveChanges();
            }

            return this.GridOperation(model, request);
        }
    }
}