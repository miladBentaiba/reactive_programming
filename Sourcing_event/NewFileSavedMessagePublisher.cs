public sealed class NewFileSavedMessagePublisher : IObservable<string>, IDisposable
{
    private readonly FileSystemWatcher watcher;
    public NewFileSavedMessagePublisher(string path)
    {
        //creates a new file system event router 
        this.watcher = new FileSystemWatcher(path);
        //register for handling File Created event 
        this.watcher.Created += OnFileCreated;
        //enable event routing 
        this.watcher.EnableRaisingEvents = true;
    }

    //signal all observers a new file arrived 
    private void OnFileCreated(object sender, FileSystemEventArgs e)
    {
        foreach (var observer in subscriberList)
            observer.OnNext(e.FullPath);
    }

    //the subscriber list 
    private readonly List<IObserver<string>> subscriberList = new List<IObserver<string>>();

    public IDisposable Subscribe(IObserver<string> observer)
    {
        //register the new observer 
        subscriberList.Add(observer);

        return null;
    }

    public void Dispose()
    {
        //disable file system event routing 
        this.watcher.EnableRaisingEvents = false;
        //deregister from watcher event handler 
        this.watcher.Created -= OnFileCreated;
        //dispose the watcher 
        this.watcher.Dispose();

        //signal all observers that job is done 
        foreach (var observer in subscriberList)
            observer.OnCompleted();
    }
}
