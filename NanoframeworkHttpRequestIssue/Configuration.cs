namespace NanoframeworkHttpRequestIssue
{
    internal static class Configuration
    {
        public const string WifiSSID = "IOT";
        public const string WifiPassword = "19761976";

        /// <summary>
        /// A random http (non https!) url that accepts a GET request
        /// </summary>
        public const string NonHttpsUrl = "http://192.168.2.150:5150/";
            // "http://192.168.2.150:5150/weatherforecast";
            // "http://192.168.2.140/api/f3DDsr0fdxWMmorIGWyLOYuvmpHB7PJRBLCefQU7/lights/3/state";
            // "http://192.168.2.136:6789/package-info.json";
            //"http://192.168.2.140/api/f3DDsr0fdxWMmorIGWyLOYuvmpHB7PJRBLCefQU7/lights/3/state";
            //"http://data.companieshouse.gov.uk/doc/company/00001419.json";
    }
}
