using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace mPOS.WebAPI.Mapping
{
    public class MappingProfileForTrnCollectionReverse
    {
        public AutoMapper.MapperConfiguration config { get; set; }
        public AutoMapper.IMapper mapper { get; set; }

        public MappingProfileForTrnCollectionReverse()
        {
            config = new AutoMapper.MapperConfiguration(cfg => cfg.AddProfile<TrnCollectionProfileReverse>());
            mapper = config.CreateMapper();
        }
    }

    public class TrnCollectionProfileReverse : AutoMapper.Profile
    {
        public TrnCollectionProfileReverse()
        {
            CreateMap<POCO.TrnCollection, Data.TrnCollection>()
                .ForMember(dest => dest.TrnCollectionLines, conf => conf.MapFrom(value => value.TrnCollectionLines));
            CreateMap<POCO.TrnCollectionLine, Data.TrnCollectionLine>()
                .IgnoreAllPropertiesWithAnInaccessibleSetter();
        }
    }
}