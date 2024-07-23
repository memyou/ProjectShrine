using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class TimeLineController : MonoBehaviour
{
    //タイムラインの取得
    public PlayableDirector director;
    public bool isPlay;

    //スクリプトが有効になった時、イベントハンドラ設定
    private void OnEnable()
    {
        isPlay = false;
        director.stopped += OnPlayableDirectorStopped;
    }

    //停止時に呼び出されるメソッド
    void OnPlayableDirectorStopped(PlayableDirector aDirector)
    {
        if (director == aDirector)
        {
            //停止時の処理
            isPlay = true;
            Debug.Log("完了");

            //自gameobjectの非活性化
            gameObject.SetActive(false);
        }
    }

    //スクリプトが無効になった時、イベントハンドラ解除
    private void OnDisable()
    {
        director.stopped -= OnPlayableDirectorStopped;
        gameObject.SetActive(false);

    }
}
