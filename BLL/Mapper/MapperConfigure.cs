using AutoMapper;
using BLL.DTOs;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Mapper
{
    public class MapperConfigure: Profile
    {
        public MapperConfigure() 
        {
            CreateMap<Category,CategoryVM> ();
            CreateMap<NewsArticle,NewsArticleVM> ();    
            CreateMap<Tag,TagVM> ();
            CreateMap<SystemAccountVM,SystemAccountVM>().ReverseMap();
        }
    }

}
