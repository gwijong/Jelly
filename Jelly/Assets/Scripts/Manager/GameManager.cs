using System.Collections;
using System.Collections.Generic;
using UnityEngine.Android;
using UnityEngine;
using System.IO;
using System.Text;

public class GameManager : MonoBehaviour
{
    public static SoundManager soundmanager { get; private set; }
    public Sprite[] jellySpriteList;
    public string[] jellyNameList;
    public int[] jellyGelatinList;
    public int[] jellyGoldList;
    public bool[] jellyUnlockList;
    public int[] numGoldList;
    public int[] clickGoldList;

    public int numLevel = 1;
    public int clickLevel = 1;

    public int gold;
    public int gelatin;

    public List<GameObject> jellyList;
    public static GameManager manager { get; private set; }
    public Vector3[] PointList;
    public RuntimeAnimatorController[] LevelAc;
    public GameObject jellyPrefab;
    public Vector3[] JellyPosition;
   

    public GameObject clear;
    public bool clearCheck;

    UpdateManager _update = new UpdateManager();
    public static UpdateManager update { get { return manager._update; } }
    private void Update()
    {
        update.OnUpdate();
    }

    public static void ChangeAc(Animator anim, int level)
    {
        anim.runtimeAnimatorController = manager.LevelAc[level-1];
    }

    private void Awake()
    {
        soundmanager = GameObject.Find("SoundManager").GetComponent<SoundManager>();
        if (manager == null)
        {
            manager = this;
        }
        else
        {
            Destroy(this);
        }       
        gameClearCheckAndStart();
    }
    
    public void gameClearCheckAndStart()
    {
        if (clearCheck)
        {
            clear.SetActive(true);
            NoticeManager.Msg("clear");
            GameManager.soundmanager.PlaySfxPlayer("Clear");
        }
        else
        {
            NoticeManager.Msg("start");
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
    public float bgm;
    public float sfx;
    public bool isClear;
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
