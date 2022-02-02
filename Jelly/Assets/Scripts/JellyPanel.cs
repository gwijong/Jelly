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

    private GameManager manager;
    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.Find("GameManager").GetComponent<GameManager>();
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
        if (manager.jellyUnlockList[page] == false)
        {
            lockGroup.SetActive(true);
            lockImage.sprite = manager.jellySpriteList[page];
            lockImage.GetComponent<Image>().SetNativeSize();
            lockButtonText.text = string.Format("{0:n0}", manager.jellyGelatinList[page]);
        }
        else
        {
            lockGroup.SetActive(false);
        }

        image.sprite = manager.jellySpriteList[page];
        image.GetComponent<Image>().SetNativeSize();
        nameText.text = manager.jellyNameList[page];
        buttonText.text = string.Format("{0:n0}", manager.jellyGoldList[page]);
        pageText.text = string.Format("#{0:00}", page + 1);
    }

    public void Unlock()
    {
        GelatinCoin gelCoin = GameObject.Find("LeftText").GetComponent<GelatinCoin>();

        int gel = gelCoin.gelatin;
        int price = manager.jellyGelatinList[page];

        if (gel >= price)
        {
            gelCoin.gelatin = gelCoin.gelatin - price;
            manager.jellyUnlockList[page] = true;
            Page();
        }
    }
}
