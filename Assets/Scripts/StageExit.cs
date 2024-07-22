using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using Unity.VisualScripting;
using UnityEngine;

//スコア計算タイミングと計算を行う
public class StageExit : MonoBehaviour
{
    //スコア判定に使用するオブジェクト
    public GameObject stage;
    public GameObject yashiro;
    public GameObject inari;

    public GameController gameController;

    //ステージを進んできているか
    public StageExitArea stageExitArea;

    //判定範囲内に入ったか
    bool isEnter;

    void Start()
    {
        //GameController取得
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
        stage = yashiro.transform.parent.gameObject.transform.parent.gameObject;
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log(stage);

        //プレイヤー侵入時
        if (other.CompareTag("Player"))
        {
            //ステージを実際に進んできていたら
            if (stageExitArea.GetIsEnter())
            {
                //isEnter=falseの時に一度だけ判定する
                if (!isEnter)
                {
                    if (stage.CompareTag("DefaultSushi"))
                    {
                        Debug.Log("異変なし、稲荷" + inari.activeSelf);
                        //trueならポイントリセット
                        if (inari.activeSelf) { gameController.ResetPoint(); }
                        //falseなら加算
                        if (!inari.activeSelf) { gameController.AddPoint(); }
                    }

                    //tag=otherSushiの時：異変あり
                    if (stage.CompareTag("OtherSushi"))
                    {
                        Debug.Log("異変あり、稲荷" + inari.activeSelf);
                        //trueならポイント加算
                        if (inari.activeSelf) { gameController.AddPoint(); }
                        //falseならリセット
                        if (!inari.activeSelf) { gameController.ResetPoint(); }
                    }

                    //isEnter=trueに
                    isEnter = true;
                }
            }
        }
    }
}