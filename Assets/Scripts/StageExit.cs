using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using Unity.VisualScripting;
using UnityEngine;

//スコア計算タイミングと計算を行う
public class StageExit : MonoBehaviour
{
    //スコア判定に使用するオブジェクト
    [SerializeField] GameObject stage;
    [SerializeField] GameObject yashiro;
    [SerializeField] GameObject inari;

    [SerializeField] GameController gameController;

    //ステージを進んできているか
    [SerializeField] StageExitArea stageExitArea;

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
        // Debug.Log(stage);

        //プレイヤー侵入時
        if (other.CompareTag("Player"))
        {
            //ステージを逆走していたら
            if (!stageExitArea.GetIsEnter())
            {
                //侵入フラグtrue
                isEnter = true;
            }

            //ステージを実際に進んできていたら
            else
            {
                //isEnter=falseの時に一度だけ判定する
                if (!isEnter)
                {
                    //侵入フラグtrue
                    isEnter = true;

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
                        if (inari.activeSelf)
                        {
                            gameController.AddPoint();

                            //発見し、対応できた異変を格納
                            gameController.GetAchieveList().Add(stage);
                            gameController.SetAchive(gameController.GetAchieveList().Count());

                            // Debug.Log(gameController.GetAchieveList().Count());
                        }
                        //falseならリセット
                        if (!inari.activeSelf) { gameController.ResetPoint(); }
                    }
                }
            }
        }
    }
}