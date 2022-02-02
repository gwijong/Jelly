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
    float speed = 0.8f;
    const int expPoint = 10;

    public int id;
    public int level;

    [SerializeField]
    float exp = 0;

    public static GameObject topLeft;
    public static GameObject bottomRight;
    public GameObject Manager;

    bool idle = true;
    bool walk = false;
    bool timer = false;
    public bool outside 
    { get
        {
            if (this.gameObject.transform.position.x < topLeft.transform.position.x || this.gameObject.transform.position.x > bottomRight.transform.position.x)
            {
                return true;
            }
            else if (this.gameObject.transform.position.y > topLeft.transform.position.y || this.gameObject.transform.position.y < bottomRight.transform.position.y)
            {
                return true;
            }
            else
            {
                return false;
            }
        } 
    }

    Vector3 point;

    // Start is called before the first frame update
    void Start()
    {
        if (topLeft == null) topLeft = GameObject.Find("TopLeft");
        if (bottomRight == null) bottomRight = GameObject.Find("BottomRight");

        id = 0;
        level = 1;
    }

    // Update is called once per frame
    void Update()
    {

        SetExp();

        if (timer == false)
        {
            timer = true;
            StopCoroutine(StateTimer());
            StartCoroutine(StateTimer());
        }

        if (walk)
        {
            transform.Translate(speedX * Time.deltaTime, speedY * Time.deltaTime, speedY * Time.deltaTime);
        };
    }

    IEnumerator StateTimer()
    {
        float nextSecond = Random.Range(minTimerRange, maxTimerRange);

        if (idle == true)
        {
            walk = false;
            this.gameObject.GetComponent<Animator>().SetBool("isWalk", false);
            yield return new WaitForSeconds(nextSecond);
            idle = false;
        }
        else if (idle == false)
        {        
            SetWalk(nextSecond);
            yield return new WaitForSeconds(nextSecond);
            idle = true;
        }
        

        timer = false;

    }


    //다음 불러올 때까지 얼마나 걸릴 것인지 nextSecond
    //여기에 speed를 곱하면 어디까지 갈 것인지!
    void SetWalk(float nextSecond)
    {
        //이동 가능한 왼쪽 영역             왼쪽 끝과 나의 차이!                  //nextSecond로 나누면! 끝까지 가는데 어느 속도로 가면 도착하는지
        float possibleLeft = (transform.position.x - topLeft.transform.position.x) / nextSecond;  // 이동거리/이동시간 = 속도
        //                                오른쪽 끝과 나의 차이!
        float possibleRight = (bottomRight.transform.position.x - transform.position.x) / nextSecond;
        //                                위쪽 끝과 나의 차이!
        float possibleTop = (topLeft.transform.position.y - transform.position.y) / nextSecond;
        //                                아래쪽 끝과 나의 차이!
        float possibleBottom = (transform.position.y - bottomRight.transform.position.y) / nextSecond;

        walk = true;
       
        //                원래 속도로 가면 왼쪽 끝으로 넘어가버림!
        speedX = Random.Range(-(Mathf.Min(possibleLeft, speed)), (Mathf.Min(possibleRight, speed)));  //가중치에 따라 확률이 달라짐
        speedY = Random.Range(-(Mathf.Min(possibleBottom, speed)), (Mathf.Min(possibleTop, speed)));


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


    void SetExp()
    {
        exp = exp + Time.deltaTime;
        if (level == 3)
        {
            return;
        }

        if (exp >= (expPoint * level))
        {
            level = level + 1;
            exp = 0;
            Manager.GetComponent<GameManager>().ChangeAc(this.gameObject.GetComponent<Animator>(), level);
        }
    }

    public void Touch()
    {
        exp += 1;
        GameObject.Find("LeftText").GetComponent<GelatinCoin>().GetGelatin(id + 1, level);
        GetComponent<Animator>().SetBool("isWalk", false);
        GetComponent<Animator>().SetTrigger("doTouch");
        speedX = 0;
        speedY = 0;
    }

}

