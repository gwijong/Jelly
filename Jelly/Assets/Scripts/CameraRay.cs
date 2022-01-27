using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRay : MonoBehaviour
{
    public static Jelly grab { get; private set; }
    private Vector3 grabPosition = Vector3.zero;
    private float grabTime;
    [SerializeField]
    private float grabDelay;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Touch();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            Release();
        }

        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        //�� ���ĺ��� ��� ����!
        if(grab != null && (grabTime + grabDelay) <= Time.realtimeSinceStartup)
        {
            grab.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0.5f);
            grab.transform.position = new Vector2(mousePos.x, mousePos.y);
        };
    }

    void Touch()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.GetRayIntersection(ray, Mathf.Infinity);

        if (hit.collider == null)
        {
            return;
        }

        grab = hit.transform.GetComponent<Jelly>();
        if (grab != null)
        {
            grabPosition = grab.transform.position;
            //���� �������� �󸶳� ��������! (���� �ð�)
            grabTime = Time.realtimeSinceStartup;
        }; 
    }

    void Release()
    {
        if (grab != null)
        {
            grab.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
            if ((grabTime + grabDelay) > Time.realtimeSinceStartup)
            {
                grab.Touch();
            };

            if(ButtonSell.CursorOnSellButton)
            {
                GameObject.Find("RightText").GetComponent<GoldCoin>().SellJelly(grab);
            }
            else if(grab.outside)
            {
                grab.transform.position = grabPosition;
            };
        };
        grab = null;
    }
}