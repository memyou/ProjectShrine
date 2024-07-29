using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;

public class StageInteract : MonoBehaviour
{
    //ゲームオブジェクト
    [SerializeField] GameObject ruleUI;
    [SerializeField] GameObject ruleImage;
    [SerializeField] GameObject remy;
    [SerializeField] GameObject inari;

    //イメージ表示状態
    bool isShowImage;

    //remy起動場所まで侵入したかどうか
    bool moveRemy;

    void Start()
    {
        ruleUI.SetActive(false);
        ruleImage.SetActive(false);
        inari.SetActive(false);
        remy.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (!moveRemy)
        {
            if (other.CompareTag("Player"))
            {
                moveRemy = true;

                //コライダー侵入でUI表示
                ruleUI.SetActive(true);

                //remyを起動
                remy.SetActive(true);
            }
        }

    }


    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (!isShowImage)
            {
                if (Input.GetKeyDown(KeyCode.F))
                {
                    isShowImage = true;
                    StartCoroutine(RuleImage());
                }
            }
            else
            {
                //表示状態でｆキー、イメージ非表示
                if (Input.GetKeyDown(KeyCode.F))
                {
                    isShowImage = false;
                }
            }

        }
    }

    IEnumerator RuleImage()
    {
        //trueの時
        ruleImage.SetActive(true);

        //isShowImage=false==trueの時
        yield return new WaitUntil(() => isShowImage == false);

        ruleImage.SetActive(false);

        yield break;

    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //コライダー出たらUI非表示、もしイメージ出したままなら非表示
            ruleUI.SetActive(false);
            ruleImage.SetActive(false);
        }
    }

    void OnDestroy()
    {
        ruleUI = ruleImage = remy = inari = null;
    }
}
