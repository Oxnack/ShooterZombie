using PlayerController;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Enemy
{
    public class Zombie : MonoBehaviour             //сорян что без солида, неуспел, дедлайн сгорел
    {
        [SerializeField] private float _speed = 5f; // Скорость перемещения
        [SerializeField] private float _distance = 1f;
        [SerializeField] private float _time = 1f;
        [SerializeField] private int _damage = 15;

        [SerializeField] private Slider _sliderHp;
        [SerializeField] private Text _textHp;


        private GameObject _target; // Ссылка на целевой объект
        private Rigidbody _rb;
        private Animator _animator;
        private Attack _attack = new Attack();
        public HP Hp = new HP();

        private bool _hasDeath = false;



        void Start()
        {
            _target = GameObject.FindGameObjectWithTag("Player");
            _rb = GetComponent<Rigidbody>();
            _animator = GetComponent<Animator>();

            _attack.transform = transform;
            _attack.player = _target;
            _attack.animator = _animator;

            _animator.SetBool("walk", true);
        }

        void Update()
        {
            Vector3 targetPosition = new Vector3(_target.transform.position.x, transform.position.y, _target.transform.position.z); // Устанавливаем позицию цели по x и z
            Vector3 lookDirection = (targetPosition - transform.position).normalized; // Нормализуем вектор направления взгляда

            // Поворачиваем объект в направлении цели только по осям x и z
            if (!_hasDeath) transform.LookAt(transform.position + lookDirection, Vector3.up);


            Vector3 moveDirection = transform.forward;

            // Двигаем объект с помощью Rigidbody.MovePosition
            _rb.MovePosition(_rb.position + moveDirection * _speed * Time.deltaTime);

            _attack.damage = _damage;
            if ( _attack.isAttacking == false)
            {
                _attack.isAttacking = true;
                StartCoroutine(_attack.GetDamageByTime(_time, _distance));
            }

            _sliderHp.value = Hp.hp;
            _textHp.text = Hp.hp.ToString();

            if(Hp.life == false && _hasDeath == false)
            {
                _hasDeath = true;
                _target.GetComponent<Player>().Kills.GetKill(1);
                _speed = 0; 
                _damage = 0;
                _animator.SetBool("death", true);
                StartCoroutine(DelayAnim());
            }
        }

        public IEnumerator DelayAnim()
        {
            _rb.useGravity = false;
            gameObject.GetComponent<CapsuleCollider>().enabled = false; 
            yield return new WaitForSeconds(5f);
            Destroy(gameObject);
        }



    }

    public class Attack
    {
        public Transform transform;
        public GameObject player;
        public Animator animator;
        public bool isAttacking = false;
        public int damage;
        public IEnumerator GetDamageByTime(float time, float distance)
        {
            while (Vector3.Distance(transform.position, player.transform.position) < distance && isAttacking == true)
            {
                animator.SetBool("walk", false);
                animator.SetBool("attack", true);
                yield return new WaitForSeconds(time);
                player.GetComponent<Player>().Hp.GetAttack(damage);
            }
            animator.SetBool("attack", false);
            animator.SetBool("walk", true);
            isAttacking = false;
        }
    }
}




