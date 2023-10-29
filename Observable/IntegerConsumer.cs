/// <summary> 
/// Consumes numeric values that divides without rest by a given number 
/// </summary> 
public partial class IntegerConsumer : IObserver<int>
{
    public readonly int validDivider;
    //the costructor asks for a divider 
    public IntegerConsumer(int validDivider)
    {
        this.validDivider = validDivider;
    }

    private bool finished = false;
    public void OnCompleted()
    {
        if (finished)
            OnError(new Exception("This consumer already finished it's lifecycle"));
        else
        {
            finished = true;
            Console.WriteLine("{0}: END", GetHashCode());
        }
    }

    public void OnError(Exception error)
    {
        Console.WriteLine("{0}: {1}", GetHashCode(), error.Message);
    }

    public void OnNext(int value)
    {
        if (finished)
            OnError(new Exception("This consumer finished its lifecycle"));

        //the simple business logic is made by checking divider result 
        else if (value % validDivider == 0)
            Console.WriteLine("{0}: {1} divisible by {2}", GetHashCode(), value, validDivider);
    }
}

