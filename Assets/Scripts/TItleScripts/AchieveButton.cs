using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AchieveButton : MonoBehaviour
{
    [SerializeField] GameObject BackImage;
    [SerializeField] TextMeshProUGUI GetAchieve;
    [SerializeField] TextMeshProUGUI CloseText;
    int resultScore;

    bool showAchieve;


    // Start is called before the first frame update
    void Start()
    {
        BackImage.SetActive(false);
        resultScore = PlayerPrefs.GetInt("SCORE");
    }

    // Update is called once per frame
    void Update()
    {
        if (showAchieve)
        {
            if (Input.GetKey(KeyCode.F))
            {
                BackImage.SetActive(false);
            }
        }
    }

    // ボタンが押された場合、今回呼び出される関数
    public void OnClick()
    {
        showAchieve = true;
        BackImage.SetActive(true);
        this.GetAchieve.text = $"発見した異変の数は{resultScore}/18個";
        this.CloseText.text = $"F:閉じる";
    }
}
