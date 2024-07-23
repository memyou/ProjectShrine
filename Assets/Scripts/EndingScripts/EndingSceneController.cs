using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Playables;

//エンディングシーンのコントロールを行う
/*
エンディングシーンでの演出
タイムラインで制御
・プレイヤー：自動的に移動
・プレイヤー停止、目の前に二択が出現
    ∟　1：振り返る→左回りでゆっくり視点移動、鳥居の方を向く→出会った異変たちとご対面
    ∟　2：振り返らない→お参りする
・エンドロール再生
*/

public class EndingSceneController : MonoBehaviour
{
    //開始とエンディング１のタイムライン
    public PlayableDirector endingDirector;
    public GameObject endingTimeLine;
    TimeLineController ending;

    //エンディング２のタイムライン
    public PlayableDirector ending2Director;
    public GameObject ending2TimeLine;
    TimeLineController ending2;

    //エンドロール操作
    public GameObject endRollDirector;

    //エンディングイベント操作
    public GameObject endingEventDirector;

    //イベントUI
    public GameObject selectEndingPanel;
    //エンドロールUI
    public GameObject endRollPanel;

    //BGM
    public AudioSource BGM;

    public GameObject ihen;

    //fade
    public FadeController fade;

    private void Awake()
    {
        //タイムライン活性化
        endingTimeLine.SetActive(true);

        //エンディング２非活性
        ending2TimeLine.SetActive(false);
        // ending2Director.enabled = false;

        //endRollDirecter非活性
        endRollDirector.SetActive(false);

        //UI非表示
        Panel_NotActive();
        endRollPanel.SetActive(false);

        //異変非表示
        ihen.SetActive(false);

    }

    private void Start()
    {
        //fadeout
        fade.DoFadeOut();

        //マウスポインタ中央固定、不可視化
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        //プレイヤー(0,1,6)、trigger(0,1,21)の位置まで移動する、タイムラインによる制御
        endingDirector.Play();

    }

    //振り返るを選択
    public void LookBackTrigger()
    {
        //マウスポインタ不可視化
        Cursor.visible = false;

        // Debug.Log("振り返る");

        //選択肢パネル非表示
        Panel_NotActive();

        //異変表示
        // ihen.SetActive(true);

        //コルーチン
        StartCoroutine(E_LookBackCoroutine());
    }

    //振り返らない選択
    public void NotlookBackTorigger()
    {
        //マウスポインタ不可視化
        Cursor.visible = false;
        // Debug.Log("振り返らない");

        //選択肢パネル非表示
        Panel_NotActive();

        //スタートタイムライン非活性
        endingTimeLine.SetActive(false);

        //BGM停止
        BGM.Stop();

        //エンディング２タイムライン活性
        ending2TimeLine.SetActive(true);

        //参拝したらエンドロールへ
        StartCoroutine(E_NotLookBackCoroutine());
    }

    void Panel_NotActive()
    {
        selectEndingPanel.SetActive(false);
    }

    //振り返る
    IEnumerator E_LookBackCoroutine()
    {
        //背後を左回りで振り返る
        //タイムラインを17秒の部分から再生
        endingDirector.time = 17;
        endingDirector.Resume();

        //タイムラインの再生が終わったら
        yield return new WaitUntil(() => !endingTimeLine.activeInHierarchy == true);

        //BGM停止
        BGM.Stop();
        //異変非表示
        // ihen.SetActive(false);

        //エンドロール再生
        endRollPanel.SetActive(true);
        endRollDirector.SetActive(true);

        //コルーチン停止
        yield break;
    }

    //振り返らない
    IEnumerator E_NotLookBackCoroutine()
    {
        //タイムライン再生が終わったら
        yield return new WaitUntil(() => !ending2TimeLine.activeInHierarchy == true);

        //ムービー後、エンドロールへ
        endRollPanel.SetActive(true);
        endRollDirector.SetActive(true);

        //コルーチン停止
        yield break;
    }

}
