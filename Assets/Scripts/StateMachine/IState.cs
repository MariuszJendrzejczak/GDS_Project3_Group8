public interface IState
{
    void Update();
    void HandleInput();

    void Enter(params object[] args);
    void Exit();
}
