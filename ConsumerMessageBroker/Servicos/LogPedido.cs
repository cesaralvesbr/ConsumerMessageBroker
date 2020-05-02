using ConsumerMessageBroker.Models;
using KissLog;
using System;

namespace ConsumerMessageBroker.Servicos
{
    public static class LogPedido
    {
        public static void LogErro()
        {
            ILogger logger = new Logger(url: "LogError");
            logger.Error($"Erro ao efetuar pedido.Data:{ DateTime.Now}");
            Logger.NotifyListeners(logger);

        }
        public static void LogErro(Pedido pedido)
        {
            ILogger logger = new Logger(url: "LogError");
            logger.Error($"Erro ao efetuar pedido. ID={pedido.Id} Data:{ DateTime.Now}");
            Logger.NotifyListeners(logger);
        }

        public static void LogSucesso(Pedido pedido)
        {
            KissLog.ILogger logger = new Logger(url: "LogSucesso");
            logger.Info($"Pedido efetuado com sucesso. ID={pedido.Id} Data={ DateTime.Now}");
            Logger.NotifyListeners(logger);
        }
    }
}
