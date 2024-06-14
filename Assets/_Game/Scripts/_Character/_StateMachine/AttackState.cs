using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : IState<Bot>
{
    private float changeTime = 0f;
    public void OnEnter(Bot t)
    {
        t.OnStop();
        t.OnAttack();
    }

    public void OnExcute(Bot t)
    {
        changeTime += Time.deltaTime;
        if (changeTime > 2f)
        {
            t.ChangeState(new PatrolState());
        }
    }

    public void OnExit(Bot t)
    {
    }
}
