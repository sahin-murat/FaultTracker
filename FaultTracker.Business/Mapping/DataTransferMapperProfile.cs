using AutoMapper;
using FaultTracker.Business.DataTransfer.Request;
using FaultTracker.Business.DataTransfer.Shared;
using FaultTracker.Data.Entities;
using System.Data.Common;
using System.Security.Cryptography;

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

            //Map VehicleTypeRequestDto =>  VehicleType 
            CreateMap<VehicleTypeRequestDto, VehicleType>();

            #endregion

            #region Vehicle Mappers

            //Map Custom VehicleType => VehicleTypeSharedDto
            CreateMap<Vehicle, VehicleSharedDto>()
                .ForMember(destinationMember: dest => dest.VehicleTypeName, memberOptions: opt =>opt.MapFrom(mapExpression:src => src.VehicleType.Name))
                .ForMember(destinationMember: dest => dest.UserFullName, memberOptions: opt => opt.MapFrom(mapExpression: src => $"{src.User.FirstName} {src.User.LastName}"));

            CreateMap<VehicleSharedDto, Vehicle>();

            //Map VehicleRequestDto =>  Vehicle 
            CreateMap<VehicleRequestDto, Vehicle>();

            #endregion

        }
    }
}
