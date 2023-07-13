using Cinema.DataAccessLayer;
using Cinema.Extensions;
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
    public class MovieViewModel : INotifyPropertyChanged
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
                _MovieManager.Delete(SelectedItem.Id);
                Movies.Remove(SelectedItem);
            }
            catch
            {
                MessageBox.Show("Ошибка удаления записи из базы данных (возможно, существуют необорванные связи)", "Ошибка удаления", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK);
            }
        }

        private MovieModel _selectedItem;
        public MovieModel SelectedItem
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

        private ObservableCollection<MovieModel> _Movies;
        public ObservableCollection<MovieModel> Movies
        {
            get
            {
                return _Movies;
            }
            set
            {
                _Movies = value;
                OnPropertyChanged(nameof(Movies));
            }
        }

        private MovieEntityManager _MovieManager;

        public MovieViewModel()
        {
            _MovieManager = (MovieEntityManager)EntityManagersFactory.Instance.GetObject("Movie");
            UpdateList(null, null);
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        }

        private DelegateCommand addCommand;
        public ICommand AddCommand => new DelegateCommand((x) => AddMovie());

        private void AddMovie()
        {
            var addMovieViewModel = new ChangeMovieViewModel(Abstract.RepositoryEventType.Create, null);
            var addMovieWindow = new ChangeMovieView(addMovieViewModel);

            addMovieWindow.Show();
            addMovieWindow.Closed += UpdateList;
        }

        private void UpdateList(object? sender, EventArgs e)
        {
            Movies = new ObservableCollection<MovieModel>(_MovieManager.ReadAll().Select(x => x.Map<MovieModel>()));
            foreach (var m in Movies)
            {
                m.DisplayPhoto = string.IsNullOrWhiteSpace(m.Photo) ? null : new Uri(m.Photo);
            }
        }

        private DelegateCommand updateCommand;
        public ICommand UpdateCommand => new DelegateCommand((x) => UpdateMovie());

        private void UpdateMovie()
        {
            var changeMovieViewModel = new ChangeMovieViewModel(Abstract.RepositoryEventType.Update, SelectedItem);
            var changeMovieWindow = new ChangeMovieView(changeMovieViewModel);

            changeMovieWindow.Show();
            changeMovieWindow.Closed += UpdateList;
        }
    }
}
