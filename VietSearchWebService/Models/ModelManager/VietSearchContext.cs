using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using VietSearchWebService.Models.ModelObject;

namespace VietSearchWebService.Models.ModelManager
{
    public class VietSearchContext : DbContext
    {
        public VietSearchContext()
            : base("name=VietSearchConnectionString")
        {
            
        }

        DbSet<City> _cities;

        public DbSet<City> cities
        {
            get { return _cities; }
            set { _cities = value; }
        }

        DbSet<District> _districts;

        public DbSet<District> districts
        {
            get { return _districts; }
            set { _districts = value; }
        }
        
        DbSet<Street> _streets;

        public DbSet<Street> streets
        {
            get { return _streets; }
            set { _streets = value; }
        }

        DbSet<PlaceType> _placeTypes;

        public DbSet<PlaceType> placeTypes
        {
            get { return _placeTypes; }
            set { _placeTypes = value; }
        }

        DbSet<Place> _places;

        public DbSet<Place> places
        {
            get { return _places; }
            set { _places = value; }
        }

        DbSet<PlacePicture> _placePictures;

        public DbSet<PlacePicture> placePictures
        {
            get { return _placePictures; }
            set { _placePictures = value; }
        }

        DbSet<Account> _accounts;

        public DbSet<Account> accounts
        {
            get { return _accounts; }
            set { _accounts = value; }
        }

        DbSet<Appoint> _appoints;

        public DbSet<Appoint> appoints
        {
            get { return _appoints; }
            set { _appoints = value; }
        }

        DbSet<Comment> _comments;

        public DbSet<Comment> comments
        {
            get { return _comments; }
            set { _comments = value; }
        }

        DbSet<Rate> _rates;

        public DbSet<Rate> rates
        {
            get { return _rates; }
            set { _rates = value; }
        }

        DbSet<MenuItemType> _menuItemTypes;

        public DbSet<MenuItemType> menuItemTypes
        {
            get { return _menuItemTypes; }
            set { _menuItemTypes = value; }
        }

        DbSet<MenuItem> _menuItems;

        public DbSet<MenuItem> menuItems
        {
            get { return _menuItems; }
            set { _menuItems = value; }
        }

        DbSet<Menu> _menus;

        public DbSet<Menu> menus
        {
            get { return menus; }
            set { menus = value; }
        }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new CityMap());
            modelBuilder.Configurations.Add(new DistrictMap());
            modelBuilder.Configurations.Add(new StreetMap());
            modelBuilder.Configurations.Add(new PlaceTypeMap());
            modelBuilder.Configurations.Add(new PlaceMap());
            modelBuilder.Configurations.Add(new PlacePictureMap());
            modelBuilder.Configurations.Add(new AccountMap());
            modelBuilder.Configurations.Add(new AppointMap());
            modelBuilder.Configurations.Add(new CommentMap());
            modelBuilder.Configurations.Add(new RateMap());
            modelBuilder.Configurations.Add(new MenuItemMap());
            modelBuilder.Configurations.Add(new MenuItemTypeMap());
            modelBuilder.Configurations.Add(new MenuMap());
        }
    }
}