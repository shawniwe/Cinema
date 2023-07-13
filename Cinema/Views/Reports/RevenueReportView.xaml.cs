using Cinema.DataAccessLayer;
using Cinema.Factories;
using Cinema.Models;
using Cinema.ViewModels.Reports;
using CinemaNS.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Cinema.Views.Reports
{
    /// <summary>
    /// Логика взаимодействия для RevenueReportView.xaml
    /// </summary>
    public partial class RevenueReportView : Page
    {
        public RevenueReportView()
        {
            InitializeComponent();

            var model = new RevenueReportViewModel();
            this.DataContext = model;

            var cinemaManager = (CinemaEntityManager)EntityManagersFactory.Instance.GetObject("Cinema");
            model.Cinemas = new ObservableCollection<CinemaNS.Entities.Cinema>(cinemaManager.ReadAll());
            model.ReportList = new ObservableCollection<RevenueModel>();
        }
    }
}
