using UnityEngine;

[CreateAssetMenu(fileName ="Dialog_NonName", menuName = "ScriptableObject/Dialog", order = 1)]
public class DialogDataSO : ScriptableObject
{
    [TextArea]
    public string content;
}
