using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [Tooltip("항상 BGM은 0번에 저장하기, 그 외 인덱스는 효과음")]
    public SoundDataSO[] datas;
    public List<AudioSource> sources;

    [Tooltip("모든 사운드 파일 개수 (효과음)")]
    public int sourceCount = 8; // 효과음 (BGM1개는 초기화 때 추가해서 총 개수는 sourceCount + 1임)

    [Range(0f, 1f)]
    public float effectSoundValue; // TODO : 나중에 필요하면 private로 변경하기

    public float EffectSoundValue
    {
        get => effectSoundValue;
        set
        {
            effectSoundValue = Mathf.Clamp(value, 0f, 1f);
            SetEffectSoundValue(effectSoundValue);
        }
    }

    [Range(0f, 1f)]
    public float bgmValue;

    public float BgmValue
    {
        get => bgmValue;
        set
        {
            bgmValue = Mathf.Clamp(value, 0f, 1f);
            SetBGMValue(bgmValue);
        }
    }

    private void Awake()
    {
        InitManager();
    }

    private void Start()
    {
#if UNITY_EDITOR
        testInit();
#endif
    }

    private void Update()
    {
#if UNITY_EDITOR
        TestInput();
#endif
    }

    private void InitManager()
    {
        GameObject child = new GameObject("SoundSources");

        for(int i = 0; i < sourceCount + 1; i++)
        {
            AudioSource source = child.AddComponent<AudioSource>();
            sources.Add(source);
            source.playOnAwake = false;
        }

        int bgmIndex = GetSoundClipByType(SoundType.InGameBGM);
        sources[0].clip = datas[bgmIndex].AudioClip;
        sources[0].loop = true;
    }

    public void PlayBGM()
    {
        sources[0].Play();
    }

    public void StopBGM()
    {
        sources[0].Stop();
    }

    public void PlayEffectSound(SoundType type)
    {
        if (type == SoundType.InGameBGM) return; // BGM이면 무시

        int targetIndex = GetEmptyIndex();
        if (targetIndex == -1) return; // 비어있는 소스가 없음

        int soundClipIndex = GetSoundClipByType(type); // datas 배열에서 해당 타입이 있는 인덱스 찾기
        sources[targetIndex].clip = datas[soundClipIndex].AudioClip; // 비어 있는 오디오 소스에서 클립 저장

        StartCoroutine(ClearClipProcess(targetIndex));
    }
    
    private void SetEffectSoundValue(float value)
    { 
        for(int i = 1; i < sources.Count; i++)
        {
            sources[i].volume = value;
        }
    }

    private void SetBGMValue(float value)
    {
        sources[0].volume = value;
    }

    private IEnumerator ClearClipProcess(int sourceIndex)
    {
        float timeElapsed = 0.0f;        

        while (timeElapsed < sources[sourceIndex].clip.length)
        {
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        sources[sourceIndex].clip = null; // 클립 비우기
    }

    /// <summary>
    /// 데이터값들 중 선택한 타입의 위치 인덱스를 구하는 함수
    /// </summary>
    /// <param name="type">구하려는 타입</param>
    /// <returns>해당 사운드클립이 있는 배열 인덱스</returns>
    private int GetSoundClipByType(SoundType type)
    {
        for(int i = 0; i < datas.Length; i++)
        {
            if (datas[i].type == type) return i; // 해당 타입을 가진 인덱스 반환
        }

        return 1; // 없으면 1번째 소리 반환
    }

    // 오디오 소스중 빈 인덱스 찾는 함수
    private int GetEmptyIndex()
    {
        for(int i = 1; i < sources.Count; i++)
        {
            if(sources[i].clip == null) return i; // 비어있는 해당 인덱스 반환
        }

        return -1; // 없으면 0 반환
    }

#if UNITY_EDITOR
    [Space(20f)]
    public SoundType testPlayType;

    private void testInit()
    {
    }
    private void TestInput()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Debug.Log($"1");
            PlayEffectSound(testPlayType);
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            Debug.Log($"2");
            EffectSoundValue += 0.2f;
            BgmValue += 0.2f;
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            Debug.Log($"3");
            EffectSoundValue -= 0.2f;
            BgmValue -= 0.2f;
        }
    }
#endif
}
