using Cinema.Abstract;
using Cinema.DataAccessLayer;
using Cinema.Extensions;
using Cinema.Factories;
using Cinema.Models;
using Cinema.Views;
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
    public class ChangeHallViewModel : INotifyPropertyChanged
    {
        public Action CloseAction { get; set; }
        public ICommand EventCommand
        {
            get
            {
                return new DelegateCommand((x) => { HandleCommand(); });
            }
        }

        public ICommand AddRowCommand
        {
            get
            {
                return new DelegateCommand((x) => { AddRow(); });
            }
        }
        public ICommand EditRowCommand
        {
            get
            {
                return new DelegateCommand((x) => { EditRow(); });
            }
        }

        private void EditRow()
        {
            if (Model.Id == null)
            {
                var model = new Hall();
                model.Cinema = SelectedCinema;
                model.Number = Model.Number;
                HallManager.Create(model);
                //var cmanager = (CinemaEntityManager)EntityManagersFactory.Instance.GetObject("Cinema");
                //cmanager.AddHall(SelectedCinema.Id, model);
                Model = model.Map<HallModel>();
            }
            // call edit window
            if (SelectedRow != null)
            {
                var rmanager = (RowEntityManager)EntityManagersFactory.Instance.GetObject("Row");
                var m = rmanager.Read(x => x.Id == SelectedRow.Id).FirstOrDefault();

                if(m != null)
                {
                    var wnd = new ChangeRowView(new ChangeRowViewModel(RepositoryEventType.Update, m.Map<RowModel>()));
                    wnd.Show();

                    wnd.Closed += UpdateRowsList;
                }
            }
        }

        private void UpdateRowsList(object? sender, EventArgs e)
        {
            var rmanager = (RowEntityManager)EntityManagersFactory.Instance.GetObject("Row");
            Model.Rows = rmanager.Read(x => x.Hall.Id == Model.Id).ToList();

            if(Model != null && Model.Rows != null)
            {
                RowsList = new ObservableCollection<RowModel>(Model.Rows.Select(x => x.Map<RowModel>()));
            }
        }

        public ICommand DeleteRowCommand
        {
            get
            {
                return new DelegateCommand((x) => { DeleteRow(); });
            }
        }

        private void DeleteRow()
        {
            try
            {
                if (SelectedRow != null)
                {
                    var rowManager = (RowEntityManager)EntityManagersFactory.Instance.GetObject("Row");
                    var toDelete = rowManager.Read(x => x.Id == SelectedRow.Id).FirstOrDefault();
                    rowManager.Delete(toDelete.Id);
                    RowsList.Remove(SelectedRow);
                }
            }
            catch
            {
                MessageBox.Show("Ошибка удаления записи из базы данных (возможно, существуют необорванные связи)", "Ошибка удаления", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK);
            }
        }

        private void AddRow()
        {
            if(Model.Id == null)
            {
                var model = new Hall();
                model.Cinema = SelectedCinema;
                model.Number = Model.Number;
                HallManager.Create(model);
                Model = model.Map<HallModel>();
            }

            var newRow = new Row()
            {
                Number = HallManager.RowsCount(Model.Id) + 1
            };

            HallManager.AddRow(Model.Id, newRow);
            RowsList.Add(newRow.Map<RowModel>());
            //RowsList = new ObservableCollection<RowModel>(rowManager.Read(x => x.Hall.Id == Model.Map<Hall>().Id).Select(x => x.Map<RowModel>()));

            //var wnd = new ChangeRowView(new ChangeRowViewModel(RepositoryEventType.Create));
            //wnd.Show();
        }

        private ObservableCollection<RowModel> _rowsList;
        public ObservableCollection<RowModel> RowsList
        {
            get { return _rowsList; }
            set
            {
                _rowsList = value;
                OnPropertyChanged(nameof(RowsList));
            }
        }

        private RowModel _selectedRow;

        public RowModel SelectedRow
        {
            get { return _selectedRow; }
            set
            {
                _selectedRow = value;
                OnPropertyChanged(nameof(SelectedRow));
            }
        }


        private void HandleCommand()
        {
            try
            {
                switch (EventType)
                {
                    case RepositoryEventType.Create:
                        if(!HallManager.Read(x => x.Id == Model.Id).Any())
                        {
                            HallManager.Create(Model.Map<Hall>());
                        }
                        break;
                    case RepositoryEventType.Update:
                        HallManager.Update(Model.Map<Hall>());
                        break;
                }
            }
            catch (Exception ex) { }
            finally
            {
                CloseAction();
            }
        }

        private HallModel _model;
        public HallModel Model
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

        public HallEntityManager HallManager { get; private set; }
        public RepositoryEventType EventType { get; private set; }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        }

        public void UpdatePlaces()
        {
            var pmanager = (PlaceEntityManager)EntityManagersFactory.Instance.GetObject("Place");
            foreach (var r in Model.Rows)
            {
                r.Places = pmanager.Read(x => x.Row.Id == r.Id).ToList();
            }
        }

        public ChangeHallViewModel(RepositoryEventType eventType, HallModel model = null)
        {
            HallManager = (HallEntityManager)EntityManagersFactory.Instance.GetObject("Hall");
            EventType = eventType;
            Model = model;
            if(Model == null)
            {
                Model = new HallModel();
            }
            else
            {
                SelectedCinema = Model.Cinema;
            }
            //if(eventType == RepositoryEventType.Create)
            //{
            //    var m = new Hall();
            //    var cmanager = (CinemaEntityManager)EntityManagersFactory.Instance.GetObject("Cinema");
            //    m.Cinema = cmanager.ReadAll().FirstOrDefault();
            //    HallManager.Create(m);
            //    Model = m.Map<HallModel>();
            //}

            //UpdatePlaces();

            if (Model.Rows != null)
            {
                RowsList = new ObservableCollection<RowModel>(Model.Rows.Select(x => x.Map<RowModel>()));
            }
            else
            {
                RowsList = new ObservableCollection<RowModel>();
            }
        }

        public ChangeHallViewModel()
        {

        }
        private ObservableCollection<CinemaNS.Entities.Cinema> _cinemas;

        public ObservableCollection<CinemaNS.Entities.Cinema> Cinemas
        {
            get
            {
                return _cinemas;
            }
            set
            {
                _cinemas = value;
                OnPropertyChanged(nameof(Cinemas));
            }
        }

        private CinemaNS.Entities.Cinema _selectedCinema;

        public CinemaNS.Entities.Cinema SelectedCinema
        {
            get
            {
                return _selectedCinema;
            }
            set
            {
                _selectedCinema = value;
                if (Model != null)
                {
                    Model.Cinema = _selectedCinema;
                }
                OnPropertyChanged(nameof(SelectedCinema));
            }
        }
    }
}
