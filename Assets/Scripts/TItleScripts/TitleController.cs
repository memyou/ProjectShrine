using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;
using UnityEditor;


public class TitleController : MonoBehaviour
{
    //タイトル画面
    public GameObject titleMenu;

    // //遷移前表示するUI
    public GameObject infoText;

    //タイトル背景画面
    public GameObject titleImg;

    // //シーン遷移前のフェードアウト用パネル
    // public GameObject fadePanel;

    //フェードアウト
    public FadeController fade;

    //Audio
    AudioSource audioSource;
    public AudioClip suzu;

    //遷移用bool
    bool isClicked;
    bool toGameScene;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        infoText.SetActive(false);
    }

    void Update()
    {
        if (isClicked)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                toGameScene = true;
                isClicked = false;
            }
        }
    }

    public void OnStartButtonClicked()
    {
        isClicked = true;

        // StartCoroutine(GameStart());
        //SceneManager.LoadScene("Main");

        StartCoroutine(ToGame());
    }

    // IEnumerator GameStart()
    // {
    //     //ボタンを押されたら
    //     ShowManu();

    //     //toGameScene=true==trueでシーン遷移
    //     yield return new WaitUntil(() => toGameScene == true);

    //     //フェードインする
    //     fadePanel.SetActive(true);
    //     fadePanel.GetComponent<Image>().DOFade(1.0f, 1.0f);

    //     yield return new WaitForSeconds(1.0f);

    //     Initiate.Fade("Main", Color.black, 1.0f);

    //     yield break;
    // }

    void ShowManu()
    {
        //titleMenuを非活性
        titleMenu.SetActive(false);
        //titleImgをA5A5A5に
        titleImg.GetComponent<Image>().color = new Color(165f / 255f, 165f / 255f, 165f / 255f, 255f / 255f);
        //infoText表示
        infoText.SetActive(true);
    }

    IEnumerator ToGame()
    {
        ShowManu();

        yield return new WaitUntil(() => toGameScene == true);

        audioSource.PlayOneShot(suzu);

        fade.DoFadeIn("Main");

        yield break;
    }
}
