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

    public GameObject topLeft;
    public GameObject bottomRight;
    public GameObject Manager;

    bool idle = true;
    bool walk = false;
    bool timer = false;
    bool Outside = false;
    Vector3 point;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
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
        if(Outside == false)
        {
            speedX = Random.Range(-0.8f, 0.8f);
            speedY = Random.Range(-0.8f, 0.8f);
        }
        if(Outside == true)
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
            Outside = true;       
        }else if (this.gameObject.transform.position.y> topLeft.transform.position.y || this.gameObject.transform.position.y < bottomRight.transform.position.y)
        {
            Outside = true;
        }
        else
        {
            Outside = false;
        }       
    }

    void SetPoint()
    {
        point = Manager.GetComponent<GameManager>().PointList[Random.Range(0, 2)];
    }
}
