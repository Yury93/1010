using UnityEngine;

public class EventSystem : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    void Update()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = Camera.main.nearClipPlane;
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(mousePosition);

        // Создаем луч из камеры в позицию курсора
        Vector3 direction = mousePos - Camera.main.transform.position;
        direction.Normalize();
        // Выполняем Raycast
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.transform.position, direction);

        // Если луч "хитит" объект
        if (hit.collider != null)
        {
            // Проверяем, является ли "хитнутый" объект нужным нам спрайтом
            if (hit.collider.GetComponent<CellItem>())
            {
                Debug.Log("Мышь попала в спрайт!");
                // Здесь можно добавить код для обработки события
            }
        }
    }
}