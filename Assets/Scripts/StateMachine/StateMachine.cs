using UnityEngine;

public class StateMachine<T>
{
    public IState<T> CurrentState { get; private set; }
    private readonly T _ctx;
    public StateMachine(T ctx)
    {
        _ctx = ctx;
    }

    public void Initialize(IState<T> startState)
    {
        CurrentState = startState;
        CurrentState.OnEnter(_ctx);
    }

    public void ChangeState(IState<T> nextState)
    {
        if(nextState == null)
        {
            Debug.LogError("Trying to change to a null state.");
            return;
        }

        if (ReferenceEquals(CurrentState, nextState))
        {
            Debug.LogWarning("Trying to change to the same state.");
            return;
        } 
        
        CurrentState?.OnExit(_ctx);
        CurrentState = nextState;
        CurrentState.OnEnter(_ctx);
    }

    public void Tick()
    {
        CurrentState?.OnUpdate(_ctx);
    }
}
