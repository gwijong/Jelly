using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GelatinCoin : MonoBehaviour
{
    float value = 0;
    public int gelatin = 0;
    [SerializeField]
    Text text;


    void Start()
    {
        //GetVariable();
    }

    private void LateUpdate()
    {
        value = Mathf.SmoothStep(float.Parse(text.text), gelatin, 0.5f);
        text.text = string.Format("{0:n0}", Mathf.Round(value));
        if (gelatin > 99999999)
        {
            gelatin = 99999999;
        }
    }


    void GetVariable()
    {
        gelatin = int.Parse(text.text);
    }

    public void GetGelatin(int id, int level)
    {
        gelatin = gelatin + id * level * GameManager.manager.clickLevel;      
    }
}
