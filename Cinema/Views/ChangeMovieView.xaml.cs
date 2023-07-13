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
    /// Логика взаимодействия для ChangeMovieView.xaml
    /// </summary>
    public partial class ChangeMovieView : Window
    {
        public ChangeMovieView(ChangeMovieViewModel viewModel)
        {
            InitializeComponent();
            this.DataContext = viewModel;
            if(viewModel.CloseAction == null)
            {
                viewModel.CloseAction = new Action(this.Close);
            }

            var countriesManager = (EntityManager<Country>)EntityManagersFactory.Instance.GetObject("Country");
            viewModel.Countries = new ObservableCollection<Country>(countriesManager.ReadAll());

            var genresManager = (EntityManager<Genre>)EntityManagersFactory.Instance.GetObject("Genre");
            viewModel.Genres = new ObservableCollection<Genre>(genresManager.ReadAll());
        }
    }
}
