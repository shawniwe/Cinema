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
    public class DistrictViewModel : INotifyPropertyChanged
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
                _districtManager.Delete(SelectedItem.Id);
                Districts.Remove(SelectedItem);
            }
            catch
            {
                MessageBox.Show("Ошибка удаления записи из базы данных (возможно, существуют необорванные связи)", "Ошибка удаления", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK);
            }
        }

        private District _selectedItem;
        public District SelectedItem
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

        private ObservableCollection<District> _districts;
        public ObservableCollection<District> Districts
        {
            get
            {
                return _districts;
            }
            set
            {
                _districts = value;
                OnPropertyChanged(nameof(Districts));
            }
        }

        private EntityManager<District> _districtManager;

        public DistrictViewModel()
        {
            _districtManager = (EntityManager<District>)EntityManagersFactory.Instance.GetObject("District");
            Districts = new ObservableCollection<District>(_districtManager.ReadAll());
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        }

        private DelegateCommand addCommand;
        public ICommand AddCommand => new DelegateCommand((x) => AddDistrict());

        private void AddDistrict()
        {
            var addDistrictViewModel = new ChangeDistrictViewModel(Abstract.RepositoryEventType.Create, null);
            var addDistrictWindow = new ChangeDistrictView(addDistrictViewModel);

            addDistrictWindow.Show();
            addDistrictWindow.Closed += UpdateList;
        }

        private void UpdateList(object? sender, EventArgs e)
        {
            Districts = new ObservableCollection<District>(_districtManager.ReadAll());
        }

        private DelegateCommand updateCommand;
        public ICommand UpdateCommand => new DelegateCommand((x) => UpdateDistrict());

        private void UpdateDistrict()
        {
            var changeDistrictViewModel = new ChangeDistrictViewModel(Abstract.RepositoryEventType.Update, SelectedItem);
            var changeDistrictWindow = new ChangeDistrictView(changeDistrictViewModel);

            changeDistrictWindow.Show();
            changeDistrictWindow.Closed += UpdateList;
        }
    }
}
