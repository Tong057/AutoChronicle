using AutoChronicle.Model.Utils;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;

namespace AutoChronicle.View
{
    /// <summary>
    /// Логика взаимодействия для GalleryPage.xaml
    /// </summary>
    public partial class GalleryPage : UserControl
    {
        private const string UnsplashClientId = "lZw9iw0q6sfPSO0TjLg0kfHLik2IoN7uOzidngiAKX8";
        private const string UnsplashApiUrl = "https://api.unsplash.com/photos/random?query=cars&client_id=";

        public GalleryPage()
        {
            InitializeComponent();
            GenerateNextImageAsync();
        }

        private async void NextImageButton_Click(object sender, RoutedEventArgs e)
        {  
            await GenerateNextImageAsync();
        }

        private async Task GenerateNextImageAsync()
        {
            WaitingTextBox.Visibility = Visibility.Visible;
            Storyboard storyboard = CreateStoryBoard(); // StoryBoard for animation

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    string endpoint = UnsplashApiUrl + UnsplashClientId;
                    HttpResponseMessage response = await client.GetAsync(endpoint);

                    if (response.IsSuccessStatusCode)
                    {
                        string jsonContent = await response.Content.ReadAsStringAsync();
                        dynamic jsonData = Newtonsoft.Json.JsonConvert.DeserializeObject(jsonContent);
                        string imageUrl = jsonData.urls.regular;

                        storyboard.Begin(); // Show animation
                        CarImage.Source = new BitmapImage(new Uri(imageUrl));
                    }
                    else
                    {
                        MessageBox.Show("Failed to fetch image. Status code: " + response.StatusCode);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
                finally
                {
                    WaitingTextBox.Visibility = Visibility.Collapsed;
                }
            }
        }

        private Storyboard CreateStoryBoard()
        {
            DoubleAnimation fadeInAnimation = CreateFadeInAnimation();
            Storyboard storyboard = new Storyboard();
            Storyboard.SetTarget(fadeInAnimation, CarImage);
            Storyboard.SetTargetProperty(fadeInAnimation, new PropertyPath(Image.OpacityProperty));
            storyboard.Children.Add(fadeInAnimation);

            return storyboard;
        }

        private DoubleAnimation CreateFadeInAnimation()
        {
            return new DoubleAnimation
            {
                From = 0.0,
                To = 1.0,
                Duration = TimeSpan.FromSeconds(2),
                EasingFunction = new CubicEase { EasingMode = EasingMode.EaseInOut }
            };
        }

    }

}
