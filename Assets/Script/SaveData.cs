using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SaveData : MonoBehaviour
{
    public static List<ObjectCreator> objects = new List<ObjectCreator>();

    [SerializeField] ObjectCreator objectPrefeb;
   


    const string OBJECT_SUB = "/Object";
    const string OBJECT_COUNT_SUB = "/Object";

    private void Awake()
    {
        LoadObjectData();
    }
    void OnApplicationQuit()
    {
        SavedObject();
    }

    void SavedObject()
    {
        BinaryFormatter bf = new BinaryFormatter();
        string path = Application.persistentDataPath + OBJECT_SUB + SceneManager.GetActiveScene().buildIndex;
        string countpath = Application.persistentDataPath + OBJECT_COUNT_SUB + SceneManager.GetActiveScene().buildIndex;

        FileStream countStrem = new FileStream(countpath, FileMode.Create);
        bf.Serialize(countStrem, objects.Count);
        countStrem.Close();

        for (int i = 0; i<objects.Count; i++)
        {
            FileStream stream = new FileStream(path + i, FileMode.Create);
            ObjectData data = new ObjectData(objects[i]);

            bf.Serialize(stream, data);
            stream.Close();
        }
    }
    
    void LoadObjectData()
    {
        BinaryFormatter bf = new BinaryFormatter();
        string path = Application.persistentDataPath + OBJECT_SUB + SceneManager.GetActiveScene().buildIndex;
        string countpath = Application.persistentDataPath + OBJECT_COUNT_SUB + SceneManager.GetActiveScene().buildIndex;
        int objectCount = 0;

        if (File.Exists(countpath))
        {
            FileStream countStream = new FileStream(countpath, FileMode.Open);
            objectCount = (int) bf.Deserialize(countStream);
            countStream.Close();
        }
        else
        {
            Debug.LogError("path not found in" + countpath);
        }

        for(int i = 0; i < objectCount; i++)
        {
            if (File.Exists(path + i))
            {
                FileStream stream = new FileStream(path + i, FileMode.Open);
                ObjectData data = bf.Deserialize(stream) as ObjectData;

                stream.Close();

                Vector3 pos = new Vector3(data.position[0], data.position[1], data.position[2]);

               
                
            }
            else
            {
                Debug.LogError("path not found in" + path + i);
            }
        }
    }
}
