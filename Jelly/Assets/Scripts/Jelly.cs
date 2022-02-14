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
    const int expPoint = 50;

    public int id;
    public int level;


    public float exp = 0;
    float nextSecond = 0;

    public static GameObject topLeft;
    public static GameObject bottomRight;

    bool idle = true;
    bool walk = false;

    public bool outside 
    { get
        {
            if (this.transform.position.x < topLeft.transform.position.x || this.transform.position.x > bottomRight.transform.position.x)
            {
                return true;
            }
            else if (this.transform.position.y > topLeft.transform.position.y || this.transform.position.y < bottomRight.transform.position.y)
            {
                return true;
            }
            else
            {
                return false;
            };
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        if (topLeft == null) topLeft = GameObject.Find("TopLeft");
        if (bottomRight == null) bottomRight = GameObject.Find("BottomRight");

        Manager.Input.UpdateMethod -= SetExp;
        Manager.Input.UpdateMethod += SetExp;
        Manager.Input.UpdateMethod -= SetState;
        Manager.Input.UpdateMethod += SetState;
    }



    void SetNextSecond()
    {
        nextSecond = Time.realtimeSinceStartup + Random.Range(minTimerRange, maxTimerRange);
    }


    //���� �ҷ��� ������ �󸶳� �ɸ� ������ nextSecond
    //���⿡ speed�� ���ϸ� ������ �� ������!
    void SetWalk(float nextSecond)
    {
        walk = true;

        //�̵� ������ ���� ����             ���� ���� ���� ����!                  //nextSecond�� ������! ������ ���µ� ��� �ӵ��� ���� �����ϴ���
        float possibleLeft = (transform.position.x - topLeft.transform.position.x) / nextSecond;  // �̵��Ÿ�/�̵��ð� = �ӵ�
        //                                ������ ���� ���� ����!
        float possibleRight = (bottomRight.transform.position.x - transform.position.x) / nextSecond;
        //                                ���� ���� ���� ����!
        float possibleTop = (topLeft.transform.position.y - transform.position.y) / nextSecond;
        //                                �Ʒ��� ���� ���� ����!
        float possibleBottom = (transform.position.y - bottomRight.transform.position.y) / nextSecond;
       
        //                ���� �ӵ��� ���� ���� ������ �Ѿ����!
        speedX = Random.Range(-(Mathf.Min(possibleLeft, speed)), (Mathf.Min(possibleRight, speed)));  //����ġ�� ���� Ȯ���� �޶���
        speedY = Random.Range(-(Mathf.Min(possibleBottom, speed)), (Mathf.Min(possibleTop, speed)));

        GetComponent<SpriteRenderer>().flipX = speedX < 0;

        this.GetComponent<Animator>().SetBool("isWalk", true);
    }


    void SetExp()
    {
        exp = exp + Time.deltaTime;
        if (level == 3)
        {
            return;
        };

        if (exp >= (expPoint * level))
        {
            level = level + 1;
            exp = 0;
            GameManager.ChangeAc(this.GetComponent<Animator>(), level);
            GameObject.Find("SoundManager").GetComponent<SoundManager>().PlaySfxPlayer("Grow");
        };
    }

    public void Touch()
    {
        GameObject.Find("SoundManager").GetComponent<SoundManager>().PlaySfxPlayer("Touch");
        exp += 1;
        GameObject.Find("LeftText").GetComponent<GelatinCoin>().GetGelatin(id + 1, level);
        GetComponent<Animator>().SetBool("isWalk", false);
        GetComponent<Animator>().SetTrigger("doTouch");
        speedX = 0;
        speedY = 0;
        //���� �ð����� ���߱�!
        SetNextSecond(); 
    }

    public void SetState()
    {
        if (nextSecond <= Time.realtimeSinceStartup)
        {
            //���� �ð����� ���߱�!
            SetNextSecond();

            if (idle == true)
            {
                walk = false;
                this.GetComponent<Animator>().SetBool("isWalk", false);
                idle = false;
            }
            else if (idle == false)
            {
                SetWalk(nextSecond - Time.realtimeSinceStartup);
                idle = true;
            };
        };

        if (walk)
        {
            transform.Translate(speedX * Time.deltaTime, speedY * Time.deltaTime, speedY * Time.deltaTime);
        };
    }
}