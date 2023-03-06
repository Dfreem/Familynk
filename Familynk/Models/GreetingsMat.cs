namespace Familynk.Models;

internal class GreetingsMat
{
    public IMessageContainer FrontDoorMessage { get; set; } = default!;
    public object MyProperty { get; set; }
}