using System.Collections.Generic;
using UnityEngine;

// �� �����͸� ����ȭ�ϱ� ���� �ڵ�
[CreateAssetMenu(fileName = "Ball_Data", menuName = "scritableObject/Ball", order = 1)]
public class BallDataSO : ScriptableObject
{
    [System.Serializable]
    public struct itemFeature
    {
        // Ball�� �ʱⰪ
        public float Speed;
        public float ScaleMod;
        public float CameraToBallDestroyDistance;
        public Vector3 MinVector;
        public Vector3 MaxVector;
    };

    public List<itemFeature> feature = new List<itemFeature>();
}
