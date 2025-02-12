using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player : MonoBehaviour
{
    [SerializeField] private GameObject objectPrefab; // 오브젝트 프리팹
    [SerializeField] private float spawnInterval = 2f; // 오브젝트 생성 간격
    [SerializeField] private float removeDelay = 3f; // 오브젝트가 사라지기까지 대기 시간
    [SerializeField] private float rotationSpeed = 0.1f; // 더 느리게 회전하도록 속도 조절
    [SerializeField] private float lerpTime = 0f; // 회전 비율을 위한 변수

    private List<GameObject> spawnedObjects = new List<GameObject>(); // 생성된 오브젝트들
    private GameObject target = null; // 현재 바라보는 오브젝트
    private bool isFirstTargetSet = false; // 첫 번째 타겟이 설정되었는지 여부

    void Start()
    {
        // 일정 간격마다 오브젝트 생성
        StartCoroutine(SpawnObjects());
    }

    void Update()
    {
        // 첫 번째 타겟이 설정되면 그 타겟만 바라보게 함
        if (target != null)
        {
            LookAtTarget(target);
        }
    }

    // 오브젝트를 일정 간격마다 생성
    private IEnumerator SpawnObjects()
    {
        while (true)
        {
            // 랜덤 위치에 오브젝트 생성
            Vector3 randomPosition = new Vector3(Random.Range(-10f, 10f), 0, Random.Range(-10f, 10f));
            GameObject spawnedObject = Instantiate(objectPrefab, randomPosition, Quaternion.identity);
            spawnedObjects.Add(spawnedObject);

            if (spawnedObjects.Count == 1 && target == null)
            {
                // 첫 번째 타겟을 설정 (생성된 첫 번째 오브젝트)
                target = spawnedObject;
            }

            // 오브젝트가 사라지기까지 기다림
            yield return new WaitForSeconds(removeDelay);
            spawnedObjects.Remove(spawnedObject);
            Destroy(spawnedObject);

            // 타겟을 새로 설정
            if (target != null) // 첫 번째 타겟이 설정된 후에는 거리 비교를 통해 타겟을 변경
            {
                UpdateTarget();
            }

            // 오브젝트가 생성된 후 다시 대기
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    // 가장 가까운 오브젝트를 선택하고 바라보기
    private void UpdateTarget()
    {
        Debug.Log(spawnedObjects.Count);

        // 플레이어와 오브젝트들 간의 거리 계산 후, 가장 가까운 오브젝트 선택
        if (spawnedObjects.Count == 0)
        {
            target = null;
            return;
        }

        float closestDistance = Mathf.Infinity;
        GameObject closestObject = null;

        // 모든 오브젝트들을 순차적으로 비교
        foreach (GameObject obj in spawnedObjects)
        {
            float distance = Vector3.Distance(transform.position, obj.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestObject = obj;
            }
        }

        // 가장 가까운 오브젝트를 타겟으로 설정
        if (closestObject != target) // 타겟이 변경될 때만
        {
            lerpTime = 0.0f;
            target = closestObject;
        }
    }

    // 목표 오브젝트를 서서히 바라보기
    private void LookAtTarget(GameObject target)
    {
        if (target != null)
        {
            // 목표 지점으로의 회전만 부드럽게 보간
            Vector3 targetPosition = target.transform.position;

            // 목표와 플레이어의 위치를 기준으로 y축만 사용하여 각도를 구함
            targetPosition.y = transform.position.y; // y값을 고정시켜서 x, z 축 회전만 발생하도록 함

            // 회전 각도를 구하기 위해 목표와 플레이어의 방향을 계산
            Quaternion targetRotation = Quaternion.LookRotation(targetPosition - transform.position);

            float lerpTime = Time.deltaTime / 1.0f;

            Debug.Log(lerpTime);

            // 회전 비율을 느리게 증가 (Time.deltaTime 대신 더 작은 값 사용)
            lerpTime += lerpTime * rotationSpeed; // 회전 속도 조절

            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, lerpTime);
        }
    }
}
