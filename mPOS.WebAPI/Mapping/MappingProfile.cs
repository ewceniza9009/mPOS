using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace mPOS.WebAPI.Mapping
{
    public class MappingProfile<TSource, TDestination>
    {
        public AutoMapper.MapperConfiguration config { get; set; }
        public AutoMapper.Mapper mapper { get; set; }

        public MappingProfile()
        {
            config = new AutoMapper.MapperConfiguration(x => x.CreateMap<TSource, TDestination>().IgnoreAllPropertiesWithAnInaccessibleSetter());
            mapper = new AutoMapper.Mapper(config);
        }
    }
}