using Grpc.Net.Client;
using JamaisCouché;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToujoursDebout.Services
{
    public class JamaisCouche
    {
        private readonly GrpcChannel _channel;
        private readonly JamaisCoucheService.JamaisCoucheServiceClient _client;

        public JamaisCouche(string address = "http://91.168.126.83:5009")
        {
            this._channel = GrpcChannel.ForAddress(address);
            this._client = new JamaisCoucheService.JamaisCoucheServiceClient(_channel);
        }

        public void SendMessage()
        {
            string message = Console.ReadLine();
            while (message != "stop")
            {
                var response = _client.ReceiveData(new SendDataRequest { Message = message });

                Console.WriteLine("--- Client ---");
                Console.WriteLine(response.Message);
                message = Console.ReadLine();
            }
        }
    }
}
