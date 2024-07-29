using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;

public class KokeshiController : MonoBehaviour
{
    //看板を抜けると三秒後にこけしがひょっこりはん
    [SerializeField] Transform kokeshi;

    void OnTriggerEnter(Collider other)
    {
        // Debug.Log("入った");
        kokeshi.transform.DOLocalRotate(new Vector3(0, 0, -50), 1f).SetEase(Ease.OutBounce).SetDelay(2f);
    }

    void OnDestroy() { kokeshi = null; }
}
