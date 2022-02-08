using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonOption : MonoBehaviour
{
    public GameObject panel;
    ButtonPanel jellyButtonPanel;
    ButtonPanel plantButtonPanel;

    float timer;
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
            Hide();
        }
    }

    public void Hide()
    {
        if (panel.activeInHierarchy)
        {
            Time.timeScale = 1;
            panel.SetActive(false);
            GameObject.Find("SoundManager").GetComponent<SoundManager>().PlaySfxPlayer("PauseOut");
        }
        else if (jellyButtonPanel.isCheck || plantButtonPanel.isCheck)
        {
            if (jellyButtonPanel.isCheck)
            {
                jellyButtonPanel.Panel.GetComponent<Animator>().SetTrigger("doHide");
                jellyButtonPanel.isCheck = false;
                GameObject.Find("SoundManager").GetComponent<SoundManager>().PlaySfxPlayer("Button");
            }

            if (plantButtonPanel.isCheck)
            {
                plantButtonPanel.Panel.GetComponent<Animator>().SetTrigger("doHide");
                plantButtonPanel.isCheck = false;
                GameObject.Find("SoundManager").GetComponent<SoundManager>().PlaySfxPlayer("Button");
            }
        }
        else
        {
            StartCoroutine("Panel");
        }
    }

    IEnumerator Panel()
    {
        panel.SetActive(true);
        GameObject.Find("SoundManager").GetComponent<SoundManager>().PlaySfxPlayer("PauseIn");
        yield return new WaitForSeconds(0.2f);
        if (panel.activeInHierarchy)
        {
            Time.timeScale = 0;
        }
    }

    public void Exit()
    {     
        timer = timer +Time.unscaledTime;
        GameObject.Find("SoundManager").GetComponent<SoundManager>().PlaySfxPlayer("PauseOut");
        if (timer > 0.5f)
        {
            Debug.Log("게임 종료");
            Application.Quit();           
        }

    }
}
