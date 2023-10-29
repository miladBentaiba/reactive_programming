namespace Observable
{
    /// <summary> 
    /// Handle observer subscription lifecycle 
    /// </summary> 
    public sealed class Subscription<T> : IDisposable
    {
        private readonly IObserver<T> observer;
        public Subscription(IObserver<T> observer)
        {
            this.observer = observer;
        }

        //the event signalling that the observer has 
        //completed its lifecycle 
        public event EventHandler<IObserver<T>> OnCompleted;

        public void Dispose()
        {
            if (OnCompleted != null)
                OnCompleted(this, observer);

            observer.OnCompleted();
        }
    }
}
