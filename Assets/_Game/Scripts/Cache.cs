using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Cache
{
    private static Dictionary<Collider, CharacterObject> characters = new Dictionary<Collider, CharacterObject>();

    public static CharacterObject GetCharacter(Collider collider)
    {
        if (!characters.ContainsKey(collider))
        {
            characters.Add(collider, collider.GetComponent<CharacterObject>());
        }

        return characters[collider];
    }
}