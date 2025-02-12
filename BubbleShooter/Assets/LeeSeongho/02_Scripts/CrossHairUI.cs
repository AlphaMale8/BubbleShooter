using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class CrossHairUI : MonoBehaviour
{
    private Gun gun;

    private RectTransform crossHairRect;

    private void Awake()
    {
        gun = FindAnyObjectByType<Gun>();
        crossHairRect = GetComponent<RectTransform>();
    }

    private void Update()
    {
        if(gun.MonstersList.Count > 0)
        {
            SetCrossHairRectPosition(gun.MonstersList.First().gameObject);
        }
    }

    /// <summary>
    /// 크로스헤어 UI 위치 설정용 함수
    /// </summary>
    /// <param name="targetObject">조준할 오브젝트</param>
    public void SetCrossHairRectPosition(GameObject targetObject)
    {
        if (targetObject == null) // 오브젝트 없으면 정렬
        {
            crossHairRect.anchoredPosition = Vector2.zero;
            return;
        }

        Camera mainCam = Camera.main;
        Vector3 targetScreenVec = mainCam.WorldToScreenPoint(targetObject.transform.position);

        crossHairRect.anchoredPosition = new Vector2(targetScreenVec.x - (Screen.width / 2), targetScreenVec.y - (Screen.height / 2));
    }
}