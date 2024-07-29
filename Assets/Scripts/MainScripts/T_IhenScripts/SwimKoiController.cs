using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class SwimKoiController : MonoBehaviour
{
    //タイムライン
    [SerializeField] GameObject swimKoi;
    // [SerializeField] PlayableDirector swimTimeLine;

    private void Start()
    {
        //プレイヤーが一定の場所にいくまではタイムライン非活性
        swimKoi.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        //タイムライン活性化と再生
        swimKoi.SetActive(true);
    }

    void OnDestroy()
    {
        swimKoi = null;
        // swimTimeLine = null;
    }
}
