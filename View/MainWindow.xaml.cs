using AutoChronicle.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AutoChronicle
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            AppTheme.ChangeTheme(Theme.Dark);
            MainWindowViewModel mainVM = new MainWindowViewModel();
            mainVM.OnVerticalOffsetChanged += ViewModel_OnVerticalOffsetChanged;
            DataContext = mainVM;
        }
        private void HeaderGrid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }
        private void TurnButton_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ViewModel_OnVerticalOffsetChanged(object sender, EventArgs e)
        {
            if (sender is MainWindowViewModel mainVW)
            {
                MainScrollViewer.ScrollToVerticalOffset(mainVW.VerticalOffset);
            }
        }

    }
}
