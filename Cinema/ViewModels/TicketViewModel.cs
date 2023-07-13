using Cinema.DataAccessLayer;
using Cinema.Entities;
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
    public class TicketViewModel : INotifyPropertyChanged
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
                TicketList.Remove(SelectedItem);
            }
            catch
            {
                MessageBox.Show("Ошибка удаления записи из базы данных (возможно, существуют необорванные связи)", "Ошибка удаления", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK);
            }
        }

        private Ticket _selectedItem;
        public Ticket SelectedItem
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

        private ObservableCollection<Ticket> _sessionList;
        public ObservableCollection<Ticket> TicketList
        {
            get
            {
                return _sessionList;
            }
            set
            {
                _sessionList = value;
                OnPropertyChanged(nameof(TicketList));
            }
        }

        private TicketEntityManager _sessionManager;

        public TicketViewModel()
        {
            _sessionManager = (TicketEntityManager)EntityManagersFactory.Instance.GetObject("Ticket");
            TicketList = new ObservableCollection<Ticket>(_sessionManager.ReadAll());
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        }

        private DelegateCommand addCommand;
        public ICommand AddCommand => new DelegateCommand((x) => AddTicket());

        private void AddTicket()
        {
            var addTicketViewModel = new ChangeTicketViewModel(Abstract.RepositoryEventType.Create, null);
            var addTicketWindow = new ChangeTicketView(addTicketViewModel);

            addTicketWindow.Show();
            addTicketWindow.Closed += UpdateList;
        }

        private DelegateCommand updateCommand;
        public ICommand UpdateCommand => new DelegateCommand((x) => UpdateTicket());

        private void UpdateTicket()
        {
            var changeTicketViewModel = new ChangeTicketViewModel(Abstract.RepositoryEventType.Update, SelectedItem.Map<TicketModel>());
            var changeTicketWindow = new ChangeTicketView(changeTicketViewModel);

            changeTicketWindow.Show();
            changeTicketWindow.Closed += UpdateList;
        }

        private void UpdateList(object? sender, EventArgs e)
        {
            var cinemas = _sessionManager.ReadAll().ToList();
            TicketList = new ObservableCollection<Ticket>(cinemas);
        }
    }
}
