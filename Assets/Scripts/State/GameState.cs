



public abstract class GameState
{
    protected GameManager gameManager;

    public GameState(GameManager manager)
    {
        gameManager = manager;
    }

    // Enter와 Exit에서 상태를 변화하지 말것!
    public abstract void Enter();
    public abstract void Update();
    public abstract void Exit();
    
    public enum Type
    {
        Ready,
        Playing,
        Matching,
        GameEnd,
        Max
    }
}
