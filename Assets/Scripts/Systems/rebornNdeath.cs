using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rebornNdeath : MonoBehaviour
{
    // ���������� ������ � �����, ����� ������� �� ������ ��������� �������
    public void RebornPlayer()
    {
        FindObjectOfType<PlayerController>().ReturnAlive();
    }
    public void KillPlayer()
    {
        FindObjectOfType<PlayerController>().Death();
    }
}
