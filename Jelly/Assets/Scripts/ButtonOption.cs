using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonOption : MonoBehaviour
{
    public GameObject panel;
    ButtonPanel jellyButtonPanel;
    ButtonPanel plantButtonPanel;
    // Start is called before the first frame update
    void Start()
    {
        jellyButtonPanel = GameObject.Find("JellyButton").GetComponent<ButtonPanel>();
        plantButtonPanel = GameObject.Find("PlantButton").GetComponent<ButtonPanel>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(panel.activeInHierarchy)
            {
                Time.timeScale = 1;
                panel.SetActive(false);
            }else if (jellyButtonPanel.isCheck|| plantButtonPanel.isCheck)
            {
                if (jellyButtonPanel.isCheck)
                {
                    jellyButtonPanel.Panel.GetComponent<Animator>().SetTrigger("doHide");
                    jellyButtonPanel.isCheck = false;
                }

                if(plantButtonPanel.isCheck)
                {
                    plantButtonPanel.Panel.GetComponent<Animator>().SetTrigger("doHide");
                    plantButtonPanel.isCheck = false;
                }
            }
            else
            {
                StartCoroutine("Panel");
            }
        }
    }


    IEnumerator Panel()
    {
        panel.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        if (panel.activeInHierarchy)
        {
            Time.timeScale = 0;
        }       
    }
}
