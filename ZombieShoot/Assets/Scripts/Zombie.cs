using PlayerController;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Enemy
{
    public class Zombie : MonoBehaviour             //сорян что без солида, неуспел, дедлайн сгорел
    {
        [SerializeField] private float _speed = 5f; // Скорость перемещения

        private Transform _target; // Ссылка на целевой объект
        private Rigidbody _rb;

        void Start()
        {
            _target = GameObject.FindGameObjectWithTag("Player").transform;
            _rb = GetComponent<Rigidbody>();
        }

        void Update()
        {
            Vector3 targetPosition = new Vector3(_target.position.x, transform.position.y, _target.position.z); // Устанавливаем позицию цели по x и z
            Vector3 lookDirection = (targetPosition - transform.position).normalized; // Нормализуем вектор направления взгляда

            // Поворачиваем объект в направлении цели только по осям x и z
            transform.LookAt(transform.position + lookDirection, Vector3.up);


            Vector3 moveDirection = transform.forward;

            // Двигаем объект с помощью Rigidbody.MovePosition
            _rb.MovePosition(_rb.position + moveDirection * _speed * Time.deltaTime);
        }
    }

    public class Attack
    {
        public Transform transform;
        public GameObject player;

        public void GetAttack(float distance, int damage)
        {
            if (Vector3.Distance(transform.position, player.transform.position) < distance)
            { 
                player.GetComponent<Player>().Hp.GetAttack(damage);
            }
        }

        private IEnumerator GetDamageByTime(float time, float distance)
        {
            yield return new WaitForSeconds(time);
            while ()
        }
    }
}




