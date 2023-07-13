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
    public class RevenueReportViewModel : INotifyPropertyChanged
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
            var ticketManager = (TicketEntityManager)EntityManagersFactory.Instance.GetObject("Ticket");
            var result = ticketManager.ReadAll();

            if (SelectedCinema != null)
            {
                result = result.Where(x => x.Session != null && x.Session.Hall != null && x.Session.Hall.Cinema == SelectedCinema);
            }

            if (StartDate.HasValue)
            {
                result = result.Where(x => x.Session.StartDate >= StartDate.Value);
            }

            if (EndDate.HasValue)
            {
                result = result.Where(x => x.Session.StartDate <= EndDate);
            }

            var cinemas = result.Select(x => x.Session.Hall.Cinema).Distinct();

            ReportList.Clear();
            foreach (var c in cinemas)
            {
                ReportList.Add(new RevenueModel()
                {
                    Cinema = c,
                    Revenue = result.Where(x => x.Session.Hall.Cinema == c).Sum(x => x.Session.Cost)
                });
            }
        }

        public RevenueReportViewModel()
        {
        }

        private Session _selectedItem;

        public Session SelectedItem
        {
            get { return _selectedItem; }
            set { _selectedItem = value; OnPropertyChanged(nameof(SelectedItem)); }
        }


        private ObservableCollection<RevenueModel> _reportList;
        public ObservableCollection<RevenueModel> ReportList
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
