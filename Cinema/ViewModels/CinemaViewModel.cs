using Cinema.DataAccessLayer;
using Cinema.Extensions;
using Cinema.Factories;
using Cinema.Models;
using Cinema.Views;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;

namespace Cinema.ViewModels
{
    public class CinemaViewModel : INotifyPropertyChanged
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
                _cinemaManager.Delete(SelectedItem.Id);
                CinemaList.Remove(SelectedItem);
            }
            catch
            {
                MessageBox.Show("Ошибка удаления записи из базы данных (возможно, существуют необорванные связи)", "Ошибка удаления", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK);
            }
        }

        private CinemaNS.Entities.Cinema _selectedItem;
        public CinemaNS.Entities.Cinema SelectedItem
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

        private ObservableCollection<CinemaNS.Entities.Cinema> _cinemaList;
        public ObservableCollection<CinemaNS.Entities.Cinema> CinemaList
        {
            get
            {
                return _cinemaList;
            }
            set
            {
                _cinemaList = value;
                OnPropertyChanged(nameof(CinemaList));
            }
        }

        private CinemaEntityManager _cinemaManager;

        public CinemaViewModel()
        {
            _cinemaManager = (CinemaEntityManager)EntityManagersFactory.Instance.GetObject("Cinema");
            CinemaList = new ObservableCollection<CinemaNS.Entities.Cinema>(_cinemaManager.ReadAll());
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        }

        private DelegateCommand addCommand;
        public ICommand AddCommand => new DelegateCommand((x) => AddCinema());

        private void AddCinema()
        {
            var addCinemaViewModel = new ChangeCinemaViewModel(Abstract.RepositoryEventType.Create, null);
            var addCinemaWindow = new ChangeCinemaView(addCinemaViewModel);

            addCinemaWindow.Show();
            addCinemaWindow.Closed += UpdateList;
        }

        private DelegateCommand updateCommand;
        public ICommand UpdateCommand => new DelegateCommand((x) => UpdateCinema());

        private void UpdateCinema()
        {
            var changeCinemaViewModel = new ChangeCinemaViewModel(Abstract.RepositoryEventType.Update, SelectedItem.Map<CinemaModel>());
            var changeCinemaWindow = new ChangeCinemaView(changeCinemaViewModel);

            changeCinemaWindow.Show();
            changeCinemaWindow.Closed += UpdateList;
        }

        private void UpdateList(object? sender, EventArgs e)
        {
            var cinemas = _cinemaManager.ReadAll().ToList();
            CinemaList = new ObservableCollection<CinemaNS.Entities.Cinema>(cinemas);
        }
    }
}
