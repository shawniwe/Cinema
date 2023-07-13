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
    /// Логика взаимодействия для ChangeCinemaView.xaml
    /// </summary>
    public partial class ChangeCinemaView : Window
    {
        public ChangeCinemaView(ChangeCinemaViewModel viewModel)
        {
            InitializeComponent();
            this.DataContext = viewModel;
            if (viewModel.CloseAction == null)
            {
                viewModel.CloseAction = new Action(this.Close);
            }
            var districtManager = (EntityManager<District>)EntityManagersFactory.Instance.GetObject("District");
            viewModel.AllDistricts = new ObservableCollection<District>(districtManager.ReadAll());
            if (viewModel.Model.District != null)
            {
                viewModel.SelectedDistrict = viewModel.AllDistricts.FirstOrDefault(x => x.Id == viewModel.Model.District.Id);
            }
        }
    }
}
