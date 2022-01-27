using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoldCoin : MonoBehaviour
{
    public static int value = 0;
    [SerializeField]
    int gold = 0;
    [SerializeField]
    Text text;
    public static GameObject Manager;
    public GameObject manager;
    void Start()
    {
        Manager = manager;
        GetVariable();
    }

    private void LateUpdate()
    {
        StartCoroutine("MoneyTimer");
        gold = (int)Mathf.SmoothStep(value, gold, 0.5f);
        text.text = $"{gold}";
        if (value > 99999999 || gold > 99999999)
        {
            value = 99999999;
            gold = 99999999;
        }
    }

    // Update is called once per frame
    void Update()
    {
        SellJelly();
    }

    void GetVariable()
    {
        value = int.Parse(text.text);
    }

    public static void SellJelly()
    {       
        int level = Jelly.level;
        int id = Jelly.id;
        int goldList = Manager.GetComponent<GameManager>().jellyGoldList[id];
        value = level * goldList;
    }
    IEnumerator MoneyTimer()
    {
        yield return new WaitForSeconds(0.5f);
        value = gold;
    }
}
