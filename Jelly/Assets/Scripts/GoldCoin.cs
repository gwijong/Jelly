using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoldCoin : MonoBehaviour
{
    int value = 0;
    public int gold = 0;
    [SerializeField]
    Text text;
    void Start()
    {
        GetVariable();
    }

    private void LateUpdate()
    {
        value = (int)Mathf.SmoothStep(int.Parse(text.text), gold, 0.5f) + 1;
        text.text = $"{value}";
        if (gold > 99999999)
        {
            gold = 99999999;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //SellJelly();
    }

    void GetVariable()
    {
        gold = int.Parse(text.text);
    }

    public void SellJelly(Jelly jelly) 
    {       
        int level = jelly.level;
        int id = jelly.id;
        int goldList = GameObject.Find("GameManager").GetComponent<GameManager>().jellyGoldList[id];
        gold = gold +(level * goldList);
        Destroy(jelly.gameObject);
    }

}
