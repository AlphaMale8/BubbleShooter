using UnityEngine;
using static SoundDataSO;

public class SoundManager : MonoBehaviour
{
    public AudioData[] datas;
    public AudioSource[] sources;

    public void Play(int index)
    {
        //GetComponentsInChildren
        sources[0].clip = datas[0].AudioClip;
        // fire a
        GetComponent<AudioSource>().Play(); // 
        GetComponent<AudioSource>().Stop();
    }
}
