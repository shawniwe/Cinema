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
    public class GenreViewModel : INotifyPropertyChanged
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
                _genreManager.Delete(SelectedItem.Id);
                Genres.Remove(SelectedItem);
            }
            catch
            {
                MessageBox.Show("Ошибка удаления записи из базы данных (возможно, существуют необорванные связи)", "Ошибка удаления", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK);
            }
        }

        private Genre _selectedItem;
        public Genre SelectedItem
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

        private ObservableCollection<Genre> _genres;
        public ObservableCollection<Genre> Genres
        {
            get
            {
                return _genres;
            }
            set
            {
                _genres = value;
                OnPropertyChanged(nameof(Genres));
            }
        }

        private EntityManager<Genre> _genreManager;

        public GenreViewModel()
        {
            _genreManager = (EntityManager<Genre>)EntityManagersFactory.Instance.GetObject("Genre");
            Genres = new ObservableCollection<Genre>(_genreManager.ReadAll());
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        }

        private DelegateCommand addCommand;
        public ICommand AddCommand => new DelegateCommand((x) => AddGenre());

        private void AddGenre()
        {
            var addGenreViewModel = new ChangeGenreViewModel(Abstract.RepositoryEventType.Create, null);
            var addGenreWindow = new ChangeGenreView(addGenreViewModel);

            addGenreWindow.Show();
            addGenreWindow.Closed += UpdateList;
        }

        private void UpdateList(object? sender, EventArgs e)
        {
            Genres = new ObservableCollection<Genre>(_genreManager.ReadAll());
        }

        private DelegateCommand updateCommand;
        public ICommand UpdateCommand => new DelegateCommand((x) => UpdateGenre());

        private void UpdateGenre()
        {
            var changeGenreViewModel = new ChangeGenreViewModel(Abstract.RepositoryEventType.Update, SelectedItem);
            var changeGenreWindow = new ChangeGenreView(changeGenreViewModel);

            changeGenreWindow.Show();
            changeGenreWindow.Closed += UpdateList;
        }
    }
}
