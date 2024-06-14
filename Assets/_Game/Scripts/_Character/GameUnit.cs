using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUnit : MonoBehaviour
{
    public PoolType poolType;
    private Transform tf;
    public Transform TF
    {
        get
        {
            //tf = tf ?? gameObject.transform;
            if (tf == null)
            {
                tf = transform;
            }
            return tf;
        }
    }
}