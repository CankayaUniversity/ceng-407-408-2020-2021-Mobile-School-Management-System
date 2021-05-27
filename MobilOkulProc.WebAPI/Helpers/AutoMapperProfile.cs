using AutoMapper;
using MobilOkulProc.Entities.Concrete;
using MobilOkulProc.WebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace MobilOkulProc.WebAPI.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<zUser, UserModel>();
            CreateMap<RegisterModel, zUser>();
            CreateMap<UpdateModel, zUser>();
        }
    }
}
