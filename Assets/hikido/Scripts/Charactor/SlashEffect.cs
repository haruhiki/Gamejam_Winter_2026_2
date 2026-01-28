using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlashEffect : MonoBehaviour
{
    //斬撃エフェクト
    [SerializeField] private GameObject slashEffect;

    //斬撃エフェクトの発生ポイント
    [SerializeField] private Transform SlashPoint;

    //アニメーションイベントで使用
    public void SlashEffectAttack()
    {
        Quaternion _rotation = SlashPoint.rotation;
        var slashEffectprefa = Instantiate<GameObject>(slashEffect, SlashPoint.position, _rotation);
    }

}
