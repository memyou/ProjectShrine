using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AshiotoController : MonoBehaviour
{
    //足音
    public GameObject gO_Ashioto;

    //通過した時にプレイヤーを別スクリプトに格納
    Ashioto ashioto;

    void Start()
    {
        gO_Ashioto.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        //トリガーに入ったら
        gO_Ashioto.SetActive(true);

        //足音のスクリプト取得
        ashioto = gO_Ashioto.GetComponent<Ashioto>();

        //ashioto.targetにプレイヤー代入
        ashioto.target = other.gameObject.transform.parent.gameObject;

        //ashioto.targetRbにプレイヤーの親rigidbody代入
        ashioto.targetRb = other.gameObject.transform.parent.GetComponent<Rigidbody>();

        //ashioto.seにashiotoのAudioSource代入
        ashioto.se = gO_Ashioto.GetComponent<AudioSource>();
    }
}
