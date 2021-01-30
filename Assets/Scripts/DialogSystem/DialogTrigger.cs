using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogTrigger : MonoBehaviour
{
    private DialogManager dialog;
    private void Start()
    {
        dialog = FindObjectOfType<DialogManager>();
    }
    public void SetReference(DialogManager dm)
    {
        dialog = dm;
    }
    public void Trigger()
    {
        dialog.StartDialog();
    }
    public void GetDialog()
    {
        dialog.GetDialog();
    }
}
