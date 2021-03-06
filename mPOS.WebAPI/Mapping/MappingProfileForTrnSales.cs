﻿using System.Web.ClientServices.Providers;

namespace mPOS.WebAPI.Mapping
{
    public class MappingProfileForTrnSales
    {
        public AutoMapper.MapperConfiguration config { get; set; }
        public AutoMapper.IMapper mapper { get; set; }

        public MappingProfileForTrnSales()
        {
            config = new AutoMapper.MapperConfiguration(cfg => cfg.AddProfile<TrnSalesProfile>());
            mapper = config.CreateMapper();
        }
    }

    public class TrnSalesProfile : AutoMapper. Profile
    {
        public TrnSalesProfile()
        {
            CreateMap<Data.TrnSale, POCO.TrnSales>()
                .ForMember(dest => dest.TrnSalesLines, conf => conf.MapFrom(value => value.TrnSalesLines));
            CreateMap<Data.TrnSale, POCO.TrnSales>()
                .ForMember(dest => dest.CustomerName, conf => conf.MapFrom(value => value.MstCustomer.Customer));
            CreateMap<Data.TrnSalesLine, POCO.TrnSalesLine>();
            CreateMap<Data.TrnSalesLine, POCO.TrnSalesLine>()
                .ForMember(dest => dest.MstItem, conf => conf.MapFrom(value => value.MstItem));
            CreateMap<Data.MstItem, POCO.MstItem>();                
        }
    }

    public class MappingProfileForTrnSalesReverse
    {
        public AutoMapper.MapperConfiguration config { get; set; }
        public AutoMapper.IMapper mapper { get; set; }

        public MappingProfileForTrnSalesReverse()
        {
            config = new AutoMapper.MapperConfiguration(cfg => cfg.AddProfile<TrnSalesProfileReverse>());
            mapper = config.CreateMapper();
        }
    }

    public class TrnSalesProfileReverse : AutoMapper.Profile
    {
        public TrnSalesProfileReverse()
        {
            CreateMap<POCO.TrnSales, Data.TrnSale>()
                .ForMember(dest => dest.TrnSalesLines, conf => conf.MapFrom(value => value.TrnSalesLines));
            CreateMap<POCO.TrnSalesLine, Data.TrnSalesLine>()
                .IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<POCO.TrnSalesLine, Data.TrnSalesLine>()
                .ForMember(dest => dest.MstItem, conf => conf.MapFrom(value => value.MstItem));
            CreateMap<POCO.MstItem, Data.MstItem>();
        }
    }
}