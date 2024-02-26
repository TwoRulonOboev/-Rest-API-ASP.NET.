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

namespace ClientPharmo
{
    public partial class MainWindow : Window
    {
        private readonly ApiService _apiService;
        private List<Medicine> _medicines;

        public MainWindow()
        {
            InitializeComponent();

            // Создаем экземпляр ApiService для взаимодействия с веб-сервисом
            _apiService = new ApiService();

            // Загружаем список лекарств при запуске приложения
            LoadMedicines();
        }

        private async void LoadMedicines()
        {
            // Получаем список лекарств с помощью метода GetMedicines из ApiService
            _medicines = await _apiService.GetMedicines();

            // Устанавливаем список лекарств в качестве источника данных для элемента управления MedicineList
            MedicineList.ItemsSource = _medicines;
        }

        private async void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            // Получаем выбранное лекарство из списка
            var selectedMedicine = (Medicine)MedicineList.SelectedItem;

            // Проверяем, что лекарство выбрано
            if (selectedMedicine != null)
            {
                // Удаляем выбранное лекарство из списка
                _medicines.Remove(selectedMedicine);

                // Обновляем источник данных для элемента управления MedicineList
                MedicineList.ItemsSource = null;
                MedicineList.ItemsSource = _medicines;
            }
        }

        private async void CheckExpiryButton_Click(object sender, RoutedEventArgs e)
        {
            // Фильтруем список лекарств, оставляя только просроченные
            var expiredMedicines = _medicines.Where(m => m.ExpiryDate < DateTime.Now).ToList();

            // Обновляем источник данных для элемента управления MedicineList, отображая только просроченные лекарства
            MedicineList.ItemsSource = null;
            MedicineList.ItemsSource = expiredMedicines;
        }
    }
}
