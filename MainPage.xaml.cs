using System.Collections.ObjectModel;


namespace miniZadanie6
{
    public partial class MainPage : ContentPage
    {
        public Command RefreshCommand { get; set; }

        public bool IsRefreshing { get; set; }


        static List<City> city = new List<City>();

        public ObservableCollection<City> OriginalItems { get; set; } = new ObservableCollection<City>();
        public MainPage()
        {
            InitializeComponent();

            city.Add(new City { id = 0, Location = "Bratislava", CheckIn = DateTime.Now.AddDays(-10), CheckOut = DateTime.Now.AddDays(-9) });
            city.Add(new City { id = 1, Location = "Los Angeles", CheckIn = DateTime.Now.AddDays(-20), CheckOut = DateTime.Now.AddDays(-10) });
            city.Add(new City { id = 2, Location = "Vienna", CheckIn = DateTime.Now.AddDays(-15), CheckOut = DateTime.Now.AddDays(-11) });
            city.Add(new City { id = 3, Location = "Paris", CheckIn = DateTime.Now.AddDays(-20), CheckOut = DateTime.Now.AddDays(-15) });
            city.Add(new City { id = 4, Location = "Prague", CheckIn = DateTime.Now.AddDays(-30), CheckOut = DateTime.Now.AddDays(-28) });
            city.Add(new City { id = 5, Location = "Seoul", CheckIn = DateTime.Now.AddDays(-30), CheckOut = DateTime.Now.AddDays(-17) });
            city.Add(new City { id = 6, Location = "Berlin", CheckIn = DateTime.Now.AddDays(-35), CheckOut = DateTime.Now.AddDays(-20) });

            OriginalItems = new ObservableCollection<City>(city);
            CityList.ItemsSource = OriginalItems;

            RefreshCommand = new Command(() => {
                refre.IsRefreshing = true;
                OriginalItems = new ObservableCollection<City>(city);
                CityList.ItemsSource = OriginalItems;
                refre.IsRefreshing = false;
            });
            BindingContext = this;
        }

        //Vyhladavanie v zozname
        private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            var searchTerm = e.NewTextValue;
            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                OriginalItems = new ObservableCollection<City>(city);
            }
            else
            {
                OriginalItems = new ObservableCollection<City>(
                    city.Where(item => item.Location.Contains(e.NewTextValue, StringComparison.OrdinalIgnoreCase)));
            }
            CityList.ItemsSource = OriginalItems;
        }
        //Vymazanie po kliknuti
        private void CityList_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            OriginalItems.Remove((City)e.SelectedItem);
          
         
        }
    }

    public class City {
        public int id { get; set; }
        public string Location { get; set; }
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
    
    }
}
