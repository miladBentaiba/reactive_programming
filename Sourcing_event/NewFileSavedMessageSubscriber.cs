
/// <summary> 
/// A tremendously basic implementation 
/// </summary> 
public sealed class NewFileSavedMessageSubscriber : IObserver<string>
{
    public void OnCompleted()
    {
        Console.WriteLine("-> END");
    }

    public void OnError(Exception error)
    {
        Console.WriteLine("-> {0}", error.Message);
    }

    public void OnNext(string value)
    {
        Console.WriteLine("-> {0}", value);
    }
}