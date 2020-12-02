using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogManager : MonoBehaviour
{
    [Header("Dialog Settings")]

    public Dialog dialog;
    public TextMeshProUGUI btnText;
    public TextMeshProUGUI dialogText;

    public Queue<string> dialogSentences = new Queue<string>();

    public Animator dialogAnimator;
    private void OnTriggerEnter2D(Collider2D collision)
    {
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
    }
    public void StartDialog()
    {
        dialogSentences.Clear();
        StopAllCoroutines();
        foreach (string sentence in dialog.sentenses)
        {
            dialogSentences.Enqueue(sentence);
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
            yield return null;
            yield return null;
            yield return null;
        }
    }
    public void GetDialog()
    {
        if(dialogSentences.Count > 0)
        {
            string sentense = dialogSentences.Dequeue();
            StartCoroutine(WriteSentence(sentense));
        }
        else
        {
            EndDialog();
        }
    }
}
