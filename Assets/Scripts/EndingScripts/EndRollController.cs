using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.SceneManagement;


//エンドロールのコントロール

public class EndRollController : MonoBehaviour
{
    //キャンバス取得
    public RectTransform endRollCanvas;
    //エンドロールパネル
    public GameObject endRollPanel;

    //ED曲
    AudioSource music;

    //TMPro取得
    public TextMeshProUGUI endRollTitle;
    public TextMeshProUGUI endRollText;
    public TextMeshProUGUI endRollMsg;
    public TextMeshProUGUI keyInfo;

    //テキストボックスのサイズ取得
    float titleBoxSize;
    float textBoxSize;
    float msgBoxSize;

    // //画面の縦のサイズ
    float screenHeight;

    //キャンバスの縦サイズ
    // float canvasHeight;

    //テキストのスクロールスピード
    public float textScrollSpeed = 30f;

    //トリガー
    bool isOutTitle;
    bool isOutText;
    bool isStopMsg;

    //コルーチン
    Coroutine endRollTitleCoroutine;
    Coroutine endRollMsgCoroutine;

    void Awake()
    {
        //ED曲取得
        music = GetComponent<AudioSource>();
        keyInfo.enabled = false;
    }

    void Start()
    {
        //画面のサイズを取得
        //currentResolutionある方がいいのか？ない方がいいのか？
        screenHeight = Screen.height;

        //キャンバスの縦サイズ取得
        // canvasHeight = endRollCanvas.rect.height;
        // Debug.Log("canvasY" + canvasHeight);

        Debug.Log(screenHeight);
        // Debug.Break();

        //テキストボックスのサイズ取得
        titleBoxSize = endRollTitle.preferredHeight;
        textBoxSize = endRollText.preferredHeight;
        msgBoxSize = endRollMsg.preferredHeight;

        // Debug.Log(textBoxSize);
        // Debug.Break();

        //全テキストボックスの位置を調整
        SetPosition();

        //ED再生
        music.Play();
    }

    void FixedUpdate()
    {
        //スペースキー押すとスキップしてタイトルへ
        if (Input.GetKeyDown(KeyCode.Space))
        {
            endRollMsgCoroutine = StartCoroutine(GoToStartScene());
        }

        //エンドロールを下から上にスクロール
        //タイトルのスクロール
        if (!isOutTitle)
        {
            //isOutTitle:falseなら3秒停止後にスクロール
            endRollTitleCoroutine = StartCoroutine(StartEndRollTitle());
        }

        //テキスト:isOutTitle:trueかつisOutText:falseならtextのスクロール実行
        if (isOutTitle && !isOutText)
        {
            // float limit = textBoxSize + canvasHeight / 2;
            float limit = textBoxSize;
            if (limit <= endRollText.rectTransform.localPosition.y)
            {
                //画面上辺中央にきたらfalse
                isOutText = true;
                //画面外に出たらオブジェクトをたたむ
                endRollText.enabled = false;
            }

            //スクロール
            ScrollStart(endRollText);
        }

        //メッセージ:isOutTitle:trueかつisOutText:trueかつisStopMsg:false
        //メッセージを中央まで送って停止、その後タイトルへ
        if (isOutTitle && isOutText && !isStopMsg)
        {
            //中央で止まる
            if (endRollMsg.rectTransform.localPosition.y >= 0)
            {
                isStopMsg = true;
            }

            ScrollStart(endRollMsg);


        }
        else if (isOutText && isOutText && isStopMsg)
        {
            endRollMsgCoroutine = StartCoroutine(GoToStartScene());
        }
    }

    //スクロールをする
    void ScrollStart(TextMeshProUGUI endRoll)
    {
        //スペースキーで早送り：３倍速
        if (Input.GetKey(KeyCode.LeftShift) | Input.GetKey(KeyCode.RightShift))
        {
            endRoll.transform.position = new Vector2(endRoll.transform.position.x,
                endRoll.transform.position.y + textScrollSpeed * 3 * Time.deltaTime);
        }
        else
        {
            endRoll.transform.position = new Vector2(endRoll.transform.position.x,
                endRoll.transform.position.y + textScrollSpeed * Time.deltaTime);
        }
    }

    //初期位置設定
    void SetPosition()
    {
        //タイトル位置
        float center = titleBoxSize / 2;
        endRollTitle.rectTransform.localPosition = new Vector3(0, center, 0);

        //テキストの位置、下辺中央
        // float bottom = canvasHeight / 2;
        float bottom = screenHeight / 2;
        // Debug.Log("画面縦サイズ" + canvasHeight);
        // Debug.Log("画面の半分" + bottom);


        endRollText.rectTransform.localPosition = new Vector3(0, -bottom, 0);

        //メッセージの位置、下辺中央
        endRollMsg.rectTransform.localPosition = new Vector3(0, -bottom, 0);

        // Debug.Break();
    }

    //タイトルを3秒待った後にスクロールさせ、画面外に出たらたたむ
    IEnumerator StartEndRollTitle()
    {
        //３秒まつ
        yield return new WaitForSeconds(3f);

        keyInfo.enabled = true;

        //画面外に出たら
        // float limit = titleBoxSize + canvasHeight / 2;
        float limit = titleBoxSize + screenHeight / 2;
        if (limit <= endRollTitle.rectTransform.localPosition.y)
        {
            //フラグをtrue
            isOutTitle = true;

            //trueになったらオブジェクトをたたむ
            endRollTitle.enabled = false;
        }
        else
        {
            //スクロール
            ScrollStart(endRollTitle);
        }

        //コルーチン停止
        StopCoroutine(endRollTitleCoroutine);
    }

    //3秒停止後、タイトルシーンへ移行
    IEnumerator GoToStartScene()
    {
        //3秒停止
        yield return new WaitForSeconds(3f);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        //コルーチン停止とタイトル遷移
        StopCoroutine(endRollMsgCoroutine);
        SceneManager.LoadScene("TitleScene");
    }

}
