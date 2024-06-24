using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : IState<Bot>
{
    public void OnEnter(Bot t)
    {
        t.ChangeAnimation(Constant.ANIM_IS_RUN);
        Vector3 randomPoint = t.currentMap.GetRandomPointOnNavMesh(t.TF.position, 20f);
        t.SetDestination(randomPoint);
    }

    public void OnExcute(Bot t)
    {
        if (t.IsDestination)
        {
            t.ChangeState(new IdleState());
        }
    }

    public void OnExit(Bot t)
    {

    }
}
