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
using System.Text.RegularExpressions;

namespace VietSearchWindowsPhone.Tools
{
    public static class UriToolKits
    {
        private static readonly Regex QueryStringRegex = new Regex(@"[\?&](?<name>[^&=]+)=(?<value>[^&=]+)");

        public static IEnumerable<KeyValuePair<string, string>> ParseQueryString(this string uri)
        {
            if (uri == null)
                throw new ArgumentException("uri");

            var matches = QueryStringRegex.Matches(uri);
            for (var i = 0; i < matches.Count; i++)
            {
                var match = matches[i];
                yield return new KeyValuePair<string, string>(match.Groups["name"].Value, match.Groups["value"].Value);
            }
        }
    }
}
