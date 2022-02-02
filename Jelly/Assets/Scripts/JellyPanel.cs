using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JellyPanel : MonoBehaviour
{
    public int page = 0;
    public Image image;
    public Text nameText;
    public Text buttonText;
    public Text pageText;
    public GameObject lockGroup;
    public Image lockImage;
    public Text lockButtonText;
    // Start is called before the first frame update
    void Start()
    {
        Page();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PageDown()
    {
        if (page <= 0)
        {
            return;
        }
        page = page - 1;
        Page();
    }

    public void PageUp()
    {
        if(page>= 11)
        {
            return;
        }
        page = page + 1;
        Page();
    }

    void Page()
    {
        if (GameObject.Find("GameManager").GetComponent<GameManager>().jellyUnlockList[page] == false)
        {
            lockGroup.SetActive(true);
            lockImage.sprite = GameObject.Find("GameManager").GetComponent<GameManager>().jellySpriteList[page];
            lockImage.GetComponent<Image>().SetNativeSize();
            lockButtonText.text = string.Format("{0:n0}", GameObject.Find("GameManager").GetComponent<GameManager>().jellyGelatinList[page]);
        }
        else
        {
            lockGroup.SetActive(false);
        }
        image.sprite = GameObject.Find("GameManager").GetComponent<GameManager>().jellySpriteList[page];
        image.GetComponent<Image>().SetNativeSize();
        nameText.text = GameObject.Find("GameManager").GetComponent<GameManager>().jellyNameList[page];
        buttonText.text = string.Format("{0:n0}", GameObject.Find("GameManager").GetComponent<GameManager>().jellyGoldList[page]);
        pageText.text = string.Format("#{0:00}", page + 1);
    }

    public void Unlock()
    {
        int gel = GameObject.Find("LeftText").GetComponent<GelatinCoin>().gelatin;
        int price = GameObject.Find("GameManager").GetComponent<GameManager>().jellyGelatinList[page];

        if (gel >= price)
        {
            GameObject.Find("LeftText").GetComponent<GelatinCoin>().gelatin = GameObject.Find("LeftText").GetComponent<GelatinCoin>().gelatin - price;
            GameObject.Find("GameManager").GetComponent<GameManager>().jellyUnlockList[page] = true;
            Page();
        }
    }
}
