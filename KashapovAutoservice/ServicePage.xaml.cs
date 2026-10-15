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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace KashapovAutoservice
{
    /// <summary>
    /// Логика взаимодействия для ServicePage.xaml
    /// </summary>
    public partial class ServicePage : Page
    {
        public ServicePage()
        {
            InitializeComponent();
            var currentServices = KashapovAutoserviceEntities.GetContext().Service.ToList();
            ServiceListView.ItemsSource = currentServices;

            ComboType.SelectedIndex = 0;

            UpdateServices();
        }

        private void UpdateServices()
        {
            var currentServices = KashapovAutoserviceEntities.GetContext().Service.ToList();

            switch (ComboType.SelectedIndex)
            {
                case 0:
                    currentServices = currentServices.Where(p => (p.Discount >= 0 && p.Discount <= 1)).ToList();
                    break;
                case 1:
                    currentServices = currentServices.Where(p => (p.Discount >= 0 && p.Discount < 0.05)).ToList();
                    break;
                case 2:
                    currentServices = currentServices.Where(p => (p.Discount >= 0.05 && p.Discount < 0.15)).ToList();
                    break;
                case 3:
                    currentServices = currentServices.Where(p => (p.Discount >= 0.15 && p.Discount < 0.3)).ToList();
                    break;
                case 4:
                    currentServices = currentServices.Where(p => (p.Discount >= 0.3 && p.Discount < 0.7)).ToList();
                    break;
                case 5:
                    currentServices = currentServices.Where(p => (p.Discount >= 0.7 && p.Discount <= 1)).ToList();
                    break;
            }

            currentServices = currentServices.Where(p => p.Title.ToLower().Contains(TBoxSearch.Text.ToLower())).ToList();

            if (RButtonAsc.IsChecked.Value)
            {
                currentServices = currentServices.OrderBy(p => p.Cost).ToList();
            }
            if (RButtonDesc.IsChecked.Value)
            {
                currentServices = currentServices.OrderByDescending(p => p.Cost).ToList();
            }

            ServiceListView.ItemsSource = currentServices;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new AddEditPage());
        }

        private void TBoxSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateServices();
        }

        private void ComboType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateServices();
        }

        private void RButtonAsc_Checked(object sender, RoutedEventArgs e)
        {
            UpdateServices();
        }

        private void RButtonDesc_Checked(object sender, RoutedEventArgs e)
        {
            UpdateServices();
        }
    }
}
