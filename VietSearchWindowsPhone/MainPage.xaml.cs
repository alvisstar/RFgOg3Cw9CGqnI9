using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using VietSearchWindowsPhone.ViewModels;
using System.IO;
using System.Runtime.Serialization.Json;

namespace VietSearchWindowsPhone
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        private List<PlaceViewModel> listPlace = new List<PlaceViewModel>();
        public MainPage()
        {
            InitializeComponent();
            this.DataContext = listPlace;
            // Set the data context of the listbox control to the sample data
            
            this.Loaded += new RoutedEventHandler(MainPage_Loaded);

            //PlaceViewModel place = new PlaceViewModel {homeNumber ="12", placeName = "Cafe Windows 4".ToUpper(), rate = 4, ordinal = 1 };
            //place.placeType = new PlaceTypeModel { placeTypeName = "Cafe" };
            //place.street = new StreetViewModel { streetName = "Alexander Rosh" };
            //place.district = new DistrictViewModel { districtName = "Quận 1" };
            //place.city = new CityViewModel { cityName = "Hồ Chí Minh" };
            //place.fullAddress = place.homeNumber + " " + place.street.streetName + ", " + place.district.districtName;
            //listPlace.Add(place);
            //place = new PlaceViewModel { homeNumber = "4", placeName = "Cafe Đá", rate = 5, ordinal = 2 };
            //place.placeType = new PlaceTypeModel { placeTypeName = "Cafe" };
            //place.street = new StreetViewModel { streetName = "Alexander Rosh" };
            //place.district = new DistrictViewModel { districtName = "Quận 1" };
            //place.city = new CityViewModel { cityName = "Hồ Chí Minh" };
            //place.fullAddress = place.homeNumber + " " + place.street.streetName + ", " + place.district.districtName;
            //listPlace.Add(place);

            
            
            
        }

        private void GetPlaceCallBack(IAsyncResult asynchronousResult)
        {
            String strResponse = "";

            Deployment.Current.Dispatcher.BeginInvoke(() =>
            {
                try
                {

                    HttpWebRequest httpRequest = (HttpWebRequest)asynchronousResult.AsyncState;
                    WebResponse response = httpRequest.EndGetResponse(asynchronousResult);
                    Stream stream = response.GetResponseStream();
                    StreamReader reader = new StreamReader(stream);
                    strResponse = reader.ReadToEnd();
                    DataContractJsonSerializer jsonSerializer = new DataContractJsonSerializer(typeof(List<PlaceViewModel>));
                    listPlace = (List<PlaceViewModel>)jsonSerializer.ReadObject(stream);
                   
                 
                    listSearchResult.ItemsSource = listPlace;
                }
                catch
                {
                   
                }
                finally
                {
                    GC.Collect();

                }
               
            });
        }

       
          

       
        // Load data for the ViewModel Items
        private void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            if (!App.ViewModel.IsDataLoaded)
            {
                App.ViewModel.LoadData();
            }
        }

       

       
       

        private void Search(object sender, RoutedEventArgs e)
        {
            string uri = App.SERVICE_URI + "/Get?keyword=" + txtSearch.Text + "&&cityId=CI100000049";
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(uri);
            request.ContentType = "application/json";
            request.Accept = "application/json";
            request.Method = "GET";
            
            request.BeginGetResponse(new AsyncCallback(GetPlaceCallBack), request);
        }
        

        
        
       
        
    }
}