
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class ReadyState : GameState
{
    public ReadyState(GameManager manager) : base(manager)
    {
    }

    public float readyTime;

    public override void Enter()
    {
        gameManager.board.SetBoard();
        readyTime = 3.0f;
    }

    public override void Update()
    {
        readyTime -= Time.deltaTime;
        gameManager.ReadyUpdated(readyTime);
        if (readyTime <= 0.0f)
        {
            gameManager.ChangeState(Type.Playing);
        }
    }

    public override void Exit()
    {
    }
}
