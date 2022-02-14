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
        GameManager.update.UpdateMethod -= OnUpdate;
        GameManager.update.UpdateMethod += OnUpdate;
    }


    public void Hide()
    {
        if (panel.activeInHierarchy)
        {
            Time.timeScale = 1;
            panel.SetActive(false);
            GameManager.soundmanager.PlaySfxPlayer("PauseOut");
        }
        else if (jellyButtonPanel.isCheck || plantButtonPanel.isCheck)
        {
            if (jellyButtonPanel.isCheck)
            {
                jellyButtonPanel.Panel.GetComponent<Animator>().SetTrigger("doHide");
                jellyButtonPanel.isCheck = false;
                GameManager.soundmanager.PlaySfxPlayer("Button");
            }

            if (plantButtonPanel.isCheck)
            {
                plantButtonPanel.Panel.GetComponent<Animator>().SetTrigger("doHide");
                plantButtonPanel.isCheck = false;
                GameManager.soundmanager.PlaySfxPlayer("Button");
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
        GameManager.soundmanager.PlaySfxPlayer("PauseIn");
        yield return new WaitForSeconds(0.2f);
        if (panel.activeInHierarchy)
        {
            Time.timeScale = 0;
        }
    }

    public void Exit()
    {     
        timer = timer +Time.unscaledTime;
        GameManager.soundmanager.PlaySfxPlayer("PauseOut");
        if (timer > 0.5f)
        {
            Debug.Log("게임 종료");
            Application.Quit();           
        }
    }

    void OnUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Hide();
        }
    }
}
