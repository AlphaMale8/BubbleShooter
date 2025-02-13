using UnityEngine;

public enum SoundType
{
    InGameBGM,
    FireA, FireB, FireC,
    GunChange,
    Defeat,
    StageClear
}

[CreateAssetMenu(fileName = "Sound_Name", menuName ="ScriptableObject/Sound", order = 1)]
public class SoundDataSO : ScriptableObject
{
    public SoundType type;
    public AudioClip AudioClip;
}
