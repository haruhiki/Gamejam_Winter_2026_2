using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHP : MonoBehaviour
{
    [SerializeField] GamaManagerSO _gameSO;
    [SerializeField] private float MAX_HP;

    [SerializeField] private Slider currentHPSlider;
    [SerializeField] private Slider fadeSlider;
    [SerializeField] private float fadeSpeed = 2.0f;
    [SerializeField] private float damageInterval;
    bool _isInterval = false;

    private void Start()
    {
        _gameSO.statusHP = MAX_HP;
        //現状のスライダーゲージ
        if (currentHPSlider != null) 
        {
            currentHPSlider.maxValue = MAX_HP;
            currentHPSlider.value = MAX_HP;
        }

        //フェード用のスライダーゲージ
        if (fadeSlider != null) 
        {
            fadeSlider.maxValue = MAX_HP;
            fadeSlider.value = MAX_HP;
        }
    }

    private void Update()
    {
        FadeHPSlider();   
    }

    private void FadeHPSlider() 
    {
        if(fadeSlider != null && currentHPSlider != null) 
        {
            fadeSlider.value = Mathf.Lerp(
                fadeSlider.value,
                currentHPSlider.value, 
                Time.deltaTime * fadeSpeed 
                );
        }
    }

    private void UpdateHPBarUI() 
    {
        Debug.Log("PlayerHP");
        currentHPSlider.value = _gameSO.statusHP;
    }

    public IEnumerator HitDamage(int takeDamage) 
    {
        if (!_isInterval) 
        {
            _isInterval = true;
            if (_gameSO.statusHP >= 0)
            {
                _gameSO.statusHP -= takeDamage;
                UpdateHPBarUI();
                //敗北時の処理
                if (_gameSO.statusHP <= 0)
                {
                    _gameSO.gameflg = true;
                    //イベント購読
                    _gameSO.SceneChange?.Invoke();
                }
                yield return new WaitForSeconds(damageInterval);
                _isInterval = false;
            }
        }
    }

}
