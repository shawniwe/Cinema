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
    /// Логика взаимодействия для ChangeMovieActorView.xaml
    /// </summary>
    public partial class ChangeMovieActorView : Window
    {
        public ChangeMovieActorView(ChangeMovieActorViewModel viewModel)
        {
            InitializeComponent();
            this.DataContext = viewModel;
            if(viewModel.CloseAction == null)
            {
                viewModel.CloseAction = new Action(this.Close);
            }

            var pmanager = (EntityManager<Person>)EntityManagersFactory.Instance.GetObject("Person");
            viewModel.Persons = new ObservableCollection<Person>(pmanager.ReadAll());
            if(viewModel.Model != null)
            {
                viewModel.SelectedPerson = viewModel.Model.Actor;
            }
        }
    }
}
