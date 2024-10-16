using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEndState : GameState
{
    public GameEndState(GameManager manager) : base(manager)
    {
    }

    public override void Enter()
    {
        gameManager.EndGame();
    }

    public override void Update()
    {
    }

    public override void Exit()
    {
    }
}
