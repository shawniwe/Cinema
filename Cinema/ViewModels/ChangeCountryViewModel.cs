using Cinema.Abstract;
using Cinema.Factories;
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
    public class ChangeCountryViewModel : INotifyPropertyChanged
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
                        CountryManager.Create(_model);
                        break;
                    case RepositoryEventType.Update:
                        CountryManager.Update(_model);
                        break;
                }
            }
            catch (Exception ex) { }
            finally
            {
                CloseAction();
            }
        }

        private Country _model;
        public Country Model
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

        public EntityManager<Country> CountryManager { get; private set; }
        public RepositoryEventType EventType { get; private set; }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        }

        public ChangeCountryViewModel(RepositoryEventType eventType, Country model = null)
        {
            CountryManager = (EntityManager<Country>)EntityManagersFactory.Instance.GetObject("Country");
            EventType = eventType;
            _model = model;
            if(_model == null)
            {
                _model = new Country();
            }
        }

        public ChangeCountryViewModel()
        {

        }
    }
}
