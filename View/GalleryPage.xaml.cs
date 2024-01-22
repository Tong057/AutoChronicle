using AutoChronicle.Model.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace AutoChronicle.View
{
    /// <summary>
    /// Логика взаимодействия для GalleryPage.xaml
    /// </summary>
    public partial class GalleryPage : UserControl
    {
        private Random _rand = new Random();
        private int _previousIndex;
        private FileInfo[] _avaliableImages;
        public GalleryPage()
        {
            InitializeComponent();
            InitializeAvaliableImages();
            GenerateNextImage();
        }

        private void InitializeAvaliableImages()
        {
            string workingDirectory = Environment.CurrentDirectory;
            string projectDirectory = Directory.GetParent(workingDirectory).Parent.Parent.FullName;
            string imagesDirectory = projectDirectory + "/Resources/Images";
            DirectoryInfo dirInfo = new DirectoryInfo(imagesDirectory);
            _avaliableImages = dirInfo.GetFiles();
        }

        private void NextImageButton_Click(object sender, RoutedEventArgs e)
        {
            GenerateNextImage();
        }

        private void GenerateNextImage()
        {
            int index;
            while ((index = _rand.Next(0, _avaliableImages.Length)) == _previousIndex);
            CarImage.Source = new BitmapImage(new Uri(_avaliableImages[index].FullName));
            CarNameTextBlock.Text = Path.GetFileNameWithoutExtension(_avaliableImages[index].FullName);
        }
    }
}
