using PlayerController;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Enemy
{
    public class Zombie : MonoBehaviour             //����� ��� ��� ������, �������, ������� ������
    {
        [SerializeField] private float _speed = 5f; // �������� �����������

        private Transform _target; // ������ �� ������� ������
        private Rigidbody _rb;

        void Start()
        {
            _target = GameObject.FindGameObjectWithTag("Player").transform;
            _rb = GetComponent<Rigidbody>();
        }

        void Update()
        {
            Vector3 targetPosition = new Vector3(_target.position.x, transform.position.y, _target.position.z); // ������������� ������� ���� �� x � z
            Vector3 lookDirection = (targetPosition - transform.position).normalized; // ����������� ������ ����������� �������

            // ������������ ������ � ����������� ���� ������ �� ���� x � z
            transform.LookAt(transform.position + lookDirection, Vector3.up);


            Vector3 moveDirection = transform.forward;

            // ������� ������ � ������� Rigidbody.MovePosition
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




