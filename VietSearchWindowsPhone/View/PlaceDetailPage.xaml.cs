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
using System.Text;
using VietSearchWindowsPhone.Tools;
using Microsoft.Phone.Controls.Maps.Platform;
namespace VietSearchWindowsPhone.View
{
    public partial class PlaceDetailPage : PhoneApplicationPage
    {
        string statusFacebook = "";

        const string APP_ID = "AguRAMwQEtd4D9lck2K2gyyqKfU_ZKFvInqzChc5nYJiA8-e-7JzgGKPuoqalqco";
        PlaceViewModel placeViewModel;
        List<CommentViewModel> listComment = new List<CommentViewModel>();
        private double ZOOM_LEVEL = 15;


        public PlaceDetailPage()
        {
            InitializeComponent();
            placeViewModel = new PlaceViewModel();
            map.CredentialsProvider = new ApplicationIdCredentialsProvider
            {
                ApplicationId = APP_ID
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

            //Add marker to select place on the map
            Pushpin serviceLocationPushpin = new Pushpin();
            serviceLocationPushpin.Content = placeViewModel.placeName;
            GeoCoordinate serviceLocation = new GeoCoordinate(placeViewModel.latitude, placeViewModel.longitude);
            serviceLocationPushpin.Location = serviceLocation;
            map.Children.Add(serviceLocationPushpin);
            map.SetView(serviceLocation, ZOOM_LEVEL);

            GeoCoordinateWatcher gpsWatcher = new GeoCoordinateWatcher();
            //Default place if cannot get from gps
            double latitude = 10.8515;
            double longitude = 106.7513;

            var currentLocation = gpsWatcher.Position;
            if (!currentLocation.Location.IsUnknown)
            {
                latitude = currentLocation.Location.Latitude;
                longitude = currentLocation.Location.Longitude;
            }

            //Add marker to your current gps position on the map
            Pushpin currentPositionPushin = new Pushpin();
            currentPositionPushin.Content = "Your Position";
            GeoCoordinate currentPosition = new GeoCoordinate(latitude, longitude);
            currentPositionPushin.Location = currentPosition;
            map.Children.Add(currentPositionPushin);

            //Calculate Route
            routeservice.RouteServiceClient routeServiceClient = new routeservice.RouteServiceClient("BasicHttpBinding_IRouteService");
            routeServiceClient.CalculateRouteCompleted += new EventHandler<routeservice.CalculateRouteCompletedEventArgs>(routeService_CalculateRouteCompledted);
            routeservice.RouteRequest routeRequest = new routeservice.RouteRequest();
            routeRequest.Credentials = new Credentials();
            routeRequest.Credentials.ApplicationId = APP_ID;
            routeRequest.Options = new routeservice.RouteOptions();
            routeRequest.Options.RoutePathType = routeservice.RoutePathType.Points;
            routeRequest.Waypoints = new System.Collections.ObjectModel.ObservableCollection<routeservice.Waypoint>();

            routeservice.Waypoint currentWayPoint = new routeservice.Waypoint();
            currentWayPoint.Description = "Your position";
            currentWayPoint.Location = new Location();
            currentWayPoint.Location.Latitude = latitude;
            currentWayPoint.Location.Longitude = longitude;

            routeservice.Waypoint destWayPoint = new routeservice.Waypoint();
            destWayPoint.Description = placeViewModel.placeName;
            destWayPoint.Location = new Location();
            destWayPoint.Location.Latitude = placeViewModel.latitude;
            destWayPoint.Location.Longitude = placeViewModel.longitude;

            routeRequest.Waypoints.Add(currentWayPoint);
            routeRequest.Waypoints.Add(destWayPoint);
            routeServiceClient.CalculateRouteAsync(routeRequest);
        }

        void routeService_CalculateRouteCompledted(object sender, routeservice.CalculateRouteCompletedEventArgs e)
        {
            // If the route calculate was a success and contains a route, then draw the route on the map.
            if ((e.Result.ResponseSummary.StatusCode == routeservice.ResponseStatusCode.Success) & (e.Result.Result.Legs.Count != 0))
            {
                // Set properties of the route line you want to draw.
                Color routeColor = Colors.Blue;
                SolidColorBrush routeBrush = new SolidColorBrush(routeColor);
                MapPolyline routeLine = new MapPolyline();
                routeLine.Locations = new LocationCollection();
                routeLine.Stroke = routeBrush;
                routeLine.Opacity = 0.50;
                routeLine.StrokeThickness = 5.0;
                // Retrieve the route points that define the shape of the route.
                foreach (Location p in e.Result.Result.RoutePath.Points)
                {
                    routeLine.Locations.Add(new Location { Latitude = p.Latitude, Longitude = p.Longitude });
                }
                // Add a map layer in which to draw the route.
                MapLayer myRouteLayer = new MapLayer();
                map.Children.Add(myRouteLayer);
                // Add the route line to the new layer.
                myRouteLayer.Children.Add(routeLine);
                // Figure the rectangle which encompasses the route. This is used later to set the map view.
                /*double centerlatitude = (routeLine.Locations[0].Latitude + routeLine.Locations[routeLine.Locations.Count - 1].Latitude) / 2;
                double centerlongitude = (routeLine.Locations[0].Longitude + routeLine.Locations[routeLine.Locations.Count - 1].Longitude) / 2;
                Location centerloc = new Location();
                centerloc.Latitude = centerlatitude;
                centerloc.Longitude = centerlongitude;
                double north, south, east, west;
                if ((routeLine.Locations[0].Latitude > 0) && (routeLine.Locations[routeLine.Locations.Count - 1].Latitude > 0))
                {
                    north = routeLine.Locations[0].Latitude > routeLine.Locations[routeLine.Locations.Count - 1].Latitude ? routeLine.Locations[0].Latitude : routeLine.Locations[routeLine.Locations.Count - 1].Latitude;
                    south = routeLine.Locations[0].Latitude < routeLine.Locations[routeLine.Locations.Count - 1].Latitude ? routeLine.Locations[0].Latitude : routeLine.Locations[routeLine.Locations.Count - 1].Latitude;
                }
                else
                {
                    north = routeLine.Locations[0].Latitude < routeLine.Locations[routeLine.Locations.Count - 1].Latitude ? routeLine.Locations[0].Latitude : routeLine.Locations[routeLine.Locations.Count - 1].Latitude;
                    south = routeLine.Locations[0].Latitude > routeLine.Locations[routeLine.Locations.Count - 1].Latitude ? routeLine.Locations[0].Latitude : routeLine.Locations[routeLine.Locations.Count - 1].Latitude;
                }
                if ((routeLine.Locations[0].Longitude < 0) && (routeLine.Locations[routeLine.Locations.Count - 1].Longitude < 0))
                {
                    west = routeLine.Locations[0].Longitude < routeLine.Locations[routeLine.Locations.Count - 1].Longitude ? routeLine.Locations[0].Longitude : routeLine.Locations[routeLine.Locations.Count - 1].Longitude;
                    east = routeLine.Locations[0].Longitude > routeLine.Locations[routeLine.Locations.Count - 1].Longitude ? routeLine.Locations[0].Longitude : routeLine.Locations[routeLine.Locations.Count - 1].Longitude;
                }
                else
                {
                    west = routeLine.Locations[0].Longitude > routeLine.Locations[routeLine.Locations.Count - 1].Longitude ? routeLine.Locations[0].Longitude : routeLine.Locations[routeLine.Locations.Count - 1].Longitude;
                    east = routeLine.Locations[0].Longitude < routeLine.Locations[routeLine.Locations.Count - 1].Longitude ? routeLine.Locations[0].Longitude : routeLine.Locations[routeLine.Locations.Count - 1].Longitude;
                }
                 * */
            }
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
                    placeViewModel = (PlaceViewModel)jsonSerializer.ReadObject(stream);
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


        private void checkin_Click(object sender, EventArgs e)
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
                Style statusTextBoxStyle = new Style(typeof(RadTextBox));
                //statusTextBoxStyle.Setters.Add(new Setter(RadTextBox.InputScopeProperty,""));
                string statusTitle = "Nhập trạng thái";
                string status = "Vui lòng nhập trạng thái";
                RadInputPrompt.Show(statusTitle, MessageBoxButtons.OKCancel, status, InputMode.Text, statusTextBoxStyle, closedHandler: (closedArgs) =>
                {
                    if (closedArgs.Result == DialogResult.OK)
                    {

                       // FacebookClientHelper.Instance.getShop();
                        statusFacebook = closedArgs.Text +"__ tại "+ placeViewModel.homeNumber + " " + placeViewModel.street.streetName + ", " + placeViewModel.district.districtName +", " + placeViewModel.city.cityName;
                        FacebookClientHelper.Instance.PostMessageOnWall(statusFacebook, new UploadStringCompletedEventHandler(PostMessageOnWallCompleted));

                    }
                    else
                    {

                    }

                });
            }
        }


        void PostMessageOnWallCompleted(object sender, UploadStringCompletedEventArgs e)
        {
            if (e.Cancelled)
                return;
            if (e.Error != null)
            {
                MessageBox.Show("Error Occurred: " + e.Error.Message);
                return;
            }

            System.Diagnostics.Debug.WriteLine(e.Result);

            string result = e.Result;
            byte[] data = Encoding.UTF8.GetBytes(result);
            MemoryStream memStream = new MemoryStream(data);
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(ResponseData));
            ResponseData responseData = (ResponseData)serializer.ReadObject(memStream);

            if (responseData.id != null && !responseData.id.Equals(""))
            {
                // Success
                MessageBox.Show("Cập nhật trạng thái thành công!");
            }
            else if (responseData.error != null && responseData.error.code == 190)
            {
                if (responseData.error.error_subcode == 463)
                {
                    // Access Token Expired, need to get new token
                    FacebookClientHelper.Instance.ExchangeAccessToken(new UploadStringCompletedEventHandler(ExchangeAccessTokenCompleted));
                }
                else
                {
                    // Another Error with Access Token, need to clear the Access Token
                    FacebookClientHelper.Instance.accessToken = "";

                }
            }
            else
            {
                // Error
            }
        }
        void ExchangeAccessTokenCompleted(object sender, UploadStringCompletedEventArgs e)
        {
            // Acquire access_token and expires timestamp
            IEnumerable<KeyValuePair<string, string>> pairs = UriToolKits.ParseQueryString(e.Result);
            string accessToken = KeyValuePairUtils.GetValue(pairs, "access_token");

            if (accessToken != null && !accessToken.Equals(""))
            {
                MessageBox.Show("Access Token Exchange Failed");
                return;
            }

            // Save access_token
            FacebookClientHelper.Instance.accessToken = accessToken;
            FacebookClientHelper.Instance.PostMessageOnWall(statusFacebook, new UploadStringCompletedEventHandler(PostMessageOnWallCompleted));
        }

   	private void buttonZoomIn_Click(object sender, RoutedEventArgs e)
        {
            map.ZoomLevel++;
        }

        private void buttonZoomOut_Click(object sender, RoutedEventArgs e)
        {
            map.ZoomLevel--;
        }


    }
}