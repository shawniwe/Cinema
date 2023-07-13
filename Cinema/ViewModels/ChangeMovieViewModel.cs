using Cinema.Abstract;
using Cinema.DataAccessLayer;
using Cinema.Extensions;
using Cinema.Factories;
using Cinema.Models;
using Cinema.Views;
using CinemaNS.DataAccessLayer;
using CinemaNS.Entities;
using Microsoft.Win32;
using System;
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
    public class ChangeMovieViewModel : INotifyPropertyChanged
    {
        public Action CloseAction { get; set; }
        public ICommand EventCommand
        {
            get
            {
                return new DelegateCommand((x) => { HandleCommand(); });
            }
        }

        public ICommand LoadPhotoCommand
        {
            get
            {
                return new DelegateCommand((x) => { LoadPhoto(); });
            }
        }

        public ICommand AddActorCommand
        {
            get
            {
                return new DelegateCommand((x) => { AddActor(); });
            }
        }

        public ICommand DeleteActorCommand
        {
            get
            {
                return new DelegateCommand((x) => { DeleteActor(); });
            }
        }
        public ICommand AddProducerCommand
        {
            get
            {
                return new DelegateCommand((x) => { AddProducer(); });
            }
        }

        public ICommand DeleteOperatorCommand   
        {
            get
            {
                return new DelegateCommand((x) => { DeleteOperator(); });
            }
        }

        private void DeleteOperator()
        {
            try
            {
                var movieManager = (MovieEntityManager)EntityManagersFactory.Instance.GetObject("Movie");
                movieManager.DeleteOperator(Model.Id, SelectedOperator.Id);
                MovieOperators.Remove(SelectedOperator);
            }
            catch
            {
                MessageBox.Show("Ошибка удаления записи из базы данных (возможно, существуют необорванные связи)", "Ошибка удаления", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK);
            }
        }

        public ICommand AddOperatorCommand
        {
            get
            {
                return new DelegateCommand((x) => { AddOperator(); });
            }
        }

        private void AddOperator()
        {
            if (Model.Id == null)
            {
                var entry = Model.Map<Movie>();
                MovieManager.Create(entry);
                Model = entry.Map<MovieModel>();
            }
            var wnd = new ChangeMovieOperatorView(new ChangeMovieOperatorViewModel(RepositoryEventType.Create, Model.Id, null));
            wnd.Show();
            wnd.Closed += UpdateOperatorList;
        }

        private void UpdateOperatorList(object? sender, EventArgs e)
        {
            var entry = MovieManager.Read(x => x.Id == Model.Id).FirstOrDefault();
            if (entry != null)
            {
                Model = entry.Map<MovieModel>();
                MovieOperators = new ObservableCollection<MovieOperator>(entry.MovieOperators);
            }
        }

        private void AddProducer()
        {
            if (Model.Id == null)
            {
                var entry = Model.Map<Movie>();
                MovieManager.Create(entry);
                Model = entry.Map<MovieModel>();
            }
            //
            var wnd = new ChangeMovieProducerView(new ChangeMovieProducerViewModel(RepositoryEventType.Create, Model.Id, null));
            wnd.Show();
            wnd.Closed += UpdateProducerList;
        }

        private void UpdateProducerList(object? sender, EventArgs e)
        {
            var entry = MovieManager.Read(x => x.Id == Model.Id).FirstOrDefault();
            if (entry != null)
            {
                Model = entry.Map<MovieModel>();
                MovieProducers = new ObservableCollection<MovieProducer>(entry.MovieProducers);
            }
        }

        public ICommand DeleteProducerCommand
        {
            get
            {
                return new DelegateCommand((x) => { DeleteProducer(); });
            }
        }

        private void DeleteProducer()
        {
            try
            {
                var movieManager = (MovieEntityManager)EntityManagersFactory.Instance.GetObject("Movie");
                movieManager.DeleteProducer(Model.Id, SelectedProducer.Id);
                MovieProducers.Remove(SelectedProducer);
            }
            catch
            {
                MessageBox.Show("Ошибка удаления записи из базы данных (возможно, существуют необорванные связи)", "Ошибка удаления", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK);
            }
        }

        private void DeleteActor()
        {
            try
            {
                var movieManager = (MovieEntityManager)EntityManagersFactory.Instance.GetObject("Movie");
                movieManager.DeleteActor(Model.Id, SelectedActor.Id);
                MovieActors.Remove(SelectedActor);
            }
            catch
            {
                MessageBox.Show("Ошибка удаления записи из базы данных (возможно, существуют необорванные связи)", "Ошибка удаления", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK);
            }

        }

        private void AddActor()
        {
            if(Model.Id == null)
            {
                var entry = Model.Map<Movie>();
                MovieManager.Create(entry);
                Model = entry.Map<MovieModel>();
            }
            //
            var wnd = new ChangeMovieActorView(new ChangeMovieActorViewModel(RepositoryEventType.Create, Model.Id, null));
            wnd.Show();
            wnd.Closed += UpdateActorList;
        }

        private void UpdateActorList(object? sender, EventArgs e)
        {
            var entry = MovieManager.Read(x => x.Id == Model.Id).FirstOrDefault();
            if(entry != null)
            {
                Model = entry.Map<MovieModel>();
                MovieActors = new ObservableCollection<MovieActor>(entry.MovieActors);
            }
        }

        private void LoadPhoto()
        {
            var dialog = new OpenFileDialog();
            if(dialog.ShowDialog() == true)
            {
                Model.Photo = dialog.FileName;
                DisplayPhoto = new Uri(Model.Photo);
            }
        }

        private void HandleCommand()
        {
            try
            {
                switch (EventType)
                {
                    case RepositoryEventType.Create:
                        if(!MovieManager.Read(x => x.Id == Model.Id).Any())
                        {
                            MovieManager.Create(Model.Map<Movie>());
                        }
                        else
                        {
                            MovieManager.Update(Model.Map<Movie>());
                        }
                        break;
                    case RepositoryEventType.Update:
                        MovieManager.Update(Model.Map<Movie>());
                        break;
                }
            }
            catch (Exception ex) { }
            finally
            {
                CloseAction();
            }
        }

        private Uri _displayPhoto;

        public Uri DisplayPhoto
        {
            get
            {
                return _displayPhoto;
            }
            set
            {
                _displayPhoto = value;
                OnPropertyChanged(nameof(DisplayPhoto));
            }
        }

        private ObservableCollection<MovieActor> _movieActors;

        public ObservableCollection<MovieActor> MovieActors
        {
            get { return _movieActors; }
            set { _movieActors = value; OnPropertyChanged(nameof(MovieActors)); }
        }
         
        private MovieOperator _selectedOperator;

        public MovieOperator SelectedOperator
        {
            get { return _selectedOperator; }
            set { _selectedOperator = value; OnPropertyChanged(nameof(SelectedOperator)); }
        }

        private ObservableCollection<MovieOperator> _movieOperators;

        public ObservableCollection<MovieOperator> MovieOperators
        {
            get { return _movieOperators; }
            set { _movieOperators = value; OnPropertyChanged(nameof(MovieOperators)); }
        }

        private MovieProducer _selectedProducer;

        public MovieProducer SelectedProducer
        {
            get { return _selectedProducer; }
            set { _selectedProducer = value; OnPropertyChanged(nameof(SelectedProducer)); }
        }

        private ObservableCollection<MovieProducer> _movieProducers;

        public ObservableCollection<MovieProducer> MovieProducers
        {
            get { return _movieProducers; }
            set { _movieProducers = value; OnPropertyChanged(nameof(MovieProducers)); }
        }


        private MovieModel _model;
        public MovieModel Model
        {
            get
            {

                return _model;
            }
            set
            {
                _model = value;
                OnPropertyChanged(nameof(Model));
            }
        }

        private MovieActor _selectedActor;

        public MovieActor SelectedActor
        {
            get { return _selectedActor; }
            set { _selectedActor = value; OnPropertyChanged(nameof(SelectedActor)); }
        }


        public MovieEntityManager MovieManager { get; private set; }
        public RepositoryEventType EventType { get; private set; }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        }

        public ChangeMovieViewModel(RepositoryEventType eventType, MovieModel model = null)
        {
            MovieManager = (MovieEntityManager)EntityManagersFactory.Instance.GetObject("Movie");
            EventType = eventType;
            _model = model;
            if(Model == null)
            {
                Model = new MovieModel();
            }
            else
            {
                SelectedGenre = Model.Genre;
                SelectedCountry = Model.Country;
            }

            if(Model.MovieActors != null)
            {
                MovieActors = new ObservableCollection<MovieActor>(Model.MovieActors);
            }
            if(Model.MovieProducers != null)
            {
                MovieProducers = new ObservableCollection<MovieProducer>(Model.MovieProducers);
            }
            if(Model.MovieOperators != null)
            {
                MovieOperators = new ObservableCollection<MovieOperator>(Model.MovieOperators);
            }
            DisplayPhoto = string.IsNullOrWhiteSpace(Model.Photo) ? null : new Uri(Model.Photo);
        }

        public ChangeMovieViewModel()
        {

        }

        private Genre _selectedGenre;

        public Genre SelectedGenre 
        {
            get
            {
                return _selectedGenre;
            }
            set
            {
                _selectedGenre = value;
                if (Model != null)
                {
                    Model.Genre = _selectedGenre;
                }

                OnPropertyChanged(nameof(_selectedGenre));
            }
        }

        private ObservableCollection<Genre> _genres;

        public ObservableCollection<Genre> Genres
        {
            get { return _genres; }
            set { _genres = value; OnPropertyChanged(nameof(Genres)); }
        }

        private Country _selectedCountry;
        public Country SelectedCountry
        {
            get
            {
                return _selectedCountry;
            }
            set
            {
                _selectedCountry = value;
                if(Model != null)
                {
                    Model.Country = _selectedCountry;
                }

                OnPropertyChanged(nameof(SelectedCountry));
            }
        }

        private ObservableCollection<Country> _countries;

        public ObservableCollection<Country> Countries
        {
            get { return _countries; }
            set { _countries = value; OnPropertyChanged(nameof(Countries)); }
        }

    }
}
