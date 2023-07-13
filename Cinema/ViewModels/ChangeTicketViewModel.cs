using Cinema.Abstract;
using Cinema.DataAccessLayer;
using Cinema.Entities;
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
    public class ChangeTicketViewModel : INotifyPropertyChanged
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
                        TicketManager.Create(Model.Map<Ticket>());
                        break;
                    case RepositoryEventType.Update:
                        TicketManager.Update(Model.Map<Ticket>());
                        break;
                }
            }
            catch (Exception ex) { }
            finally
            {
                CloseAction();
            }
        }

        private TicketModel _model;
        public TicketModel Model
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

        public TicketEntityManager TicketManager { get; private set; }
        public RepositoryEventType EventType { get; private set; }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        }

        public ChangeTicketViewModel(RepositoryEventType eventType, TicketModel model = null)
        {
            TicketManager = (TicketEntityManager)EntityManagersFactory.Instance.GetObject("Ticket");
            EventType = eventType;
            Model = model;
            if(Model == null)
            {
                Model = new TicketModel();
            }
        }

        public ChangeTicketViewModel()
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

        private ObservableCollection<Place> _allPlaces;

        public ObservableCollection<Place> AllPlaces
        {
            get
            {
                return _allPlaces;
            }
            set
            {
                _allPlaces = value;
                OnPropertyChanged(nameof(AllPlaces));
            }
        }

        private Place _selectedPlace;

        public Place SelectedPlace
        {
            get
            {
                return _selectedPlace;
            }
            set
            {
                _selectedPlace = value;
                if (Model != null)
                {
                    Model.Place = _selectedPlace;
                }
                OnPropertyChanged(nameof(SelectedPlace));
            }
        }
        private ObservableCollection<Session> _allSessions;

        public ObservableCollection<Session> AllSessions
        {
            get
            {
                return _allSessions;
            }
            set
            {
                _allSessions = value;
                OnPropertyChanged(nameof(AllSessions));
            }
        }

        private Session _selectedSession;

        public Session SelectedSession
        {
            get
            {
                return _selectedSession;
            }
            set
            {
                _selectedSession = value;
                if (Model != null)
                {
                    Model.Session = _selectedSession;
                }
                
                var buyedPlacesOnThisSession = TicketManager.Read(x => x.Session == _selectedSession).Select(x => x.Place).ToList();

                AllPlaces = new ObservableCollection<Place>(_selectedSession
                                                                    .Hall
                                                                    .Rows
                                                                    .SelectMany(x => x.Places)
                                                                    .Where(x => !buyedPlacesOnThisSession.Contains(x)));
                OnPropertyChanged(nameof(SelectedSession));
            }
        }
    }
}