using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager
{
    #region Pool
    class Pool
    {
        public GameObject Original { get; private set; }
        public Transform Root;// { get; set; }

        Stack<Poolable> _poolStack = new Stack<Poolable>();  //스택 생성

        public void Init(GameObject original, int count = 5)
        {
            Original = original;
            Root = new GameObject().transform;
            Root.name = $"{original.name}_Root";

            for (int i = 0; i < count; i++)
                Push(Create());
        }

        Poolable Create()
        {
            GameObject go = Object.Instantiate<GameObject>(Original);
            go.name = Original.name;
            return go.GetOrAddComponent<Poolable>();
        }

        public void Push(Poolable poolable)  //풀에 넣는거
        {
            if (poolable == null)
                return;

            poolable.transform.parent = Root;
            poolable.gameObject.SetActive(false);  //비활성화
            poolable.IsUsing = false;

            _poolStack.Push(poolable);  //스택에 밀어넣기
        }

        public Poolable Pop(Transform parent)
        {
            Poolable poolable;

            if (_poolStack.Count > 0)//스택에 남은게 있는 경우
                poolable = _poolStack.Pop();//스택에서 꺼내온다
            else  //스택에 남은게 없는 경우
                poolable = Create();//지정 숫자 이상으로 생성할 경우 추가로 생성해준다

            poolable.gameObject.SetActive(true);

            // DontDestroyOnLoad 해제 용도
            if (parent == null)
                poolable.transform.parent = Managers.Scene.CurrentScene.transform;

            poolable.transform.parent = parent;  //부모 아래에 붙임
            poolable.IsUsing = true;

            return poolable;
        }
    }
    #endregion

    Dictionary<string, Pool> _pool = new Dictionary<string, Pool>();  //딕셔너리 안에 스택 관리 클래스 Pool이 있음
    Transform _root;

    public void Init()//초기화
    {
        if (_root == null)
        {
            _root = new GameObject { name = "@Pool_Root" }.transform;
            Object.DontDestroyOnLoad(_root);
        }
    }

    public void CreatePool(GameObject original, int count = 5)
    {
        Pool pool = new Pool();  //클래스 인스턴스 생성
        pool.Init(original, count);  //초기화
        pool.Root.parent = _root;  //@Pool_Root 안에 넣기

        _pool.Add(original.name, pool);
    }

    public void Push(Poolable poolable)
    {
        string name = poolable.gameObject.name;  //이름 밀어넣기
        if (_pool.ContainsKey(name) == false)  //딕셔너리에서 이름 같은거 있는가?
        {
            GameObject.Destroy(poolable.gameObject);  //못찾으면 지운다
            return;
        }

        _pool[name].Push(poolable);  //비활성화로 스택에 밀어넣고 대기시키기
    }

    public Poolable Pop(GameObject original, Transform parent = null)   //맨 마지막거 뽑아오기
    {
        if (_pool.ContainsKey(original.name) == false)
            CreatePool(original);

        return _pool[original.name].Pop(parent);//어느 부모에 붙일 건가?
    }

    public GameObject GetOriginal(string name)
    {
        if (_pool.ContainsKey(name) == false)  //사전에 같은 이름이 없는 경우
            return null;
        return _pool[name].Original;  //최초의 원본
    }

    public void Clear()
    {
        foreach (Transform child in _root)//계층창에서 root부모 오브젝트 지우기
            GameObject.Destroy(child.gameObject);//자식 뺑이 돌면서 다 지우기

        _pool.Clear(); //딕셔너리 클리어
    }
}
