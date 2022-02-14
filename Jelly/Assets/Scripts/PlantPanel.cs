using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlantPanel : MonoBehaviour
{
    public Text numButtonText;
    public Text numSubText;
    public Button numButton;

    public Text clickButtonText;
    public Text clickSubText;
    public Button clickButton;


    void Start()
    {
        if (GameManager.manager.clickLevel >= GameManager.manager.clickGoldList.Length - 2)
        {
            clickButton.gameObject.SetActive(false);
        }

        if (GameManager.manager.numLevel >= GameManager.manager.numGoldList.Length - 2)
        {
            numButton.gameObject.SetActive(false);
        }

        numButtonText.text = $"{GameManager.manager.numGoldList[GameManager.manager.numLevel]}";
        numSubText.text = $"�������뷮 {GameManager.manager.numLevel*2}";

        clickButtonText.text = $"{GameManager.manager.numGoldList[GameManager.manager.clickLevel]}";
        clickSubText.text = $"Ŭ�����귮 {GameManager.manager.clickLevel}";
    }

    public void Num()
    {
        GoldCoin goldCoin = GameObject.Find("RightText").GetComponent<GoldCoin>();
        if (goldCoin.gold >= GameManager.manager.numGoldList[GameManager.manager.numLevel])
        {
            if (GameManager.manager.numLevel >= GameManager.manager.numGoldList.Length - 2)
            {
                GameManager.manager.numLevel = 5;
                numSubText.text = "�������뷮 10";
                numButton.gameObject.SetActive(false);
                return;
            }
            goldCoin.gold = goldCoin.gold - GameManager.manager.numGoldList[GameManager.manager.numLevel];
            GameManager.manager.numLevel += 1;
            numSubText.text = $"�������뷮 {GameManager.manager.numLevel*2}";
            numButtonText.text = $"{GameManager.manager.numGoldList[GameManager.manager.numLevel]}";
            GameObject.Find("SoundManager").GetComponent<SoundManager>().PlaySfxPlayer("Unlock");
        }
        else
        {
            GameObject.Find("SoundManager").GetComponent<SoundManager>().PlaySfxPlayer("Fail");
            NoticeManager.Msg("notGold");
        }
    }

    public void Click()
    {
        GoldCoin goldCoin = GameObject.Find("RightText").GetComponent<GoldCoin>();
        if (goldCoin.gold >= GameManager.manager.clickGoldList[GameManager.manager.clickLevel])
        {
            if (GameManager.manager.clickLevel >= GameManager.manager.clickGoldList.Length - 2)
            {
                GameManager.manager.clickLevel = 5;
                clickSubText.text = "Ŭ�����귮 x 5";
                clickButton.gameObject.SetActive(false);
                return;
            }
            goldCoin.gold = goldCoin.gold - GameManager.manager.clickGoldList[GameManager.manager.clickLevel];
            GameManager.manager.clickLevel += 1;
            clickSubText.text = $"Ŭ�����귮 x {GameManager.manager.clickLevel}";
            clickButtonText.text = $"{GameManager.manager.clickGoldList[GameManager.manager.clickLevel]}";
            GameObject.Find("SoundManager").GetComponent<SoundManager>().PlaySfxPlayer("Unlock");
        }
        else
        {
            GameObject.Find("SoundManager").GetComponent<SoundManager>().PlaySfxPlayer("Fail");
            NoticeManager.Msg("notGold");
        }
    }
}
