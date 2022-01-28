using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonOption : MonoBehaviour
{
    public bool isClick = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isClick == false && Input.GetKeyDown(KeyCode.Escape))
        {
            IsClick();
        }
        if (isClick == true && Input.GetKeyDown(KeyCode.Escape))
        {
            Time.timeScale = 1;
            isClick = false;
            GameObject.Find("OptionButton").GetComponent<ButtonPanel>().Panel.SetActive(false);
        }
    }

    void IsClick()
    {

        if (GameObject.Find("JellyButton").GetComponent<ButtonPanel>().isCheck == false && GameObject.Find("PlantButton").GetComponent<ButtonPanel>().isCheck == false)
        {
            Time.timeScale = 0;
            isClick = true;
        }
        else
        {
            GameObject.Find("OptionButton").GetComponent<ButtonPanel>().Panel.SetActive(true);
            GameObject.Find("JellyButton").GetComponent<ButtonPanel>().Panel.GetComponent<Animator>().SetTrigger("doHide");
            GameObject.Find("JellyButton").GetComponent<ButtonPanel>().isCheck = false;
            GameObject.Find("PlantButton").GetComponent<ButtonPanel>().Panel.GetComponent<Animator>().SetTrigger("doHide");
            GameObject.Find("PlantButton").GetComponent<ButtonPanel>().isCheck = false;
        } 
    }
}
