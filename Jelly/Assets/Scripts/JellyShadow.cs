using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JellyShadow : MonoBehaviour
{
    public float shadowSize = -0.1f;

    void Start()
    {
        int id = this.GetComponentInParent<Jelly>().id;
        switch (id)
        {
            case 0:
                shadowSize = -0.05f;
                this.transform.localPosition = new Vector3(0, shadowSize, 0);
                break;
            case 6:
                shadowSize = -0.12f;
                this.transform.localPosition = new Vector3(0, shadowSize, 0);
                break;
            case 3:
                shadowSize = -0.14f;
                this.transform.localPosition = new Vector3(0, shadowSize, 0);
                break;
            case 10:
                shadowSize = -0.16f;
                this.transform.localPosition = new Vector3(0, shadowSize, 0);
                break;
            case 11:
                shadowSize = -0.16f;
                this.transform.localPosition = new Vector3(0, shadowSize, 0);
                break;
            default:
                shadowSize = -0.1f;
                this.transform.localPosition = new Vector3(0, shadowSize, 0);
                break;

        }
            

            
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
