using UnityEngine;

public class MovingInStakePanel : MonoBehaviour
{
    // ���� ��� ������������ ������� ����� ���������� �������
    [SerializeField] private GameObject[] _stakePanelPoints;

    // ����������� ��������� �������� ��� �������� �������� ������� ������
    public static int IndexPoint = 0;

    // ���������� ��� �������� ���� (��� ��������)
    private Ray _ray;

    // ����������� ���������� ��� �������� ���������� ����������� ���� � ��������
    public static RaycastHit2D HitInfo;

    private void Start()
    {
        IndexPoint = 0;
    }

    /// <summary>
    /// ��������� ����� ����������� ������� � ������
    /// </summary>
    public void InStakePanel()
    {
        if (IndexPoint != _stakePanelPoints.Length)
        {
            Vector2 screenPoint;

            // ���������� ������� �����: ���� ��� �������
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
