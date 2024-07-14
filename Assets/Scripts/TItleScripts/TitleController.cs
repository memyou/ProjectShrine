using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using Unity.VisualScripting;
using System.Threading;

public class TitleController : MonoBehaviour
{
    //タイトル画面
    public GameObject titlePanel;

    //遷移前表示するUI
    public GameObject infoPanel;

    //遷移用bool
    bool isClicked;
    bool toGameScene;

    void Start()
    {
        infoPanel.SetActive(false);
    }

    void Update()
    {
        if (isClicked)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                toGameScene = true;
            }
        }
    }

    public void OnStartButtonClicked()
    {
        isClicked = true;

        StartCoroutine(GameStart());
        //SceneManager.LoadScene("Main");
    }

    IEnumerator GameStart()
    {
        //ボタンを押されたら
        infoPanel.SetActive(true);
        titlePanel.SetActive(false);

        yield return new WaitUntil(() => toGameScene == true);

        Initiate.Fade("Main", Color.black, 1.0f);

        yield break;
    }

}
