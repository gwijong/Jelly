using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndDropItem : MonoBehaviour
{
    [SerializeField] SpriteRenderer spriteRenderer;

    bool isBeingHeld = false;
    float timer = 0.5f;
    bool timeCheck = false;

    private void Start()
    {

    }
    private void Update()
    {
        if(timeCheck == true)
        {
            timer = timer - Time.deltaTime;
        }

        if (isBeingHeld)
        {
            Vector2 mousePos;
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            this.gameObject.transform.position = new Vector2(mousePos.x, mousePos.y);
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
                isBeingHeld = true;
            }
        }
    }

    private void OnMouseUp()
    {
        timeCheck = false;
        timer = 0.5f;
        spriteRenderer.color = new Color(1f, 1f, 1f, 1f);
        isBeingHeld = false;
    }




}
