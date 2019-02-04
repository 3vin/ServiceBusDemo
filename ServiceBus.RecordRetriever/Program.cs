using System;
using System.Configuration;
using System.Messaging;

namespace ServiceBus.RecordRetriever
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var appSettings = ConfigurationManager.AppSettings;
            var path = appSettings["path"];

            //Every second retrieve a message if available in the queue 
            using (var messageQueue = new MessageQueue())
            {
                messageQueue.Path = path;

                if (!MessageQueue.Exists(messageQueue.Path))
                {
                    MessageQueue.Create(messageQueue.Path);
                }

                while (true)
                {
                    var messages = messageQueue.GetAllMessages();

                    if (messages.Length > 0)
                    {
                        var message = messageQueue.Receive();

                        if (message != null)
                        {
                            Console.WriteLine("Message recieved");
                        }
                    }
                    else
                    {
                        Console.WriteLine("No messages");
                    }

                    System.Threading.Thread.Sleep(1000);
                }
            }
        }
    }
}