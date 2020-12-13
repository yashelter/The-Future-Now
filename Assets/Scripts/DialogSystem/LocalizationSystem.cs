using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalizationSystem : MonoBehaviour
{
    public Dictionary<string, string> cSVtable;
    private void Start()
    {
        UpdateDict();
    }
    public void UpdateDict()
    {
        int rowN = 0;
        TextAsset table = Resources.Load<TextAsset>("fre");
        string[] lines = table.text.Split(new char[] { '\n' }, System.StringSplitOptions.RemoveEmptyEntries);
        if (!PlayerPrefs.HasKey("language"))
        {
            PlayerPrefs.SetString("language", "eng");
        }
        string[] line0 = lines[0].Split(new char[] { ';' }, System.StringSplitOptions.RemoveEmptyEntries);
        for(int i = 0; i < line0.Length; i++)
        {
            if(line0[i] == PlayerPrefs.GetString("language"))
            {
                rowN = i;
                break;
            }
        }
        cSVtable = new Dictionary<string, string>();
        for (int i = 0; i < lines.Length; i++)
        {
            string[] line = lines[i].Split(new char[] { ';' }, System.StringSplitOptions.RemoveEmptyEntries);
            cSVtable.Add(line[0], line[rowN]);
        }
    }
    public string GetKey(string key)
    {
        return cSVtable[key];
    }
    public void SetLanguage(string lang)
    {
        PlayerPrefs.SetString("language", lang);
        TextSetter[] texts = FindObjectsOfType<TextSetter>();
        foreach (var text in texts)
        {
            text.UpdateText();
        }
    }

}
