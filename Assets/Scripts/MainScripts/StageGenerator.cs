using System.Collections;
using System.Collections.Generic;
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

    // Start is called before the first frame update
    void Start()
    {
        currentChipIndex = startChipIndex - 1;
        UpdateStage(preInstantiate);

    }

    // Update is called once per frame
    void Update()
    {
        //現在のステージチップのインデックス
        int charaPositionIndex = CharaStageChipIndex();
        // Debug.Log("charaposIndex:" + charaPositionIndex);
        // Debug.Log("いまここ：" + stages.transform.GetChild(1).gameObject.name);

        //次のステージチップに入ったらステージの更新処理をおこなう
        if (charaPositionIndex + preInstantiate > currentChipIndex)
        {
            UpdateStage(charaPositionIndex + preInstantiate);
        }
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
        // Debug.Break();

        return stageObject;
    }

    //Stageオブジェクトのリスト番号をランダムにピックアップ
    public int NextStage()
    {
        return Random.Range(0, stageChips.Length);
    }

    //一番古いステージを削除
    void DestroyOldestStage()
    {
        GameObject oldStage = generatedStageList[0];
        generatedStageList.RemoveAt(0);
        Destroy(oldStage);
    }
}
