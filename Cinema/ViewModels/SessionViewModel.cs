using Cinema.DataAccessLayer;
using Cinema.Extensions;
using Cinema.Factories;
using Cinema.Models;
using Cinema.Views;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;

namespace Cinema.ViewModels
{
    public class SessionViewModel : INotifyPropertyChanged
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
                _sessionManager.Delete(SelectedItem.Id);
                SessionList.Remove(SelectedItem);
            }
            catch
            {
                MessageBox.Show("Ошибка удаления записи из базы данных (возможно, существуют необорванные связи)", "Ошибка удаления", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK);
            }
        }

        private CinemaNS.Entities.Session _selectedItem;
        public CinemaNS.Entities.Session SelectedItem
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

        private ObservableCollection<CinemaNS.Entities.Session> _sessionList;
        public ObservableCollection<CinemaNS.Entities.Session> SessionList
        {
            get
            {
                return _sessionList;
            }
            set
            {
                _sessionList = value;
                OnPropertyChanged(nameof(SessionList));
            }
        }

        private SessionEntityManager _sessionManager;

        public SessionViewModel()
        {
            _sessionManager = (SessionEntityManager)EntityManagersFactory.Instance.GetObject("Session");
            SessionList = new ObservableCollection<CinemaNS.Entities.Session>(_sessionManager.ReadAll());
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        }

        private DelegateCommand addCommand;
        public ICommand AddCommand => new DelegateCommand((x) => AddSession());

        private void AddSession()
        {
            var addSessionViewModel = new ChangeSessionViewModel(Abstract.RepositoryEventType.Create, null);
            var addSessionWindow = new ChangeSessionView(addSessionViewModel);

            addSessionWindow.Show();
            addSessionWindow.Closed += UpdateList;
        }

        private DelegateCommand updateCommand;
        public ICommand UpdateCommand => new DelegateCommand((x) => UpdateSession());

        private void UpdateSession()
        {
            var changeSessionViewModel = new ChangeSessionViewModel(Abstract.RepositoryEventType.Update, SelectedItem.Map<SessionModel>());
            var changeSessionWindow = new ChangeSessionView(changeSessionViewModel);

            changeSessionWindow.Show();
            changeSessionWindow.Closed += UpdateList;
        }

        private void UpdateList(object? sender, EventArgs e)
        {
            var cinemas = _sessionManager.ReadAll().ToList();
            SessionList = new ObservableCollection<CinemaNS.Entities.Session>(cinemas);
        }
    }
}
