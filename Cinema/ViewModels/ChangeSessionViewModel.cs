using Cinema.Abstract;
using Cinema.DataAccessLayer;
using Cinema.Extensions;
using Cinema.Factories;
using Cinema.Models;
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
    public class ChangeSessionViewModel : INotifyPropertyChanged
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
                        SessionManager.Create(Model.Map<CinemaNS.Entities.Session>());
                        break;
                    case RepositoryEventType.Update:
                        SessionManager.Update(Model.Map<CinemaNS.Entities.Session>());
                        break;
                }
            }
            catch (Exception ex) { }
            finally
            {
                CloseAction();
            }
        }

        private SessionModel _model;
        public SessionModel Model
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

        public SessionEntityManager SessionManager { get; private set; }
        public RepositoryEventType EventType { get; private set; }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        }

        public ChangeSessionViewModel(RepositoryEventType eventType, SessionModel model = null)
        {
            SessionManager = (SessionEntityManager)EntityManagersFactory.Instance.GetObject("Session");
            EventType = eventType;
            Model = model;
            if(Model == null)
            {
                Model = new SessionModel();
                Model.StartDate = DateTime.Now;
            }
        }

        public ChangeSessionViewModel()
        {

        }

        protected bool SetProperty<T>(ref T field, T newValue, [CallerMemberName] string propertyName = null)
        {
            if (!Equals(field, newValue))
            {
                field = newValue;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
                return true;
            }

            return false;
        }

        private ObservableCollection<Movie> _allMovies;

        public ObservableCollection<Movie> AllMovies
        {
            get
            {
                return _allMovies;
            }
            set
            {
                _allMovies = value;
                OnPropertyChanged(nameof(AllMovies));
            }
        }

        private Movie _selectedMovie;

        public Movie SelectedMovie
        {
            get
            {
                return _selectedMovie;
            }
            set
            {
                _selectedMovie = value;
                if (Model != null)
                {
                    Model.Movie = _selectedMovie;
                }
                OnPropertyChanged(nameof(SelectedMovie));
            }
        }
        private ObservableCollection<Hall> _allHalls;

        public ObservableCollection<Hall> AllHalls
        {
            get
            {
                return _allHalls;
            }
            set
            {
                _allHalls = value;
                OnPropertyChanged(nameof(AllHalls));
            }
        }

        private Hall _selectedHall;

        public Hall SelectedHall
        {
            get
            {
                return _selectedHall;
            }
            set
            {
                _selectedHall = value;
                if (Model != null)
                {
                    Model.Hall = _selectedHall;
                }
                OnPropertyChanged(nameof(SelectedHall));
            }
        }
    }
}
