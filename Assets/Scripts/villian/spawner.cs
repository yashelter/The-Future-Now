using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawner : MonoBehaviour
{
    public Object spawnObject;
    public float seconds;
    void Start()
    {
        StartCoroutine("spawn");
    }

    public IEnumerator spawn()
    {
        while (true)
        {
            Instantiate(spawnObject);
            yield return new WaitForSeconds(seconds);
        }

    }
    
}
