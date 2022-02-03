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
    public GameObject jelly;

    Vector3 spawn1 = new Vector3(0, 0, 0);
    Vector3 spawn2 = new Vector3(1, 1, 0);
    Vector3 spawn3 = new Vector3(1, -1, 0);
    Vector3 spawn4 = new Vector3(-1, 1, 0);
    Vector3 spawn5 = new Vector3(1, 0, 0);
    Vector3 spawn6 = new Vector3(-1, 0, 0);
    Vector3[] spawn;
    private GameManager manager;
    // Start is called before the first frame update
    void Start()
    {
        spawn = new Vector3[] { spawn1, spawn2, spawn3, spawn4, spawn5, spawn6 };
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

    public void buy()
    {
        GoldCoin goldCoin = GameObject.Find("RightText").GetComponent<GoldCoin>();

        if (goldCoin.gold >= manager.jellyGoldList[page])
        {
            goldCoin.gold = goldCoin.gold - manager.jellyGoldList[page];
            Instantiate(jelly);
            jelly.GetComponent<SpriteRenderer>().sprite = manager.jellySpriteList[page];
            jelly.GetComponent<Jelly>().id = page;
            jelly.GetComponent<Jelly>().level = 1;
            jelly.GetComponent<Jelly>().exp = 0;    
            jelly.transform.position = spawn[(int)Random.Range(0, 6)];
        }
    
    }
}
