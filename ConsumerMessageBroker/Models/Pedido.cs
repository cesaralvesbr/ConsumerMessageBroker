using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsumerMessageBroker.Models
{
    public class Pedido
    {        
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public int Quantidade { get; set; }
        public double Valor { get; set; }
    }
}
