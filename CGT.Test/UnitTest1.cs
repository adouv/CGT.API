using System;
using System.IO;
using CGT.Api.DTO;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using RabbitMQ.Client;
using System.Text;
using CGT.Api.Service.Manage.CheckTicket;

namespace CGT.Test {
    [TestClass]
    public class UnitTest1 {

        public void Init() {

        }

        [TestMethod]
        public void TestMethod1() {
            FileStream fs = new FileStream(@"C:\Users\Administrator\Desktop\3.xlsx", FileMode.Open);

            TravelOrderImportService travelOrderImportService = new TravelOrderImportService();

            travelOrderImportService.SetData(new RequestModel() {
                MerchantId = "CS01",
                Data = "VZCskTARh7BJoIr6o9IqqWGPJ/F0XEubnwrK9ITaHffUtvaI1W/YGC5VolHZ4S5SVYlwt23VfKxu8XCZ6OolIejGJ87t+6zU4XQ4n5T0+M+Hc6sWTyGiz6HcdEki2KFZ1JRHNdcvaTOd1y7g7B2B3/Mhz+bA6pZUDinuzr0CdiRuYWj9FlxJbhmGJcKPcN8GdgYyUst2zkuBUEWwqSUgzmlT+1v4ajIoXy/3ocfRHieinMiWC04J0FVqD94jGdgHr8fNfwha+JlK1z6IzfaXkxHS5MlHYK2PihfwNlGNzKSxfxoFl0FHG+IyqoivEgnhLrxQLA904XtdJIFXTe/2wkhihc0BsPlbOLZECuusV8ITBnyCQpjjxW1lqqbOvIGt4+OVjQMZRtFL2/+RoZbRSVCH7UiIOBvvJJzR07L51GoJdIz0IWf6//73avHKdOvn",
                EncryptKey = "dr9sMQqUfuHfy5augb05gfgA7pyOpjmMLm0BsRfwkgKO3Zh83HTDU4po1RB6TyZBywfSwjsXErCy8JE8BCov0wEpd9Pd9ZnneapuOo3hCNKIjTKjTItPJKD25rWV9EyPhtrbKsyhHZN5MN0YR/0HdvMlSjgsDu9Hjo4y9F+5Ggyz4mqbVxqOEzBK3SaaNp+28flyx0EocPBO4WQm/T+xYLPBooyySLWidi+y7cB3QuAzOW8jts2wKeiRK6k4L9YTaVg1dQewGHPZ8PrEdldRIdxJS43bXpcLQcVd03qYeEVyFHQXAR8X7dwxkJm88TixXklASZ+j+7l4Dl46RPlTqA==",
                GuidKey = ""
            });

            travelOrderImportService.ExcelStream = fs;

            var responseMessage = travelOrderImportService.Execute();

            Console.WriteLine(responseMessage);
        }

        [TestMethod]
        public void TestMQ() {
            var factory = new ConnectionFactory() { HostName = "10.168.97.165", UserName = "pengguang", Password = "pengguang19911024" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel()) {
                channel.QueueDeclare(queue: "ManageRiskModel",
                                     durable: true,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                string message = "Hello World!";
                var body = Encoding.UTF8.GetBytes(message);

                channel.BasicPublish(exchange: "",
                                     routingKey: "ManageRiskModel",
                                     basicProperties: null,
                                     body: body);
                Console.WriteLine(" [x] Sent {0}", message);
            }

            Console.WriteLine(" Press [enter] to exit.");
        }
    }
}
