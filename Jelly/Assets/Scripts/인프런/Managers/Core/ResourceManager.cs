using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager 
{ 
    public T Load<T>(string path) where T : Object  //path : 폴더 파일 경로
    {
        if(typeof(T) == typeof(GameObject))  //자료형 비교
        {
            string name = path;
            int index = name.LastIndexOf('/'); //documents/Unity/Prefab.pref
            if(index >= 0)
            {
                name = name.Substring(index + 1); // Substring은 index + 뒤에 있는거부터 싹 다 날림, 날린것을 돌려줌
            }

            GameObject go = Managers.Pool.GetOriginal(name);
            if(go!= null)
            {
                return go as T;
            }
        }

        return Resources.Load<T>(path);
    }

    /// <param name="path">프리팹이름</param>
    /// <param name="parent">누구 아래에 두고 싶을때</param>
    public GameObject Instantiate(string path, Transform parent = null)  //path는 프리팹 이름
    {
        GameObject original = Load<GameObject>($"Prefabs/{path}");
        if (original == null)
        {
            Debug.Log($"Failed to load prefab : {path}");
            return null;
        }

        if(original.GetComponent<Poolable>() != null)  //Poolable 컴포넌트가 들어있는 프리팹일 경우 Pool로 만들어준다.
        {
            return Managers.Pool.Pop(original, parent).gameObject;  //Pool에서 꺼내온다
        }

        GameObject go = Object.Instantiate(original, parent);       
        go.name = original.name;

        return go;
    }

    public void Destroy(GameObject go)
    {
        if (go == null)
        {
            return;
        }
        Poolable poolable = go.GetComponent<Poolable>();  //Poolable 스크립트 달고 있는지 체크
        if (poolable != null)  //pollable이 있으면
        {
            Managers.Pool.Push(poolable);  //활성화된 오브젝트들 비활성화 해서 풀에 들어간다
            return;
        }

        Object.Destroy(go);
    }
}

    
