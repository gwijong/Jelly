using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JellyPanel : MonoBehaviour
{
    public static int page = 0;
    public Image image;
    public Text nameText;
    public Text buttonText;
    public Text pageText;
    public GameObject lockGroup;
    public Image lockImage;
    public Text lockButtonText;
    public GameObject jelly;
    bool Clearcheck = true;

    private GameManager manager;
    // Start is called before the first frame update
    void Start()
    {

        manager = GameManager.manager;
        Page();
    }


    public void PageDown()
    {
        if (page <= 0)
        {
            return;
        }
        page = page - 1;
        Page();
        GameManager.soundmanager.PlaySfxPlayer("Button");
    }

    public void PageUp()
    {
        if(page>= 11)
        {
            return;
        }
        page = page + 1;
        Page();
        GameManager.soundmanager.PlaySfxPlayer("Button");
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
        
        int gel = GameManager.manager.gelatin;
        int price = manager.jellyGelatinList[page];

        if (gel >= price)
        {
            GameManager.manager.gelatin = GameManager.manager.gelatin - price;
            manager.jellyUnlockList[page] = true;
            Page();
            GameManager.soundmanager.PlaySfxPlayer("Unlock");
            
            for(int i = 0; i< manager.jellyUnlockList.Length; i++)
            {
                if(manager.jellyUnlockList[i] == false)
                {
                    Clearcheck = false;
                    break;
                }              
            }
            if (Clearcheck)
            {
                manager.clearCheck = true;
                manager.gameClearCheckAndStart();
            }
            Clearcheck = true;
        }
        else
        {
            GameManager.soundmanager.PlaySfxPlayer("Fail");
            NoticeManager.Msg("notGelatin");
        }
    }
}
