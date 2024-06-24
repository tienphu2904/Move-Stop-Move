using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletForward : BulletObject
{
    protected override void Update()
    {
        base.Update();
        TF.Translate(TF.forward * moveSpeed * Time.deltaTime, Space.World);
    }
}
