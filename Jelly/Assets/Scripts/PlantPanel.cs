using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlantPanel : MonoBehaviour
{
    public Text buttonText;
    public Text subText;
    public Button btn;
    // Start is called before the first frame update
    void Start()
    {
        buttonText.text = $"{GameManager.manager.numGoldList[GameManager.manager.numLevel]}";
        subText.text = $"�������뷮 {GameManager.manager.numLevel}";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Num()
    {
        GoldCoin goldCoin = GameObject.Find("RightText").GetComponent<GoldCoin>();
        if (goldCoin.gold >= GameManager.manager.numGoldList[GameManager.manager.numLevel])
        {
            if (GameManager.manager.numLevel >= GameManager.manager.numGoldList.Length - 2)
            {
                GameManager.manager.numLevel = 5;
                subText.text = "�������뷮 10";
                btn.gameObject.SetActive(false);
                return;
            }
            goldCoin.gold = goldCoin.gold - GameManager.manager.numGoldList[GameManager.manager.numLevel];
            GameManager.manager.numLevel += 1;
            subText.text = $"�������뷮 {GameManager.manager.numLevel*2}";
            buttonText.text = $"{GameManager.manager.numGoldList[GameManager.manager.numLevel]}";
        }
    }

    public void Click()
    {

    }
}
