using System.Collections.Generic;
using UnityEngine;

// 볼 데이터를 직렬화하기 위한 코드
[CreateAssetMenu(fileName = "Ball_Data", menuName = "scritableObject/Ball", order = 1)]
public class BallDataSO : ScriptableObject
{
    [System.Serializable]
    public struct itemFeature
    {
        // Ball의 초기값
        public float Speed;
        public float ScaleMod;
        public float CameraToBallDestroyDistance;
        public Vector3 MinVector;
        public Vector3 MaxVector;
    };

    public List<itemFeature> feature = new List<itemFeature>();
}
