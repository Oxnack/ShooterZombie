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
        [SerializeField] private float _distance = 1f;
        [SerializeField] private float _time = 1f;
        [SerializeField] private int _damage = 15;


        private GameObject _target; // ������ �� ������� ������
        private Rigidbody _rb;

        private Attack _attack = new Attack();

        private bool _isAttacking = false;


        void Start()
        {
            _target = GameObject.FindGameObjectWithTag("Player");
            _rb = GetComponent<Rigidbody>();

            _attack.transform = transform;
            _attack.player = _target;

            
            
        }

        void Update()
        {
            Vector3 targetPosition = new Vector3(_target.transform.position.x, transform.position.y, _target.transform.position.z); // ������������� ������� ���� �� x � z
            Vector3 lookDirection = (targetPosition - transform.position).normalized; // ����������� ������ ����������� �������

            // ������������ ������ � ����������� ���� ������ �� ���� x � z
            transform.LookAt(transform.position + lookDirection, Vector3.up);


            Vector3 moveDirection = transform.forward;

            // ������� ������ � ������� Rigidbody.MovePosition
            _rb.MovePosition(_rb.position + moveDirection * _speed * Time.deltaTime);

            if (_isAttacking == false)
            {
                StartCoroutine(_attack.GetDamageByTime(_time, _distance, _damage));
            }
            else
            {

            }
        }
    }

    public class Attack
    {
        public Transform transform;
        public GameObject player;

        public IEnumerator GetDamageByTime(float time, float distance, int damage)
        {
            while (Vector3.Distance(transform.position, player.transform.position) < distance)
            {
                yield return new WaitForSeconds(time);
                player.GetComponent<Player>().Hp.GetAttack(damage);
            }
            
        }
    }
}




