using AutoMapper;
using TektonLabs.Domain.DTOs.Request;
using TektonLabs.Domain.DTOs.Response;
using TektonLabs.Service.EventHandlers.Company.Commands;

namespace TektonLabs.Service.EventHandlers.Mappers
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<ProductCreateCommand, ProductCreateRequest>()
                .ForMember(dest => dest.Name,
                    option => option.MapFrom(
                        source => source.Name))
                .ForMember(dest => dest.Description,
                    option => option.MapFrom(
                        source => source.Description))
                .ForMember(dest => dest.Stock,
                    option => option.MapFrom(
                        source => source.Stock))
                .ForMember(dest => dest.Price,
                    option => option.MapFrom(
                        source => source.Price))
                .ForMember(dest => dest.Status,
                    option => option.MapFrom(
                        source => source.Status))
                .ForMember(dest => dest.CreationUser,
                    option => option.MapFrom(
                        source => source.CreationUser));

            CreateMap<ProductUpdateCommand, ProductUpdateRequest>()
                .ForMember(dest => dest.ProductId,
                    option => option.MapFrom(
                        source => source.ProductId))
                .ForMember(dest => dest.Name,
                    option => option.MapFrom(
                        source => source.Name))
                .ForMember(dest => dest.Description,
                    option => option.MapFrom(
                        source => source.Description))
                .ForMember(dest => dest.Stock,
                    option => option.MapFrom(
                        source => source.Stock))
                .ForMember(dest => dest.Price,
                    option => option.MapFrom(
                        source => source.Price))
                .ForMember(dest => dest.Status,
                    option => option.MapFrom(
                        source => source.Status))
                .ForMember(dest => dest.ModificationUser,
                    option => option.MapFrom(
                        source => source.ModificationUser));

            CreateMap<Domain.Entities.Product, ProductResponse>()
                .ForMember(dest => dest.ProductId,
                    option => option.MapFrom(
                        source => source.ProductId))
                .ForMember(dest => dest.Name,
                    option => option.MapFrom(
                        source => source.Name))
                .ForMember(dest => dest.Description,
                    option => option.MapFrom(
                        source => source.Description))
                .ForMember(dest => dest.Stock,
                    option => option.MapFrom(
                        source => source.Stock))
                .ForMember(dest => dest.Price,
                    option => option.MapFrom(
                        source => source.Price))
                //.ForMember(dest => dest.Status,
                //    option => option.MapFrom(
                //        source => source.Status))
                .ForMember(dest => dest.CreationUser,
                    option => option.MapFrom(
                        source => source.CreationUser));
        }
    }
}
