using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : MonoBehaviour
{
    [SerializeField] private float _speed = 5f; // �������� �����������

    private Transform target; // ������ �� ������� ������
    private Rigidbody rb;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        Vector3 targetPosition = new Vector3(target.position.x, transform.position.y, target.position.z); // ������������� ������� ���� �� x � z
        Vector3 lookDirection = (targetPosition - transform.position).normalized; // ����������� ������ ����������� �������

        // ������������ ������ � ����������� ���� ������ �� ���� x � z
        transform.LookAt(transform.position + lookDirection, Vector3.up);


        Vector3 moveDirection = transform.forward;

        // ������� ������ � ������� Rigidbody.MovePosition
        rb.MovePosition(rb.position + moveDirection * _speed * Time.deltaTime);
    }
}




