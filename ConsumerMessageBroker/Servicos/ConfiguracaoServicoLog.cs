using KissLog;
using KissLog.Apis.v1.Listeners;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsumerMessageBroker.Servicos
{
    public static class ConfiguracaoServicoLog
    {
        public static void ConfigureKissLog()
        {
            string organizationId = "65cf3373-064a-4d5a-b536-ee5df8d0772f";
            string applicationId = "2e7aac68-2e99-4d09-b8ea-b5d8219d8530";

            ILogListener listener = new KissLogApiListener(new KissLog.Apis.v1.Auth.Application(organizationId, applicationId))
            {
                UseAsync = false
            };

            KissLogConfiguration.Listeners.Add(listener);
        }
    }
}
