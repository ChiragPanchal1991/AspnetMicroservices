using AutoMapper;
using Discount.Grpc.Entities;
using Discount.Grpc.Protos;

namespace Discount.Grpc.Mapper
{
    public class CouponMapper :  Profile
    {
        public CouponMapper()
        {
            CreateMap<Coupon, CouponModel>().ReverseMap();
        }

    }
}
