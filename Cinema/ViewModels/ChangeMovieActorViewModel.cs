using Cinema.Abstract;
using Cinema.DataAccessLayer;
using Cinema.Factories;
using CinemaNS.DataAccessLayer;
using CinemaNS.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Cinema.ViewModels
{
    public class ChangeMovieActorViewModel : INotifyPropertyChanged
    {
        public Action CloseAction { get; set; }
        public ICommand EventCommand
        {
            get
            {
                return new DelegateCommand((x) => { HandleCommand(); });
            }
        }

        private void HandleCommand()
        {
            try
            {
                switch (EventType)
                {
                    case RepositoryEventType.Create:
                        var movieManager = (MovieEntityManager)EntityManagersFactory.Instance.GetObject("Movie");
                        movieManager.AddActor(MovieId, Model);
                        break;
                    case RepositoryEventType.Update:
                        MovieActorManager.Update(_model);
                        break;
                }
            }
            catch (Exception ex) { }
            finally
            {
                CloseAction();
            }
        }

        private MovieActor _model;
        public MovieActor Model
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

        private Person _selectedPerson;

        public Person SelectedPerson
        {
            get { return _selectedPerson; }
            set { _selectedPerson = value; Model.Actor = _selectedPerson; OnPropertyChanged(nameof(SelectedPerson)); }
        }



        private ObservableCollection<Person> _persons;

        public ObservableCollection<Person> Persons
        {
            get { return _persons; }
            set { _persons = value; OnPropertyChanged(nameof(Persons)); }
        }


        public EntityManager<MovieActor> MovieActorManager { get; private set; }
        public RepositoryEventType EventType { get; private set; }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        }

        public long? MovieId { get; set; }

        public ChangeMovieActorViewModel(RepositoryEventType eventType, long? movieId, MovieActor model = null)
        {
            MovieId = movieId;
            MovieActorManager = (EntityManager<MovieActor>)EntityManagersFactory.Instance.GetObject("MovieActor");
            EventType = eventType;
            Model = model;
            if(Model == null)
            {
                Model = new MovieActor();
            }
        }

        public ChangeMovieActorViewModel()
        {

        }
    }
}
