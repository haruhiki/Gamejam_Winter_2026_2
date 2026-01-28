using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "GameManager", menuName = "GameManagerSO")]
public class GamaManagerSO : ScriptableObject 
{
    [Header("受け渡しステータス")]
    public float statusHP = 100.0f;
    public float  statusMoveSpeed = 10.0f;
    public float statusMoveJump = 200.0f;
    public float statusAttackDelay = 0.5f;
    public float slashSpeed = 30.0f;
    public float effectlifeTime = 0.5f;
    public int   value = 0;
    [SerializeField] public int randomValue = 4; //イベントの数
    

    [Header("タイム関連")]
    public float eventTime = 10;
    public float gameTime = 0;
    public float gameTimeEnd = 180; 

    [Header("管理用フラグ")]
    public bool gameflg = false; //ゲームフラグ　ー＞クリア判定
    public bool damageFlg = false;

    public Action GameEventPlayer;
    public Action GameEventEnemy;
    public Action SceneChange;

    /// <summary> /// フラグリセット  /// </summary>
    public void Reset()
    {
        gameflg = false;
        damageFlg = false;
        statusHP = 100.0f;
        statusMoveJump = 200.0f;
        statusMoveSpeed = 10.0f;
        statusAttackDelay = 0.5f;
        slashSpeed = 30.0f;
        effectlifeTime = 0.5f;

    }

}
