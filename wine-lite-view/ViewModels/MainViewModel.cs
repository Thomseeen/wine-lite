using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Windows.Input;

using wine_lite_view.Models;

namespace wine_lite_view.ViewModels {
    public class MainViewModel : INotifyPropertyChanged {
        #region Private Fields
        private ICommand _clickCmd1;
        private ICommand _clickCmd2;

        private readonly WineLiteContext db;
        #endregion

        #region Properties
        public string Title => $"Wine Lite (v{Assembly.GetEntryAssembly().GetName().Version})";

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
            db = new WineLiteContext();
        }
        #endregion

        #region Command Methods
        public void AddDummyData() {
            db.Wines.AddRange(new List<WineModel>(){
                new WineModel() { Name = "Test1" },
                new WineModel() { Name = "Test2" }
            });

            db.SaveChanges();
        }

        public void ReadData() {
            var test = db.Wines.ToList();
        }
        #endregion

        #region Eventhandling
        public void MainView_Closing(object sender, System.ComponentModel.CancelEventArgs e) {
            db.Dispose(); ;
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
