using AutoChronicle.Model.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AutoChronicle.View
{
    /// <summary>
    /// Логика взаимодействия для BrandPage.xaml
    /// </summary>
    public partial class CarBrandPage : UserControl
    {
        public CarBrandPage(string? carBrand, string language)
        {
            InitializeComponent();
            if (carBrand != null)
            {
                BrandNameTextBlock.Text = carBrand.ToUpper();
                HistoryTextBlock.Text = DataReader.ReadCarHistory(carBrand, language);
                try
                {
                    LogoImage.Source = new BitmapImage(new Uri(DataReader.GetImagePath(carBrand)));
                }
                catch
                {
                    LogoImage.Source = null;
                }
                
            }

        }
    }
}
