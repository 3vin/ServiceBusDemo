using System;
using System.Configuration;
using System.Messaging;

namespace ServiceBus.Inserter
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var appSettings = ConfigurationManager.AppSettings;
            var path = appSettings["path"];

            //Every second post a message with an ID
            using (var messageQueue = new MessageQueue())
            {
                messageQueue.Path = path;

                if (!MessageQueue.Exists(messageQueue.Path))
                {
                    MessageQueue.Create(messageQueue.Path);
                }

                while (true)
                {
                    var message = new Message();

                    var random = new Random();

                    message.Body = random.Next(9) + 1;

                    messageQueue.Send(message);
                    Console.WriteLine("Message posted");

                    System.Threading.Thread.Sleep(1000);
                }
            }
        }
    }
}