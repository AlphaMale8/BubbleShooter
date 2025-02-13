using UnityEngine;

public class DialogController : MonoBehaviour
{
    DialogUI dc;

    public DialogDataSO data;

    private void Awake()
    {
        dc = FindAnyObjectByType<DialogUI>();
    }

    private void Start()
    {
        dc.SetDialogText(data, 1.5f);
    }
}
