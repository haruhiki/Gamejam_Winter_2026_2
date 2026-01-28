using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackConotorol : MonoBehaviour
{
    private WeaponColision currentCollision;
    public void SetcurrentWeaponCollision(WeaponColision colli) 
    {
        currentCollision = colli;
    }

    //アニメーションイベントでの呼び出し
    public void EnableHitBox() 
    {
        if (currentCollision) { currentCollision.SetCollisionActive(true); }
    }

    public void DisableHitBox() 
    {
        if (currentCollision) { currentCollision.SetCollisionActive(false); }
    }
}
