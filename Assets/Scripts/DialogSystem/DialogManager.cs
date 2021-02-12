using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
    [Header("Dialog Settings")]

    public Dialog dialog;
    public Queue<string> dialogSentences = new Queue<string>();

    private TextMeshProUGUI dialogText;
    private TextMeshProUGUI btnText;
    private Button pauseBTN; 
    private Animator dialogAnimator;

    private bool coroutineEnded = true;
    private string lastSentence;
    private LocalizationSystem localizationSystem;
    
    private void Start()
    {
        // add component of localization
        localizationSystem = FindObjectOfType<LocalizationSystem>();
        // add text component of dialog
        dialogText = GameObject.Find("DialogText").GetComponent<TextMeshProUGUI>();
        // add text component of button
        btnText = GameObject.Find("StartDialogText").GetComponent<TextMeshProUGUI>();
        // add animator component of dialog
        dialogAnimator = GameObject.Find("Dialog").GetComponent<Animator>();
        // add component of pause button, to activate it when need
        pauseBTN = GameObject.Find("PausedButton").GetComponent<Button>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // подготавливаем новый диалог
        FindObjectOfType<DialogTrigger>().SetReference(this);
        btnText.text = localizationSystem.GetKey("startdialog") + '\n' +
            localizationSystem.GetKey(dialog.keyName);
        dialogAnimator.SetBool("isActive", true);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        EndDialog();
    }
    private void EndDialog()
    {
        dialogAnimator.SetBool("isActive", false);
        dialogAnimator.SetBool("inDialog", false);
        pauseBTN.interactable = true;
    }
    public void StartDialog()
    {
        pauseBTN.interactable = false;
        dialogSentences.Clear();
        StopAllCoroutines();
        foreach (string key in dialog.keys)
        {
            dialogSentences.Enqueue(localizationSystem.GetKey(key));
        }
        dialogAnimator.SetBool("inDialog", true);
        GetDialog();

    }
    private IEnumerator WriteSentence(string sentence)
    {
        string written = "";
        foreach (char symbol in sentence.ToCharArray())
        {
            written += symbol;
            dialogText.text = written;
            yield return new WaitForSeconds(.05f);
            // звуке
        }
        coroutineEnded = true;
    }
    public void GetDialog()
    {
        StopAllCoroutines();
        if (coroutineEnded)
        {
            if (dialogSentences.Count > 0)
            {
                string sentense = dialogSentences.Dequeue();
                lastSentence = sentense;
                coroutineEnded = false;
                StartCoroutine(WriteSentence(sentense));
            }
            else
            {
                EndDialog();
            }
        }
        else
        {
            dialogText.text = lastSentence;
            coroutineEnded = true;
        }
       
       
    }
}
