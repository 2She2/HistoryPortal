using HistoryPortal.Data;
using HistoryPortal.Data.Models;
using HistoryPortal.Web.Areas.Administration.Controllers.Base;
using HistoryPortal.Web.Areas.Administration.ViewModels.CommentsAdmin;
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


using Model = HistoryPortal.Data.Models.Comment;
using ViewModel = HistoryPortal.Web.Areas.Administration.ViewModels.CommentsAdmin.CommentsAdminViewModel;
using System.Data.Entity;

namespace HistoryPortal.Web.Areas.Administration.Controllers
{
    public class CommentsAdminController : KendoGridAdministrationController
    {
        public CommentsAdminController(IHistoryPortalData data, ISanitizer sanitizer)
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
            var comments = this.Data
                .Comments
                .All()
                .Project<Comment>().To<CommentsAdminViewModel>();

            return comments;
        }

        protected override T GetById<T>(object id)
        {
            return this.Data.Comments.GetById(id) as T;
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
                var dbModel = this.GetById<Comment>(model.Id);

                dbModel.Content = model.Content;

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
                this.Data.Comments.Delete(model.Id);
                this.Data.SaveChanges();
            }

            return this.GridOperation(model, request);
        }
    }
}