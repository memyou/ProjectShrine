using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReAshioto : MonoBehaviour
{
    //コンポーネント
    [SerializeField] AudioSource audioSource;

    //プレイヤー
    [SerializeField] Rigidbody playerRb;

    //プレイヤーの移動状態を判定
    float checkMin = -1;
    float checkMax = 1;

    bool isPlayerMove;

    float moveX, moveZ;

    void FixedUpdate()
    {
        if (playerRb != null)
        {
            //移動状態のチェック
            MoveCheck();

            //移動していたら鳴らす
            SePlay();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerRb = other.gameObject.transform.parent.GetComponent<Rigidbody>();
        }
    }

    void MoveCheck()
    {
        moveX = playerRb.velocity.x;
        moveZ = playerRb.velocity.z;

        if (checkMin < moveX & moveX < checkMax & checkMin < moveZ & moveZ < checkMax) { isPlayerMove = false; }
        else { isPlayerMove = true; }
    }

    void SePlay()
    {
        if (isPlayerMove) { audioSource.pitch = 1; return; }
        audioSource.pitch = 0f;
        audioSource.Play();
    }

    void OnDestroy()
    {
        audioSource = null;
        playerRb = null;
    }
}
