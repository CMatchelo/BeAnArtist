using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class CharSave
{
    public static void SaveChar(CharBTN charBTN)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Path.Combine(Application.persistentDataPath, "char.value");
        FileStream stream = new FileStream(path, FileMode.Create);

        CharData data = new CharData(charBTN);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static CharData LoadChar()
    {
        string path = Path.Combine(Application.persistentDataPath, "char.value");
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
}

