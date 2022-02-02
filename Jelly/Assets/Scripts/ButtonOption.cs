using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonOption : MonoBehaviour
{
    public GameObject panel;
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
            StartCoroutine("Panel");
        }
        if (isClick == true && Input.GetKeyDown(KeyCode.Escape))
        {
            Time.timeScale = 1;
            isClick = false;
            panel.SetActive(false);
        }
    }


    IEnumerator Panel()
    {
        panel.SetActive(true);
        if(GameObject.Find("JellyButton").GetComponent<ButtonPanel>().isCheck == true )
        {
            GameObject.Find("JellyButton").GetComponent<ButtonPanel>().Panel.GetComponent<Animator>().SetTrigger("doHide");
            GameObject.Find("JellyButton").GetComponent<ButtonPanel>().isCheck = false;
        }
        if ( GameObject.Find("PlantButton").GetComponent<ButtonPanel>().isCheck == true)
        {
            GameObject.Find("PlantButton").GetComponent<ButtonPanel>().Panel.GetComponent<Animator>().SetTrigger("doHide");
            GameObject.Find("PlantButton").GetComponent<ButtonPanel>().isCheck = false;
        }
        yield return new WaitForSeconds(0.5f);
        Time.timeScale = 0;
        isClick = true;
    }
}
