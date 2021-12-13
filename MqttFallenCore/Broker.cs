using MQTTnet;
using MQTTnet.Protocol;
using MQTTnet.Server;
using static System.Console;

namespace MqttFallenCore;

public static class Broker
{
    public static void Start()
    {
        /*var optionBuilder = new MqttServerOptionsBuilder()
            .WithConnectionValidator(c => c.ReasonCode = MqttConnectReasonCode.Success)
            .WithApplicationMessageInterceptor(context =>
            {
                var newData = Encoding.UTF8.GetBytes(DateTime.Now.ToString("O"));
                var oldData = context.ApplicationMessage.Payload;
                var mergeData = newData.Concat(newData).ToArray();
            })
            .WithConnectionBacklog(100)
            .WithDefaultEndpointPort(1884);


        var mqttServer = new MqttFactory().CreateMqttServer();

        mqttServer.StartAsync(optionBuilder.Build()).Wait();

        WriteLine("Broker is running...");
        WriteLine("Press any key to exit.");
        ReadLine();
        mqttServer.StopAsync().Wait();*/
        
        
        
        //configure options
        var optionsBuilder = new MqttServerOptionsBuilder()
            .WithConnectionValidator(c =>
            {
                WriteLine($"{c.ClientId} connection validator for c.Endpoint: {c.Endpoint}");
                c.ReasonCode = MqttConnectReasonCode.Success;
            })
            .WithApplicationMessageInterceptor(context =>
            {
                //Console.WriteLine("WithApplicationMessageInterceptor block merging data");
                //var newData = Encoding.UTF8.GetBytes(DateTime.Now.ToString("O"));
                //var oldData = context.ApplicationMessage.Payload;
                //var mergedData = newData.Concat(oldData).ToArray();
                //context.ApplicationMessage.Payload = mergedData;
            })
            .WithConnectionBacklog(100)
            .WithDefaultEndpointPort(1884);


        //start server
        var mqttServer = new MqttFactory().CreateMqttServer();
        mqttServer.StartAsync(optionsBuilder.Build()).Wait();

        WriteLine($"Broker is Running: Host: {mqttServer.Options.DefaultEndpointOptions.BoundInterNetworkAddress} Port: {mqttServer.Options.DefaultEndpointOptions.Port}");
        WriteLine("Press any key to exit.");
        ReadLine();

        //To keep the app running in container
        //https://stackoverflow.com/questions/38549006/docker-container-exits-immediately-even-with-console-readline-in-a-net-core-c
        Task.Run(() => Thread.Sleep(Timeout.Infinite)).Wait();

        mqttServer.StopAsync().Wait();
    }
}