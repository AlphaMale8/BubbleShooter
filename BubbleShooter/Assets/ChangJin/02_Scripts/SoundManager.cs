using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [Tooltip("�׻� BGM�� 0���� �����ϱ�, �� �� �ε����� ȿ����")]
    public SoundDataSO[] datas;
    public List<AudioSource> sources;

    [Tooltip("��� ���� ���� ���� (ȿ����)")]
    public int sourceCount = 8; // ȿ���� (BGM1���� �ʱ�ȭ �� �߰��ؼ� �� ������ sourceCount + 1��)

    [Range(0f, 1f)]
    public float effectSoundValue; // TODO : ���߿� �ʿ��ϸ� private�� �����ϱ�

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
        if (type == SoundType.InGameBGM) return; // BGM�̸� ����

        int targetIndex = GetEmptyIndex();
        if (targetIndex == -1) return; // ����ִ� �ҽ��� ����

        int soundClipIndex = GetSoundClipByType(type); // datas �迭���� �ش� Ÿ���� �ִ� �ε��� ã��
        sources[targetIndex].clip = datas[soundClipIndex].AudioClip; // ��� �ִ� ����� �ҽ����� Ŭ�� ����

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

        sources[sourceIndex].clip = null; // Ŭ�� ����
    }

    /// <summary>
    /// �����Ͱ��� �� ������ Ÿ���� ��ġ �ε����� ���ϴ� �Լ�
    /// </summary>
    /// <param name="type">���Ϸ��� Ÿ��</param>
    /// <returns>�ش� ����Ŭ���� �ִ� �迭 �ε���</returns>
    private int GetSoundClipByType(SoundType type)
    {
        for(int i = 0; i < datas.Length; i++)
        {
            if (datas[i].type == type) return i; // �ش� Ÿ���� ���� �ε��� ��ȯ
        }

        return 1; // ������ 1��° �Ҹ� ��ȯ
    }

    // ����� �ҽ��� �� �ε��� ã�� �Լ�
    private int GetEmptyIndex()
    {
        for(int i = 1; i < sources.Count; i++)
        {
            if(sources[i].clip == null) return i; // ����ִ� �ش� �ε��� ��ȯ
        }

        return -1; // ������ 0 ��ȯ
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
