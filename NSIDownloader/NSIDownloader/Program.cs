using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
            string downloadToFolderPath = "C:\\";
            string FIPS = "06";
            string xmin = "0";
            string xmax = "1";
            string ymin = "1";
            string ymax = "1";

            System.Console.WriteLine(webClient.DownloadString(HECNSIRedirectURL));
            System.Console.Read();
        }
    }
}
