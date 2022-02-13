using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager
{
    int _order = 10;
    // Start is called before the first frame update
    Stack<UI_Popup> _popupStack = new Stack<UI_Popup>();
    UI_Scene _sceneUI = null;

    public GameObject Root
    {
        get  //UI_Root 게임오브젝트 생성
        {
            GameObject root = GameObject.Find("@UI_Root");
            if (root == null)
            {
                root = new GameObject { name = "@UI_Root" };
            }
            return root;
        }
    }

    public void SetCanvas(GameObject go, bool sort = true)
    {
        Canvas canvas = Util.GetOrAddComponent<Canvas>(go);
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;  //캔버스 모드 오버레이
        canvas.overrideSorting = true;

        if (sort)
        {
            canvas.sortingOrder = _order; 
            _order = _order + 1;
        }
        else //정렬 off일 경우
        {
            canvas.sortingOrder = 0;
        }
    }

    public T MakeWorldSpaceUI<T>(Transform parent = null, string name = null) where T : UI_Base
    {
        if (string.IsNullOrEmpty(name))
        {
            name = typeof(T).Name;
        }

        GameObject go = Managers.Resource.Instantiate($"UI/WorldSpace/{name}");

        if (parent != null)  //부모가 있을 경우
        {
            go.transform.SetParent(parent);  //부모 아래에 go 를 달아주기
        }

        Canvas canvas = go.GetOrAddComponent<Canvas>();
        canvas.renderMode = RenderMode.WorldSpace;
        canvas.worldCamera = Camera.main;

        return Util.GetOrAddComponent<T>(go);  //컴포넌트 가지고오기

    }

    public T MakeSubItem<T>(Transform parent = null, string name = null) where T : UI_Base
    {
        if (string.IsNullOrEmpty(name))
        {
            name = typeof(T).Name;
        }

        GameObject go = Managers.Resource.Instantiate($"UI/SubItem/{name}");

        if (parent != null)  //부모가 있을 경우
        {
            go.transform.SetParent(parent);  //부모 아래에 go 를 달아주기
        }

        return Util.GetOrAddComponent<T>(go);  //컴포넌트 가지고오기

    }

    public T ShowSceneUI<T>(string name = null) where T : UI_Scene
    {
        if (string.IsNullOrEmpty(name))  //이름이 없는 경우
        {
            name = typeof(T).Name;  //자료형을 이름으로 쓴다
        }

        GameObject go = Managers.Resource.Instantiate($"UI/Scene/{name}");

        T sceneUI = Util.GetOrAddComponent<T>(go);//컴포넌트 가지고오기
        _sceneUI = sceneUI;
      


        go.transform.SetParent(Root.transform);
        return sceneUI;

    }

    public T ShowPopupUI<T>(string name = null) where T : UI_Popup
    {
        if (string.IsNullOrEmpty(name))
        {
            name = typeof(T).Name;
        }

        GameObject go = Managers.Resource.Instantiate($"UI/Popup/{name}");

        T popup = Util.GetOrAddComponent<T>(go);
        _popupStack.Push(popup);  //팝업 스택

        go.transform.SetParent(Root.transform);
            return popup;
        
    }
    public void ClosePopupUI(UI_Popup popup)
    {
        if (_popupStack.Count == 0)
        {
            return;
        }
        if(_popupStack.Peek() != popup)  //기본제공 스택에서 popup을 못 찾은 경우
        {
            Debug.Log("Close Popup Failed!");
            return;
        }
        ClosePopupUI();
    }

    public void ClosePopupUI()
    {
        if(_popupStack.Count == 0)
        {
            return;
        }

        UI_Popup popup = _popupStack.Pop();  //스택에서 꺼내오기
        Managers.Resource.Destroy(popup.gameObject);
        //popup = null;
        _order = _order - 1;
    }

    public void CloseAllPopupUI()
    {
        while(_popupStack.Count > 0)
        {
            ClosePopupUI();
        }
    }

    public void Clear()
    {
        CloseAllPopupUI();
        _sceneUI = null;
    }
}
