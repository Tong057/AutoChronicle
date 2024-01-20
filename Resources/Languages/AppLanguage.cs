using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AutoChronicle.Resources.Languages
{
    class AppLanguage
    {
        public static void ChangeLanguage(Language language)
        {
            Uri languageuri = new Uri($"Resources/Languages/{language}.xaml", UriKind.Relative);
            ResourceDictionary Language = new ResourceDictionary() { Source = languageuri };

            App.Current.Resources.Clear();
            App.Current.Resources.MergedDictionaries.Add(Language);
        }
    }

    internal enum Language
    {
        ENG,
        RUS,
        POL
    }
}
