using UnityEngine;

public class BulletGaugeUI : HpGaugeUI
{
    private GunController gc;

    private void Awake()
    {
        gc = FindAnyObjectByType<GunController>();
    }

    private void Update()
    {
        if (gc == null) return;

        SetSliderValue(gc.GetGauge());
    }

#if UNITY_EDITOR
    float maxBullet = 30;

    float currnetBullet = 0;
    float CurrentBullet
    {
        get => currnetBullet;
        set
        {
            currnetBullet = Mathf.Clamp(value, 0f, maxBullet);
        }
    }

    protected override void testInit()
    {
        CurrentBullet = maxBullet;
    }

    float recoverAmount = 2;
    float timer = 0;
    float maxTimer = 2f;
    protected override void TestInput()
    {
        Debug.Log(CurrentBullet);
        timer += Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Q))
        {
            Debug.Log("�Ѿ� ������ ����");
            CurrentBullet -= 2f;
            SetSliderValue(CurrentBullet / maxBullet);
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            Debug.Log("�Ѿ� ä���");
            CurrentBullet = maxBullet;
            SetSliderValue(CurrentBullet / maxBullet);
        }

        //if(timer > maxTimer) // �Ѿ� ������ ȸ��
        //{
        //    timer = 0;
        //    CurrentBullet += recoverAmount;
        //    SetSliderValue(CurrentBullet / maxBullet);
        //    Debug.Log("�Ѿ� ������ ����");
        //}
    }
#endif
}
