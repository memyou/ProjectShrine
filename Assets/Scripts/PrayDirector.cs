using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PrayDirector : MonoBehaviour
{
    //お供えに必要なもの
    public GameObject inari;
    public GameObject prayUI;

    AudioSource audioSource;

    //範囲内にいるかどうか
    bool isEnter;

    //祈ったかどうか
    bool isPray;


    void Start()
    {
        prayUI.SetActive(false);
        audioSource = GetComponent<AudioSource>();
    }

    void OnTriggerEnter(Collider other)
    {   //player侵入中はUI表示
        if (other.CompareTag("Player"))
        {
            // Debug.Log("プレイヤーお祈り可能");
            prayUI.SetActive(true);
            isEnter = true;
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (!isPray)
            {
                //侵入中にEキーで稲荷ずしお供え
                if (isEnter && Input.GetKey(KeyCode.E))
                {
                    // Debug.Log("お祈りした");
                    isPray = true;
                    inari.SetActive(true);
                    audioSource.Play();
                    prayUI.SetActive(false);
                }
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        isEnter = false;
        prayUI.SetActive(false);
    }


}
