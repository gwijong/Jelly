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
        Load();
        if (manager == null)
        {
            manager = this;
        }
        else
        {
            Destroy(this);
        }

        //jellyItemList.Add(new JellyItem(0, 1));
    }

    void Update()
    {
        timer = timer + Time.deltaTime;
        if(timer > 5)
        {
            timer = 0;
            Save();
        }
        
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

    public void Save()
    {
        for(int i = 0; i<jellyList.Count; i++)
        {
            jellyItemList.Add(new JellyItem(i, jellyList[i].GetComponent<Jelly>().id, jellyList[i].GetComponent<Jelly>().level));
        }
        Debug.Log("저장하기");
        JsonData JellyJson = JsonMapper.ToJson(jellyItemList);

        File.WriteAllText(Application.dataPath + "/Save/JellyData.json", JellyJson.ToString());
    }

    public void Load()
    {
        string Jsonstring = File.ReadAllText(Application.dataPath + "/Save/JellyData.json");
        Debug.Log("Jsonstring");

        JsonData jellyData = JsonMapper.ToObject(Jsonstring);

        for(int i = 0; i < jellyData.Count; i++)
        {
            /*
            GameObject instanceJelly = Instantiate(Jelly);
            instanceJelly.GetComponent<SpriteRenderer>().sprite = manager.jellySpriteList[int.Parse(jellyData[i]["Key"])];
            instanceJelly.GetComponent<Jelly>().id = page;
            instanceJelly.GetComponent<Jelly>().level = 1;
            instanceJelly.GetComponent<Jelly>().exp = 0;
            */
            /*
            Debug.Log(jellyData[i]["Key"].ToString());
            Debug.Log(jellyData[i]["ID"].ToString());
            Debug.Log(jellyData[i]["Level"].ToString());
            */
        }
    }
}
