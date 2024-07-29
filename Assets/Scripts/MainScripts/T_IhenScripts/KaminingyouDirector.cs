using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;

/*
看板のあたりでカウントダウン開始
挑戦中のBGM：未確定
表示情報：残り枚数、制限時間、ボタン
現状、クリアできなくても何も起こらない
*/
public class KaminingyouDirector : MonoBehaviour
{
    //紙人形配列
    [SerializeField] GameObject[] kamininngyous;
    [SerializeField] int count;

    //カウントダウン
    [SerializeField] float countdown = 30f;

    //入ってから初回のみ挑戦可能
    bool isTry;

    //通過したかどうか
    bool isEnter;

    //チャレンジ成功したかどうか
    bool isChallenge;

    //UI
    [SerializeField] GameObject kaminingyouUI;
    [SerializeField] TextMeshProUGUI kami_count;
    [SerializeField] TextMeshProUGUI timetext;
    [SerializeField] TextMeshProUGUI peelOff;

    //SE
    [SerializeField] AudioClip good;
    [SerializeField] AudioClip bad;
    [SerializeField] AudioClip peel;
    [SerializeField] AudioSource audioSource;

    private void Start()
    {
        kaminingyouUI.SetActive(false);
        count = kamininngyous.Length;

        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        //挑戦初回
        if (!isTry)
        {
            //看板通ったら
            if (isEnter)
            {
                TimeCountdown();
                KaminingyouCount();
                ChalllengeChecked();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //UI可視化
            kaminingyouUI.SetActive(true);

            //侵入フラグtrue
            isEnter = true;
            Debug.Log("通過");
        }
    }


    void TimeCountdown()
    {
        //時間表示
        timetext.text = countdown.ToString("f1") + "sec";

        //challenge成功ならカウントダウンしない
        if (isChallenge) { return; }

        //時間カウントダウン
        countdown -= Time.deltaTime;
    }

    //紙人形の枚数カウント
    void KaminingyouCount()
    {
        //残数表示
        kami_count.text = $"{count}/{kamininngyous.Length}";
        //枚数が０になった時
        if (count == 0)
        {
            isTry = true;

            isChallenge = true;
        }
    }

    //チャレンジの成否チェック
    void ChalllengeChecked()
    {
        //challenge成功したら
        if (isChallenge)
        {
            isTry = true;
            // Debug.Log("成功");
            peelOff.text = "挑戦成功";

            //成功SE流してからUI非活性
            audioSource.PlayOneShot(good);

            StartCoroutine(ResultCoroutine());
        }
        //challenge成功しておらずタイムアップした時
        else if (countdown <= 0)
        {
            isTry = true;

            peelOff.text = "挑戦失敗";

            // Debug.Log("失敗");

            //失敗SE流してからUI非活性
            audioSource.PlayOneShot(bad);

            StartCoroutine(ResultCoroutine());
        }
    }

    IEnumerator ResultCoroutine()
    {
        //三秒待つ
        yield return new WaitForSeconds(3f);

        //UI非活性
        kaminingyouUI.SetActive(false);

        //コルーチン停止
        yield break;
    }

    public void Kami_peelOff()
    {
        audioSource.PlayOneShot(peel);
        count--;
    }

    void OnDestroy()
    {
        kamininngyous = null;
        kaminingyouUI = null;
        kami_count = timetext = peelOff = null;
        good = bad = null;
    }
}