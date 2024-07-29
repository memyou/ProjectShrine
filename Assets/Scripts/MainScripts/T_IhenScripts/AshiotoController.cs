using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AshiotoController : MonoBehaviour
{
    //足音
    [SerializeField] GameObject gO_Ashioto;

    //通過した時にプレイヤーを別スクリプトに格納
    [SerializeField] Ashioto ashioto;

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
        ashioto.SetTarget(other.gameObject.transform.parent.gameObject);

        //ashioto.targetRbにプレイヤーの親rigidbody代入
        ashioto.SetTargetRb(other.gameObject.transform.parent.GetComponent<Rigidbody>());

        //ashioto.seにashiotoのAudioSource代入
        ashioto.SetSe(gO_Ashioto.GetComponent<AudioSource>());
    }

    void OnDestroy()
    {
        gO_Ashioto = null;
        ashioto = null;
    }
}
