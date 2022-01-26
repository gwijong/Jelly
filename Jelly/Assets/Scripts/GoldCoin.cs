using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoldCoin : MonoBehaviour
{
    [SerializeField]
    int value = 0;
    [SerializeField]
    int gold = 0;
    [SerializeField]
    Text text;

    void Start()
    {
        GetVariable();
    }

    private void LateUpdate()
    {
        gold = (int)Mathf.SmoothStep(value, gold, 0.5f);
        text.text = $"{gold}";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void GetVariable()
    {
        value = int.Parse(text.text);
    }
}
