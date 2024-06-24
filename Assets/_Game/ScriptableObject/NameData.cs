using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "My Assets/Name Data")]
public class NameData : ScriptableObject
{
    public List<string> characterNames;
}