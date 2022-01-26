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
        gelatin = (int)Mathf.SmoothStep(value, gelatin, 0.5f);
        text.text = $"{gelatin}";
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
