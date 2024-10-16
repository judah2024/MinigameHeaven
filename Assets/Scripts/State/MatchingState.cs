using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchingState : GameState
{
    public MatchingState(GameManager manager) : base(manager)
    {
    }

    public override void Enter()
    {
        gameManager.CheckMatch();
    }

    public override void Update()
    {
        gameManager.UpdateTimer();
    }

    public override void Exit()
    {
    }
}
