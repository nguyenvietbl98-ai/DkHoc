using System.Windows;

namespace WpfUI2
{
    public partial class MainWindow : Window
    {
        // Khởi tạo cửa sổ và nhận MainViewModel được bơm vào từ DI Container
        public MainWindow(MainViewModel viewModel)
        {
            InitializeComponent();

            // Thiết lập cầu nối dữ liệu giữa Giao diện (XAML) và Logic (ViewModel)
            this.DataContext = viewModel;
        }
    }
}