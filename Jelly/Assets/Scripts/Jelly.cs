using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Jelly : MonoBehaviour
{
    [SerializeField]
    int minTimerRange = 3;
    [SerializeField]
    int maxTimerRange = 5;
    [SerializeField]
    float speedX = 1;
    [SerializeField]
    float speedY = 1;
    public static int id { get; set; }
    public static int level { get; set; }

    [SerializeField]
    float exp = 0;

    public GameObject topLeft;
    public GameObject bottomRight;
    public GameObject Manager;

    bool idle = true;
    bool walk = false;
    bool timer = false;
    public static bool outside{ get; set; }

    Vector3 point;

    // Start is called before the first frame update
    void Start()
    {
        id = 0;
        level = 1;
        outside = false;
    }

    // Update is called once per frame
    void Update()
    {
        SetExp();

        CheckBorder();

        if (timer == false)
        {
            timer = true;
            StartCoroutine("StateTimer");
        }
        if (walk)
        {
            Walk();
        }
        Touch();
    }

    IEnumerator StateTimer()
    {     
        yield return new WaitForSeconds(Random.Range(minTimerRange, maxTimerRange));
        if (idle == true)
        {
            Idle();
            idle = false;
        }
        else if (idle == false)
        {
            SetWalk();
            idle = true;
        }
        timer = false;
    }

    void Idle()
    {
        walk = false;
        this.gameObject.GetComponent<Animator>().SetBool("isWalk", false);
    }
    void SetWalk()
    {
        walk = true;
        if (outside == false)
        {
            speedX = Random.Range(-0.8f, 0.8f);
            speedY = Random.Range(-0.8f, 0.8f);
        }
        if (outside == true)
        {
            speedX = (point - this.gameObject.transform.position).normalized.x;
            speedY = (point - this.gameObject.transform.position).normalized.y;
        }

        if (speedX < 0)
        {
            this.gameObject.GetComponent<SpriteRenderer>().flipX = true;
        }
        else
        {
            this.gameObject.GetComponent<SpriteRenderer>().flipX = false;
        }

        this.gameObject.GetComponent<Animator>().SetBool("isWalk", true);
    }
    void Walk()
    {
        this.gameObject.transform.Translate(speedX * Time.deltaTime, speedY * Time.deltaTime, speedY * Time.deltaTime);
    }

    void CheckBorder()
    {
        if (this.gameObject.transform.position.x < topLeft.transform.position.x || this.gameObject.transform.position.x > bottomRight.transform.position.x)
        {
            outside = true;
        }
        else if (this.gameObject.transform.position.y > topLeft.transform.position.y || this.gameObject.transform.position.y < bottomRight.transform.position.y)
        {
            outside = true;
        }
        else
        {
            outside = false;
        }
    }

    void SetPoint()
    {
        point = Manager.GetComponent<GameManager>().PointList[Random.Range(0, 3)];
    }

    void Touch()
    {
        if (Input.GetMouseButtonDown(0))
        {
            
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.GetRayIntersection(ray, Mathf.Infinity);

            if(hit.collider == null)
            {
                return;
            }

            if (hit.collider.tag=="Jelly")
            {
                exp = exp + 1;
                GelatinCoin.getGelatin(id+1, level);
                this.gameObject.GetComponent<Animator>().SetTrigger("doTouch");
                speedX = 0;
                speedY = 0;

                StopCoroutine("StateTimer");
                StartCoroutine("StateTimer");
            }          
        }
    }

    void SetExp()
    {
        exp = exp + Time.deltaTime;
        if (level == 3)
        {
            return;
        }

        if (exp<(50 * level))
        {
            return;
        }

        if (exp >= (50 * level))
        {
            level = level + 1;
            exp = 0;
            Manager.GetComponent<GameManager>().ChangeAc(this.gameObject.GetComponent<Animator>(),level);
        }

    }

}

