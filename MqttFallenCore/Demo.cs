namespace MqttFallenCore;

using static Console;

public static class Demo
{
    /// <summary>
    /// A Redundant Method of console write 
    /// </summary>
    /// <param name="message"></param>
    public static void Message(string message) => Write(message);

    /// <summary>
    /// A Demo Method for testing greeting
    /// </summary>
    /// <param name="name"></param>
    public static void Greeting(string name) => Write($"Have a sweet dream, {name} its {DateTime.Now.Hour} PM");


    /// <summary>
    /// Sum of two number type must be integer
    /// </summary>
    /// <param name="numOne"></param>
    /// <param name="numTwo"></param>
    /// <returns></returns>
    public static int SumTwoNumber(int numOne, int numTwo) => numOne + numTwo;
}