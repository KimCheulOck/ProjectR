using System.Collections.Generic;

public enum ObserverMessage
{
    None = 0,
    UIRefresh,
    GameReady,
    GameStart,
    GameResult,
    ReplayGame,

    NextStage,
    CreateIngredients,

    RefreshScore,

    CheckGoogleLogin,
    Exit,
}

public class ObserverHandler : ISubject
{
    private static ObserverHandler instance = null;
    public static ObserverHandler Instance
    {
        get
        {
            if (instance == null)
                instance = new ObserverHandler();
            return instance;
        }
    }

    private Dictionary<ObserverMessage, List<IObserver>> observers = new Dictionary<ObserverMessage, List<IObserver>>();

    public void AddObserver(ObserverMessage id, IObserver addObserver)
    {
        if (observers.ContainsKey(id))
        {
            observers[id].Add(addObserver);
        }
        else
        {
            List<IObserver> addObservers = new List<IObserver>();
            addObservers.Add(addObserver);
            observers.Add(id, addObservers);
        }
    }

    public void RemoveObserver(IObserver removeObserver)
    {
        foreach (var observer in observers)
        {
            if (observer.Value == null || observer.Value.Count == 0)
                continue;

            observer.Value.Remove(removeObserver);
        }
    }

    public void RemoveObserver(ObserverMessage id, IObserver removeObserver)
    {
        if (observers.ContainsKey(id))
            observers[id].Remove(removeObserver);
    }

    public void NotifyObserver(ObserverMessage id, params object[] message)
    {
        if (observers.ContainsKey(id))
        {
            for (int i = 0; i < observers[id].Count; ++i)
            {
                if (observers[id][i] == null)
                    continue;

                observers[id][i].RefrashObserver(id, message);
            }
        }
    }
}
