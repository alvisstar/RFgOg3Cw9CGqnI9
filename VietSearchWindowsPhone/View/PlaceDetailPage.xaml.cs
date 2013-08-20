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
using Facebook.Client;
using VietSearchWindowsPhone.FacebookUtility;
namespace VietSearchWindowsPhone.View
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

        private void btnComment_Click(object sender, RoutedEventArgs e)
        {
            if ("".Equals(FacebookClientHelper.Instance.accessToken))
            {
                MessageBoxResult result = MessageBox.Show("Xác Nhận Đăng Nhập Facebook", "", MessageBoxButton.OKCancel);

                if (result == MessageBoxResult.OK)
                {
                    NavigationService.Navigate(new Uri("/View/FacebookLoginPage.xaml", UriKind.Relative));
                }

            }
            else
            {
                CommentViewModel commentViewModel = new CommentViewModel();
                commentViewModel.accountId = FacebookClientHelper.Instance.userId;
                commentViewModel.placeId = placeViewModel.placeId;
                commentViewModel.commentContent = txtComment.Text;
                commentViewModel.isLock = false;
                MemoryStream memoryStream = new MemoryStream();
                DataContractJsonSerializer jsonSer =
                new DataContractJsonSerializer(typeof(CommentViewModel));
                jsonSer.WriteObject(memoryStream, commentViewModel);
                memoryStream.Position = 0;
                StreamReader sr = new StreamReader(memoryStream);
                var json = sr.ReadToEnd();
                var webClient = new WebClient();
                webClient.Headers[HttpRequestHeader.ContentType] = "application/json";
                webClient.UploadStringCompleted += this.CompleteComment;
                webClient.UploadStringAsync(new Uri(App.COMMENT_URI), "POST", json);
               
            }


        }
        private void CompleteComment(object sender, UploadStringCompletedEventArgs e)
        {

            MessageBox.Show("Bình Luận Thành Công");
        }
       
    }
}