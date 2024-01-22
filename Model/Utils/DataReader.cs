using System;
using System.IO;
using System.Reflection;

namespace AutoChronicle.Model.Utils
{
    public class DataReader
    {
        public static string ExecutablePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

        public static string[] ReadCarBrands()
        {
            string folderPath = Path.Combine(ExecutablePath, "Model", "Data");
            string[] brandsDirectories = Directory.GetDirectories(folderPath);

            return Array.ConvertAll(brandsDirectories,
                dir => Path.GetRelativePath(folderPath, dir));
        }

        public static string ReadCarHistory(string carBrand, string language)
        {
            string filePath = Path.Combine(ExecutablePath, "Model", "Data", carBrand, $"{carBrand}_{language}.txt");

            using (FileStream fs = new FileStream(filePath, FileMode.Open))
            {
                using (StreamReader sr = new StreamReader(fs))
                {
                    return sr.ReadToEnd();
                }
            }
        }

        public static string GetImagePath(string carBrand)
        {
            return Path.Combine(ExecutablePath, "Model", "Data", carBrand, $"{carBrand}.png");
        }
    }
}
