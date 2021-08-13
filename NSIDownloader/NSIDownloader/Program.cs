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
            //CHECKING the HEC Redirect URL
            //string HECNSIRedirectURL = "http://www.hec.usace.army.mil/fwlink/?linkid=1&type=string"; //HEC Redirect URL points to the wrong URL
            //WebClient webClient = new WebClient();
            //string NSIURL = webClient.DownloadString(HECNSIRedirectURL);
            //webClient.Dispose();

            string NSIURL;

            //Defining the data to be downloaded
            //NSIURL = "https://ec2-3-212-154-125.compute-1.amazonaws.com/nsiapi/"; // This link is outdated. Was present in a PPT Presentation, but we now have our own domain to link to. 
            NSIURL = "https://nsi-dev.sec.usace.army.mil/nsiapi/"; //Download Link
            //NSIURL = "https://cwbi-mae2-proxy.sec.usace.army.mil/nsiapi/"; //another download link attempt
            NSIURL += "structures?bbox=";// Pulling Structures using bounding box
            NSIURL += "-81.58418,30.25165,-81.58161,30.26939,-81.55898,30.26939,-81.55281,30.24998,-81.58418,30.25165";//defining coordinates of box
            System.Console.WriteLine("downloading data from " + NSIURL);


            //Calling the download methods
            _ = FetchDataWithCredentials(NSIURL);
            //FetchDataNoCreds(NSIURL);
            
            
            while (true)
            {
                //keeping command prompt open
            }

        }


        //These two methods are the proper way to access the database according to Randy  
        private static async Task FetchDataWithCredentials(string NSIURL)
        {
            using (var httpClientHandler = new HttpClientHandler())
            {
                httpClientHandler.ServerCertificateCustomValidationCallback = (message,cert,chain,errors) => sccvc(message,cert,chain,errors);
                using (var client = new HttpClient(httpClientHandler))
                {

                    var message = await client.GetAsync(new Uri(NSIURL));
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
        private static bool sccvc(HttpRequestMessage message, System.Security.Cryptography.X509Certificates.X509Certificate2 cert, System.Security.Cryptography.X509Certificates.X509Chain chain, System.Net.Security.SslPolicyErrors errors)
        {
            return true;
        }




        //First Attempt. Fails. Not the recommended approach.
        private static void FetchDataNoCreds(string NSIURL)
        {
            using (var webClient = new WebClient())
            {
                var download = webClient.DownloadString(NSIURL);
                Console.WriteLine(download);
            }
            System.Console.Read();
        }


        }
    }
