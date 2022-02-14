using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class UpdateManager : MonoBehaviour
{
    public Action UpdateMethod = null;
    public void OnUpdate()
    {
        if (UpdateMethod != null)
        {
            UpdateMethod.Invoke();
        }
    }
}
