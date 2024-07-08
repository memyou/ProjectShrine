using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;

public class StageInteract : MonoBehaviour
{
    //ゲームオブジェクト
    public GameObject ruleUI;
    public GameObject ruleImage;
    public GameObject remy;
    public GameObject inari;

    //コンポーネント
    AudioSource audioSource;

    //イメージ表示可能状態
    bool canShowImage;
    //イメージ表示状態
    bool isShowImage;

    //remy起動場所まで侵入したかどうか
    bool moveRemy;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        ruleUI.SetActive(false);
        ruleImage.SetActive(false);
        inari.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (!moveRemy)
        {
            if (other.CompareTag("Player"))
            {
                moveRemy = true;
                canShowImage = true;
                // Debug.Log(canShowImage);

                //コライダー侵入でUI表示
                ruleUI.SetActive(true);

                //remyを起動
                remy.SetActive(true);
            }
        }

    }

    void OnTriggerStay(Collider other)
    {
        //fキー押下でイメージ表示
        if (Input.GetKeyDown(KeyCode.F))
        {
            // Debug.Log("F-key");
            isShowImage = true;
            ruleImage.SetActive(true);
            canShowImage = true;
        }

        //表示状態でｆキー離すとイメージ非表示
        if (isShowImage && Input.GetKeyUp(KeyCode.F))
        {
            ruleImage.SetActive(false);
            canShowImage = false;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canShowImage = false;

            //コライダー出たらUI非表示、もしイメージ出したままなら非表示
            ruleUI.SetActive(false);
            ruleImage.SetActive(false);
        }
    }
}
