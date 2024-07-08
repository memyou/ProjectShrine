using System;
using System.Collections;
using System.Collections.Generic;
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

    void Start()
    {
        playerController = player.GetComponent<PlayerController>();
        point = 0;
    }

    void Update()
    {
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

    public int GetPoint() { return point; }

}
