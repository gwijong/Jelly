using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinText : MonoBehaviour
{
    [SerializeField]
    Text goldText;
    [SerializeField]
    Text gelatinText;

    // Start is called before the first frame update
    void Start()
    {
        GameManager.update.UpdateMethod += OnUpdate;
    }

    // Update is called once per frame
    void OnUpdate()
    {
        UpdateText(ref GameManager.manager.gold, goldText);
        UpdateText(ref GameManager.manager.gelatin, gelatinText);
    }

    void UpdateText(ref int goal, Text target)
    {
        target.text = string.Format("{0:n0}", Mathf.Round(Mathf.SmoothStep(float.Parse(target.text), goal, 0.5f)));
        if (goal > 99999999)
        {
            goal = 99999999;
        }
    }
}
