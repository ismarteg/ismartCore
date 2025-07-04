using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesCore.Mapper
{
    public static class MappingExtensions
    {
        public static TDestination MapItem<TDestination>(this object source)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            return AutoMapperService.Mapper.Map<TDestination>(source);
        }

        public static List<TDestination> MapList<TDestination>(this IEnumerable<object> source)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            return AutoMapperService.Mapper.Map<List<TDestination>>(source);
        }
    }
}
