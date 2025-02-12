using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Player : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 0.1f; // 더 느리게 회전하도록 속도 조절
    [SerializeField] private float lerpTime = 0f; // 회전 비율을 위한 변수
    [SerializeField] private BallManager ballManager;
    [SerializeField] private GameObject gun;

    private GameObject target = null; // 현재 바라보는 오브젝트

    void Awake()
    {
        GameObject go = Instantiate<GameObject>(gun, transform);
        go.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + 3.0f);
    }

    void Update()
    {
        // 공들이 하나라도 있으면, 가장 가까운 공을 타겟으로 설정
        if (ballManager.Instance.ballList.Count > 0)
        {
            // 공들의 중앙 좌표 계산
            Vector3 centralPoint = CalculateCentralPoint(ballManager.Instance.ballList);

            // 중앙 좌표를 바라보도록 회전
            LookAtTarget(centralPoint);
        }
    }

    // 목표 오브젝트를 서서히 바라보기
    private void LookAtTarget(Vector3 targetPosition)
    {
        if (targetPosition != null)
        {
            // 목표 지점으로의 회전만 부드럽게 보간
            targetPosition.y = transform.position.y; // y값을 고정시켜서 x, z 축 회전만 발생하도록 함

            // 회전 각도를 구하기 위해 목표와 플레이어의 방향을 계산
            Quaternion targetRotation = Quaternion.LookRotation(targetPosition - transform.position);

            float lerpTime = Time.deltaTime / 1.0f;

            // 회전 비율을 느리게 증가 (Time.deltaTime 대신 더 작은 값 사용)
            lerpTime += lerpTime * rotationSpeed; // 회전 속도 조절

            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, lerpTime);
        }
    }

    // 공들의 중앙 좌표 계산
    private Vector3 CalculateCentralPoint(List<GameObject> balls)
    {
        if (balls.Count == 0) return transform.position;

        Vector3 centralPoint = Vector3.zero;
        foreach (var ball in balls)
        {
            centralPoint += ball.transform.position;
        }

        centralPoint /= balls.Count; // 공들의 평균 좌표로 중앙 지점 계산
        return centralPoint;
    }
}
