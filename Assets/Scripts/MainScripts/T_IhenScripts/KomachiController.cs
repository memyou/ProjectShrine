using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

/*
プレイヤーからのレイが当たったら小町の仮面が正面に出現
*/
public class KomachiController : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] GameObject orientation;
    [SerializeField] LayerMask komachi;

    [SerializeField] GameObject omen;

    bool haveOrientation;
    bool showOmen;

    void Start()
    {
        omen.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        //トリガーを通過したらplayerのorientation取得する
        player = other.gameObject.transform.parent.gameObject;
        orientation = player.transform.GetChild(1).gameObject;

        haveOrientation = true;
    }

    void Update()
    {
        if (haveOrientation)
        {
            //orientation：視線の方向にrayを飛ばす
            Ray ray = new Ray(orientation.transform.position, orientation.transform.forward);
            if (Physics.Raycast(ray, out RaycastHit hit, 1f, komachi))
            {
                //komachiレイヤーのコライダーにレイがぶつかったら一度だけ小町の仮面が出現する
                if (!showOmen)
                {
                    omen.SetActive(true);
                    StartCoroutine(ShowOmen());
                }
            }
        }
    }

    IEnumerator ShowOmen()
    {
        showOmen = true;

        yield return new WaitForSeconds(2f);
        omen.SetActive(false);

        yield break;
    }

    void OnDestroy()
    {
        player = orientation = omen = null;
    }
}
