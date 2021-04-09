using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

using wine_lite_view.Models;

namespace wine_lite_view.ViewModels {
    public class MainViewModels {
        private ICommand _clickCmd1;
        private ICommand _clickCmd2;

        private readonly WineLiteContext db;

        public bool CanExecute => true;

        public ICommand ClickCmd1 {
            get => _clickCmd1 ??= new CommandHandler(() => AddDummyData(), () => CanExecute);
        }
        public ICommand ClickCmd2 {
            get => _clickCmd2 ??= new CommandHandler(() => ReadData(), () => CanExecute);
        }

        public MainViewModels() {
            db = new WineLiteContext();
        }

        ~MainViewModels() {
            db.Dispose();
        }

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
    }
}
