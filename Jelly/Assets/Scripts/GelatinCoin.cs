using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GelatinCoin : MonoBehaviour
{
    [SerializeField]
    int value = 0;
    [SerializeField]
    int gelatin = 0;
    [SerializeField]
    Text text;


    void Start()
    {
        GetVariable();
    }

    private void LateUpdate()
    {
        value = (int)Mathf.SmoothStep(int.Parse(text.text), gelatin, 0.5f);
        text.text = $"{value}";
        if (value > 99999999 || gelatin > 99999999)
        {
            value = 99999999;
            gelatin = 99999999;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void GetVariable()
    {
        gelatin = int.Parse(text.text);
    }

    public void getGelatin(int id, int level)
    {
        gelatin = gelatin + id * level;
    }
}
