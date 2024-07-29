using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FadeController : MonoBehaviour
{
    //フェードに使用するパネル
    [SerializeField] Image fadePanel;

    //フェード時間
    [SerializeField] float fadeDuration = 1.0f;

    //パネル設定の色を使用するFadeIn
    public void DoFadeIn() { StartCoroutine(FadeIn()); }

    //パネル設定の色を使用するシーン遷移つきFadeIn
    public void DoFadeIn(string nextSceneName) { StartCoroutine(FadeIn(nextSceneName)); }

    //パネル設定の色を使用するfadeOut
    public void DoFadeOut() { StartCoroutine(FadeOut()); }

    //fadeIn
    public IEnumerator FadeIn()
    {
        //フェード開始時の色
        Color startColor = fadePanel.color;
        //終了時の色
        Color endColor = new Color(startColor.r, startColor.g, startColor.b, 1.0f);

        yield return StartCoroutine(Fade(startColor, endColor));

        fadePanel.enabled = false;

        yield break;
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
        Color startColor = new Color(fadePanel.color.r, fadePanel.color.g, fadePanel.color.b, 1.0f);
        //終了時の色
        Color endColor = new Color(startColor.r, startColor.g, startColor.b, 0.0f);

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