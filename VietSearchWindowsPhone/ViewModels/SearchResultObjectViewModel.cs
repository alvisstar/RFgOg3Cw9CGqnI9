using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace VietSearchWindowsPhone.ViewModels
{
    public class SearchResultObjectViewModel
    {
        public SearchResultObjectViewModel()
        {
            numResultPlace = 0;
            listResultPlace = new ObservableCollection<PlaceViewModel>();
        }
        int _numResultPlace;

        public int numResultPlace
        {
            get { return _numResultPlace; }
            set { _numResultPlace = value; }
        }

        
        ObservableCollection<PlaceViewModel> _listResultPlace;

        public ObservableCollection<PlaceViewModel> listResultPlace
        {
            get { return _listResultPlace; }
            set { _listResultPlace = value; }
        }

    }
}
