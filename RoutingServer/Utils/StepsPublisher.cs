using Apache.NMS;
using System;

namespace ActiveMQTest
{
    internal class StepsPublisher
    {
        public static void SendNewMessage(string text, string queueName)
        {

            Console.WriteLine($"Adding message to queue topic: {queueName}");

            string brokerUri = $"activemq:tcp://localhost:61616";  // Default port
            IConnectionFactory factory = new Apache.NMS.ActiveMQ.ConnectionFactory(brokerUri);

            using (IConnection connection = factory.CreateConnection())
            {
                connection.Start();

                using (ISession session = connection.CreateSession(AcknowledgementMode.AutoAcknowledge))
                using (IDestination dest = session.GetTopic(queueName))
                using (IMessageProducer producer = session.CreateProducer(dest))
                {
                    producer.DeliveryMode = MsgDeliveryMode.NonPersistent;

                    producer.Send(session.CreateTextMessage(text));
                    Console.WriteLine($"Sent {text} messages");
                }
            }
        }


    }
}
