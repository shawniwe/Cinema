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
    /// Логика взаимодействия для ChangeSessionView.xaml
    /// </summary>
    public partial class ChangeSessionView : Window
    {
        public ChangeSessionView(ChangeSessionViewModel viewModel)
        {
            InitializeComponent();
            this.DataContext = viewModel;
            if (viewModel.CloseAction == null)
            {
                viewModel.CloseAction = new Action(this.Close);
            }
            var hallManager = (EntityManager<Hall>)EntityManagersFactory.Instance.GetObject("Hall");
            viewModel.AllHalls = new ObservableCollection<Hall>(hallManager.ReadAll());
            if (viewModel.Model.Hall != null)
            {
                viewModel.SelectedHall = viewModel.AllHalls.FirstOrDefault(x => x.Id == viewModel.Model.Hall.Id);
            }

            var movieManager = (EntityManager<Movie>)EntityManagersFactory.Instance.GetObject("Movie");
            viewModel.AllMovies = new ObservableCollection<Movie>(movieManager.ReadAll());
            if (viewModel.Model.Movie != null)
            {
                viewModel.SelectedMovie = viewModel.AllMovies.FirstOrDefault(x => x.Id == viewModel.Model.Movie.Id);
            }
        }
    }
}
