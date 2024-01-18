using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AutoChronicle.Model.Utils
{
    public class DataReader
    {
        private static string _relativeDataDirectory = @"\Model\Data";

        public static string[] ReadCarBrands()
        {
            string dataDirectory = GetDataDirectory();
            string[] absoluteDirectories = Directory.GetDirectories(dataDirectory);

            return Array.ConvertAll(absoluteDirectories,
                dir => Path.GetRelativePath(dataDirectory, dir));
        }

        public static string ReadCarHistory(string carBrand, string language)
        {
            string dataDirectory = GetDataDirectory();
            string filePath = @$"{dataDirectory}\{carBrand}\{carBrand}_{language}.txt";

            using (FileStream fs = new FileStream(filePath, FileMode.Open))
            {
                using (StreamReader sr = new StreamReader(fs))
                {
                    return sr.ReadToEnd();
                }
            }
        }

        private static string GetDataDirectory()
        {
            string workingDirectory = Environment.CurrentDirectory;
            string projectDirectory = Directory.GetParent(workingDirectory).Parent.Parent.FullName;
            return projectDirectory + _relativeDataDirectory;
        }

        public static string GetImagePath(string carBrand)
        {
            return $@"{GetDataDirectory()}\{carBrand}\{carBrand}.png";
        }
    }
}
