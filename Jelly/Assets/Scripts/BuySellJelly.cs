using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuySellJelly : MonoBehaviour
{
    Vector3[] spawnPos;
    void Start()
    {
        
        spawnPos = new Vector3[]
        {
            new Vector3(0, 0, 0),
            new Vector3(1, 1, 0),
            new Vector3(1, -1, 0),
            new Vector3(-1, 1, 0),
            new Vector3(1, 0, 0),
            new Vector3(-1, 0, 0)
        };
    }
    public void buy(int page)
    {
        if (GameManager.manager.jellyList.Count>= GameManager.manager.numLevel*2)
        {
            NoticeManager.Msg("notNum");
            GameObject.Find("SoundManager").GetComponent<SoundManager>().PlaySfxPlayer("Fail");
            return;
        }
        page = JellyPanel.page;
        GoldCoin goldCoin = GameObject.Find("RightText").GetComponent<GoldCoin>();
        if (goldCoin.gold >= GameManager.manager.jellyGoldList[page])
        {
            goldCoin.gold = goldCoin.gold - GameManager.manager.jellyGoldList[page];
            GameObject instanceJelly = Instantiate(GameManager.manager.jellyPrefab);
            instanceJelly.GetComponent<SpriteRenderer>().sprite = GameManager.manager.jellySpriteList[page];
            instanceJelly.GetComponent<Jelly>().id = page;
            instanceJelly.GetComponent<Jelly>().level = 1;
            instanceJelly.GetComponent<Jelly>().exp = 0;
            instanceJelly.transform.position = spawnPos[(int)Random.Range(0, 6)];
            GameManager.manager.jellyList.Add(instanceJelly);
            GameObject.Find("SoundManager").GetComponent<SoundManager>().PlaySfxPlayer("Buy");
        }
        else
        {
            NoticeManager.Msg("notGold");
            GameObject.Find("SoundManager").GetComponent<SoundManager>().PlaySfxPlayer("Fail");
        }
    }

    public static void SellJelly(Jelly jelly)
    {
        int level = jelly.level;
        int id = jelly.id;
        int goldList = GameObject.Find("GameManager").GetComponent<GameManager>().jellyGoldList[id];
        GameObject.Find("RightText").GetComponent<GoldCoin>().gold += (int)(level * goldList);
        GameObject.Find("GameManager").GetComponent<GameManager>().jellyList.Remove(jelly.gameObject);
        GameObject.Find("SoundManager").GetComponent<SoundManager>().PlaySfxPlayer("Sell");
        Manager.Input.UpdateMethod -= jelly.GetComponent<Jelly>().SetState;

        Destroy(jelly.gameObject);
    }
}
