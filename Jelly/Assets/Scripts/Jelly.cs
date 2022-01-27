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
            StartCoroutine(StateTimer());
        }

        if (walk)
        {
            Walk();
        };
    }

    IEnumerator StateTimer()
    {
        float nextSecond = Random.Range(minTimerRange, maxTimerRange);

        if (idle == true)
        {
            Idle();
            idle = false;
        }
        else if (idle == false)
        {
            SetWalk(nextSecond);
            idle = true;
        }
        yield return new WaitForSeconds(nextSecond);

        timer = false;

    }

    void Idle()
    {
        walk = false;
        this.gameObject.GetComponent<Animator>().SetBool("isWalk", false);
    }

    //���� �ҷ��� ������ �󸶳� �ɸ� ������ nextSecond
    //���⿡ speed�� ���ϸ� ������ �� ������!
    void SetWalk(float nextSecond)
    {
        //�̵� ������ ���� ����             ���� ���� ���� ����!                  //nextSecond�� ������! ������ ���µ� ��� �ӵ��� ���� �����ϴ���
        float possibleLeft = (transform.position.x - topLeft.transform.position.x) / nextSecond;  // �̵��Ÿ�/�̵��ð� = �ӵ�
        //                                ������ ���� ���� ����!
        float possibleRight = (bottomRight.transform.position.x - transform.position.x) / nextSecond;
        //                                ���� ���� ���� ����!
        float possibleTop = (topLeft.transform.position.y - transform.position.y) / nextSecond;
        //                                �Ʒ��� ���� ���� ����!
        float possibleBottom = (transform.position.y - bottomRight.transform.position.y) / nextSecond;

        walk = true;
       
        //                ���� �ӵ��� ���� ���� ������ �Ѿ����!
        speedX = Random.Range(-(Mathf.Min(possibleLeft, speed)), (Mathf.Min(possibleRight, speed)));  //����ġ�� ���� Ȯ���� �޶���
        speedY = Random.Range(-(Mathf.Min(possibleBottom, speed)), (Mathf.Min(possibleTop, speed)));

        /*
        if (outside)
        {
            SetPoint();
            speedX = (point - this.gameObject.transform.position).normalized.x;
            speedY = (point - this.gameObject.transform.position).normalized.y;
        }
        else
        {
            speedX = Random.Range(-0.8f, 0.8f);
            speedY = Random.Range(-0.8f, 0.8f);
        }*/

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
        transform.Translate(speedX * Time.deltaTime, speedY * Time.deltaTime, speedY * Time.deltaTime);
    }

    void SetPoint()
    {
        point = Manager.GetComponent<GameManager>().PointList[Random.Range(0, 3)];
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
        GetComponent<Animator>().SetTrigger("doTouch");
        speedX = 0;
        speedY = 0;
        StopCoroutine("StateTimer");
        StartCoroutine("StateTimer");

    }

}

