using AutoMapper;
using FaultTracker.Business.DataTransfer.Request;
using FaultTracker.Business.DataTransfer.Shared;
using FaultTracker.Data.Entities;
using System.Data.Common;
using System.Linq;
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

            #region Status Mappers

            //Map Status => StatusSharedDto
            CreateMap<Status, StatusSharedDto>();

            //Map StatusSharedDto =>  Status 
            CreateMap<StatusSharedDto, Status>();

            //Map StatusRequestDto =>  Status 
            CreateMap<StatusRequestDto, Status>();
            #endregion

            #region Picture Group Mappers

            //Map PictureGroup => PictureGroupSharedDto
            CreateMap<PictureGroup, PictureGroupSharedDto>();

            //Map PictureGroupSharedDto =>  PictureGroup 
            CreateMap<PictureGroupSharedDto, PictureGroup>();

            //Map PictureGroupRequestDto =>  PictureGroup 
            CreateMap<PictureGroupRequestDto, PictureGroup>();
            #endregion

            #region Maintenance Mappers

            //Map Maintenance => MaintenanceSharedDto
            CreateMap<Maintenance, MaintenanceSharedDto>()
                .ForMember(dest => dest.VehiclePlateNo, opt => opt.MapFrom(src => src.Vehicle.PlateNo))
                .ForMember(dest => dest.VehicleTypeName, opt => opt.MapFrom(src => src.Vehicle.VehicleType.Name))
                .ForMember(dest => dest.DriverFullName, opt => opt.MapFrom(src => $"{src.User.FirstName} {src.User.LastName}"))
                .ForMember(dest => dest.DriverPhoneNumber, opt => opt.MapFrom(src => src.User.PhoneNumber))
                .ForMember(dest => dest.PictureImage, opt => opt.MapFrom(src => src.PictureGroup.PictureImage))
                .ForMember(dest => dest.StatusName, opt => opt.MapFrom(src => src.Status.Name))
                .ForMember(dest => dest.MaintenanceHistories, opt => opt.MapFrom(src => src.MaintenanceHistories.Select(mh => new MaintenanceHistorySharedDto { Text = mh.text, ID = mh.ID, ActionTypeName = mh.ActionType.Name })));

            //Map MaintenanceRequestDto =>  Maintenance 
            CreateMap<MaintenanceRequestDto, Maintenance>();

            #endregion

            #region Maintenance Histories Mappers

            //Map MaintenanceHistory => MaintenanceHistorySharedDto
            CreateMap<MaintenanceHistory, MaintenanceHistorySharedDto>()
                .ForMember(dest => dest.ActionTypeName, opt => opt.MapFrom(src => src.ActionType.Name));

            //Map MaintenanceHistorySharedDto =>  MaintenanceHistory 
            CreateMap<MaintenanceHistorySharedDto, MaintenanceHistory>();

            //Map MaintenanceHistoryRequestDto =>  MaintenanceHistory 
            CreateMap<MaintenanceHistoryRequestDto, MaintenanceHistory>();
            #endregion

            #region Action Type Mappers

            //Map ActionType => ActionTypeSharedDto
            CreateMap<ActionType, ActionTypeSharedDto>();

            //Map ActionTypeSharedDto =>  ActionType 
            CreateMap<ActionTypeSharedDto, ActionType>();

            //Map ActionTypeRequestDto =>  MaintenanceHistory 
            CreateMap<ActionTypeRequestDto, ActionType>();
            #endregion

        }
    }
}
