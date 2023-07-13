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
    /// Логика взаимодействия для ChangeHallView.xaml
    /// </summary>
    public partial class ChangeHallView : Window
    {
        public ChangeHallView(ChangeHallViewModel viewModel)
        {
            InitializeComponent();
            this.DataContext = viewModel;
            if(viewModel.CloseAction == null)
            {
                viewModel.CloseAction = new Action(this.Close);
            }

            var cinemaManager = (EntityManager<CinemaNS.Entities.Cinema>)EntityManagersFactory.Instance.GetObject("Cinema");
            viewModel.Cinemas = new ObservableCollection<CinemaNS.Entities.Cinema>(cinemaManager.ReadAll());
            //if (viewModel.Model != null && viewModel.Model.Cinema != null)
            //{
            //    viewModel.SelectedCinema = viewModel.Cinemas.FirstOrDefault(x => x.Id == viewModel.Model.Cinema.Id);
            //}
        }
    }
}
