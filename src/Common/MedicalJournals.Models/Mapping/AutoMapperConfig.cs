using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using AutoMapper;
using MedicalJournals.Helpers;
using MedicalJournals.Models.Interfaces;

namespace MedicalJournals.Models.Mapping
{
    public class AutoMapperConfig 
    {
        public void Execute(IMapperConfigurationExpression config)
        {
            var assemblies = GetType().GetAssembly().GetAppReferencedAssemblies();
            var types = new List<TypeInfo>();

            var assembliestList = assemblies.Where(x => x.FullName.Contains("MedicalJournals"));
            foreach (var assembly in assembliestList)
            {
                types.AddRange(assembly.DefinedTypes);
            }

            LoadStandardMappings(config, types);

            LoadCustomMappings(config, types);
        }

        private static void LoadCustomMappings(IMapperConfigurationExpression config, IEnumerable<TypeInfo> types)
        {
            var maps = (from t in types
                        from i in t.GetInterfaces()
                        where typeof(IHaveCustomMappings).IsAssignableFrom(t.AsType()) &&
                              !t.IsAbstract &&
                              !t.IsInterface
                        select (IHaveCustomMappings)Activator.CreateInstance(t.AsType())).ToArray();

            foreach (var map in maps)
            {
                map.CreateMappings(config);
            }
        }

        private static void LoadStandardMappings(IMapperConfigurationExpression config, IEnumerable<TypeInfo> types)
        {
            var maps = (from t in types
                        from i in t.GetInterfaces()
                        where i.GetTypeInfo().IsGenericType && i.GetGenericTypeDefinition() == typeof(IMapFrom<>) &&
                              !t.IsAbstract &&
                              !t.IsInterface
                        select new
                        {
                            Source = i.GetGenericArguments()[0],
                            Destination = t
                        }).ToArray();

            foreach (var map in maps)
            {
                config.CreateMap(map.Source, map.Destination.AsType());
            }
        }
    }
}
