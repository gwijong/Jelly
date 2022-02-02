using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPanel : MonoBehaviour
{ 
    public bool isCheck = false;
    public Sprite showSprite;
    public Sprite hideSprite;
    public GameObject Panel;
    public GameObject AnotherBtn;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void IsClick()
    {
        if (isCheck == false)
        {
            if(AnotherBtn.GetComponent<ButtonPanel>().isCheck == true)
            {
                AnotherBtn.GetComponent<ButtonPanel>().Panel.GetComponent<Animator>().SetTrigger("doHide");
                AnotherBtn.GetComponent<ButtonPanel>().isCheck = false;
            }
            isCheck = true;
            Panel.GetComponent<Animator>().SetTrigger("doShow");
        }
        else if (isCheck == true || Input.GetKeyDown(KeyCode.Escape))
        {
            isCheck = false;
            Panel.GetComponent<Animator>().SetTrigger("doHide");
        }
    }
}
