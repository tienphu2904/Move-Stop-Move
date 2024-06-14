using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : IState<Bot>
{
    private float changeTime = 0f;

    public void OnEnter(Bot t)
    {
        t.OnStop();
    }

    public void OnExcute(Bot t)
    {
        if (GameManager.Ins.IsState(GameState.Gameplay))
        {
            if (t.CanAttack)
            {
                t.ChangeState(new AttackState());
            }
            changeTime += Time.deltaTime;
            if (changeTime > 2f)
            {
                t.ChangeState(new PatrolState());
            }
        }
    }

    public void OnExit(Bot t)
    {

    }
}
