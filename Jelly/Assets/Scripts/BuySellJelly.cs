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
            GameManager.soundmanager.PlaySfxPlayer("Fail");
            return;
        }
        page = JellyPanel.page;
        if (GameManager.manager.gold >= GameManager.manager.jellyGoldList[page])
        {
            GameManager.manager.gold = GameManager.manager.gold - GameManager.manager.jellyGoldList[page];
            GameObject instanceJelly = Instantiate(GameManager.manager.jellyPrefab);
            instanceJelly.GetComponent<SpriteRenderer>().sprite = GameManager.manager.jellySpriteList[page];
            Jelly jelly = instanceJelly.GetComponent<Jelly>();
            jelly.id = page;
            jelly.level = 1;
            jelly.exp = 0;
            instanceJelly.transform.position = spawnPos[(int)Random.Range(0, 6)];
            GameManager.manager.jellyList.Add(instanceJelly);
            GameManager.soundmanager.PlaySfxPlayer("Buy");
        }
        else
        {
            NoticeManager.Msg("notGold");
            GameManager.soundmanager.PlaySfxPlayer("Fail");
        }
    }

    public static void SellJelly(Jelly jelly)
    {
        int level = jelly.level;
        int id = jelly.id;
        int goldList = GameManager.manager.jellyGoldList[id];
        GameManager.manager.gold += level * goldList;
        GameManager.manager.jellyList.Remove(jelly.gameObject);
        GameManager.soundmanager.PlaySfxPlayer("Sell");

        Destroy(jelly.gameObject);
    }
}
