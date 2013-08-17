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
using System.Runtime.Serialization.Json;
using System.IO;

namespace VietSearchWindowsPhone
{
    public partial class PlaceDetailPage : PhoneApplicationPage
    {
        PlaceViewModel placeViewModel;
        public PlaceDetailPage()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            string placeId = "";

            if (NavigationContext.QueryString.TryGetValue("placeId", out placeId))
            {
                placeViewModel = new PlaceViewModel();
                string uri = App.PLACE_URI + "/Get?placeId=" + placeId;
                HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(uri);
                request.BeginGetResponse(new AsyncCallback(GetPlaceDetailCallBack), request);
            }
        }


        private void GetPlaceDetailCallBack(IAsyncResult asynchronousResult)
        {
            Deployment.Current.Dispatcher.BeginInvoke(() =>
            {
                try
                {
                    HttpWebRequest httpRequest = (HttpWebRequest)asynchronousResult.AsyncState;
                    WebResponse response = httpRequest.EndGetResponse(asynchronousResult);
                    Stream stream = response.GetResponseStream();

                    DataContractJsonSerializer jsonSerializer = new DataContractJsonSerializer(typeof(PlaceViewModel));
                    placeViewModel  = (PlaceViewModel)jsonSerializer.ReadObject(stream);
                    
                    response.Close();
                }
                catch
                {
                }
            });
        }

       
    }
}