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
             string getStructures(string bbox)
            {
                var apiUrl = String.Format($"https://nsi-dev.sec.usace.army.mil/nsiapi/structures?bbox={bbox}");
                //next line disables all certificate checks.  Probably should not do this in production
                ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, error) => { return true; };

                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(apiUrl);
                using (WebResponse response = request.GetResponse())
                {
                    Console.WriteLine(((HttpWebResponse)response).StatusDescription);
                    Stream receiveStream = response.GetResponseStream();
                    using (StreamReader reader = new StreamReader(receiveStream, Encoding.UTF8))
                    {
                        //line below will read and wait for all content.  It is commented out because the better approach is to read and process the stream as it is received
                        //Console.WriteLine(reader.ReadToEnd()); 
                        int bufferSize = 1024;
                        char[] buffer = new char[bufferSize];
                        while (!reader.EndOfStream) //process the stream in chunks
                        {
                            reader.ReadBlock(buffer, 0, bufferSize);
                            Console.WriteLine(new string(buffer));
                        }
                    }
                }
                Console.WriteLine("\nFinished");
                return null;
            }
            string myStructures =  getStructures("-81.58418,30.25165,-81.58161,30.26939,-81.55898,30.26939,-81.55281,30.24998,-81.58418,30.25165");
        }
       
    }
}
