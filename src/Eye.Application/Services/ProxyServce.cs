using Eye.Application.Services.Interface;
using Eye.Contract.Share.Models;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Eye.Application.Services
{
    public class ProxyServce: IProxyServce
    {
        public ProxyServce()
        {

        }

        public async Task<bool> IsProxyWorking(string proxyAddress, string proxyUsername, string proxyPassword, string testUrl)
        {
            try
            {
                // Create proxy
                var proxy = new WebProxy(proxyAddress, true);

                // If credentials are provided, set them
                if (!string.IsNullOrEmpty(proxyUsername) && !string.IsNullOrEmpty(proxyPassword))
                {
                    proxy.Credentials = new NetworkCredential(proxyUsername, proxyPassword);
                }

                // Setup HttpClientHandler to use the proxy
                var httpClientHandler = new HttpClientHandler
                {
                    Proxy = proxy,
                    UseProxy = true
                };

                // Create HttpClient
                using (HttpClient client = new HttpClient(httpClientHandler))
                {
                    // Set a timeout in case of delays
                    client.Timeout = TimeSpan.FromSeconds(10);

                    // Send a test request to the provided URL
                    HttpResponseMessage response = await client.GetAsync(testUrl);

                    // Check if the response was successful
                    return response.IsSuccessStatusCode;
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions like timeouts, bad proxy credentials, etc.
                Console.WriteLine($"Error: {ex.Message}");
                return false;
            }
        }

        public List<ProxyModel> ReadProxies(string filePath)
        {
            var proxyList = new List<ProxyModel>();

            try
            {
                using (StreamReader reader = new StreamReader(filePath))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        string[] parts = line.Split(':');
                        if (parts.Length == 4)
                        {
                            string ip = parts[0];
                            string port = parts[1];
                            string username = parts[2];
                            string password = parts[3];

                            ProxyModel proxy = new ProxyModel(ip, port, username, password);
                            proxyList.Add(proxy);
                        }
                    }
                }
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine($"File not found: {filePath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading the file: {ex.Message}");
            }

            return proxyList;
        }
    }

    
}
