using UnityEngine;

public class CreateObjects : MonoBehaviour
{
    [SerializeField] private Transform _canvasPosition; // ������� ������ (�������), ������������ ������� ����� ����������� �������
    [SerializeField] private Transform _spawnPoint; // ����� ������ ����� ��������
    [SerializeField] private GameObject[] _arrayOfPrefabs; // ������ ��������, �� �������� ��������� ������� ���������� ������� ��� ��������
    [SerializeField] private int _cycles; // ���������� ������ �������� ��������

    public int _maxNumberOfObjects; // ������������ ���������� ��������, ������� ����� ���� �������

    private int _allowedNumberOfObjects; // ���������� ���������� ������� ���� �������
    private int _randomIndexObject; // ������ ���������� ������� �� ������� ��������

    public bool IsSpawning; // ����, ������������ ����������� �������� ����� ��������

    private GameObject _newObject; // ��������� ������

    private void Start()
    {
        // ��������� ���������� ������ ������ �� ������������� ���������� �������� � ����� ������� ��������
        _cycles = _maxNumberOfObjects / _arrayOfPrefabs.Length;
        _allowedNumberOfObjects = _cycles; // ������������� ���������� ���������� ������� ������� ������ ���������� ������
        _randomIndexObject = Random.Range(0, _arrayOfPrefabs.Length); // ��������� ������� ������� ������� �� �������
        IsSpawning = true; // �������� ����� �������� ��������
    }

    private void FixedUpdate()
    {
        Create(); // ������ ������ �������� ��������
    }

    /// <summary>
    /// �����, ��������� ����� �������
    /// </summary>
    private void Create()
    {
        if (IsSpawning == true) // ��������� ������� ����������� �������� ��������
        {
            // ��������� ������� ����� ������ �� ������ ������� �������
            _spawnPoint.position = new Vector2(_canvasPosition.position.x, transform.position.y);

            if (_allowedNumberOfObjects > 0) // ���� ��� ����� ��������� ������� �������� ����
            {
                // ������������ ����� ������ � ����� ������
                _newObject = Instantiate(_arrayOfPrefabs[_randomIndexObject], _spawnPoint.position, Quaternion.identity);

                // ����������� ������ ������� �������� �������� ���������� (����� �������)
                _newObject.transform.SetParent(transform, false);

                // ��������� ������� ���������� ��������� �������� �������� ����
                _allowedNumberOfObjects--;

                // ��������� ����� ������������ ����� ��������
                _maxNumberOfObjects--;
            }
            else if (_allowedNumberOfObjects == 0) // ���� ����� ���������
            {
                // ������ ������ �������� ���������� �������
                _randomIndexObject = Random.Range(0, _arrayOfPrefabs.Length);

                // ��������������� ���������� ���������� ��������
                _allowedNumberOfObjects = _cycles;
            }

            if (_maxNumberOfObjects == 0) // ���� ���������� ������������ ����� ��������� ��������
            {
                IsSpawning = false; // ��������� �������� �������� ��������
            }
        }
    }

    // �����, ���������� ��� ������� ������ ����������
    public void OnUpdateButtonDown()
    {
        // �������� ������ ���� ������� �������� � ����� "Barrel"
        GameObject[] newMaxNumberObjects = GameObject.FindGameObjectsWithTag("Barrel");

        // ��������� ������������ ���������� �������� �������� ���������� ����� ��������
        _maxNumberOfObjects = newMaxNumberObjects.Length;

        // ������� ��� ������������ ������� � ����� "Barrel"
        for (int i = 0; i < newMaxNumberObjects.Length; i++)
        {
            Destroy(newMaxNumberObjects[i]); // �������� �������
        }

        // ��������� ������ ���������� �������
        _randomIndexObject = Random.Range(0, _arrayOfPrefabs.Length);

        // ��������������� ���������� ����������� ��������
        _allowedNumberOfObjects = _cycles;


        if (newMaxNumberObjects.Length % 3 != 0)
        {
            IsSpawning = false;
        }
        else
        {
            // �������� �������� ����� ��������
            IsSpawning = true;
        }
    }
}
