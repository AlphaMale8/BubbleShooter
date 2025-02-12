using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player : MonoBehaviour
{
    [SerializeField] private GameObject objectPrefab; // ������Ʈ ������
    [SerializeField] private float spawnInterval = 2f; // ������Ʈ ���� ����
    [SerializeField] private float removeDelay = 3f; // ������Ʈ�� ���������� ��� �ð�
    [SerializeField] private float rotationSpeed = 0.1f; // �� ������ ȸ���ϵ��� �ӵ� ����
    [SerializeField] private float lerpTime = 0f; // ȸ�� ������ ���� ����

    private List<GameObject> spawnedObjects = new List<GameObject>(); // ������ ������Ʈ��
    private GameObject target = null; // ���� �ٶ󺸴� ������Ʈ
    private bool isFirstTargetSet = false; // ù ��° Ÿ���� �����Ǿ����� ����

    void Start()
    {
        // ���� ���ݸ��� ������Ʈ ����
        StartCoroutine(SpawnObjects());
    }

    void Update()
    {
        // ù ��° Ÿ���� �����Ǹ� �� Ÿ�ٸ� �ٶ󺸰� ��
        if (target != null)
        {
            LookAtTarget(target);
        }
    }

    // ������Ʈ�� ���� ���ݸ��� ����
    private IEnumerator SpawnObjects()
    {
        while (true)
        {
            // ���� ��ġ�� ������Ʈ ����
            Vector3 randomPosition = new Vector3(Random.Range(-10f, 10f), 0, Random.Range(-10f, 10f));
            GameObject spawnedObject = Instantiate(objectPrefab, randomPosition, Quaternion.identity);
            spawnedObjects.Add(spawnedObject);

            if (spawnedObjects.Count == 1 && target == null)
            {
                // ù ��° Ÿ���� ���� (������ ù ��° ������Ʈ)
                target = spawnedObject;
            }

            // ������Ʈ�� ���������� ��ٸ�
            yield return new WaitForSeconds(removeDelay);
            spawnedObjects.Remove(spawnedObject);
            Destroy(spawnedObject);

            // Ÿ���� ���� ����
            if (target != null) // ù ��° Ÿ���� ������ �Ŀ��� �Ÿ� �񱳸� ���� Ÿ���� ����
            {
                UpdateTarget();
            }

            // ������Ʈ�� ������ �� �ٽ� ���
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    // ���� ����� ������Ʈ�� �����ϰ� �ٶ󺸱�
    private void UpdateTarget()
    {
        Debug.Log(spawnedObjects.Count);

        // �÷��̾�� ������Ʈ�� ���� �Ÿ� ��� ��, ���� ����� ������Ʈ ����
        if (spawnedObjects.Count == 0)
        {
            target = null;
            return;
        }

        float closestDistance = Mathf.Infinity;
        GameObject closestObject = null;

        // ��� ������Ʈ���� ���������� ��
        foreach (GameObject obj in spawnedObjects)
        {
            float distance = Vector3.Distance(transform.position, obj.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestObject = obj;
            }
        }

        // ���� ����� ������Ʈ�� Ÿ������ ����
        if (closestObject != target) // Ÿ���� ����� ����
        {
            lerpTime = 0.0f;
            target = closestObject;
        }
    }

    // ��ǥ ������Ʈ�� ������ �ٶ󺸱�
    private void LookAtTarget(GameObject target)
    {
        if (target != null)
        {
            // ��ǥ ���������� ȸ���� �ε巴�� ����
            Vector3 targetPosition = target.transform.position;

            // ��ǥ�� �÷��̾��� ��ġ�� �������� y�ุ ����Ͽ� ������ ����
            targetPosition.y = transform.position.y; // y���� �������Ѽ� x, z �� ȸ���� �߻��ϵ��� ��

            // ȸ�� ������ ���ϱ� ���� ��ǥ�� �÷��̾��� ������ ���
            Quaternion targetRotation = Quaternion.LookRotation(targetPosition - transform.position);

            float lerpTime = Time.deltaTime / 1.0f;

            Debug.Log(lerpTime);

            // ȸ�� ������ ������ ���� (Time.deltaTime ��� �� ���� �� ���)
            lerpTime += lerpTime * rotationSpeed; // ȸ�� �ӵ� ����

            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, lerpTime);
        }
    }
}
