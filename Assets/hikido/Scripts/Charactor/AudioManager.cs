using System;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    //BGMデータ管理用
    [Serializable] public class BGMSoundData 
    {
        //追加していく >>
        public enum BGMDATA 
        {
            TITLE,  //タイトル用BGM
            GAME,   //ゲーム用BGM
            BRANK,
        }
        public BGMDATA bgm;
        public AudioClip bgmClip;
        [Range(0, 1)] public float bgmVolume = 1;
    }

    //SEデータ管理用
    [Serializable] public class SESoundData 
    {
        public enum SEDATA 
        {
            PLAYER,
            ENEMY,
            SYSTEM,
            BRANK,
        }
        public SEDATA data;
        public AudioClip seClip;
        [Range(0, 1)] public float seVolume = 1;
    }

    //SEカテゴリ管理
    [Serializable] public class SECategory 
    {
        //カテゴリ名で管理
        public string categoryName;
        public List<SESoundData> sounds = new List<SESoundData>();
    }

    [Header("SE/BGMの音源リスト")]
    [SerializeField] AudioSource seSource;
    [SerializeField] AudioSource bgmSource;
    [SerializeField] List<BGMSoundData> bgmData;
    [SerializeField] List<SECategory> seCategories;

    [Header("Volume管理")]
    [SerializeField] public float masterVolume = 1;
    [SerializeField] public float bgmmasterVolume = 1;
    [SerializeField] public float semasterVolume = 1;

    [SerializeField] private GamaManagerSO _gameSO;

    #region singleton
    public static AudioManager Instance { get;private set; }

    private void Awake()
    {
        if(Instance == null) 
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else { Destroy(gameObject); }
    }
    #endregion

    //BGM再生・調整
    public void PlayBGM(BGMSoundData.BGMDATA bgm) 
    {
        BGMSoundData data = bgmData.Find(data => data.bgm == bgm);
        if(data != null) 
        {
            bgmSource.clip = data.bgmClip;
            bgmSource.volume = data.bgmVolume * masterVolume * bgmmasterVolume;
            bgmSource.Play();
        }
        else { Debug.LogError("指定されたBGMデータが見つかりません。" + bgm); }
    }

    //SE再生・調整
    public void PlaySE(string categoryName,SESoundData.SEDATA se) 
    {
        SECategory category = seCategories.Find(category => category.categoryName == categoryName);
        if(category != null) 
        {
            SESoundData data = category.sounds.Find(sound => sound.data == se);
            if(data != null) 
            {
                seSource.volume = data.seVolume * masterVolume * semasterVolume;
                seSource.PlayOneShot(data.seClip);
            }
            else { Debug.LogError("指定されたSEが見つからない" + se); }
        }
        else { Debug.LogError("指定されたカテゴリが見つからない" + categoryName); }
    }

    //特定のSEの再生
    public void PlayspecificSE(string categoryName,int index) 
    {
        SECategory category = seCategories.Find(category => category.categoryName == categoryName);
        if (category != null)
        {
            if (index >= 0 && index < category.sounds.Count)
            {
                SESoundData data = category.sounds[index];
                seSource.volume = data.seVolume * masterVolume * semasterVolume;
                seSource.PlayOneShot(data.seClip);
            }
            else { Debug.LogError("指定されたSEのインデックスが見つかりません。" + index + categoryName); }
        }
        else { Debug.LogError("指定されたカテゴリが見つかりません。" + categoryName); }
    }

    /// <summary> /// BGMの停止 /// </summary>
    public void BGMStop()
    {
        bgmSource.Stop();
    }

    /// <summary>　/// BGMの一時停止　/// </summary>
    private void BGMPause()
    {
        bgmSource.Pause();
    }

    /// <summary>　/// BGMの再開　/// </summary>
    private void BGMunPause()
    {
        bgmSource.UnPause();
    }

}
