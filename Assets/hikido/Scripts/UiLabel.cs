using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.UI;

public class UiLabel : MonoBehaviour
{
    [SerializeField] public Text _timeText;
    [SerializeField] GamaManagerSO _gameSO;
    public GameObject _gameimgae;
    public Image _image;
    public Sprite[] _spriteImage;
    
    
    float counttime = 10;

    private void Start()
    {
      _gameimgae.SetActive(false);
    }

    private void Update() 
    {
       CountDownTime();
       ImageActive();
    }


    //イメージ切り替え
    private void ImageActive()
    {
        _gameimgae.SetActive(true);
        switch (_gameSO.value)
        {
            case 0:
                _image.sprite = _spriteImage[0];
                break;
            case 1:
                _image.sprite = _spriteImage[1];
                break;
            case 2:
                _image.sprite = _spriteImage[2];
                break;
            case 3:
                _image.sprite = _spriteImage[3];
                break;
            case 4:
                _image.sprite = _spriteImage[4];
                break;
        }
    }

    private IEnumerator DelayUI() 
    {
        int delaytime = 3;
        yield return new WaitForSeconds(delaytime);
    }


    //10秒カウントダウンUI用
    private void CountDownTime()
    {
        _timeText.text =  _gameSO.eventTime.ToString("F0");
    }


}
