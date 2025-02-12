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
    /// ũ�ν���� UI ��ġ ������ �Լ�
    /// </summary>
    /// <param name="targetObject">������ ������Ʈ</param>
    public void SetCrossHairRectPosition(GameObject targetObject)
    {
        if (targetObject == null) // ������Ʈ ������ ����
        {
            crossHairRect.anchoredPosition = Vector2.zero;
            return;
        }

        Camera mainCam = Camera.main;
        Vector3 targetScreenVec = mainCam.WorldToScreenPoint(targetObject.transform.position);

        crossHairRect.anchoredPosition = new Vector2(targetScreenVec.x - (Screen.width / 2), targetScreenVec.y - (Screen.height / 2));
    }
}