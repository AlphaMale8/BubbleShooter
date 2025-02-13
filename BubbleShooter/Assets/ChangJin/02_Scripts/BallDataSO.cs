using System.Collections.Generic;
using UnityEngine;

// 볼 데이터를 직렬화하기 위한 코드
[CreateAssetMenu(fileName = "Ball_Data", menuName = "scritableObject/Ball", order = 1)]
public class BallDataSO : ScriptableObject
{
    [System.Serializable]
    public struct BallFeature
    {
        // Ball의 초기값
        public float Speed;
        public float ScaleMod;
        public float CameraToBallDestroyDistance;
        public int Health;
        // 일단 프리팹 자체를 생성하는 것으로 구현
        public GameObject NormalMesh;
        public GameObject DamagedMesh;
    };

    public List<Vector3> SpawnPosition;
    public List<BallFeature> feature = new List<BallFeature>();
}
