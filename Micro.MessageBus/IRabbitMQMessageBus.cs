using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Micro.MessageBus
{
    public interface IRabbitMQMessageBus
    {
        void PublishMessage(BaseMessage message, string queueName);       
    }
}
