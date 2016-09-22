using System;
using System.IO;
using System.Reflection;

namespace MedicalJournals.Helpers
{
    public static class FileHelper
    {

// ReSharper disable once InconsistentNaming
        public static FileInfo GetLoggingConfigFile(Assembly assembly)
        {
            // try and get the config file
            // if this doesn't work - logging will not be activated - but
            // application should continue
            var codeBase = assembly.CodeBase.Replace("file:///", String.Empty);
            var path = Path.GetDirectoryName(codeBase);

            var file = new FileInfo(path + @"\log4net.ui.config");

            if (!file.Exists)
            {
                file = new FileInfo(path + @"\log4net.qp.config");
            }

            return file;
        }    
    }
}