using AutoMapper;
using Discount.grpc.Models;
using Discount.grpc.Protos;

namespace Discount.grpc.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Coupon, CouponRequest>().ReverseMap();
        }
    }
}
