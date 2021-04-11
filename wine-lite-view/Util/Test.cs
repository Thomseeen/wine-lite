using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wine_lite_view.Models;

namespace wine_lite_view.Util {
    public static class Test {
        public static void AddDummyData(WineLiteContext db) {
            #region Vendors
            db.Vendors.Add(new Producer {
                VendorId = 1,
                Name = "Tommasi",
                Street = "via Ronchetto, 4",
                ZipCode = 37029,
                City = "Verona",
                Comment = "Sounds almost like Thomas",
                Country = "Italy",
                PhoneNumber = "+39 045 7701266",
                EMail = "wine@tommasi.com",
                Website = "https://www.tommasi.com"
            });
            db.Vendors.Add(new Producer {
                VendorId = 2,
                Name = "Weingut Merkle",
                Street = "Blankenhornstraße 12",
                ZipCode = 74343,
                City = "Sachsenheim",
                Comment = "Good local stuff",
                Country = "Germany",
                PhoneNumber = "+49 7046 7677",
                EMail = "info@weingut-merkle.de",
                Website = "https://www.weingut-merkle.de"
            });
            db.Vendors.Add(new Vendor {
                VendorId = 3,
                Name = "Rewe Maulbronn",
                Street = "August-Kienzle-Straße 9",
                ZipCode = 75433,
                City = "Maulbronn",
                Comment = "Well, it's a Rewe",
                Country = "Germany",
                PhoneNumber = "+49 7043 953972",
                Website = "https://www.rewe.de/marktseite/maulbronn/840189/rewe-markt-august-kienzle-strasse-9/"
            });
            #endregion

            db.SaveChanges();

            #region Wines
            db.Wines.Add(new Wine {
                WineId = 1,
                Name = "Valpolicella Classico Riserva",
                Country = "Italy",
                Year = 2010,
                Taste = "A deep ruby red with garnet highlights",
                Location = "Vigneto Ca' Florian",
                Region = "Valpolicella",
                Vine = "Corvina 75%, Corvinone 20%, Rondinella 5%",
                WineType = WineType.Red,
                Producer = (Producer)db.Vendors.Find(1)
            });
            db.Wines.Add(new Wine() {
                WineId = 2,
                Name = "﻿Mueller Thurgau",
                Country = "Germany",
                Year = 2020,
                Taste = "Salbeiblätter Tonkabohne Waldmeister",
                Location = "Hochebene",
                Region = "BaWü",
                Vine = "Mueller Thurgau",
                WineType = WineType.White,
                Producer = (Producer)db.Vendors.Find(2),
                Vendors = new ObservableCollection<Vendor>() {
                    db.Vendors.Find(3)
                }
            });
            #endregion

            db.SaveChanges();

            #region Tastings
            db.Tastings.Add(new Tasting() {
                Name = "Weinkellerparty",
                Taster = "Thomas",
                Date = DateTime.Today,
                Wine = db.Wines.Find(2),
                Ratings = new ObservableCollection<Rating>() {
                    new Rating() {
                        Name = "Schwere",
                        Rate = 5,
                        Comment = ""
                    },
                    new Rating() {
                        Name = "Süße",
                        Rate = 2,
                        Comment = ""
                    }
                }
            });
            #endregion

            db.SaveChanges();

            #region Bookings
            db.Bookings.Add(new Booking {
                Quantity = 25,
                Price = 10.5f,
                Date = DateTime.Today.AddDays(-52),
                Vendor = db.Vendors.Find(3),
                Wine = db.Wines.Find(2)
            });
            db.Bookings.Add(new Booking {
                Quantity = 5,
                Price = 25.3f,
                Date = DateTime.Today.AddDays(-105),
                Vendor = db.Vendors.Find(1),
                Wine = db.Wines.Find(1)
            });
            db.Bookings.Add(new Booking {
                Quantity = -1,
                Date = DateTime.Today.AddDays(-1),
                Wine = db.Wines.Find(1)
            });
            db.Bookings.Add(new Booking {
                Quantity = -1,
                Date = DateTime.Today.AddDays(-2),
                Wine = db.Wines.Find(1)
            });
            db.Bookings.Add(new Booking {
                Quantity = -2,
                Date = DateTime.Today.AddDays(-20),
                Wine = db.Wines.Find(2)
            });
            #endregion

            db.SaveChanges();
        }
    }
}
