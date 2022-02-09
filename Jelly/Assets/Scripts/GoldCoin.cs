using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoldCoin : MonoBehaviour
{
    public float value = 0;
    public int gold = 0;
    [SerializeField]
    Text text;
    void Start()
    {
        //GetVariable();
    }

    private void LateUpdate()
    {
        value = Mathf.SmoothStep(float.Parse(text.text), gold, 0.5f) ;
        text.text = string.Format("{0:n0}",Mathf.Round(value));
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





}
