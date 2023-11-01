using System;
using System.Diagnostics;
using System.Net.Http;
using System.Net.NetworkInformation;
using System.Threading;
using nanoFramework.Networking;

namespace NanoframeworkHttpRequestIssue
{
    public class Program
    {
        public static void Main()
        {
            ConnectWifi();
            BeginSendHttpRequests();
        }

        private static void BeginSendHttpRequests()
        {
            var httpClient = new HttpClient()
            {
                Timeout = TimeSpan.FromSeconds(5),
            };

            while (true)
            {
                try
                {
                    string response = httpClient.GetString(Configuration.NonHttpsUrl);
                    Debug.WriteLine($"Response: {response}");
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.ToString());
                }

                Thread.Sleep(10000);
            }
        }

        private static void ConnectWifi()
        {
            CancellationTokenSource cs = new(60000);
            bool success = false;
            while (!success)
            {
                success = WifiNetworkHelper.ConnectDhcp(Configuration.WifiSSID, Configuration.WifiPassword, requiresDateTime: true, token: cs.Token);
                if (!success)
                {
                    Debug.WriteLine("Failed to connect to network. Will retry in 5 seconds.");
                    Thread.Sleep(5000);
                }
            }
            Debug.WriteLine($"IP: {NetworkInterface.GetAllNetworkInterfaces()?[0]?.IPv4Address}");
        }
    }
}
