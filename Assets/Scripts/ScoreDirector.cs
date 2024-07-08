using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

//プレイヤーにつける
public class ScoreDirector : MonoBehaviour
{
    //プレイヤー
    public GameObject player;
    public PlayerController playerController;

    //ポイント計算に使用するゲームオブジェクト
    public GameObject inari;

    //ポイント計算に使用するスクリプト
    public GameController gameController;

    void Update()
    {
        Debug.Log($"now point:{gameController.GetPoint()}");
    }

    void OnTriggerEnter(Collider other)
    {
        //paperタグのついたトリガーに侵入したら
        if (other.CompareTag("Paper"))
        {
            GameObject paper = other.transform.gameObject;
            // Debug.Log(paper.name);

            //ポイントに応じた看板を表示
            GameObject info = paper.transform.Find($"{gameController.GetPoint()}nomine").gameObject;
            info.SetActive(true);
        }


        //侵入フラグtrueで判定するようにする
        //トリガー範囲の先に進まず、手前で一度後退した場合にも再度判定が利くように

        //sushiタグのついたトリガーに侵入したら
        if (other.CompareTag("Sushi"))
        {

            //オブジェクトを取得
            GameObject yashiro = other.gameObject.transform.parent.gameObject;
            GameObject stage = yashiro.transform.root.gameObject;

            inari = yashiro.transform.Find("Inari").gameObject;

            Debug.Log(stage.name);
            // Debug.Log(yashiro.name);
            // Debug.Log(inari.name);

            //tag=defaultsushiの時：異変なし
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


        }

    }
}
