using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    public static void SaveChar(CharBTN charBTN, string path)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        //string path = Path.Combine(Application.persistentDataPath, "char.value");
        FileStream stream = new FileStream(path, FileMode.Create);
        CharData data = new CharData(charBTN);
        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static void SaveGameManager(GameManager gameManager, string path)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        //string path = Path.Combine(Application.persistentDataPath, "char.value");
        FileStream stream = new FileStream(path, FileMode.Create);
        GameManagerData data = new GameManagerData(gameManager);
        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static CharData LoadChar(string path)
    {
        if (System.IO.File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            CharData data = formatter.Deserialize(stream) as CharData;
            stream.Close();
            return data;
        }
        else
        {
            Debug.LogError("Save file not found in " + path);
            return null;
        }
    }

    public static GameManagerData LoadGameManager(string path)
    {
        if (System.IO.File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            GameManagerData data = formatter.Deserialize(stream) as GameManagerData;
            stream.Close();
            return data;
        }
        else
        {
            Debug.LogError("Save file not found in " + path);
            return null;
        }
    }
}