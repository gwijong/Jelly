using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using LitJson;

public class GameManager : MonoBehaviour
{
    public Sprite[] jellySpriteList;
    public string[] jellyNameList;
    public int[] jellyGelatinList;
    public int[] jellyGoldList;
    public bool[] jellyUnlockList;
    public List<GameObject> jellyList;
    public List<JellyItem> jellyItemList = new List<JellyItem>();
    private static GameManager manager = null;
    public Vector3[] PointList;
    public RuntimeAnimatorController[] LevelAc;
    public GameObject Jelly;
    float timer = 0;

    public static void ChangeAc(Animator anim, int level)
    {
        anim.runtimeAnimatorController = manager.LevelAc[level-1];
    }
    private void Start()
    {
        
        if (manager == null)
        {
            manager = this;
        }
        else
        {
            Destroy(this);
        }

    }

    void Update()
    {
        /*
        timer = timer + Time.deltaTime;
        if(timer > 5)
        {
            timer = 0;
        }
        */
    }

    public class JellyItem
    {
        public int Key;
        public int ID;
        public int Level;

        public JellyItem(int key, int id, int level)
        {
            Key = key;
            ID = id;
            Level = level;
        }
    }

    
    /*
    public void Save()
    {
        for(int i = 0; i<jellyList.Count; i++)
        {
            jellyItemList.Add(new JellyItem(i, jellyList[i].GetComponent<Jelly>().id, jellyList[i].GetComponent<Jelly>().level));
        }
        Debug.Log("저장하기");
        //JsonData JellyJson = JsonMapper.ToJson(jellyItemList);
        string JellyJson = JsonMapper.ToJson(jellyItemList);

        //File.WriteAllText(Application.dataPath + "/Save/JellyData.json", JellyJson.ToString());
        File.WriteAllText(Application.dataPath + "/Save/JellyData.json", JellyJson);
    }

    public void Load()
    {
        string Jsonstring = File.ReadAllText(Application.dataPath + "/Save/JellyData.json");
        
        //List<JellyItem> itemsToLoad = manager.jellyItemList<JellyItem>(File.ReadAllText(Application.dataPath + "/Save/JellyData.json"));


        JsonData jellyData = JsonMapper.ToObject(Jsonstring);

        for(int i = 0; i < jellyData.Count; i++)
        {
            
            int keyNum = (int)jellyData[i]["Key"];
            int idNum = (int)jellyData[i]["ID"];
            int levelNum = (int)jellyData[i]["Level"];
            Debug.Log(keyNum);
            Debug.Log(idNum);
            Debug.Log(levelNum);
            GameObject instanceJelly = Instantiate(Jelly);
            instanceJelly.GetComponent<SpriteRenderer>().sprite = manager.jellySpriteList[idNum];
            instanceJelly.GetComponent<Jelly>().id = idNum;
            instanceJelly.GetComponent<Jelly>().level = levelNum;
            instanceJelly.GetComponent<Jelly>().exp = 0;
            Debug.Log("젤리 로드중");
        }
    }
    */



}
