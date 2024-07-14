using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;

public class GameController : MonoBehaviour
{
    //フィールド生成用スクリプト
    public StageGenerator stageGenerator;

    //プレイヤーとそのスクリプト
    public GameObject player;
    PlayerController playerController;

    //進行状況用変数
    int point;

    //ノルマ数
    const int MAX_STAGE = 5;

    //ポーズ画面を開いているかどうか
    bool isPause;
    //ポーズ画面用UI
    public GameObject pauseUI;

    void Start()
    {
        playerController = player.GetComponent<PlayerController>();
        point = 0;

        pauseUI.SetActive(false);
    }

    void Update()
    {
        //Pauseのチェック
        CheckedPause();

        //pointがMAX_STAGEと同数であれば
        if (point == MAX_STAGE)
        {
            enabled = false;

            //２秒後に遷移メソッド呼び出し
            Invoke("TurnToEnd", 2.0f);
        }
    }

    void TurnToEnd()
    {
        //黒フェードして遷移
        Initiate.Fade("Ending-T", Color.black, 1.0f);
    }

    public void AddPoint()
    {
        point += 1;
    }

    public void ResetPoint()
    {
        point = 0;
    }

    //ポイント参照
    public int GetPoint() { return point; }

    //ポーズ画面を表示する
    void CheckedPause()
    {
        //isPause=falseの時にH_keyでisPause=true
        if (!isPause)
        {
            if (Input.GetKeyDown(KeyCode.H))
            {
                Debug.Log("表示H押した");
                //H_key押したら
                isPause = true;

                StartCoroutine(Pause());
            }
        }
        else
        {//isPause=trueの時にH_keyでisPause=false
            if (Input.GetKeyDown(KeyCode.H))
            {
                isPause = false;
            }
        }
    }

    //ポーズ
    IEnumerator Pause()
    {
        // H_keyが押された時の処理
        //pauseUIを活性化
        pauseUI.SetActive(true);

        //isPause=false==trueになったら
        yield return new WaitUntil(() => isPause == false);

        //pauseUIを非活性化
        pauseUI.SetActive(false);

        // //コルーチン終了
        yield break;
    }
}
