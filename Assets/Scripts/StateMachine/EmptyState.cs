public class EmptyState : IState
{
    public void Update() { }
    public void HandleInput() { }
    public void Enter(params object[] args) { }
    public void Exit() { }
}
