using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    private float hp = 0;

    public float Hp
    {
        get => hp;
        set
        {
            hp = value;
            OnHpChange?.Invoke(hp);
        }
    }

    public Action<float> OnHpChange;
}
