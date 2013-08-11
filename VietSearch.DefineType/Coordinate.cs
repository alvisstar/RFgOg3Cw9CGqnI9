using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VietSearch.DefineType
{
    public class Coordinate
    {
        float _Longitude;

        public float Longitude
        {
            get { return _Longitude; }
            set { _Longitude = value; }
        }
        float _Latitude;

        public float Latitude
        {
            get { return _Latitude; }
            set { _Latitude = value; }
        }
    }
}
