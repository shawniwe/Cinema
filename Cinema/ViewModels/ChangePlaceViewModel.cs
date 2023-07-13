using Cinema.Abstract;
using Cinema.Extensions;
using Cinema.Factories;
using Cinema.Models;
using CinemaNS.DataAccessLayer;
using CinemaNS.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Cinema.ViewModels
{
    public class ChangePlaceViewModel : INotifyPropertyChanged
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
                        PlaceManager.Create(_model.Map<Place>());
                        break;
                    case RepositoryEventType.Update:
                        PlaceManager.Update(_model.Map<Place>());
                        break;
                }
            }
            catch (Exception ex) { }
            finally
            {
                CloseAction();
            }
        }

        private PlaceModel _model;
        public PlaceModel Model
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

        public EntityManager<Place> PlaceManager { get; private set; }
        public RepositoryEventType EventType { get; private set; }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        }

        public ChangePlaceViewModel(RepositoryEventType eventType, Place model = null)
        {
            PlaceManager = (EntityManager<Place>)EntityManagersFactory.Instance.GetObject("Place");
            EventType = eventType;
            _model = model.Map<PlaceModel>();
            if(_model == null)
            {
                _model = new PlaceModel();
            }
        }

        public ChangePlaceViewModel()
        {

        }
    }
}
