using System;
using System.Windows;
using System.Windows.Controls.Primitives;

namespace AutoChronicle
{
    internal class AppTheme
    {
        public static void ChangeTheme(string theme)
        {
            Uri themeuri = new Uri($"Resources/Themes/{theme}.xaml", UriKind.Relative);
            ResourceDictionary Theme = new ResourceDictionary() { Source = themeuri };

            App.Current.Resources.Clear();
            App.Current.Resources.MergedDictionaries.Add(Theme);
        }

    }
}
