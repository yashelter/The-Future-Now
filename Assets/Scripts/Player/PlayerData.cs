using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public float[] playerPos;
    public int[] invetory;
    public int locationId;

    public PlayerData(float x, float y, int[] inv, int locationId)
    {
        playerPos = new float[3] { x, y, 0 };
        invetory = inv;
        this.locationId = locationId;
    }
}
