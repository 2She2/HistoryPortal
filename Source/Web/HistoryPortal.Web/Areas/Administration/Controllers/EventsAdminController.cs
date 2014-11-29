using HistoryPortal.Data;
using HistoryPortal.Data.Models;
using HistoryPortal.Web.Areas.Administration.Controllers.Base;
using HistoryPortal.Web.Areas.Administration.ViewModels.ArticlesAdmin;
using HistoryPortal.Web.Infrastructure;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using AutoMapper.QueryableExtensions;
using Kendo.Mvc.UI;
using Model = HistoryPortal.Data.Models.Event;
using ViewModel = HistoryPortal.Web.Areas.Administration.ViewModels.EventsAdmin.EventsAdminViewModel;
using System.Data.Entity;
using HistoryPortal.Web.Areas.Administration.ViewModels.EventsAdmin;

namespace HistoryPortal.Web.Areas.Administration.Controllers
{
    public class EventsAdminController : KendoGridAdministrationController
    {
        public EventsAdminController(IHistoryPortalData data, ISanitizer sanitizer)
            : base(data, sanitizer)
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        }

        // GET: Administration/Events
        public ActionResult Index()
        {
            return View();
        }

        protected override IEnumerable GetData()
        {
            var articles = this.Data
                .Events
                .All()
                .Project<Event>().To<EventsAdminViewModel>();

            return articles;
        }

        protected override T GetById<T>(object id)
        {
            return this.Data.Events.GetById(id) as T;
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
                var dbModel = this.GetById<Event>(model.Id);

                dbModel.Title = model.Title;
                dbModel.Description = model.Description;
                dbModel.Date = model.Date;

                this.ChangeEntityStateAndSave(dbModel, EntityState.Modified);
                model.ModifiedOn = dbModel.ModifiedOn;
            }

            return this.GridOperation(model, request);
        }


    }
}