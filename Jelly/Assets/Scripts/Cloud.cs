using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : MonoBehaviour
{
    [SerializeField]
    float speed = 0.1f;
    bool rightMove = true;

    void Start()
    {
        GameManager.update.UpdateMethod -= OnUpdate;
        GameManager.update.UpdateMethod += OnUpdate;
    }


    private void OnUpdate()
    {
        if (rightMove == true)
        {
            this.transform.Translate(1 * speed * Time.deltaTime, 0, 0);
        }
        if (rightMove == false)
        {
            this.transform.Translate(-1 * speed * Time.deltaTime, 0, 0);
        }

        if (this.transform.position.x < -8)
        {
            rightMove = true;
        }
        else if (this.transform.position.x > 8)
        {
            rightMove = false;
        }
    }
}
