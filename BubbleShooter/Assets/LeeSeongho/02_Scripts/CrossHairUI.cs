using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class CrossHairUI : MonoBehaviour
{
    /// <summary>
    /// 가장 가까운 적을 받기위한 Gun
    /// </summary>
    private GunController gunController;

    /// <summary>
    /// UI 위치 변환용 recttranform
    /// </summary>
    private RectTransform crossHairRect;

    private void Awake()
    {
        gunController = FindAnyObjectByType<GunController>();
        crossHairRect = GetComponent<RectTransform>();
    }

    private void Update()
    {
        if(gunController.GetTarget() != null)
        {
            SetCrossHairRectPosition(gunController.GetTarget().gameObject);
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