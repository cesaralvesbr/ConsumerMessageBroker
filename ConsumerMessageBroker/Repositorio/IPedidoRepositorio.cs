using ConsumerMessageBroker.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ConsumerMessageBroker.Repositorio
{
    public interface IPedidoRepositorio
    {
        void EfetuarPedido(Pedido pedido);
    }
}
