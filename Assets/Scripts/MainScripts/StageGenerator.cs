using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEditor.SceneManagement;
using UnityEngine;

public class StageGenerator : MonoBehaviour
{
    const int StageChipSize = 70; //ステージの長さ

    int currentChipIndex; //今作成されている一番先頭のチップの最大値


    public Transform character; //プレイヤーの位置
    public GameObject[] stageChips; //ステージチップの種類
    public int startChipIndex;
    public int preInstantiate; //前方にいくつつくっておくか
    public List<GameObject> generatedStageList = new List<GameObject>(); //出現させたステージチップの住所をリスト管理

    //ステージ用親オブジェクト
    public GameObject stages;

    //プレイヤーのスクリプト：振り返り状況を取得する
    public PlayerController playerController;

    //ひとつ前に生成したステージのインデックス
    int beforeStage;
    //現在生成したステージのインデックス
    int nextStage;

    //ワープ先のtransform
    Transform restartPos;

    bool isWarp;

    public GameController gameController;

    //fade
    public FadeController fade;

    //音
    AudioSource audioSource;

    void Start()
    {
        currentChipIndex = startChipIndex - 1;
        UpdateStage(preInstantiate);

        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        //現在のステージチップのインデックス
        int charaPositionIndex = CharaStageChipIndex();

        // // Debug.Log("charaposIndex:" + charaPositionIndex);

        //振り返っていたら次のチップへワープしてスコアをリセット
        if (playerController.GetIsLookBack())
        {
            //fade
            StartCoroutine(PlayerWarp());
        }

        //次のステージチップに入ったらステージの更新処理をおこなう
        if (charaPositionIndex + preInstantiate > currentChipIndex)
        {
            UpdateStage(charaPositionIndex + preInstantiate);
        }
    }

    //ワープ用コルーチン
    IEnumerator PlayerWarp()
    {
        //振り返り状態をfalse
        playerController.SetIsLookBack(false);
        //スコアをリセット
        gameController.ResetPoint();

        //音
        audioSource.Play();

        GameObject stage = stages.transform.GetChild(2).gameObject;
        GameObject mainStage = stage.transform.GetChild(0).gameObject;
        restartPos = mainStage.transform.GetChild(0).gameObject.transform;

        //fadein
        yield return fade.StartCoroutine("FadeIn");

        //isWarp=trueで視線再設定用のフラグをonに
        isWarp = true;

        //プレイヤーの位置をrestartPosにワープ
        character.position = restartPos.position;

        yield return fade.StartCoroutine("FadeOut");

        isWarp = false;

        yield break;
    }

    //現在のステージチップのインデックス
    public int CharaStageChipIndex()
    {
        return (int)(character.position.z / StageChipSize);
    }

    //指定のindexまでのステージチップを生成して管理下に置く
    void UpdateStage(int toChipIndex)
    {
        if (toChipIndex <= currentChipIndex) return;

        //指定のステージチップまでを作成。このfor文は最大五回まわるのでステージはつねに５枚分ある
        for (int i = currentChipIndex + 1; i <= toChipIndex; i++)
        {
            GameObject stageObject = GenerateStage(i);

            //生成したステージチップを住所管理リストに追加
            generatedStageList.Add(stageObject);
        }

        //ステージ保持上限内になるまで古いステージを削除
        while (generatedStageList.Count > preInstantiate + 2) DestroyOldestStage(); //ステージチップが6枚になった瞬間１枚削除

        currentChipIndex = toChipIndex;
    }


    //指定のインデックス位置にStageオブジェクトを生成
    GameObject GenerateStage(int chipIndex)
    {
        int nextStageChip = NextStage();

        GameObject stageObject = Instantiate(
            stageChips[nextStageChip],
            new Vector3(chipIndex * -9.1f, chipIndex * 1.7f, chipIndex * StageChipSize * 0.95f), //今の位置の一個先につくる
            Quaternion.identity
        );

        stageObject.transform.parent = stages.transform;
        // Debug.Log(stageObject.transform.parent.name);

        return stageObject;
    }

    //Stageオブジェクトのリスト番号をランダムにピックアップ
    public int NextStage()
    {
        //0:defaultStage,1~4:ihenをランダムに生成
        //同じステージが連続して出現しないようにする

        //出現するのが異変有りか無しか
        int rand = Random.Range(0, 5);

        //beforeStage=0でなく、rand=0なら異変なし
        if (beforeStage != 0 && rand == 0)
        {
            beforeStage = rand;
            return rand;
        }
        //0以外なら異変有り
        else
        {
            //nextStageにまず生成した数値を格納
            nextStage = Random.Range(1, stageChips.Length);

            //nextStageとbeforeStageを比較
            if (beforeStage != nextStage)
            {
                //違うならbeforeStageにnextStageを格納する
                beforeStage = nextStage;
            }
            else
            {
                //同じならやり直し
                NextStage();
            }

            return nextStage;
        }

    }

    //一番古いステージを削除
    void DestroyOldestStage()
    {
        GameObject oldStage = generatedStageList[0];
        generatedStageList.RemoveAt(0);
        Destroy(oldStage);
    }

    public bool GetIsWarp() { return isWarp; }
    public void SetIsWarp(bool warp) { isWarp = warp; }
}
