using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

//プレイヤーにつける→看板につけるに変更
public class ScoreDirector : MonoBehaviour
{
    //ポイント計算に使用するスクリプト
    [SerializeField] GameController gameController;

    //表示するUI
    [SerializeField] GameObject[] paper;

    void Start()
    {
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
    }
    // void Update()
    // {
    //     Debug.Log($"now point:{gameController.GetPoint()}");
    // }

    void OnTriggerEnter(Collider other)
    {
        //トリガーにPlayerが侵入したら
        if (other.CompareTag("Player"))
        {
            //ポイントに応じた看板を表示
            GameObject info = paper[gameController.GetPoint()];

            info.SetActive(true);
        }
    }
}
