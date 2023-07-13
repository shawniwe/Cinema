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
using System.Windows;
using System.Windows.Input;

namespace Cinema.ViewModels
{
    public class ChangeRowViewModel : INotifyPropertyChanged
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
                        RowManager.Create(Model.Map<Row>());
                        break;
                    case RepositoryEventType.Update:
                        RowManager.Update(_model.Map<Row>());
                        break;
                }
            }
            catch (Exception ex) { }
            finally
            {
                CloseAction();
            }
        }

        public ICommand AddPlaceCommand
        {
            get
            {
                return new DelegateCommand((x) => { AddPlace(); });
            }
        }
        public ICommand EditPlaceCommand
        {
            get
            {
                return new DelegateCommand((x) => { EditPlace(); });
            }
        }

        private void EditPlace()
        {
            // call edit window
        }

        public ICommand DeletePlaceCommand
        {
            get
            {
                return new DelegateCommand((x) => { DeletePlace(); });
            }
        }

        private void DeletePlace()
        {
            try
            {
                if (SelectedPlace != null)
                {
                    var placeManager = (PlaceEntityManager)EntityManagersFactory.Instance.GetObject("Place");
                    var toDelete = placeManager.Read(x => x.Id == SelectedPlace.Id).FirstOrDefault();
                    placeManager.Delete(toDelete.Id);
                    PlacesList.Remove(SelectedPlace);
                }
            }
            catch
            {
                MessageBox.Show("Ошибка удаления записи из базы данных (возможно, существуют необорванные связи)", "Ошибка удаления", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK);
            }
        }

        private void AddPlace()
        {
            var newPlace = new Place()
            {
                Number = RowManager.PlacesCount(Model.Id) + 1
            };

            var hmanager = (HallEntityManager)EntityManagersFactory.Instance.GetObject("Hall");
            hmanager.AddPlace(Model.Id, newPlace);
            PlacesList.Add(newPlace.Map<PlaceModel>());

            //Model = RowManager.Read(x => x.Id == Model.Id).FirstOrDefault().Map<RowModel>();
            //RowsList = new ObservableCollection<RowModel>(rowManager.Read(x => x.Hall.Id == Model.Map<Hall>().Id).Select(x => x.Map<RowModel>()));

            //var wnd = new ChangeRowView(new ChangeRowViewModel(RepositoryEventType.Create));
            //wnd.Show();
        }

        private RowModel _model;
        public RowModel Model
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

        public RowEntityManager RowManager { get; private set; }
        public RepositoryEventType EventType { get; private set; }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        }

        public ChangeRowViewModel(RepositoryEventType eventType, RowModel model = null)
        {
            RowManager = (RowEntityManager)EntityManagersFactory.Instance.GetObject("Row");
            EventType = eventType;
            Model = model;
            if(Model == null)
            {
                Model = new RowModel();
            }

            if(Model.Places != null)
            {
                PlacesList = new ObservableCollection<PlaceModel>(Model.Places.Select(x => x.Map<PlaceModel>()));
            }
            else
            {
                PlacesList = new ObservableCollection<PlaceModel>();
            }
        }

        public ChangeRowViewModel()
        {

        }

        private PlaceModel _selectedPlace;

        public PlaceModel SelectedPlace
        {
            get { return _selectedPlace; }
            set
            {
                _selectedPlace = value;
                OnPropertyChanged(nameof(SelectedPlace));
            }
        }


        private ObservableCollection<PlaceModel> _placesList;

        public ObservableCollection<PlaceModel> PlacesList
        {
            get { return _placesList; }
            set
            {
                _placesList = value;
                OnPropertyChanged(nameof(PlacesList));
            }
        }


        private CinemaNS.Entities.Hall _selectedHall;

        public CinemaNS.Entities.Hall SelectedHall
        {
            get
            {
                return _selectedHall;
            }
            set
            {
                _selectedHall = value;
                //if (Model != null)
                //{
                //    Model.Hall = _selectedHall;
                //}
                OnPropertyChanged(nameof(SelectedHall));
            }
        }
    }
}
