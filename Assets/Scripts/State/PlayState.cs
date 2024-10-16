using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayState : GameState
{
    public PlayState(GameManager manager) : base(manager)
    {
    }

    public override void Enter()
    {
    }

    public override void Update()
    {
        gameManager.UpdateTimer();
    }

    public override void Exit()
    {
    }
}
