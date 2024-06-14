using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletRoration : BulletObject
{
    [SerializeField] private Transform rotateObject;

    protected override void Update()
    {
        base.Update();
        TF.Translate(TF.forward * moveSpeed * Time.deltaTime, Space.World);
        rotateObject.Rotate(Vector3.forward * -10);
    }
}
