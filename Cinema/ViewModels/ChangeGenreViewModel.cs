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
    public class ChangeGenreViewModel : INotifyPropertyChanged
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
                        GenreManager.Create(_model);
                        break;
                    case RepositoryEventType.Update:
                        GenreManager.Update(_model);
                        break;
                }
            }
            catch (Exception ex) { }
            finally
            {
                CloseAction();
            }
        }

        private Genre _model;
        public Genre Model
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

        public EntityManager<Genre> GenreManager { get; private set; }
        public RepositoryEventType EventType { get; private set; }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        }

        public ChangeGenreViewModel(RepositoryEventType eventType, Genre model = null)
        {
            GenreManager = (EntityManager<Genre>)EntityManagersFactory.Instance.GetObject("Genre");
            EventType = eventType;
            _model = model;
            if(_model == null)
            {
                _model = new Genre();
            }
        }

        public ChangeGenreViewModel()
        {

        }
    }
}
