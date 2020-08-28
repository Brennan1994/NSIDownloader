using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace NSIDownloader
{
    class Program
    {
        static void Main(string[] args)
        {
            string HECNSIRedirectURL = "http://www.hec.usace.army.mil/fwlink/?linkid=1&type=string";

            WebClient webClient = new WebClient();
            string NSIURL = webClient.DownloadString(HECNSIRedirectURL);
            webClient.Dispose();

            NSIURL = "https://ec2-3-212-154-125.compute-1.amazonaws.com/nsiapi/";
            NSIURL += "structures?bbox=";
            NSIURL += "-81.58418,30.25165,-81.58161,30.26939,-81.55898,30.26939,-81.55281,30.24998,-81.58418,30.25165";
            System.Console.WriteLine("downloading data from " + NSIURL);

            _ = FetchData(NSIURL);

            while (true)
            {
                //keeping command prompt open
            }




            //System.Console.WriteLine();
            
        }

        private static async Task FetchData(string NSIURL)
        {
            using (var httpClientHandler = new HttpClientHandler())
            {
                httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };
                using (var client = new HttpClient(httpClientHandler))
                {

                    var message = await client.GetAsync(new System.Uri(NSIURL));
                    if (message.IsSuccessStatusCode)
                    {
                        Console.WriteLine(message.Content);
                    }
                    else
                    {
                        Console.WriteLine(message.StatusCode);
                    }
                }
            }
            System.Console.Read();
        }
    }
}