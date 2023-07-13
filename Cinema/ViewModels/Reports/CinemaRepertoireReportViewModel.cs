using Cinema.DataAccessLayer;
using Cinema.Factories;
using Cinema.Models;
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

namespace Cinema.ViewModels.Reports
{
    public class CinemaRepertoireReportViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public ICommand ShowReportCommand
        {
            get
            {
                return new DelegateCommand((x) => { GetReportList(); });
            }
        }

        private void GetReportList()
        {
            var sessionManager = (SessionEntityManager)EntityManagersFactory.Instance.GetObject("Session");
            var result = sessionManager.ReadAll();
            if(SelectedCinema != null)
            {
                result = result.Where(x => x.Hall != null && x.Hall.Cinema == SelectedCinema).ToList();
            }

            if(StartDate.HasValue)
            {
                result = result.Where(x => x.StartDate >= StartDate.Value);
            }

            if(EndDate.HasValue)
            {
                result = result.Where(x => x.StartDate <= EndDate);
            }

            ReportList.Clear();
            foreach (var r in result)
            {
                ReportList.Add(r);
            }
            
        }

        public CinemaRepertoireReportViewModel()
        {
        }

        private Session _selectedItem;

        public Session SelectedItem
        {
            get { return _selectedItem; }
            set { _selectedItem = value; OnPropertyChanged(nameof(SelectedItem)); }
        }


        private ObservableCollection<Session> _reportList;
        public ObservableCollection<Session> ReportList
        {
            get
            {
                return _reportList;
            }
            set
            {
                _reportList = value;
                OnPropertyChanged(nameof(ReportList));
            }
        }

        private DateTime? _startDate;

        public DateTime? StartDate
        {
            get { return _startDate; }
            set { _startDate = value; OnPropertyChanged(nameof(StartDate)); }
        }

        private DateTime? _endDate;

        public DateTime? EndDate
        {
            get { return _endDate; }
            set { _endDate = value; OnPropertyChanged(nameof(EndDate)); }
        }


        private ObservableCollection<CinemaNS.Entities.Cinema> _cinemas;

        public ObservableCollection<CinemaNS.Entities.Cinema> Cinemas
        {
            get { return _cinemas; }
            set { _cinemas = value; OnPropertyChanged(nameof(Cinemas)); }
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
                OnPropertyChanged(nameof(SelectedCinema));
            }
        }
    }
}
