using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace MedicalJournals.Helpers
{
    public static class AssemblyExtensions
    {
        public static Assembly GetAssembly(this Type type)
        {
            return type.GetTypeInfo().Assembly;
        }

        public static Assembly[] GetAppReferencedAssemblies(this Assembly assembly)
        {
            var assemblyNames = assembly.GetReferencedAssemblies();

            var assemblies = new List<Assembly> {assembly};
            assemblies.AddRange(assemblyNames
                .Where(p => p.Name.StartsWith("MedicalJournals"))
                .Select(item => Assembly.Load(item)));

            return assemblies.ToArray();
        }

        public static string GetVersionDescription(this Assembly assembly)
        {
            if (assembly != null)
            {
                var lastWriteTime = new System.IO.FileInfo(assembly.Location).LastWriteTime;
                return string.Format("{0} [built at {1}]", assembly.GetName().Version, lastWriteTime.ToString("yyyyMMdd HHmm"));
            }
            return string.Empty;
        }
    }
}