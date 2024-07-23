using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Playables;
using System.Linq.Expressions;

//エンディング分岐のトリガー
//接触したらプレイヤー停止、二択を選ぶ
public class EndingEventTrigger : MonoBehaviour
{
    //エンディング分岐パネル
    public GameObject selectEndingCanvas;
    //プレイヤー
    public Transform player;

    //タイムライン
    public PlayableDirector endingTimeline;

    //狐面
    public GameObject kitsune;

    private void Start()
    {
        kitsune.SetActive(false);
    }


    //シグナルで呼び出す
    //分岐パネルを表示し、timelineを一時停止する
    public void SelectEvent()
    {
        //タイムライン一時停止
        endingTimeline.Pause();

        //カーソル非固定、可視化
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        //分岐パネル活性
        selectEndingCanvas.SetActive(true);
    }

    //シグナルで呼び出す
    //狐のお面を出現させる
    public void ShowKitsune()
    {
        kitsune.SetActive(true);
    }
}
