using Cinema.Factories;
using Cinema.Models;
using Cinema.Views;
using CinemaNS.DataAccessLayer;
using CinemaNS.Entities;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Cinema.ViewModels
{
    public class CountryViewModel : INotifyPropertyChanged
    {
        public ICommand DeleteCommand
        {
            get { return new DelegateCommand((x) => { DeleteSelectedItem(); }); }
        }

        private void DeleteSelectedItem()
        {
            try
            {
                if (SelectedItem == null) return;
                _CountryManager.Delete(SelectedItem.Id);
                Countries.Remove(SelectedItem);
            }
            catch
            {
                MessageBox.Show("Ошибка удаления записи из базы данных (возможно, существуют необорванные связи)", "Ошибка удаления", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK);
            }
        }

        private Country _selectedItem;
        public Country SelectedItem
        {
            get
            {
                return _selectedItem;
            }
            set
            {
                _selectedItem = value;
                OnPropertyChanged(nameof(SelectedItem));
            }

        }

        private ObservableCollection<Country> _countries;
        public ObservableCollection<Country> Countries
        {
            get
            {
                return _countries;
            }
            set
            {
                _countries = value;
                OnPropertyChanged(nameof(Countries));
            }
        }

        private EntityManager<Country> _CountryManager;

        public CountryViewModel()
        {
            _CountryManager = (EntityManager<Country>)EntityManagersFactory.Instance.GetObject("Country");
            Countries = new ObservableCollection<Country>(_CountryManager.ReadAll());
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        }

        private DelegateCommand addCommand;
        public ICommand AddCommand => new DelegateCommand((x) => AddCountry());

        private void AddCountry()
        {
            var addCountryViewModel = new ChangeCountryViewModel(Abstract.RepositoryEventType.Create, null);
            var addCountryWindow = new ChangeCountryView(addCountryViewModel);

            addCountryWindow.Show();
            addCountryWindow.Closed += UpdateList;
        }

        private void UpdateList(object? sender, EventArgs e)
        {
            Countries = new ObservableCollection<Country>(_CountryManager.ReadAll());
        }

        private DelegateCommand updateCommand;
        public ICommand UpdateCommand => new DelegateCommand((x) => UpdateCountry());

        private void UpdateCountry()
        {
            var changeCountryViewModel = new ChangeCountryViewModel(Abstract.RepositoryEventType.Update, SelectedItem);
            var changeCountryWindow = new ChangeCountryView(changeCountryViewModel);

            changeCountryWindow.Show();
            changeCountryWindow.Closed += UpdateList;
        }
    }
}
