using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    //フィールド生成用スクリプト
    public StageGenerator stageGenerator;

    //進行状況用変数
    int point;

    //ノルマ数
    const int MAX_STAGE = 5;

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

    void OnTriggerEnter(Collider other)
    {
        //otherの親オブジェクトを取得し、inariを探す
        GameObject stage = other.gameObject.transform.parent.gameObject;
        GameObject sushi = stage.transform.Find("Inari").gameObject;

        //tagがdefaultSushiの時：異変のないステージの時
        if (other.CompareTag("DefaultSushi"))
        {
            if (sushi.activeSelf == true)
            {
                point = 0;
            }
            if (sushi.activeSelf == false)
            {
                point += 1;
            }
        }

        //tagがotherSushiの時：異変のあるステージの時
        if (other.CompareTag("OtherSushi"))
        {
            if (sushi.activeSelf == true)
            {
                point += 1;
            }
            if (sushi.activeSelf == false)
            {
                point = 0;
            }
        }

        //道案内看板文字表示変更
        if (other.CompareTag("Paper"))
        {
            GameObject papers = stage.transform.Find("TurnPapers").gameObject;

            if (point == 0)
            {
                GameObject paper = papers.transform.Find("5nomine").gameObject;
                paper.SetActive(true);
            }
            if (point == 1)
            {
                GameObject paper = papers.transform.Find("4nomine").gameObject;
                paper.SetActive(true);
            }
            if (point == 2)
            {
                GameObject paper = papers.transform.Find("3nomine").gameObject;
                paper.SetActive(true);
            }
            if (point == 3)
            {
                GameObject paper = papers.transform.Find("2nomine").gameObject;
                paper.SetActive(true);
            }
            if (point == 4)
            {
                GameObject paper = papers.transform.Find("1nomine").gameObject;
                paper.SetActive(true);
            }
        }

    }
}
