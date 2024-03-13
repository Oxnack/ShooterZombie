using Enemy;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;


namespace PlayerController
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private float _speedWalk;
        [SerializeField] private float _jumpPower;
        [SerializeField] private float _sensitivity;
        [SerializeField] private GameObject _camera;
        [SerializeField] private Slider _slider;
        [SerializeField] private Text _text;
        [SerializeField] private int _damage = 30;
        [SerializeField] private float _timeToAttack = 1f;

        private PlayerMove _playerMove = new PlayerMove();
        private PlayerShoot _playerShoot = new PlayerShoot();
        private CameraMouseLook _cameraMouseLook = new CameraMouseLook();
        private PlayerShoot PlayerShoot = new PlayerShoot();    
        public HP Hp = new HP();

        private Rigidbody _rb;
        private Vector3 _walkDirection;
        private bool _isGrounded;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();

            _playerMove.speed = _speedWalk;
            _playerMove.jumpPower = _jumpPower;
            _playerMove.rb = _rb;
            _playerMove.transform = transform;

            _cameraMouseLook.character = transform;
            _cameraMouseLook.camera = _camera;
            _cameraMouseLook.sensitivity = _sensitivity;

            PlayerShoot.time = _timeToAttack;
            PlayerShoot.damage = _damage;

        }
        private void Update()
        {
            _playerMove.Update();
            _cameraMouseLook.Update();

            _slider.value = Hp.hp;
            _text.text = Hp.hp.ToString();

            PlayerShoot.Update();
        }

        private void FixedUpdate()
        {
            _playerMove.FixedUpdate();
        }

        private void OnCollisionEnter(Collision collision)
        {
            _playerMove.OnCollisionEnter(collision);
        }
        private void OnCollisionExit(Collision collision)
        {
            _playerMove.OnCollisionExit(collision);
        }

    }

    public class PlayerMove
    {
        public float speed;
        public float jumpPower;
        public Rigidbody rb;
        public Transform transform;
        private bool _isGrounded = true;
        private Vector3 _walkDirection = new Vector3();

        public void Update()
        {
            float x = Input.GetAxis("Horizontal");             // кнопки влево вправо на клаве и  WASD тоже 
            float z = Input.GetAxis("Vertical");


            Jump(Input.GetKey(KeyCode.Space) && _isGrounded);
            _walkDirection = transform.right * x + transform.forward * z;
        }
        public void FixedUpdate()
        {
            Walk(_walkDirection);
        }

        private void Walk(Vector3 direction)
        {
            rb.MovePosition(rb.position + direction * speed * Time.deltaTime);
        }

        private void Jump(bool canJump)
        {
            if (canJump)
            {
                rb.AddForce(Vector3.up * jumpPower);
                _isGrounded = false;
            }
        }

        public void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.tag == "Ground")
            {
                _isGrounded = true;
            }
        }

        public void OnCollisionExit(Collision collision)
        {
            if (collision.gameObject.tag == "Ground")
            {
                _isGrounded = false;
            }
        }
    }

    public class CameraMouseLook
    {

        public Transform character;
        public GameObject camera;
        public float sensitivity;

        private float _xRotation;

        public void Update()
        {
            if (Input.GetMouseButtonDown(1))
            {
                Cursor.lockState = CursorLockMode.Locked;
            }
            else if (Input.GetKeyDown(KeyCode.Escape))
            {
                Cursor.lockState = CursorLockMode.None;
            }

            Tracking();
        }
        private void Tracking()
        {
            float mouseX = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime; _xRotation -= mouseY;
            _xRotation = Mathf.Clamp(_xRotation, -90, 90); camera.transform.localRotation = Quaternion.Euler(_xRotation, 0, 0);
            character.Rotate(Vector3.up * mouseX);
        }

    }

    public class PlayerShoot
    {
        public int damage = 30;
        public float time = 1f;
        private float raycastDistance = 200f;
        private bool _okToAttack = true;
        public void Update()
        {
            if (Input.GetMouseButtonDown(0) && _okToAttack == true) // Проверяем нажатие левой кнопки мыши
            {
                _okToAttack = false;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, raycastDistance))
                {
                    if (hit.collider.CompareTag("Zombie")) // Проверяем тег объекта, с которым столкнулся луч
                    {
                        hit.collider.GetComponent<Zombie>().Hp.GetAttack(damage); // Вызываем метод GetAttack() у компонента Zombie
                    }
                }
                GetOkToAttack();
            }
        }
        public IEnumerator GetOkToAttack()
        {
            yield return new WaitForSeconds(time);
            _okToAttack = true;
        }
    }

}

