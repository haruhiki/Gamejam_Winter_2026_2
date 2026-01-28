using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] GamaManagerSO _gameSO;

    private void Start()
    {
        _gameSO.Reset();
    }

    private void Update()
    {
        if(_gameSO == null) { return; }

        _gameSO.gameTime += Time.time;
        _gameSO.eventTime -= Time.deltaTime;

        if(_gameSO.gameTime <= _gameSO.gameTimeEnd) 
        {
            _gameSO.gameflg = true;
            //ƒV[ƒ“‘JˆÚ
            SceneManager.LoadScene("RankingScene");
        }
       
    }
}
