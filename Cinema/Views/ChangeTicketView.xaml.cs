using Cinema.DataAccessLayer;
using Cinema.Factories;
using Cinema.ViewModels;
using CinemaNS.DataAccessLayer;
using CinemaNS.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Cinema.Views
{
    /// <summary>
    /// Логика взаимодействия для ChangeTicketView.xaml
    /// </summary>
    public partial class ChangeTicketView : Window
    {
        public ChangeTicketView(ChangeTicketViewModel viewModel)
        {
            InitializeComponent();
            this.DataContext = viewModel;
            if (viewModel.CloseAction == null)
            {
                viewModel.CloseAction = new Action(this.Close);
            }
            var sessionManager = (EntityManager<Session>)EntityManagersFactory.Instance.GetObject("Session");

            var ticketManager = (TicketEntityManager)EntityManagersFactory.Instance.GetObject("Ticket");
            var tickets = ticketManager.ReadAll();
            var sessions = sessionManager.ReadAll();
            var freeSessions = sessions.Where(x => tickets.Where(y => y.Session == x).Select(x => x.Place).Count() != x.Hall.Rows.SelectMany(x => x.Places).Count());

            viewModel.AllSessions = new ObservableCollection<Session>(freeSessions);
            if (viewModel.Model.Session != null)
            {
                viewModel.SelectedSession = viewModel.AllSessions.FirstOrDefault(x => x.Id == viewModel.Model.Session.Id);

            }
        }
    }
}
