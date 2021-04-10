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

namespace wine_lite_view.ViewModels {
    public class MainViewModel : INotifyPropertyChanged {
        #region Constants
        private const string DEFAULT_DB_EXTENSION = ".wldb";
        private const string DEFAULT_DB_NAME = "wines";
        private readonly static string DEFAULT_DB_PATH = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
        #endregion

        #region Private Fields
        #region Commands
        private ICommand _clickExit;
        private ICommand _clickInfo;
        private ICommand _clickNew;
        private ICommand _clickOpen;
        #endregion

        private WineLiteContext _db;
        private string _currentDbPath;
        #endregion

        #region Properties
        public string Title => $"Wine Lite (v{Assembly.GetEntryAssembly().GetName().Version})";
        public ImageSource Icon => BitmapFrame.Create(new Uri("pack://application:,,,/Resources/icon.ico", UriKind.RelativeOrAbsolute));

        public string CurrentDbPath {
            get { return _currentDbPath; }
            private set { _currentDbPath = value; OnPropertyChanged(); }
        }

        public bool CanExecuteAlways => true;

        #region Commands
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
        #endregion
        #endregion

        #region Constructors
        public MainViewModel() {
            if (string.IsNullOrEmpty(Properties.Settings.Default.LastDbPath))
                CurrentDbPath = $"{DEFAULT_DB_PATH}\\{DEFAULT_DB_NAME}{DEFAULT_DB_EXTENSION}";
            else
                CurrentDbPath = Properties.Settings.Default.LastDbPath;

            _db = new WineLiteContext(CurrentDbPath);
        }
        #endregion

        #region Command Methods
        private void ExitApp() => System.Windows.Application.Current.Shutdown();

        private void ShowInfo() {
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

            if (dialog.ShowDialog() ?? false) {
                ChangeDb(dialog.FileName);
            }
        }

        private void OpenDb() {
            var dialog = new VistaOpenFileDialog() {
                Title = "Open DB",
                DefaultExt = DEFAULT_DB_EXTENSION,
                InitialDirectory = DEFAULT_DB_PATH,
            };

            if (dialog.ShowDialog() ?? false) {
                ChangeDb(dialog.FileName);
            }
        }
        #endregion

        #region Private Methods
        private void ChangeDb(string path) {
            if (Path.GetExtension(path) != DEFAULT_DB_EXTENSION || !Directory.Exists(Path.GetDirectoryName(path))) {
                MessageBox.Show("Invalid path.");
                return;
            }

            _db.Dispose();
            CurrentDbPath = path;
            _db = new WineLiteContext(CurrentDbPath);
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
