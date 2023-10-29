//Observable able to parse strings from the Console 
//and route numeric messages to all subscribers 
public class ConsoleIntegerProducer : IObservable<int>, IDisposable
{
    //the subscriber list 
    private readonly List<IObserver<int>> subscriberList = new List<IObserver<int>>();

    //the cancellation token source for starting stopping 
    //inner observable working thread 
    private readonly CancellationTokenSource cancellationSource;
    //the cancellation flag 
    private readonly CancellationToken cancellationToken;
    //the running task that runs the inner running thread 
    private readonly Task workerTask;
    public ConsoleIntegerProducer()
    {
        cancellationSource = new CancellationTokenSource();
        cancellationToken = cancellationSource.Token;
        workerTask = Task.Factory.StartNew(OnInnerWorker, cancellationToken);
    }

    //add another observer to the subscriber list 
    public IDisposable Subscribe(IObserver<int> observer)
    {
        if (subscriberList.Contains(observer))
            throw new ArgumentException("The observer is already subscribed to this observable");

        Console.WriteLine("Subscribing for {0}", observer.GetHashCode());
        subscriberList.Add(observer);

        return null;
    }

    //this code executes the observable infinite loop 
    //and routes messages to all observers on the valid 
    //message handler 
    private void OnInnerWorker()
    {
        while (!cancellationToken.IsCancellationRequested)
        {
            var input = Console.ReadLine();
            int value;

            foreach (var observer in subscriberList)
                if (string.IsNullOrEmpty(input))
                    break;
                else if (input.Equals("EXIT"))
                {
                    cancellationSource.Cancel();
                    break;
                }
                else if (!int.TryParse(input, out value))
                    observer.OnError(new FormatException("Unable to parse given value")); 
                else
                    observer.OnNext(value);
        }
        cancellationToken.ThrowIfCancellationRequested();
    }


    //cancel main task and ack all observers 
    //by sending the OnCompleted message 
    public void Dispose()
    {
        if (!cancellationSource.IsCancellationRequested)
        {
            cancellationSource.Cancel();
            while (!workerTask.IsCanceled)
                Thread.Sleep(100);
        }

        cancellationSource.Dispose();
        workerTask.Dispose();

        foreach (var observer in subscriberList)
            observer.OnCompleted();
    }

    //wait until the main task completes or went cancelled 
    public void Wait()
    {
        while (!(workerTask.IsCompleted || workerTask.IsCanceled))
            Thread.Sleep(100);
    }
}

