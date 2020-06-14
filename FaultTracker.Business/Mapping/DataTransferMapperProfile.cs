using AutoMapper;
using FaultTracker.Business.DataTransfer.Shared;
using FaultTracker.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace FaultTracker.Business.Mapping
{
    public class DataTransferMapperProfile : Profile
    {
        public DataTransferMapperProfile()
        {
            //Map user
            CreateMap<User, UserSharedDto>();
        }
    }
}
