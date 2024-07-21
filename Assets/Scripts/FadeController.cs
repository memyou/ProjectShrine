using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class FadeController : MonoBehaviour
{
    //フェードに使用するパネル
    public Image fadePanel;

    //フェード時間
    public float fadeDuration = 1.0f;

    //経過時間
    // float time;

    // //フェードの状況
    // bool isFade;

    //フェードで使用する
    // Color startColor, endColor;

    //パネル設定の色を使用するFadeIn
    public void DoFadeIn()
    {
        StartCoroutine(FadeIn());
    }

    //パネル設定の色を使用するシーン遷移つきFadeIn
    public void DoFadeIn(string nextSceneName)
    {
        StartCoroutine(FadeIn(nextSceneName));
    }

    //色を黒に指定したfadeOut
    public void DoFadeOut()
    {
        StartCoroutine(FadeOut());
    }

    //任意の色を渡すFadeIn,FadeOut
    public void DoFade(Color startColor, Color endColor)
    {
        StartCoroutine(Fade(startColor, endColor));
    }

    //任意の色を渡してシーン遷移するFadeIn
    public void DoFadeIn(Color startColor, Color endColor, string nextSceneName)
    {
        StartCoroutine(Fade(startColor, endColor, nextSceneName));
    }

    // //連続してfadeInOutする
    // public void DoFadeInToOut()
    // {
    //     StartCoroutine(FadeInToOut());
    // }

    // IEnumerator FadeInToOut()
    // {
    //     startColor = fadePanel.color;
    //     endColor = new Color(startColor.r, startColor.g, startColor.b, 1.0f);

    //     yield return StartCoroutine(Fade(startColor, endColor));

    //     yield return StartCoroutine(Fade(endColor, startColor));

    //     fadePanel.enabled = false;

    //     yield break;
    // }


    //fadeIn
    public IEnumerator FadeIn()
    {
        //フェード開始時の色
        Color startColor = fadePanel.color;
        //終了時の色
        Color endColor = new Color(startColor.r, startColor.g, startColor.b, 1.0f);

        yield return StartCoroutine(Fade(startColor, endColor));

        fadePanel.enabled = false;
    }

    public IEnumerator FadeIn(string nextSceneName)
    {
        //フェード開始時の色
        Color startColor = fadePanel.color;
        //終了時の色
        Color endColor = new Color(startColor.r, startColor.g, startColor.b, 1.0f);

        // Debug.Log(startColor);

        StartCoroutine(Fade(startColor, endColor, nextSceneName));

        yield break;
    }

    //fadeOut
    public IEnumerator FadeOut()
    {
        //フェード開始時の色
        Color startColor = new Color(0f, 0f, 0f, 1f);
        //終了時の色
        Color endColor = new Color(startColor.r, startColor.g, startColor.b, 0f);

        yield return StartCoroutine(Fade(startColor, endColor));

        fadePanel.enabled = false;

        yield break;
    }

    /*
    Color startColor,endColorを受け取るfade
    */
    public IEnumerator Fade(Color startColor, Color endColor)

    {
        //fadePanel有効化
        fadePanel.enabled = true;

        //経過時間初期化
        float time = 0.0f;

        //フェード処理
        while (time < fadeDuration)
        {
            time += Time.deltaTime;
            float t = Mathf.Clamp01(time / fadeDuration);

            fadePanel.color = Color.Lerp(startColor, endColor, t);

            yield return null;
        }

        //色を終了時のものに設定
        fadePanel.color = endColor;

        yield break;
    }

    /*
    Fadeのオーバーロード
    Color startColor,endColor,String nextScene
    */
    public IEnumerator Fade(Color startColor, Color endColor, string nextSceneName)
    {
        yield return StartCoroutine(Fade(startColor, endColor));

        SceneManager.LoadScene(nextSceneName);

        yield break;
    }
}
