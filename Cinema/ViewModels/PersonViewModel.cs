using Cinema.Factories;
using Cinema.Models;
using Cinema.Views;
using CinemaNS.DataAccessLayer;
using CinemaNS.Entities;
using System;
using System.CodeDom;
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
    public class PersonViewModel : INotifyPropertyChanged
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
                _personManager.Delete(SelectedItem.Id);
                Persons.Remove(SelectedItem);
            }
            catch
            {
                MessageBox.Show("Ошибка удаления записи из базы данных (возможно, существуют необорванные связи)", "Ошибка удаления", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK);
            }
        }

        private Person _selectedItem;
        public Person SelectedItem
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

        private ObservableCollection<Person> _persons;
        public ObservableCollection<Person> Persons
        {
            get
            {
                return _persons;
            }
            set
            {
                _persons = value;
                OnPropertyChanged(nameof(Persons));
            }
        }

        private EntityManager<Person> _personManager;

        public PersonViewModel()
        {
            _personManager = (EntityManager<Person>)EntityManagersFactory.Instance.GetObject("Person");
            Persons = new ObservableCollection<Person>(_personManager.ReadAll());
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        }

        private DelegateCommand addCommand;
        public ICommand AddCommand => new DelegateCommand((x) => AddPerson());

        private void AddPerson()
        {
            var addPersonViewModel = new ChangePersonViewModel(Abstract.RepositoryEventType.Create, null);
            var addPersonWindow = new ChangePersonView(addPersonViewModel);

            addPersonWindow.Show();
            addPersonWindow.Closed += UpdateList;
        }

        private void UpdateList(object? sender, EventArgs e)
        {
            Persons = new ObservableCollection<Person>(_personManager.ReadAll());
        }

        private DelegateCommand updateCommand;
        public ICommand UpdateCommand => new DelegateCommand((x) => UpdatePerson());

        private void UpdatePerson()
        {
            var changePersonViewModel = new ChangePersonViewModel(Abstract.RepositoryEventType.Update, SelectedItem);
            var changePersonWindow = new ChangePersonView(changePersonViewModel);

            changePersonWindow.Show();
            changePersonWindow.Closed += UpdateList;
        }
    }
}
