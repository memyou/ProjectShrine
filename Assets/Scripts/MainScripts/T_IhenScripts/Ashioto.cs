using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Ashioto : MonoBehaviour
{
    //追従相手
    public GameObject target;
    public Rigidbody targetRb;

    //追従相手のx z
    float moveX;
    float moveZ;

    //追従相手の移動を判断する最少と最大
    float checkMin = -1;
    float checkMax = 1;

    //対象の挙動
    public bool targetIsMove;

    //足音
    public AudioSource se;

    // void Start()
    // {
    //     targetRb = target.GetComponent<Rigidbody>();
    //     se = GetComponent<AudioSource>();
    // }

    void Update()
    {
        //対象が移動しているかどうかをチェックする
        MoveCheck();

        //SE、対象が移動していたら鳴らす
        SePlay();
    }

    //歩いていたら足音を鳴らす
    void SePlay()
    {
        if (targetIsMove) { return; }
        se.Play();
    }

    //対象の位置を確認
    void MoveCheck()
    {
        moveX = targetRb.velocity.x;
        moveZ = targetRb.velocity.z;

        //相手のvelocity、xとzが-1~1を超えていたら移動しているとみなす
        if (checkMin < moveX & moveX < checkMax & checkMin < moveZ & moveZ < checkMax)
        {
            // Debug.Log(targetIsMove + "止まってる判定");
            targetIsMove = false;
        }
        else
        {
            // Debug.Log(targetIsMove + "動いている判定");
            targetIsMove = true;
        }
    }
}