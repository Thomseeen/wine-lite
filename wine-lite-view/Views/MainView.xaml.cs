using MahApps.Metro.Controls;
using System.Windows;
using wine_lite_view.ViewModels;

namespace wine_lite_view {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainView {
        public MainView() {
            InitializeComponent();

            var vm = new MainViewModel();
            DataContext = vm;

            Closing += vm.MainView_Closing;
        }
    }
}
