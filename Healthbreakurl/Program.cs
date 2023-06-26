using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;

namespace Healthbreakurl
{

    class Program
    {

        static async Task Main(string[] args)
        {
            if (args.Length > 1)
            {
                string inputUrl = Environment.GetCommandLineArgs()[1];
                string inputCountCheking = Environment.GetCommandLineArgs()[2];

                string url = inputUrl.Contains("https://www.") ? inputUrl : "https://www." + inputUrl;
                int countConnection = 0;
                try
                {
                    countConnection = Convert.ToInt32(inputCountCheking);
                }
                catch (FormatException ex)
                {
                    Console.WriteLine($"Incorrect countCheking format value:({inputCountCheking})  {ex.Message}");
                }


                for (int i = 0; i < countConnection; i++)
                {
                    WebRequest request = WebRequest.CreateHttp(url);
                    try
                    {
                        using (HttpWebResponse response = (HttpWebResponse)await request.GetResponseAsync())
                        {
                            if (response.StatusCode == HttpStatusCode.OK)
                            {
                                Console.WriteLine("Success");
                            }
                            else
                            {
                                Console.WriteLine($"StatusCode:{response.StatusCode} url:{url}");
                            }
                        }
                    }
                    catch (WebException ex)
                    {
                        Console.WriteLine($"ErrorMesage:{ex.Message}  Url:{ inputUrl}");
                        break;
                    }
                }
            }
            else
            {
                Console.WriteLine($"Sorry, but we need two parameters.");
            }
            Console.ReadKey();
        }
    }
}
