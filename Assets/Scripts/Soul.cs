using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Soul : MonoBehaviour
{
    public float speed = 6f; // задали скорость которая сейчас
    public List<Vector3> targetPoint = new List<Vector3>();
    private int currentTargetIndex = 0; //Создали список точек 
    public LineRenderer lineRenderer; //линния риндеринга

    void Start()
    {
        // Инициализируем LineRenderer
        lineRenderer = GetComponent<LineRenderer>();
        if (lineRenderer == null)
        {
            lineRenderer = gameObject.AddComponent<LineRenderer>(); // Добавляем компонент, если его нет
        }

        lineRenderer.material = new Material(Shader.Find("Sprites/Default")); // Выбираем материал для линии
        lineRenderer.startWidth = 0.25f; // Толщина начала линии
        lineRenderer.endWidth = 0.25f; // Толщина конца линии
        lineRenderer.positionCount = 0; // Изначально линия пуста
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Нажатие левой кнопки мыши
        {
            // Получаем точку нажатия в мировых координатах
            Vector3 targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            targetPosition.z = transform.position.z;

            // Добавляем точку в список 
            targetPoint.Add(targetPosition);

            // Если это первая точка, то сразу задаем ее как текущую
            if (currentTargetIndex == 0)
            {
                currentTargetIndex = targetPoint.Count - 1;
            }
        }

        // Перемещение к текущей точке
        if (targetPoint.Count > 0)
        {
            transform.position = Vector2.MoveTowards(transform.position, targetPoint[currentTargetIndex], speed * Time.deltaTime);

            // Проверка достижения точки
            if (Vector2.Distance(transform.position, targetPoint[currentTargetIndex]) < 0.1f)
            {
                currentTargetIndex = (currentTargetIndex + 1) % targetPoint.Count;
            }

            // Обновление LineRenderer
            lineRenderer.positionCount = targetPoint.Count;
            for (int i = 0; i < targetPoint.Count; i++)
            {
                lineRenderer.SetPosition(i, targetPoint[i]);
            }
        }
    }
}
