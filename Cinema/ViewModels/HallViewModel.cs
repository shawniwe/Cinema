using Cinema.DataAccessLayer;
using Cinema.Extensions;
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
    public class HallViewModel : INotifyPropertyChanged
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
                _HallManager.Delete(SelectedItem.Id);
                Halls.Remove(SelectedItem);
            }
            catch
            {
                MessageBox.Show("Ошибка удаления записи из базы данных (возможно, существуют необорванные связи)", "Ошибка удаления", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK);
            }
        }

        private Hall _selectedItem;
        public Hall SelectedItem
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

        private ObservableCollection<Hall> _Halls;
        public ObservableCollection<Hall> Halls
        {
            get
            {
                return _Halls;
            }
            set
            {
                _Halls = value;
                OnPropertyChanged(nameof(Halls));
            }
        }

        private HallEntityManager _HallManager;

        public HallViewModel()
        {
            _HallManager = (HallEntityManager)EntityManagersFactory.Instance.GetObject("Hall");
            Halls = new ObservableCollection<Hall>(_HallManager.ReadAll());
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        }

        private DelegateCommand addCommand;
        public ICommand AddCommand => new DelegateCommand((x) => AddHall());

        private void AddHall()
        {
            var addHallViewModel = new ChangeHallViewModel(Abstract.RepositoryEventType.Create, null);
            var addHallWindow = new ChangeHallView(addHallViewModel);

            addHallWindow.Show();
            addHallWindow.Closed += UpdateList;
        }

        private void UpdateList(object? sender, EventArgs e)
        {
            Halls = new ObservableCollection<Hall>(_HallManager.ReadAll());
        }

        private DelegateCommand updateCommand;
        public ICommand UpdateCommand => new DelegateCommand((x) => UpdateHall());

        private void UpdateHall()
        {
            if(SelectedItem != null)
            {
                var m = _HallManager.Read(x => x.Id == SelectedItem.Id).FirstOrDefault();

                var changeHallViewModel = new ChangeHallViewModel(Abstract.RepositoryEventType.Update, m.Map<HallModel>());
                var changeHallWindow = new ChangeHallView(changeHallViewModel);

                changeHallWindow.Show();
                changeHallWindow.Closed += UpdateList;
            }
        }
    }
}
