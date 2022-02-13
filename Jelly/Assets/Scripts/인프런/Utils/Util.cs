using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Util 
{
    //게임오브젝트에서 컴포넌트를 가지고 오거나 없으면 만들어서 달아주는 부분
    public static T GetOrAddComponent<T>(GameObject go) where T : UnityEngine.Component
    {
        T component = go.GetComponent<T>();
        if (component == null)
        {
            component = go.AddComponent<T>();
        }
        return component;
    }

    public static GameObject FindChild(GameObject go, string name = null, bool recursive = false)
    {
        Transform transform = FindChild<Transform>(go, name, recursive);
        if(transform == null)
        {
            return null;
        }
        return transform.gameObject;
    }

    public static T FindChild<T>(GameObject go, string name = null, bool recursive = false) where T : UnityEngine.Object
    {
        if (go == null)
        {
            return null;
        }
        if(recursive == false) //자식의 자식한테까지는 안감
        {
            for(int i = 0; i < go.transform.childCount; i++)
            {
                //자식 쭉 돌면서 확인하기
                Transform transform = go.transform.GetChild(i);

                //원하는 자식의 이름이 없거나, 맞는 이름의 자식을 찾으면
                if(string.IsNullOrEmpty(name)||transform.name == name)
                {
                    //컴포넌트 찾기
                    T component = transform.GetComponent<T>();
                    if(component != null)
                    {
                        return component;
                    }
                }
            }
            
        }
        else //recursive가 True이면 자식의 자식한테도 전부 돌아주기
        {
            foreach (T component in go.GetComponentsInChildren<T>())
            {
                //원하는 컴포넌트를 찾은 경우에는 그걸 돌려줌
                if(string.IsNullOrEmpty(name) || component.name == name)
                {
                    return component;
                }
            }
        }

        return null;

    }


}
