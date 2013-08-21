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
using Telerik.Windows.Controls;
using Microsoft.Phone.Controls.Maps;
using System.Device.Location;
namespace VietSearchWindowsPhone.View
{
    public partial class PlaceDetailPage : PhoneApplicationPage
    {
       
        PlaceViewModel placeViewModel;
        List<CommentViewModel> listComment = new List<CommentViewModel>();
        private double ZOOM_LEVEL = 17;
        
        public PlaceDetailPage()
        {
            InitializeComponent();
            placeViewModel = new PlaceViewModel();
            map.CredentialsProvider = new ApplicationIdCredentialsProvider { 
                ApplicationId = "AguRAMwQEtd4D9lck2K2gyyqKfU_ZKFvInqzChc5nYJiA8-e-7JzgGKPuoqalqco"
            };
        }

        public void InitInfo()
        {
            txtPlaceNameInfo.Text = placeViewModel.placeName;
            txtPhoneInfo.Text = placeViewModel.phone;
            txtPlaceTypeInfo.Text = placeViewModel.placeType.placeTypeName;
            txtHomeNumberInfo.Text = placeViewModel.homeNumber;
            txtStreetInfo.Text = placeViewModel.street.streetName;
            txtDistrictInfo.Text = placeViewModel.district.districtName;
            txtCityInfo.Text = placeViewModel.city.cityName;
            Pushpin locationPushpin = new Pushpin();
            locationPushpin.Content = placeViewModel.placeName;
            GeoCoordinate location = new GeoCoordinate(placeViewModel.latitude, placeViewModel.longitude);
            locationPushpin.Location = location;
            map.Children.Add(locationPushpin);
            map.SetView(location, ZOOM_LEVEL);
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

        public void LoadListComment()
        {
            string uri = App.COMMENT_URI + "/Get?placeId=" + placeViewModel.placeId + "&&parameter=place";
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(uri);
            request.BeginGetResponse(new AsyncCallback(GetListCommentCallBack), request);
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
                    LoadListComment();
                    InitInfo();

                    response.Close();
                }
                catch
                {
                }
            });
        }
        private void GetListCommentCallBack(IAsyncResult asynchronousResult)
        {
            Deployment.Current.Dispatcher.BeginInvoke(() =>
            {
                try
                {
                    HttpWebRequest httpRequest = (HttpWebRequest)asynchronousResult.AsyncState;
                    WebResponse response = httpRequest.EndGetResponse(asynchronousResult);
                    Stream stream = response.GetResponseStream();

                    DataContractJsonSerializer jsonSerializer = new DataContractJsonSerializer(typeof(List<CommentViewModel>));
                    placeViewModel.listComment = (List<CommentViewModel>)jsonSerializer.ReadObject(stream);

                    ratingPlace.Value = placeViewModel.rating;
                    listCommentContent.ItemsSource = placeViewModel.listComment;

                    response.Close();
                }
                catch
                {
                }
            });
        }

       
        private void CompleteComment(object sender, UploadStringCompletedEventArgs e)
        {         
            txtComment.Text = "";
            LoadListComment();
            actionBusyIndicator.IsRunning = false;
            MessageBox.Show("Bình Luận Thành Công");
        }

        private void txtComment_ActionButtonTap(object sender, EventArgs e)
        {

            ProcessComment();
        }

        private void RatingPlace(object sender, System.Windows.Input.GestureEventArgs e)
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
                 MessageBoxResult result = MessageBox.Show("Xác nhận đánh giá địa điểm", "", MessageBoxButton.OKCancel);

                 if (result == MessageBoxResult.OK)
                 {
                     actionBusyIndicator.IsRunning = true;
                     RateViewModel rateViewModel = new RateViewModel();
                     rateViewModel.placeId = placeViewModel.placeId;
                     rateViewModel.accountId = FacebookClientHelper.Instance.userId;
                     rateViewModel.mark = ratingPlace.Value;
                     rateViewModel.isLock = false;
                     MemoryStream memoryStream = new MemoryStream();
                     DataContractJsonSerializer jsonSer =
                     new DataContractJsonSerializer(typeof(RateViewModel));
                     jsonSer.WriteObject(memoryStream, rateViewModel);
                     memoryStream.Position = 0;
                     StreamReader sr = new StreamReader(memoryStream);
                     var json = sr.ReadToEnd();
                     var webClient = new WebClient();
                     webClient.Headers[HttpRequestHeader.ContentType] = "application/json";
                     webClient.UploadStringCompleted += this.CompleteRating;
                     webClient.UploadStringAsync(new Uri(App.RATING_URI), "POST", json);
                 }
            }

        }

        private void CompleteRating(object sender, UploadStringCompletedEventArgs e)
        {
            actionBusyIndicator.IsRunning = false;
            MessageBox.Show("Đánh Giá Thành Công");
        }

        private void txtComment_GotFocus(object sender, RoutedEventArgs e)
        {
            txtComment.Background = new SolidColorBrush(Colors.Transparent);
        }

        private void txtComment_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key.Equals(Key.Enter))
            {
                ProcessComment();
            }
        }

        public void ProcessComment()
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
                if (txtComment.Text.CompareTo("") != 0)
                {
                    actionBusyIndicator.IsRunning = true;
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
                else
                {
                    MessageBox.Show("Xin vui lòng nhập nội dung");
                }

            }
        }

       
    }
}