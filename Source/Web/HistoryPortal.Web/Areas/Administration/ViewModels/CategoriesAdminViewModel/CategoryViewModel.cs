using AutoMapper;
using HistoryPortal.Data.Models;
using HistoryPortal.Web.Areas.Administration.ViewModels.ArticlesAdmin;
using HistoryPortal.Web.Areas.Administration.ViewModels.Base;
using HistoryPortal.Web.Infrastructure.Mapping;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HistoryPortal.Web.Areas.Administration.ViewModels.CategoriesAdminViewModel
{
    public class CategoryViewModel : AdministrationViewModel, IMapFrom<Category>, IHaveCustomMappings
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public int ArticlesCount { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<Category, CategoryViewModel>()
               .ForMember(a => a.ArticlesCount, v => v.MapFrom(a => a.Articles.Count))
               .ReverseMap();
        }
    }
}