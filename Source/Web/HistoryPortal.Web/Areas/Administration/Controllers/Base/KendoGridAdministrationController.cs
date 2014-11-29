namespace HistoryPortal.Web.Areas.Administration.Controllers.Base
{
    using System.Collections;
    using System.Data.Entity;
    using System.Web.Mvc;

    using AutoMapper;
    using Kendo.Mvc.Extensions;
    using Kendo.Mvc.UI;

    using HistoryPortal.Data;
    using HistoryPortal.Data.Common.Models;
    using HistoryPortal.Web.Areas.Administration.ViewModels.Base;
    using HistoryPortal.Web.Infrastructure;
    using System.Threading;
    using System.Globalization;

    public abstract class KendoGridAdministrationController : AdminController
    {
        public KendoGridAdministrationController(IHistoryPortalData data, ISanitizer sanitizer)
            : base(data, sanitizer)
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        }

        protected abstract IEnumerable GetData();

        protected abstract T GetById<T>(object id) where T : class;

        [HttpPost]
        public ActionResult Read([DataSourceRequest]DataSourceRequest request)
        {
            var ads =
                this.GetData()
                .ToDataSourceResult(request);

            return this.Json(ads);
        }

        [NonAction]
        protected virtual T Create<T>(object model) where T : class
        {
            if (model != null && ModelState.IsValid)
            {
                var dbModel = Mapper.Map<T>(model);
                this.ChangeEntityStateAndSave(dbModel, EntityState.Added);
                return dbModel;
            }

            return null;
        }

        [NonAction]
        protected virtual void Update<TModel, TViewModel>(TViewModel model, object id)
            where TModel : AuditInfo
            where TViewModel : AdministrationViewModel
        {
            if (model != null && ModelState.IsValid)
            {
                var dbModel = this.GetById<TModel>(id);

                Mapper.Map<TViewModel, TModel>(model, dbModel);
                this.ChangeEntityStateAndSave(dbModel, EntityState.Modified);
                model.ModifiedOn = dbModel.ModifiedOn;
            }
        }

        protected JsonResult GridOperation<T>(T model, [DataSourceRequest]DataSourceRequest request)
        {
            return Json(new[] { model }.ToDataSourceResult(request, ModelState));
        }

        protected void ChangeEntityStateAndSave(object dbModel, EntityState state)
        {
            var entry = this.Data.Context.Entry(dbModel);
            entry.State = state;
            this.Data.SaveChanges();
        }
    }
}