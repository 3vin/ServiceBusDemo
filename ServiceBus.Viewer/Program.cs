using System;
using System.Configuration;
using System.Messaging;

namespace ServiceBus.Viewer
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var appSettings = ConfigurationManager.AppSettings;
            var path = appSettings["path"];

            //Every second get the size of the Queue
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

                    Console.Clear();
                    Console.WriteLine($"{path}: {messages.Length}");

                    System.Threading.Thread.Sleep(1000);
                }
            }
        }
    }
}