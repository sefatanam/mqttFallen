using System.Text;
using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Client.Options;

namespace MqttFallenCore;

public static class Subscriber
{
#pragma warning disable CS8618
    private static IMqttClient _client;
#pragma warning restore CS8618
#pragma warning disable CS8618
    private static IMqttClientOptions _options;
#pragma warning restore CS8618

    public static void Start()
    {
        try
        {
            Console.WriteLine("Starting Subscriber....");

            //create subscriber client
            var factory = new MqttFactory();
            _client = factory.CreateMqttClient();

            //configure options
            _options = new MqttClientOptionsBuilder()
                .WithClientId("SubscriberId")
                .WithTcpServer("localhost", 1884)
                .WithCleanSession()
                .Build();

            //Handlers
            _client.UseConnectedHandler(e =>
            {
                Console.WriteLine("Connected successfully with MQTT Brokers.");

                //Subscribe to topic
#pragma warning disable CS0618
                _client.SubscribeAsync(new TopicFilterBuilder().WithTopic("test").Build()).Wait();
#pragma warning restore CS0618
            });
            _client.UseDisconnectedHandler(e => { Console.WriteLine("Disconnected from MQTT Brokers."); });
            _client.UseApplicationMessageReceivedHandler(e =>
            {
                Console.WriteLine("### RECEIVED APPLICATION MESSAGE ###");
                Console.WriteLine($"+ Topic = {e.ApplicationMessage.Topic}");
                Console.WriteLine($"+ Payload = {Encoding.UTF8.GetString(e.ApplicationMessage.Payload)}");
                Console.WriteLine($"+ QoS = {e.ApplicationMessage.QualityOfServiceLevel}");
                Console.WriteLine($"+ Retain = {e.ApplicationMessage.Retain}");
                Console.WriteLine();

                //  Task.Run(() => _client.PublishAsync("hello/world"));
            });

            //actually connect
            _client.ConnectAsync(_options).Wait();

            Console.WriteLine("Press key to exit");
            Console.ReadLine();

            //To keep the app running in container
            //https://stackoverflow.com/questions/38549006/docker-container-exits-immediately-even-with-console-readline-in-a-net-core-c
            Task.Run(() => Thread.Sleep(Timeout.Infinite)).Wait();
            _client.DisconnectAsync().Wait();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}