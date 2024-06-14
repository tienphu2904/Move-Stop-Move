using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponObject : GameUnit
{
    private const float SHOOT_DELAY_TIME = .2f;
    private const float SHOOT_SHOW_WEAPOM = .51f;
    private const float SHOOT_CAN_ATTACK_TIME = .5f;

    [SerializeField] private PoolType bulletType;

    public bool CanAttack = true;

    private void OnInit()
    {
        transform.gameObject.SetActive(true);
        Invoke(nameof(ChangeCanAttack), SHOOT_CAN_ATTACK_TIME);
    }


    public IEnumerator Shoot(CharacterObject character, Vector3 target, float size)
    {
        CanAttack = false;

        yield return new WaitForSeconds(SHOOT_DELAY_TIME);

        BulletObject bullet = SimplePool.Spawn<BulletObject>(bulletType, TF.position, Quaternion.identity);
        bullet.OnInit(character, target + TF.position.y * Vector3.up, size);
        bullet.TF.localScale = size * Vector3.one;
        transform.gameObject.SetActive(false);

        Invoke(nameof(OnInit), SHOOT_SHOW_WEAPOM);
    }

    private void ChangeCanAttack()
    {
        CanAttack = true;
    }

    // public void Throw(CharacterObject character, Action<CharacterObject, CharacterObject> onHit)
    // {
    //     BulletObject bullet = SimplePool.Spawn<BulletObject>(bulletType, TF.position, Quaternion.identity);
    //     bullet.OnInit(character, onHit);
    // }
}
