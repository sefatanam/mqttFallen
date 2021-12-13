using MqttFallenCore;
using NUnit.Framework;

namespace MqttFallenTests;

public class DemoTests
{
    [Test]
    [TestCase(1, 3, 4)]
    [TestCase(5, 3, 8)]
    [TestCase(8, 1, 9)]
    [TestCase(7, 3, 10)]
    [TestCase(10, 100, 110)]
    public void Demo_SumTwoNumber_Valid(int numOne, int numTwo, int output)
    {
        var result = Demo.SumTwoNumber(numOne, numTwo);

        Assert.AreEqual(result, output);
    }
}