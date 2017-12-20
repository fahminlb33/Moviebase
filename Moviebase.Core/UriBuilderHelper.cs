﻿using System.Collections.Specialized;
using System.Text;

namespace Moviebase.Core
{
    public class UriBuilderHelper
    {
        private readonly string _endpoint;
        private readonly string _apiKey;

        public UriBuilderHelper(string endpoint, string apikey)
        {
            _endpoint = endpoint;
            _apiKey = apikey;
        }

        public string BuildUri(string path, NameValueCollection col)
        {
            var sb = new StringBuilder();
            sb.Append(_endpoint);
            sb.Append(path);
            sb.Append("?api_key=");
            sb.Append(_apiKey);

            if (col == null) return sb.ToString();
            for (int i = 0; i < col.Count; i++)
            {
                var key = col.GetKey(i);
                sb.AppendFormat("&{0}={1}", key, col.Get(key));
            }

            return sb.ToString();
        }
    }
}
