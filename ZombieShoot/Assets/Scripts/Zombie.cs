using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : MonoBehaviour
{
    [SerializeField] private float _speed = 5f; // Скорость перемещения

    private Transform target; // Ссылка на целевой объект
    private Rigidbody rb;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        Vector3 targetPosition = new Vector3(target.position.x, transform.position.y, target.position.z); // Устанавливаем позицию цели по x и z
        Vector3 lookDirection = (targetPosition - transform.position).normalized; // Нормализуем вектор направления взгляда

        // Поворачиваем объект в направлении цели только по осям x и z
        transform.LookAt(transform.position + lookDirection, Vector3.up);


        Vector3 moveDirection = transform.forward;

        // Двигаем объект с помощью Rigidbody.MovePosition
        rb.MovePosition(rb.position + moveDirection * _speed * Time.deltaTime);
    }
}




