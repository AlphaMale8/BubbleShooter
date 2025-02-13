using UnityEngine;

public class SoundDataSO : ScriptableObject
{
    [System.Serializable]
    public struct AudioData
    {
        public string AudioName;
        public AudioClip AudioClip;
    };
}
