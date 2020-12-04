using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogTrigger : MonoBehaviour
{
    public DialogManager dialog;
    public void Trigger()
    {
        dialog.StartDialog();
    }
}
