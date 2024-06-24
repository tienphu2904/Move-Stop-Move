using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSight : MonoBehaviour
{
    [SerializeField] CharacterObject character;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Constant.TAG_CHARACTER))
        {
            CharacterObject target = Cache.GetCharacter(other);
            if (!target.IsDead && target != character)
            {
                character.AddTarget(target);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(Constant.TAG_CHARACTER))
        {
            CharacterObject target = Cache.GetCharacter(other);
            character.RemoveTarget(target);
        }
    }
}