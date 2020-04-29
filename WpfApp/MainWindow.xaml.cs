using System.Windows;
using DAL.EntityFramework;
using ViewModel.WpfViewModel;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            DataContext = new MainViewModel(new EfRepository());
            InitializeComponent();
        }
    }
}