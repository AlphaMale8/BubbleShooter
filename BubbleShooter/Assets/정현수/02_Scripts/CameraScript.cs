using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class CameraScripts : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 0.1f; // �� ������ ȸ���ϵ��� �ӵ� ����
    [SerializeField] private float lerpTime = 0f; // ȸ�� ������ ���� ����

    private BallManager ballManager;

    void Start()
    {
        // ���� ���ݸ��� ������Ʈ ����
        ballManager = FindAnyObjectByType<BallManager>();
    }

    void Update()
    {
        if (ballManager.ballList.Count > 0)
        {
            // 공들의 중앙 좌표 계산
            Vector3 centerPosition = GetBallsCenterPosition();

            // 중앙 좌표를 바라보게 설정
            LookAtTargetPosition(centerPosition);
        }
    }

    // 공들의 중앙 좌표 계산
    private Vector3 GetBallsCenterPosition()
    {
        if (ballManager.ballList.Count == 0)
            return transform.position;

        Vector3 sum = Vector3.zero;

        foreach (GameObject ball in ballManager.ballList)
        {
            sum += ball.transform.position;
        }

        // 평균값을 구하여 중앙 좌표 반환
        Vector3 center = sum / ballManager.ballList.Count;

        // y 좌표는 유지해서 회전할 때 높이가 변하지 않게 함
        center.y = transform.position.y;
        return center;
    }

    // 특정 위치를 바라보도록 회전
    private void LookAtTargetPosition(Vector3 targetPosition)
    {
        Quaternion targetRotation = Quaternion.LookRotation(targetPosition - transform.position);

        float lerpFactor = Time.deltaTime * rotationSpeed; // 부드러운 회전을 위한 보간값

        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, lerpFactor);
    }
}