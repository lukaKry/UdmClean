using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UdmClean.UI.Models;
using UdmClean.UI.Services.Base;

namespace UdmClean.UI
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateLeaveTypeDto, CreateLeaveTypeVM>().ReverseMap();

            CreateMap<LeaveTypeDto, LeaveTypeVM>().ReverseMap();
        }
    }
}
