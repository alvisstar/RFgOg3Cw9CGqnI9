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
using System.Text;
using System.Windows.Data;
using Telerik.Windows.Controls;
using Microsoft.Phone.Shell;

namespace VietSearchWindowsPhone
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        private List<PlaceViewModel> listPlace = new List<PlaceViewModel>();
        private SearchResultObjectViewModel searchResultObjectViewModel;
        int index;
        int ordinal;
        public readonly DependencyProperty ListVerticalOffsetProperty = DependencyProperty.Register("ListVerticalOffset", typeof(double), typeof(MainPage), new PropertyMetadata(new PropertyChangedCallback(OnListSearchResultVerticalOffsetChanged)));
        //ApplicationBar applicationBar;
        ScrollViewer scrollViewer;
       
        public double ListVerticalOffset
        {
            get { return (double)this.GetValue(ListVerticalOffsetProperty); }
            set { this.SetValue(ListVerticalOffsetProperty, value); }
        }

        public MainPage()
        {
            InitializeComponent();
            searchResultObjectViewModel = new SearchResultObjectViewModel();
            Refresh();
            this.DataContext = searchResultObjectViewModel;
            this.Loaded += new RoutedEventHandler(MainPage_Loaded);
            string uri = App.HANDLEINPUT_URI + "/Get";
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(uri);
            request.BeginGetResponse(new AsyncCallback(GetPlaceCallBack), request);
            borderNumResult.Visibility = Visibility.Collapsed;
            listSearchResult.Loaded += new RoutedEventHandler(listSearchResult_Loaded);

            CreateHomeApplicationBar();

        }

        public void Refresh()
        {
            index = 0;
            ordinal = 1;
            searchResultObjectViewModel.listResultPlace.Clear();
            searchResultObjectViewModel.numResultPlace = 0;
        }

        public void listSearchResult_Loaded(object sender, RoutedEventArgs e)
        {
            FrameworkElement element = (FrameworkElement)sender;
            element.Loaded -= listSearchResult_Loaded;
            scrollViewer = FindChildOfType<ScrollViewer>(element);
            if (scrollViewer == null)
            {
                throw new InvalidOperationException("ScrollViewer not found.");
            }

            Binding binding = new Binding();
            binding.Source = scrollViewer;
            binding.Path = new PropertyPath("VerticalOffset");
            binding.Mode = BindingMode.OneWay;
            this.SetBinding(ListVerticalOffsetProperty, binding);
        }



        private void GetPlaceCallBack(IAsyncResult asynchronousResult)
        {
            
            SearchResultObjectViewModel searchResultObjectTemp = new SearchResultObjectViewModel();
            Deployment.Current.Dispatcher.BeginInvoke(() =>
            {
                try
                {

                    HttpWebRequest httpRequest = (HttpWebRequest)asynchronousResult.AsyncState;
                    WebResponse response = httpRequest.EndGetResponse(asynchronousResult);
                    Stream stream = response.GetResponseStream();
                    
                    DataContractJsonSerializer jsonSerializer = new DataContractJsonSerializer(typeof(SearchResultObjectViewModel));
                    searchResultObjectTemp = (SearchResultObjectViewModel)jsonSerializer.ReadObject(stream);
                    for (int i = 0; i < searchResultObjectTemp.listResultPlace.Count; i++)
                    {
                        searchResultObjectTemp.listResultPlace[i].fullAddress = searchResultObjectTemp.listResultPlace[i].homeNumber + " " + searchResultObjectTemp.listResultPlace[i].street.streetName + ", " + searchResultObjectTemp.listResultPlace[i].district.districtName;
                        searchResultObjectTemp.listResultPlace[i].ordinal = ordinal;
                        searchResultObjectTemp.listResultPlace[i].placeName = searchResultObjectTemp.listResultPlace[i].placeName.Replace("\r", "");
                        searchResultObjectTemp.listResultPlace[i].placeName = searchResultObjectTemp.listResultPlace[i].placeName.Replace("\n", "");
                        searchResultObjectTemp.listResultPlace[i].placeName = searchResultObjectTemp.listResultPlace[i].placeName.ToUpper();
                        searchResultObjectViewModel.listResultPlace.Add(searchResultObjectTemp.listResultPlace[i]);
                        ordinal++;
                    }
                    searchResultObjectViewModel.numResultPlace = searchResultObjectTemp.numResultPlace;
                    tblNumResult.Text = ("Tìm Thấy " + searchResultObjectViewModel.numResultPlace + " Kết Quả!").ToUpper();
                   
                    borderNumResult.Visibility = Visibility.Visible;
                    this.busyIndicator.IsRunning = false;
                    this.busyIndicator1.IsRunning = false;
                    index++;
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



        private void GetAutoCompleteCallBack(IAsyncResult asynchronousResult)
        {
            Deployment.Current.Dispatcher.BeginInvoke(() =>
            {
                try
                {
                    HttpWebRequest httpRequest = (HttpWebRequest)asynchronousResult.AsyncState;
                    WebResponse response = httpRequest.EndGetResponse(asynchronousResult);
                    Stream stream = response.GetResponseStream();

                    DataContractJsonSerializer jsonSerializer = new DataContractJsonSerializer(typeof(List<string>));
                    List<string> listString = (List<string>)jsonSerializer.ReadObject(stream);
                    txtSearch.SuggestionsSource = listString;
                    response.Close();
                }
                catch
                {
                }
            });
        }

       

        private void AutoComplete(object sender, TextChangedEventArgs e)
        {
          
            string uri = App.HANDLEINPUT_URI + "/Get?keyword=" + txtSearch.Text;
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(uri);
            request.BeginGetResponse(new AsyncCallback(GetAutoCompleteCallBack), request);
        }

        private void Search(object sender, EventArgs e)
        {
            Refresh();
            this.busyIndicator.IsRunning = true;
            string uri = App.SERVICE_URI + "/Get?keyword=" + txtSearch.Text + "&&cityId=CI100000049&&index=" + index;
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(uri);
            request.BeginGetResponse(new AsyncCallback(GetPlaceCallBack), request);
        }

        private void txtSearch_GotFocus(object sender, RoutedEventArgs e)
        {
            txtSearch.Background = new SolidColorBrush(Colors.Transparent);
        }

        static T FindChildOfType<T>(DependencyObject root) where T : class
        {
            var queue = new Queue<DependencyObject>();
            queue.Enqueue(root);

            while (queue.Count > 0)
            {
                DependencyObject current = queue.Dequeue();
                for (int i = VisualTreeHelper.GetChildrenCount(current) - 1; 0 <= i; i--)
                {
                    var child = VisualTreeHelper.GetChild(current, i);
                    var typedChild = child as T;
                    if (typedChild != null)
                    {
                        return typedChild;
                    }
                    queue.Enqueue(child);
                }
            }
            return null;
        }
        
        private static void OnListSearchResultVerticalOffsetChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            MainPage page = obj as MainPage;
            ScrollViewer viewer = page.scrollViewer;
            //Checks if the Scroll has reached the last item based on the ScrollableHeight 
            bool atBottom = viewer.VerticalOffset >= viewer.ScrollableHeight;
            if (atBottom)
            {

                page.OnListVerticalOffsetChanged();

            }
        }
        private void OnListVerticalOffsetChanged()
        {
            if (index != 0 && searchResultObjectViewModel.listResultPlace.Count < searchResultObjectViewModel.numResultPlace)
            {
                this.busyIndicator1.IsRunning = true;
                string uri = App.SERVICE_URI + "/Get?keyword=" + txtSearch.Text + "&&cityId=CI100000049&&index=" + index;
                HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(uri);
                request.BeginGetResponse(new AsyncCallback(GetPlaceCallBack), request);
            }

        }

        private void txtSearch_SuggestionSelected(object sender, Telerik.Windows.Controls.SuggestionSelectedEventArgs e)
        {
            try
            {
            }
            catch
            {
            }
        }

        private void Grid_DoubleTap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            try
            {
            }
            catch
            {
            }
        }

        ApplicationBarIconButton homeSearchIconBar;
        private void CreateHomeApplicationBar()
        {
            
            //applicationBar = new ApplicationBar();
            homeSearchIconBar = new ApplicationBarIconButton();
            homeSearchIconBar.IconUri = new Uri("/Toolkit.Content/ApplicationBar.Search.png", UriKind.RelativeOrAbsolute);
            homeSearchIconBar.Text = "Tìm Kiếm";
            ApplicationBar.Buttons.Add(homeSearchIconBar);
            ApplicationBar.IsVisible = true;

           
        }

    }
}