using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class SaveSystem
{
    public static void SavePlayer(PlayerController pc)
    {
        float x = pc.PlayerTransform.position.x,
              y = pc.PlayerTransform.position.y;
        int[] inv = null;
        int locationId = pc.locationId;
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/player.annet";
        FileStream stream = new FileStream(path, FileMode.Create);
        PlayerData data = new PlayerData(x, y, inv, locationId);
        formatter.Serialize(stream, data);
        stream.Close();
    }
    public static PlayerData LoadPlayer()
    {
        string path = Application.persistentDataPath + "/player.annet";
        if (File.Exists(path)){      
            FileStream stream = new FileStream(path, FileMode.Open);
            BinaryFormatter formatter = new BinaryFormatter();
            PlayerData data = formatter.Deserialize(stream) as PlayerData;
            return data;
        }
        else
        {
            return null;
        }



    }
}
