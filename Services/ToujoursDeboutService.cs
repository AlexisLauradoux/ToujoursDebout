using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Grpc.Core;
using Grpc.Net.Client;
using Microsoft.Extensions.Logging;

namespace ToujoursDebout
{
    public class ToujoursDeboutService : ServiceToujoursDebout.ServiceToujoursDeboutBase
    {
       

        public override Task<ReceiveDataReply> ReceiveData(ReceiveDataRequest request, ServerCallContext context)
        {
            Console.WriteLine("--- Server ---");
            Console.WriteLine($"I received a message");
            Console.WriteLine($"{request.Message}");
            return Task.FromResult(new ReceiveDataReply
            {
                Message = "Roger !"
            });
        }

        private readonly GrpcChannel _channel;
        private readonly ServiceToujoursDebout.ServiceToujoursDeboutClient _client;

        public ToujoursDeboutService(string address = "https://localhost:5010")
        {
            this._channel = GrpcChannel.ForAddress(address);
            this._client = new ServiceToujoursDebout.ServiceToujoursDeboutClient(_channel);
        }

        public void SendMessage()
        {
            string message = Console.ReadLine();
            while (message != "stop")
            {
                var response = _client.ReceiveData(new ReceiveDataRequest { Message = message });
                Console.WriteLine("--- Client ---");
                Console.WriteLine(response.Message);
                message = Console.ReadLine();
            }
        }
    }
}
