using System.Collections.Generic;

public interface IReceiver
{
    void Receive();
}

public interface IObserver
{
    public List<IReceiver> Receivers
    {
        get;
    }

    void AddObserver(IReceiver _receiver)
    {
        if (!Receivers.Contains(_receiver))
        {
            Receivers.Add(_receiver);
        }
    }
	
    public void RemoveObserver(IReceiver _receiver)
    {
        if (Receivers.Contains(_receiver))
        {
            Receivers.Remove(_receiver);
        }
    }

    public void Send()
    {
        foreach (var _receiver in Receivers)
        {
            _receiver.Receive();
        }
    }
}