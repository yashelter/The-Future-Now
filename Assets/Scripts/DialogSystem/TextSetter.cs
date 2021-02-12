using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextSetter : MonoBehaviour
{
    public string key;
    private TextMeshProUGUI text;
    private LocalizationSystem lang;

    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        lang = FindObjectOfType<LocalizationSystem>();
        lang.UpdateDict();
        text.text = lang.GetKey(key);
    }
    public void UpdateText()
    {
        Start();
        text.text = lang.GetKey(key);
    }

}
