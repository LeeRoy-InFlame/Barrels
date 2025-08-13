using UnityEngine;

public class MovingInStakePanel : MonoBehaviour
{
    // Поле для сериализации массива точек размещения панелей
    [SerializeField] private GameObject[] _stakePanelPoints;

    // Статическое публичное свойство для хранения текущего индекса панели
    public static int IndexPoint = 0;

    // Переменная для хранения луча (луч кастинга)
    private Ray _ray;

    // Статическая переменная для хранения результата пересечения луча с объектом
    public static RaycastHit2D HitInfo;

    private void Start()
    {
        IndexPoint = 0;
    }

    /// <summary>
    /// Публичный метод перемещения объекта в панель
    /// </summary>
    public void InStakePanel()
    {
        if (IndexPoint != _stakePanelPoints.Length)
        {
            Vector2 screenPoint;

            // Определяем позицию ввода: мышь или касание
            if (Application.isMobilePlatform && Input.touchCount > 0)
            {
                screenPoint = Input.touches[0].position;
            }
            else
            {
                screenPoint = Input.mousePosition;
            }

            _ray = Camera.main.ScreenPointToRay(screenPoint);
            HitInfo = Physics2D.Raycast(_ray.origin, _ray.direction);

            if (HitInfo.collider != null && HitInfo.transform.gameObject.CompareTag("Barrel"))
            {
                HitInfo.transform.position = _stakePanelPoints[IndexPoint].transform.position;
                HitInfo.transform.rotation = Quaternion.identity;
                HitInfo.transform.gameObject.tag = "StakeBarrel";

                if (_stakePanelPoints[IndexPoint] != null)
                {
                    IndexPoint++;
                }
            }
        }
    }

}
