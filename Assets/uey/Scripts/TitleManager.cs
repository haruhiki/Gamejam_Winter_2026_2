using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    private bool isResult = false;

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if (isResult)
        {
            Result();
        }
    }

    public void GoGame()
    {
        isResult = true;
        SceneManager.LoadScene("title");
    }

    private void Result() 
    {
        //タイトルシーンに戻る
        if (Input.GetKey(KeyCode.Return))
        {
            SceneManager.LoadScene("title");
        }
    }

  
   
}
