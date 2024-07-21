using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

/*
プレイヤーに処理を追加しないで実装できるようにする
プレイヤーがキャスト内に侵入したら、UIではがす指示表示する
はがされたらオブジェクト破棄してはがした数カウント
*/

public class Kaminingyou : MonoBehaviour, IInteractable
{
    // public TextMeshProUGUI peelOffText;

    //キャスト用情報
    public float radius; //キャスト半径
    public LayerMask targetLayer; //キャストが取得するレイヤー：player

    Collider[] result = new Collider[1]; //取得するコライダー,プレイヤーのみなので１でOK

    private void Start()
    {

    }

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

    // //円の可視化
    // private void OnDrawGizmos()
    // {
    //     Gizmos.color = Color.red;
    //     Gizmos.DrawWireSphere(transform.position, radius);
    // }

    public void Interact()
    {
        //インタラクト時の処理、はがされたらオブジェクト破棄
        if (Input.GetKeyDown(KeyCode.F))
        {
            Debug.Log("はがした");
            KaminingyouDirector.count--;
            Destroy(gameObject);
        }

    }
}
