using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class GameManager : MonoBehaviour
{
    public Sprite[] jellySpriteList;
    public string[] jellyNameList;
    public int[] jellyGelatinList;
    public int[] jellyGoldList;
    public bool[] jellyUnlockList;
    public int[] numGoldList;
    public int[] clickGoldList;

    public int numLevel = 1;
    public int clickLevel = 1;

    public List<GameObject> jellyList;
    public static GameManager manager { get; private set; }
    public Vector3[] PointList;
    public RuntimeAnimatorController[] LevelAc;
    public GameObject jellyPrefab;
    public Vector3[] JellyPosition;
    float timer = 0;

    public static void ChangeAc(Animator anim, int level)
    {
        anim.runtimeAnimatorController = manager.LevelAc[level-1];
    }

    private void Awake()
    {
        
        if (manager == null)
        {
            manager = this;
        }
        else
        {
            Destroy(this);
        }
        Load();
    }

    private void OnApplicationQuit()
    {
        Save();
    }

    void Update()
    {

        timer = timer + Time.deltaTime;
        if(timer > 20)
        {
            timer = 0;
            Save();
        }
        JellyPosition = new Vector3[jellyList.Count];
        for (int i = 0; i < jellyList.Count; i++)
        {           
            JellyPosition[i] = jellyList[i].transform.position;
        }
    }


    
    
    public void Save()
    {
        jellyJsonObject jsonObject = new jellyJsonObject();
        jsonObject.gold = GameObject.Find("RightText").GetComponent<GoldCoin>().gold;
        jsonObject.jellyPoint = GameObject.Find("LeftText").GetComponent<GelatinCoin>().gelatin;
        jsonObject.jsonNumLevel = numLevel;
        jsonObject.jsonClickLevel = clickLevel;      
        jsonObject.jellyTransformArray = new Vector3[jellyList.Count];
        for (int i = 0; i<jellyList.Count; i++)
        {
            jsonObject.list.Add(new JellyItem(jellyList[i].GetComponent<Jelly>().id, jellyList[i].GetComponent<Jelly>().level));
            jsonObject.jellyTransformArray[i] = JellyPosition[i];
        }
        Debug.Log("저장하기");
        jsonObject.jellyUnlock = new bool[manager.jellyUnlockList.Length];
        for (int i = 0; i < manager.jellyUnlockList.Length; i++)
        {
            jsonObject.jellyUnlock[i] = manager.jellyUnlockList[i];
        }
        string JellyJson = JsonUtility.ToJson(jsonObject);
        File.WriteAllText(Application.dataPath + "/Save/JellyData.json", JellyJson);

    }

    public void Load()
    {
        jellyJsonObject jsonObject;
        Debug.Log("젤리 불러오기");
        string Jsonstring = File.ReadAllText(Application.dataPath + "/Save/JellyData.json");
        jsonObject = JsonUtility.FromJson<jellyJsonObject>(Jsonstring);
         
        for (int i =0; i<jsonObject.list.Count; i++)
        {
            GameObject instanceJelly = Instantiate(jellyPrefab);
            instanceJelly.GetComponent<Jelly>().id = jsonObject.list[i].JellyType;
            instanceJelly.GetComponent<SpriteRenderer>().sprite = manager.jellySpriteList[jsonObject.list[i].JellyType];
            instanceJelly.GetComponent<Jelly>().level = jsonObject.list[i].Level;
            instanceJelly.transform.position = jsonObject.jellyTransformArray[i];
            jellyList.Add(instanceJelly);
        }
        GameObject.Find("LeftText").GetComponent<GelatinCoin>().gelatin = jsonObject.jellyPoint;
        GameObject.Find("RightText").GetComponent<GoldCoin>().gold = jsonObject.gold;
        clickLevel = jsonObject.jsonClickLevel;
        numLevel = jsonObject.jsonNumLevel;
        
        manager.jellyUnlockList = new bool[jsonObject.jellyUnlock.Length];
        for (int i = 0; i < manager.jellyUnlockList.Length; i++)
        {
            manager.jellyUnlockList[i] = jsonObject.jellyUnlock[i];
        }
        
    }
    



}
[System.Serializable]
public class jellyJsonObject
{
    public List<JellyItem> list = new List<JellyItem>();
    public int gold;
    public int jellyPoint;
    public bool[] jellyUnlock;
    public Vector3[] jellyTransformArray;
    public int jsonNumLevel;
    public int jsonClickLevel;
}

[System.Serializable]
public class JellyItem
{
    public int JellyType;
    public int Level;

    public JellyItem(int type, int lv)
    {
        JellyType = type;
        Level = lv;
    }
}
