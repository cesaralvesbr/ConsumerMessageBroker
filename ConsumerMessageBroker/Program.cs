using ConsumerMessageBroker.INFRA;
using ConsumerMessageBroker.Models;
using ConsumerMessageBroker.Repositorio;
using ConsumerMessageBroker.Servicos;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Data.SqlClient;
using System.Text;
using System.Text.Json;

namespace ConsumerMessageBroker
{
    class Program
    {

        static void Main(string[] args)
        {
            PedidoRepositorio pedidoRepositorio = new PedidoRepositorio(new MSSQLDB());

            ConfiguracaoServicoLog.ConfigureKissLog();

            int count = 1;
            var factory = new ConnectionFactory()
            {
                HostName = "tksscirv",
                Password = "yQYTBf7E3aycV3I-WyISpNZRGhQ5yUq6",
                Uri = new System.Uri("amqp://tksscirv:yQYTBf7E3aycV3I-WyISpNZRGhQ5yUq6@owl.rmq.cloudamqp.com/tksscirv")
            };

            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {

                channel.QueueDeclare(queue: "FilaPedidos",
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (model, ea) =>
                {

                    System.Threading.Thread.Sleep(5000);
                    var body = ea.Body;
                    var message = Encoding.UTF8.GetString(body.ToArray());
                    var deserializado = JsonSerializer.Deserialize<Pedido>(message);

                    try
                    {
                        pedidoRepositorio.EfetuarPedido(deserializado);

                        Console.WriteLine(" Pedido gerado com sucesso: \n ID: {0} " +
                     "\n Nome: {1} \n Quantidade:{2} \n Preco:{3} \n NUMERO {4}",
                     deserializado.Id,
                     deserializado.Nome,
                     deserializado.Quantidade,
                     deserializado.Valor,
                     count++);

                        channel.BasicAck(ea.DeliveryTag, false);
                        LogPedido.LogSucesso(deserializado);
                    }
                    catch (Exception erro)
                    {
                        if (erro is SqlException)
                        {
                            Console.WriteLine("Erro ao gravar pedido no banco de dados");
                            channel.BasicNack(ea.DeliveryTag, false, true);
                            LogPedido.LogErro(deserializado);
                        }
                        else
                        {
                            Console.WriteLine("Erro ao processar mensagem");
                            channel.BasicNack(ea.DeliveryTag, false, true);
                            LogPedido.LogErro(deserializado);
                        }
                    }

                    Console.WriteLine("\n \n");
                };
                channel.BasicConsume(queue: "FilaPedidos",
                                     autoAck: false,
                                     consumer: consumer);
                Console.ReadLine();
            }

        }


    }
}
