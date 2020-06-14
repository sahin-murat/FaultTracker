using AutoMapper;
using FaultTracker.Business.DataTransfer.Request;
using FaultTracker.Business.DataTransfer.Shared;
using FaultTracker.Data.Entities;

namespace FaultTracker.Business.Mapping
{
    public class DataTransferMapperProfile : Profile
    {
        public DataTransferMapperProfile()
        {
            #region User Mappers

            //Map User => UserSharedDto
            CreateMap<User, UserSharedDto>();

            //Map UserSharedDto => User
            CreateMap<UserSharedDto, User>();

            //Map UserRequestDto => User
            CreateMap<UserRequestDto, User>();

            #endregion

            #region Vehicle Type Mappers

            //Map VehicleType => VehicleTypeSharedDto
            CreateMap<VehicleType, VehicleTypeSharedDto>();

            //Map VehicleTypeSharedDto =>  VehicleType 
            CreateMap<VehicleTypeSharedDto, VehicleType>();

            #endregion

        }
    }
}
