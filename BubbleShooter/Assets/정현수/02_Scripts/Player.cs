using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Player : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 0.1f; // �� ������ ȸ���ϵ��� �ӵ� ����
    [SerializeField] private float lerpTime = 0f; // ȸ�� ������ ���� ����
    [SerializeField] private BallManager ballManager;
    [SerializeField] private GameObject gun;

    private GameObject target = null; // ���� �ٶ󺸴� ������Ʈ

    void Awake()
    {
        GameObject go = Instantiate<GameObject>(gun, transform);
        go.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + 3.0f);
    }

    void Update()
    {
        // ������ �ϳ��� ������, ���� ����� ���� Ÿ������ ����
        if (ballManager.Instance.ballList.Count > 0)
        {
            // ������ �߾� ��ǥ ���
            Vector3 centralPoint = CalculateCentralPoint(ballManager.Instance.ballList);

            // �߾� ��ǥ�� �ٶ󺸵��� ȸ��
            LookAtTarget(centralPoint);
        }
    }

    // ��ǥ ������Ʈ�� ������ �ٶ󺸱�
    private void LookAtTarget(Vector3 targetPosition)
    {
        if (targetPosition != null)
        {
            // ��ǥ ���������� ȸ���� �ε巴�� ����
            targetPosition.y = transform.position.y; // y���� �������Ѽ� x, z �� ȸ���� �߻��ϵ��� ��

            // ȸ�� ������ ���ϱ� ���� ��ǥ�� �÷��̾��� ������ ���
            Quaternion targetRotation = Quaternion.LookRotation(targetPosition - transform.position);

            float lerpTime = Time.deltaTime / 1.0f;

            // ȸ�� ������ ������ ���� (Time.deltaTime ��� �� ���� �� ���)
            lerpTime += lerpTime * rotationSpeed; // ȸ�� �ӵ� ����

            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, lerpTime);
        }
    }

    // ������ �߾� ��ǥ ���
    private Vector3 CalculateCentralPoint(List<GameObject> balls)
    {
        if (balls.Count == 0) return transform.position;

        Vector3 centralPoint = Vector3.zero;
        foreach (var ball in balls)
        {
            centralPoint += ball.transform.position;
        }

        centralPoint /= balls.Count; // ������ ��� ��ǥ�� �߾� ���� ���
        return centralPoint;
    }
}
