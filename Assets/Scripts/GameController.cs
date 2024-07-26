using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;

public class GameController : MonoBehaviour
{
    //フィールド生成用スクリプト
    [SerializeField] StageGenerator stageGenerator;

    //プレイヤーとそのスクリプト
    [SerializeField] GameObject player;
    PlayerController playerController;

    //進行状況用変数
    int point;

    //ノルマ数
    const int MAX_STAGE = 5;

    //ヘルプ画面を開いているかどうか
    bool isPause;
    //ヘルプ画面用UI
    [SerializeField] GameObject pauseUI;

    //フェード
    [SerializeField] FadeController fade;


    //異変の発見数を管理する
    HashSet<GameObject> achieveList = new HashSet<GameObject>();
    public int getAchieve;

    void Start()
    {
        //発見した異変の格納
        PlayerPrefs.SetInt("SCORE", getAchieve);
        PlayerPrefs.Save();

        fade.DoFadeOut();

        playerController = player.GetComponent<PlayerController>();
        point = 0;

        pauseUI.SetActive(false);
    }

    void Update()
    {
        //Pauseのチェック
        CheckedHelp();

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
        // Initiate.Fade("Ending-T", Color.black, 1.0f);

        fade.DoFadeIn("Ending-T");
    }

    public void AddPoint()
    {
        point++;
    }

    public void ResetPoint()
    {
        point = 0;
    }

    //ポイント参照
    public int GetPoint() { return point; }

    //ヘルプ画面を表示する
    void CheckedHelp()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            if (!isPause)
            {
                isPause = true;
                StartCoroutine(Help());
            }
            else { isPause = false; }
        }
    }

    //ヘルプ
    IEnumerator Help()
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

    //hashSet
    public HashSet<GameObject> GetAchieveList() { return achieveList; }
    public void SetAchive(int count) { PlayerPrefs.SetInt("SCORE", count); }
}
