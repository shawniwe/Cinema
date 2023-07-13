using Cinema.ViewModels;
using System;
using System.Collections.Generic;
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
    /// Логика взаимодействия для ChangePersonView.xaml
    /// </summary>
    public partial class ChangePersonView : Window
    {
        public ChangePersonView(ChangePersonViewModel viewModel)
        {
            InitializeComponent();
            this.DataContext = viewModel;
            if(viewModel.CloseAction == null)
            {
                viewModel.CloseAction = new Action(this.Close);
            }
        }
    }
}
