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
    public class ChangeCinemaViewModel : INotifyPropertyChanged
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
                        CinemaManager.Create(Model.Map<CinemaNS.Entities.Cinema>());
                        break;
                    case RepositoryEventType.Update:
                        CinemaManager.Update(Model.Map<CinemaNS.Entities.Cinema>());
                        break;
                }
            }
            catch (Exception ex) { }
            finally
            {
                CloseAction();
            }
        }

        private CinemaModel _model;
        public CinemaModel Model
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

        public CinemaEntityManager CinemaManager { get; private set; }
        public RepositoryEventType EventType { get; private set; }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        }

        public ChangeCinemaViewModel(RepositoryEventType eventType, CinemaModel model = null)
        {
            CinemaManager = (CinemaEntityManager)EntityManagersFactory.Instance.GetObject("Cinema");
            EventType = eventType;
            Model = model;
            if(Model == null)
            {
                Model = new CinemaModel();
            }
        }

        public ChangeCinemaViewModel()
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

        private ObservableCollection<District> _allDistricts;

        public ObservableCollection<District> AllDistricts { 
            get
            {
                return _allDistricts;
            }
            set
            {
                _allDistricts = value;
                OnPropertyChanged(nameof(AllDistricts));
            }
        }

        private District _selectedDistrict;

        public District SelectedDistrict 
        { 
            get
            {
                return _selectedDistrict;
            }
            set
            {
                _selectedDistrict = value;
                if(Model != null)
                {
                    Model.District = _selectedDistrict;
                }
                OnPropertyChanged(nameof(SelectedDistrict));
            }
        }
    }
}
