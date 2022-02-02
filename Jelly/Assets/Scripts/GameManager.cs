using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Sprite[] jellySpriteList;
    public string[] jellyNameList;
    public int[] jellyGelatinList;
    public int[] jellyGoldList;
    public bool[] jellyUnlockList;

    private static GameManager manager = null;

    public Vector3[] PointList;

    public RuntimeAnimatorController[] LevelAc;

    public static void ChangeAc(Animator anim, int level)
    {
        anim.runtimeAnimatorController = manager.LevelAc[level-1];
    }
    private void Start()
    {
        if(manager == null)
        {
            manager = this;
        }
        else
        {
            Destroy(this);
        }
    }
}
