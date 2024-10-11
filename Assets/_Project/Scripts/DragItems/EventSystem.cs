using UnityEngine;

public class EventSystem : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    void Update()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = Camera.main.nearClipPlane;
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(mousePosition);

        // ������� ��� �� ������ � ������� �������
        Vector3 direction = mousePos - Camera.main.transform.position;
        direction.Normalize();
        // ��������� Raycast
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.transform.position, direction);

        // ���� ��� "�����" ������
        if (hit.collider != null)
        {
            // ���������, �������� �� "��������" ������ ������ ��� ��������
            if (hit.collider.GetComponent<CellItem>())
            {
                Debug.Log("���� ������ � ������!");
                // ����� ����� �������� ��� ��� ��������� �������
            }
        }
    }
}