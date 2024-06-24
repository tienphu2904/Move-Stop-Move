using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBoomerang : BulletObject
{
    private enum BoomerangStatus { MovingForward = 0, MovingBack = 1 }

    [SerializeField] private Transform rotateObject;
    private float movingBackTime;
    private BoomerangStatus boomerangStatus;

    protected override void OnEnable()
    {
        base.OnEnable();
        movingBackTime = .6f;
        boomerangStatus = BoomerangStatus.MovingForward;
    }

    protected override void Update()
    {
        //Set Moving with status
        if (boomerangStatus == BoomerangStatus.MovingForward)
        {
            TF.Translate(TF.forward * moveSpeed * Time.deltaTime, Space.World);
        }
        else
        {
            float step = moveSpeed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, character.transform.position, step);
            if (Vector3.Distance(transform.position, character.transform.position) < 0.1f)
            {
                OnDespawn();
            }
        }
        //Set status
        if (Vector3.Distance(startPoint, TF.position) > liveRange)
        {
            boomerangStatus = BoomerangStatus.MovingBack;
        }
        rotateObject.Rotate(Vector3.forward * -10);
    }
}
