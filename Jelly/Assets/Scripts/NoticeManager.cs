using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class NoticeManager : MonoBehaviour
{
    public Image NoticeImage;
    public Text NoticeText;
    string start = "모든 젤리를 해금하는 것이 목표.";
    string clear = "모든 젤리를 해금했어요!";
    string sell = "젤리를 드래그해서 주머니에 놓아 팔 수 있어요.";
    string notGelatin = "젤라틴이 부족합니다.";
    string notGold = "골드가 부족합니다.";
    string notNum = "젤리 수용량이 부족합니다";

    float timer;
    bool timeCheck = false;

    bool isNegative = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer = timer + Time.deltaTime;
        if (isNegative)
        {
            NoticeImage.GetComponent<Image>().color = new Color(255/255f, 100/255f, 100/255f,255/255f);
            NoticeText.color = new Color(255, 255, 255, 255);
        }
        else
        {
            NoticeImage.GetComponent<Image>().color = new Color(0/255f, 234/255f, 218/255f, 255 / 255f);
            NoticeText.color = new Color(0, 0, 0, 255);
        }

        if (timeCheck)
        {
            if (timer > 3)
            {              
                NoticeImage.rectTransform.anchoredPosition = new Vector2(NoticeImage.rectTransform.anchoredPosition.x, 20);
                timeCheck = false;
            }
        }
        
    }

    public void Msg(string Name)
    {
        switch (Name)
        {
            case "start":
                NoticeText.text = start;
                isNegative = false;
                break;
            case "clear":
                NoticeText.text = clear;
                isNegative = false;
                break;
            case "sell":
                NoticeText.text = sell;
                isNegative = false;
                break;
            case "notGelatin":
                NoticeText.text = notGelatin;
                isNegative = true;
                break;
            case "notGold":
                NoticeText.text = notGold;
                isNegative = true;
                break;
            case "notNum":
                NoticeText.text = notNum;
                isNegative = true;
                break;
        }
        timer = 0;
        timeCheck = true;
        NoticeImage.rectTransform.anchoredPosition = new Vector2(NoticeImage.rectTransform.anchoredPosition.x, -3);
    }
}
