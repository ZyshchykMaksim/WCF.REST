using System;
using System.ServiceModel;

namespace WCF.RESTService.Host
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                ServiceHost host = new ServiceHost(typeof(RESTService));
                host.Open();

                if (host.State == CommunicationState.Opened)
                {
                    Console.WriteLine("The service is available:");
                    foreach (var address in host.BaseAddresses)
                    {
                        Console.WriteLine(address);
                    }
                    Console.WriteLine();
                }
                else
                {
                    throw new System.Exception("The service is not in a state OPENED.");
                }
            }
            catch (System.Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.Message);
            }
            Console.ReadKey(true);
        }
    }
}