using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

/*
プレイヤーに処理を追加しないで実装できるようにする
プレイヤーがキャスト内に侵入したら、UIではがす指示表示する:できず
はがされたらオブジェクト破棄してはがした数カウント
*/

public class Kaminingyou : MonoBehaviour, IInteractable
{
    //キャスト用情報
    [SerializeField] float radius; //キャスト半径
    [SerializeField] LayerMask targetLayer; //キャストが取得するレイヤー：player

    Collider[] result = new Collider[1]; //取得するコライダー,プレイヤーのみなので１

    // [SerializeField] AudioSource audioSource;
    [SerializeField] KaminingyouDirector kaminingyouDirector;

    // void Start() { }

    private void Update()
    {
        int l = Physics.OverlapSphereNonAlloc(transform.position, radius, result, targetLayer);
        switch (l)
        {
            //プレイヤーが範囲内に入っている時:はがせる
            case 1:
                // Debug.Log("player接近中");
                Interact();
                break;
            //プレイヤーが範囲外にいるとき:はがせない
            default:
                // Debug.Log("player離脱中");
                break;
        }
    }

    public void Interact()
    {
        //インタラクト時の処理、はがされたらオブジェクト破棄
        if (Input.GetKeyDown(KeyCode.F))
        {
            Debug.Log("はがした");
            kaminingyouDirector.Kami_peelOff();
            // Destroy(gameObject);
            // audioSource.Play();
            this.gameObject.SetActive(false);
        }
    }

    void OnDestroy()
    {
        // audioSource.Play();

        result = null;
        // audioSource = null;
        kaminingyouDirector = null;
    }
}