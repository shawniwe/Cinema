using Cinema.Abstract;
using Cinema.Configuration;
using Cinema.Factories;
using Cinema.Models;
using Cinema.Views;
using Cinema.Views.Reports;
using CinemaNS.Abstract;
using CinemaNS.DataAccessLayer;
using CinemaNS.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Cinema.ViewModels
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        public IEnumerable<Models.MenuItem> MenuItems { get; private set; }

        private Models.MenuItem _selectedItem;
        public Models.MenuItem SelectedItem 
        { 
            get 
            {
                return _selectedItem;
            } 
            set 
            {
                _selectedItem = value;
                OnPropertyChanged(nameof(SelectedItem));
                LoadPage();
            } 
        }

        public Page SelectedPage { 
            get
            {
                return _selectedPage;
            }
            set
            {
                _selectedPage = value;
                OnPropertyChanged(nameof(SelectedPage));
            }
        }

        private Page _selectedPage;

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        }
        public MainWindowViewModel()
        {
            MenuItems = ApplicationConfig.Instance.Settings.Where(x => x.SettingType == SettingType.MenuItem)
               .Select(x => JsonConvert.DeserializeObject<Models.MenuItem>(x.Json));
        }
        public void LoadPage()
        {
            switch (SelectedItem.Key)
            {
                case "Person":
                    SelectedPage = new PersonView();
                    break;
                case "Genre":
                    SelectedPage = new GenreView();
                    break;
                case "District":
                    SelectedPage = new DistrictView();
                    break;
                case "Country":
                    SelectedPage = new CountryView();
                    break;
                case "Cinema":
                    SelectedPage = new CinemaView();
                    break;
                case "Hall":
                    SelectedPage = new HallView();
                    break;
                case "Movie":
                    SelectedPage = new MovieView();
                    break;
                case "Session":
                    SelectedPage = new SessionView();
                    break;
                case "Ticket":
                    SelectedPage = new TicketView();
                    break;
                case "CinemaRepertoireReport":
                    SelectedPage = new CinemaRepertoireReportView();
                    break;
                case "RevenueReport":
                    SelectedPage = new RevenueReportView();
                    break;
                default:
                    break;
            }
        }
    }
}
