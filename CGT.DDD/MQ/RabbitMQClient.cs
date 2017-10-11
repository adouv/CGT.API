using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;

namespace CGT.DDD.MQ {

    /// <summary>
    /// RabbitMQ读写类 TODO 调整
    /// </summary>

    public class RabbitMQClient
    {
 
  
 

        /// <summary>
        /// 写入队列--todo
        /// </summary>
        /// <param name="jsonRiskModel"></param>
        public void SendMessage(string jsonRiskModel)
        {

            var factory = new ConnectionFactory() { HostName = "123.57.7.161", UserName = "guest", Password = "guest" };
            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(queue: "ManageRiskModel",
                                         durable: true,
                                         exclusive: false,
                                         autoDelete: false,
                                         arguments: null);

                  
                    var body = Encoding.UTF8.GetBytes(jsonRiskModel);

                    channel.BasicPublish(exchange: "",
                                         routingKey: "ManageRiskModel",
                                         basicProperties: null,

                                         body: body);

                    

                }
            }
        }

        /// <summary>
        /// 接收消息
        /// </summary>
        /// <param name="jsonRiskModel">消息字符串</param>
        public void ReceiveMessages(Action<string> CheckPrice)
        {
            var factory = new ConnectionFactory() { HostName = "123.57.7.161", UserName = "guest", Password = "guest" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "ManageRiskModel",
                                     durable: true,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                //输入1，那如果接收一个消息，但是没有应答，则客户端不会收到下一个消息
                channel.BasicQos(0, 1, false);

                //在队列上定义一个消费者
                QueueingBasicConsumer consumer = new QueueingBasicConsumer(channel);

                //消费队列，并设置应答模式为程序主动应答
                channel.BasicConsume("ManageRiskModel", false, consumer);

                while (true)
                {
                    //阻塞函数，获取队列中的消息
                    BasicDeliverEventArgs ea = (BasicDeliverEventArgs)consumer.Queue.Dequeue();
                    byte[] bytes = ea.Body;
                    string str = Encoding.UTF8.GetString(bytes);
                    if (!string.IsNullOrEmpty(str))
                    {
                        CheckPrice(str);
                    }
                    //回复确认
                    channel.BasicAck(ea.DeliveryTag, false);
                }
            }

        }
 
    }
}
