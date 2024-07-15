using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;


public class TitleController : MonoBehaviour
{
    //タイトル画面
    public GameObject titleMenu;

    // //遷移前表示するUI
    public GameObject infoText;

    //タイトル背景画面
    public GameObject titleImg;

    //シーン遷移前のフェードアウト用パネル
    public GameObject fadePanel;


    //遷移用bool
    bool isClicked;
    bool toGameScene;

    void Start()
    {
        infoText.SetActive(false);
        fadePanel.SetActive(false);
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
        //titleMenuを非活性
        titleMenu.SetActive(false);
        //titleImgをA5A5A5に
        titleImg.GetComponent<Image>().color = new Color(165f / 255f, 165f / 255f, 165f / 255f, 255f / 255f);
        //infoText表示
        infoText.SetActive(true);

        //toGameScene=true==trueでシーン遷移
        yield return new WaitUntil(() => toGameScene == true);

        //フェードインする
        fadePanel.SetActive(true);
        fadePanel.GetComponent<Image>().DOFade(1.0f, 1.0f);

        yield return new WaitForSeconds(1.0f);

        Initiate.Fade("Main", Color.black, 1.0f);

        yield break;
    }

}
