using Cinema.DataAccessLayer;
using Cinema.Extensions;
using Cinema.Factories;
using Cinema.ViewModels;
using CinemaNS.DataAccessLayer;
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
using System.Windows.Shapes;

namespace Cinema.Views
{
    /// <summary>
    /// Логика взаимодействия для ChangeRowView.xaml
    /// </summary>
    public partial class ChangeRowView : Window
    {
        public ChangeRowView(ChangeRowViewModel viewModel)
        {
            InitializeComponent();
            this.DataContext = viewModel;
            if(viewModel.CloseAction == null)
            {
                viewModel.CloseAction = new Action(this.Close);
            }

            var hallManager = (HallEntityManager)EntityManagersFactory.Instance.GetObject("Hall");
            viewModel.SelectedHall = viewModel.Model.Hall;
        }
    }
}
