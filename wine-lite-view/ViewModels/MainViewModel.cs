using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using wine_lite_view.Models;

namespace wine_lite_view.ViewModels {
    public class MainViewModel : INotifyPropertyChanged {
        #region Constants
        private const string DEFAULT_DB_NAME = "wines.wldb";
        private readonly static string DEFAULT_DB_PATH = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
        #endregion

        #region Private Fields
        private ICommand _clickCmd1;
        private ICommand _clickCmd2;

        private readonly WineLiteContext _db;
        private string _currentDbPath;
        #endregion

        #region Properties
        public string Title => $"Wine Lite (v{Assembly.GetEntryAssembly().GetName().Version})";
        public ImageSource Icon => BitmapFrame.Create(new Uri("pack://application:,,,/Resources/icon.ico", UriKind.RelativeOrAbsolute));

        public string CurrentDbPath {
            get { return _currentDbPath; }
            private set { _currentDbPath = value; OnPropertyChanged(); }
        }

        public bool CanExecute => true;

        public ICommand ClickCmd1 {
            get => _clickCmd1 ??= new CommandHandler(() => AddDummyData(), () => CanExecute);
        }
        public ICommand ClickCmd2 {
            get => _clickCmd2 ??= new CommandHandler(() => ReadData(), () => CanExecute);
        }
        #endregion

        #region Constructors
        public MainViewModel() {
            CurrentDbPath = $"{DEFAULT_DB_PATH}\\{DEFAULT_DB_NAME}";
            _db = new WineLiteContext(CurrentDbPath);
        }
        #endregion

        #region Command Methods
        public void AddDummyData() {
            _db.Wines.AddRange(new List<WineModel>(){
                new WineModel() { Name = "Test1" },
                new WineModel() { Name = "Test2" }
            });

            _db.SaveChanges();
        }

        public void ReadData() {
            var test = _db.Wines.ToList();
        }
        #endregion

        #region Eventhandling
        public void MainView_Closing(object sender, CancelEventArgs e) {
            _db.Dispose(); ;
        }
        #endregion

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "") {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
