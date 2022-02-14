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

    // Update is called once per frame
    private void Start()
    {
        Manager.Input.UpdateMethod -= OnUpdate;
        Manager.Input.UpdateMethod += OnUpdate;
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
            //게임 시작한지 얼마나 지났는지! (현재 시간)
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

            if (ButtonSell.CursorOnSellButton)
            {
                BuySellJelly.SellJelly(grab);
            }
            else if(grab.outside)
            {
                grab.transform.position = grabPosition;
            };
        };
        grab = null;
    }

    void OnUpdate()
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

        //이 이후부터 잡기 가능!
        if (grab != null && (grabTime + grabDelay) <= Time.realtimeSinceStartup)
        {
            grab.gameObject.GetComponent<Animator>().SetBool("isWalk", false);
            grab.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0.5f);
            grab.transform.position = new Vector2(mousePos.x, mousePos.y);
        };
    }
}
