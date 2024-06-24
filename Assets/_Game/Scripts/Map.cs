using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Map : MonoBehaviour
{
    public Vector3[] spawnBotTransformList;
    public int TotalBot, MaxBot;


    public Vector3 GetRandomPointOnNavMesh(Vector3 center, float range = 50f)
    {
        Vector3 randomPoint = center + Random.insideUnitSphere * range;
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomPoint, out hit, range, NavMesh.AllAreas))
        {
            return hit.position;
        }
        return Vector3.zero;
    }
}
