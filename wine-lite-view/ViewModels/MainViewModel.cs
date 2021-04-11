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
using static System.Net.Mime.MediaTypeNames;

using Ookii.Dialogs.Wpf;
using System.Windows;
using System.Collections.ObjectModel;
using System.Windows.Data;
using Microsoft.EntityFrameworkCore;

namespace wine_lite_view.ViewModels {
    public class MainViewModel : INotifyPropertyChanged {
        #region Constants
        private const string DEFAULT_DB_EXTENSION = ".wldb";
        private const string DEFAULT_DB_NAME = "wines";
        private readonly static string DEFAULT_DB_PATH = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
        #endregion

        #region Private Fields
        #region Binding Data
        private ICollectionView _wineCollectionView;
        private ICollectionView _vendorCollectionView;
        private ICollectionView _tastingCollectionView;
        private ICollectionView _bookingCollectionView;
        #endregion

        #region Commands
        private ICommand _clickExit;
        private ICommand _clickInfo;
        private ICommand _clickNew;
        private ICommand _clickOpen;
        private ICommand _clickSave;
        #endregion

        private WineLiteContext _db;
        private string _currentDbPath;
        #endregion

        #region Properties
        public static string Title => $"Wine Lite (v{Assembly.GetEntryAssembly().GetName().Version})";
        public static ImageSource Icon => BitmapFrame.Create(new Uri("pack://application:,,,/Resources/icon.ico", UriKind.RelativeOrAbsolute));

        public string CurrentDbPath {
            get { return _currentDbPath; }
            private set { _currentDbPath = value; OnPropertyChanged(); }
        }

        #region Binding Data
        public ICollectionView WineCollectionView {
            get => _wineCollectionView;
            private set { _wineCollectionView = value; OnPropertyChanged(); }
        }
        public ICollectionView VendorCollectionView {
            get => _vendorCollectionView;
            private set { _vendorCollectionView = value; OnPropertyChanged(); }
        }
        public ICollectionView TastingCollectionView {
            get => _tastingCollectionView;
            private set { _tastingCollectionView = value; OnPropertyChanged(); }
        }
        public ICollectionView BookingCollectionView {
            get => _bookingCollectionView;
            private set { _bookingCollectionView = value; OnPropertyChanged(); }
        }
        #endregion

        #region Commands
        public static bool CanExecuteAlways => true;

        public ICommand ClickExit {
            get => _clickExit ??= new CommandHandler(() => ExitApp(), () => CanExecuteAlways);
        }
        public ICommand ClickInfo {
            get => _clickInfo ??= new CommandHandler(() => ShowInfo(), () => CanExecuteAlways);
        }
        public ICommand ClickNew {
            get => _clickNew ??= new CommandHandler(() => NewDb(), () => CanExecuteAlways);
        }
        public ICommand ClickOpen {
            get => _clickOpen ??= new CommandHandler(() => OpenDb(), () => CanExecuteAlways);
        }
        public ICommand ClickSave {
            get => _clickSave ??= new CommandHandler(() => SaveDb(), () => CanExecuteAlways);
        }
        #endregion
        #endregion

        #region Constructors
        public MainViewModel() {
            if (string.IsNullOrEmpty(Properties.Settings.Default.LastDbPath))
                ChangeDb($"{DEFAULT_DB_PATH}\\{DEFAULT_DB_NAME}{DEFAULT_DB_EXTENSION}");
            else
                ChangeDb(Properties.Settings.Default.LastDbPath);
        }
        #endregion

        #region Command Methods
        private static void ExitApp() => System.Windows.Application.Current.Shutdown();

        private static void ShowInfo() {
            MessageBox.Show(
                $"{Title}\n" +
                $"Copyright (c) 2021 Thomas Wagner (thomas@the-wagner.de)\n\n" +
                $"Track you're ever growing collection of beverages.\n",
                $"Information"
                );
        }

        private void NewDb() {
            var dialog = new VistaSaveFileDialog() {
                Title = "New DB",
                DefaultExt = DEFAULT_DB_EXTENSION,
                InitialDirectory = DEFAULT_DB_PATH,
            };

            if (dialog.ShowDialog() ?? false)
                ChangeDb(dialog.FileName);
        }

        private void OpenDb() {
            var dialog = new VistaOpenFileDialog() {
                Title = "Open DB",
                DefaultExt = DEFAULT_DB_EXTENSION,
                InitialDirectory = DEFAULT_DB_PATH,
            };

            if (dialog.ShowDialog() ?? false)
                ChangeDb(dialog.FileName);
        }

        private void SaveDb() {
            _db.SaveChanges();

            OnPropertyChanged(nameof(WineCollectionView));
            OnPropertyChanged(nameof(VendorCollectionView));
            OnPropertyChanged(nameof(TastingCollectionView));
            OnPropertyChanged(nameof(BookingCollectionView));
        }
        #endregion

        #region Private Methods
        private void ChangeDb(string path) {
            if (Path.GetExtension(path) != DEFAULT_DB_EXTENSION || !Directory.Exists(Path.GetDirectoryName(path))) {
                MessageBox.Show("Invalid path.");
                return;
            }

            if (_db != null)
                _db.Dispose();

            CurrentDbPath = path;
            _db = new WineLiteContext(CurrentDbPath);

            _db.Wines.Load();
            _db.Vendors.Load();
            _db.Tastings.Load();
            _db.Bookings.Load();

            WineCollectionView = CollectionViewSource.GetDefaultView(_db.Wines.Local.ToObservableCollection());
            VendorCollectionView = CollectionViewSource.GetDefaultView(_db.Vendors.Local.ToObservableCollection());
            TastingCollectionView = CollectionViewSource.GetDefaultView(_db.Tastings.Local.ToObservableCollection());
            BookingCollectionView = CollectionViewSource.GetDefaultView(_db.Bookings.Local.ToObservableCollection());
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
