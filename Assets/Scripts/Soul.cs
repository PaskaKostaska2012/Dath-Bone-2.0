using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Soul : MonoBehaviour
{
    public float speed = 6f; // ������ �������� ������� ������
    public List<Vector3> targetPoint = new List<Vector3>();
    private int currentTargetIndex = 0; //������� ������ ����� 
    public LineRenderer lineRenderer; //������ ����������

    void Start()
    {
        // �������������� LineRenderer
        lineRenderer = GetComponent<LineRenderer>();
        if (lineRenderer == null)
        {
            lineRenderer = gameObject.AddComponent<LineRenderer>(); // ��������� ���������, ���� ��� ���
        }

        lineRenderer.material = new Material(Shader.Find("Sprites/Default")); // �������� �������� ��� �����
        lineRenderer.startWidth = 0.25f; // ������� ������ �����
        lineRenderer.endWidth = 0.25f; // ������� ����� �����
        lineRenderer.positionCount = 0; // ���������� ����� �����
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // ������� ����� ������ ����
        {
            // �������� ����� ������� � ������� �����������
            Vector3 targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            targetPosition.z = transform.position.z;

            // ��������� ����� � ������ 
            targetPoint.Add(targetPosition);

            // ���� ��� ������ �����, �� ����� ������ �� ��� �������
            if (currentTargetIndex == 0)
            {
                currentTargetIndex = targetPoint.Count - 1;
            }
        }

        // ����������� � ������� �����
        if (targetPoint.Count > 0)
        {
            transform.position = Vector2.MoveTowards(transform.position, targetPoint[currentTargetIndex], speed * Time.deltaTime);

            // �������� ���������� �����
            if (Vector2.Distance(transform.position, targetPoint[currentTargetIndex]) < 0.1f)
            {
                currentTargetIndex = (currentTargetIndex + 1) % targetPoint.Count;
            }

            // ���������� LineRenderer
            lineRenderer.positionCount = targetPoint.Count;
            for (int i = 0; i < targetPoint.Count; i++)
            {
                lineRenderer.SetPosition(i, targetPoint[i]);
            }
        }
    }
}
