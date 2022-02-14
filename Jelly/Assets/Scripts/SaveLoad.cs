using System.Collections;
using System.Collections.Generic;
using UnityEngine.Android;
using UnityEngine;
using System.IO;
using System.Text;

public class SaveLoad : MonoBehaviour
{
    float timer = 0;

    // Start is called before the first frame update

    private void OnApplicationQuit()
    {
        Save();
    }

    private void Start()
    {
        Load();
        GameManager.update.UpdateMethod -= OnUpdate;
        GameManager.update.UpdateMethod += OnUpdate;
    }

    // Update is called once per frame
    public void OnUpdate()
    {
        timer = timer + Time.deltaTime;
        if (timer > 20)
        {
            timer = 0;
            Save();
        }
        GameManager.manager.JellyPosition = new Vector3[GameManager.manager.jellyList.Count];
        for (int i = 0; i < GameManager.manager.jellyList.Count; i++)
        {
            GameManager.manager.JellyPosition[i] = GameManager.manager.jellyList[i].transform.position;
        }
    }

    public void Save()
    {
        Debug.Log("Save");

        jellyJsonObject jsonObject = new jellyJsonObject();
        jsonObject.gold = GameManager.manager.gold;
        jsonObject.jellyPoint = GameManager.manager.gelatin;
        jsonObject.jsonNumLevel = GameManager.manager.numLevel;
        jsonObject.jsonClickLevel = GameManager.manager.clickLevel;
        jsonObject.isClear = GameManager.manager.clearCheck;
        jsonObject.bgm = GameManager.soundmanager.bgm;
        jsonObject.sfx = GameManager.soundmanager.sfx;
        jsonObject.jellyTransformArray = new Vector3[GameManager.manager.jellyList.Count];
        for (int i = 0; i < GameManager.manager.jellyList.Count; i++)
        {
            jsonObject.list.Add(new JellyItem(GameManager.manager.jellyList[i].GetComponent<Jelly>().id, GameManager.manager.jellyList[i].GetComponent<Jelly>().level));
            jsonObject.jellyTransformArray[i] = GameManager.manager.JellyPosition[i];
        }
        jsonObject.jellyUnlock = new bool[GameManager.manager.jellyUnlockList.Length];
        for (int i = 0; i < GameManager.manager.jellyUnlockList.Length; i++)
        {
            jsonObject.jellyUnlock[i] = GameManager.manager.jellyUnlockList[i];
        }
        string JellyJson = JsonUtility.ToJson(jsonObject);

        string path = Application.persistentDataPath + "/JellyData.json";

        FileStream currentFile;
        if (File.Exists(path)) File.Delete(path);
        currentFile = File.Create(path);

        currentFile.Write(Encoding.UTF8.GetBytes(JellyJson), 0, JellyJson.Length);

        //File.WriteAllText(Application.persistentDataPath + "/JellyData.json", JellyJson);

        currentFile.Close();
    }


    public void Load()
    {
        string path = Application.persistentDataPath + "/JellyData.json";
        if (!File.Exists(path)) return;

        FileStream currentFile = File.OpenRead(path);

        byte[] loadedByteArray = new byte[currentFile.Length];
        for (int i = 0; i < loadedByteArray.Length; i++)
        {
            loadedByteArray[i] = (byte)currentFile.ReadByte();
        };

        jellyJsonObject jsonObject;
        string Jsonstring = Encoding.UTF8.GetString(loadedByteArray);

        jsonObject = JsonUtility.FromJson<jellyJsonObject>(Jsonstring);

        for (int i = 0; i < jsonObject.list.Count; i++)
        {
            GameObject instanceJelly = Instantiate(GameManager.manager.jellyPrefab);
            instanceJelly.GetComponent<Jelly>().id = jsonObject.list[i].JellyType;
            instanceJelly.GetComponent<SpriteRenderer>().sprite = GameManager.manager.jellySpriteList[jsonObject.list[i].JellyType];
            instanceJelly.GetComponent<Jelly>().level = jsonObject.list[i].Level;
            instanceJelly.transform.position = jsonObject.jellyTransformArray[i];
            GameManager.ChangeAc(instanceJelly.GetComponent<Animator>(), instanceJelly.GetComponent<Jelly>().level);
            GameManager.manager.jellyList.Add(instanceJelly);
        }
        GameManager.manager.gelatin = jsonObject.jellyPoint;
        GameManager.manager.gold = jsonObject.gold;
        GameManager.manager.clickLevel = jsonObject.jsonClickLevel;
        GameManager.manager.numLevel = jsonObject.jsonNumLevel;
        GameManager.manager.clearCheck = jsonObject.isClear;
        GameManager.soundmanager.bgm = jsonObject.bgm;
        GameManager.soundmanager.sfx = jsonObject.sfx;

        GameManager.manager.jellyUnlockList = new bool[jsonObject.jellyUnlock.Length];
        for (int i = 0; i < GameManager.manager.jellyUnlockList.Length; i++)
        {
            GameManager.manager.jellyUnlockList[i] = jsonObject.jellyUnlock[i];
        }
    }
}
