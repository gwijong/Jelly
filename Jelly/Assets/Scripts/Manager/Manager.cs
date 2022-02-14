using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    static Manager s_instance; // 유일성이 보장된다
    static Manager Instance { get { Init(); return s_instance; } }

    UpdateManager _update = new UpdateManager();
    public static UpdateManager Input { get { return Instance._update; } }

    private void Update()
    {
        Input.OnUpdate();
    }
    private void Start()
    {
        Init();
    }

    static void Init()
    {
        if (s_instance == null)
        {
            GameObject go = GameObject.Find("@Manager");
            if (go == null)
            {
                go = new GameObject { name = "@Manager" };
                go.AddComponent<Managers>();
            }

            DontDestroyOnLoad(go);
            s_instance = go.GetComponent<Manager>();
        }
    }
}
