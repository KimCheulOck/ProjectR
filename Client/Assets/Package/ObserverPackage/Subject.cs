public interface ISubject
{
    void AddObserver(ObserverMessage id, IObserver observer);
    void RemoveObserver(IObserver observer);
    void RemoveObserver(ObserverMessage id, IObserver observer);
    void NotifyObserver(ObserverMessage id, object[] message);
}