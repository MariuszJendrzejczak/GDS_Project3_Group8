using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine
{
    Dictionary<string, IState> stateDict
        = new Dictionary<string, IState>();
    IState current = new EmptyState();
    public string currentStateId;

    public IState Current { get { return current; } }
    public void Add(string id, IState state) { stateDict.Add(id, state); }
    public void Remove(string id) { stateDict.Remove(id); }
    public void Clear() { stateDict.Clear(); }


    public void Change(string id, params object[] args)
    {
        current.Exit();
        IState next = stateDict[id];
        currentStateId = id;
        next.Enter(args);
        current = next;
    }
    public void SetStartingState(string id)
    {
        current = stateDict[id];
        currentStateId = id;
    }

    public void Update()
    {
        current.Update();
    }

    public void HandleInput()
    {
        current.HandleInput();
    }
}
