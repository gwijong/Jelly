using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndDropItem : MonoBehaviour
{
    [SerializeField] SpriteRenderer spriteRenderer;

    bool Hold = false;
    float timer = 0.5f;
    bool timeCheck = false;
    bool col = false;
    GameObject manager;
    public GameObject jelly{ get; set; }

    private void Start()
    {
        jelly = this.gameObject;
        manager = GameObject.Find("GameManager");
    }
    private void Update()
    {
        if(timeCheck == true)
        {
            timer = timer - Time.deltaTime;
        }

        if (Hold)
        {
            Vector2 mousePos;
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            jelly.transform.position = new Vector2(mousePos.x, mousePos.y);
        }
    }

  
    private void OnMouseDrag()
    {
        
        if (Input.GetMouseButton(0))
        {
            timeCheck = true;
            if (timer <= 0)
            {
                spriteRenderer.color = new Color(1f, 1f, 1f, .5f);
                Vector3 mousePos;
                mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Hold = true;
            }
        }
    }

    private void OnMouseUp()
    {
        if(col == false)
        {
            timeCheck = false;
            timer = 0.5f;
            spriteRenderer.color = new Color(1f, 1f, 1f, 1f);
            Hold = false;
            if (Jelly.outside == true)
            {
                Return();
            }
        }else if (col == true)
        {
            Sell();
            Destroy(jelly);
            col = false;
        }
    }

    public void Return()
    {
        jelly.transform.position = manager.GetComponent<GameManager>().PointList[Random.Range(0, 3)];
    }


    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.name == "SellButton")
        {
            col = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "SellButton")
        {
            col = false;
        }
    }

    public void Sell()
    {
        GameObject.Find("RightText").GetComponent<GoldCoin>().SellJelly();
    }

}
