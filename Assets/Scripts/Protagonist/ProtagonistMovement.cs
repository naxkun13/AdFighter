using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public class ProtagonistMovement : MonoBehaviour
{
    [SerializeField] private float speed = 250.0f;
    [SerializeField] private Joystick joystickMovement;
    [SerializeField] private Joystick joystickDirection;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float shootingDelay = .1f;
    private float _shootingDelay = 0;

    private Rigidbody2D _body;
    private Animator _anim;

    void Start() {
        _body = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
    }
    
    void Update() {
        Move();
        Fire();
    }

    private void Move() {
        float deltaY = Mathf.Approximately(Input.GetAxis("Vertical"), 0) ? joystickMovement.Vertical : Input.GetAxis("Vertical");
        float deltaX = Mathf.Approximately(Input.GetAxis("Horizontal"), 0) ? joystickMovement.Horizontal : Input.GetAxis("Horizontal");
        float deltaSpeed = speed * Time.deltaTime;                  // Убираем влияние частоты обновления кадров
        Vector2 movement = new Vector2(deltaX, deltaY) * deltaSpeed;
        _body.velocity = movement;

        _anim.SetFloat("speed", Mathf.Abs(deltaX) + Mathf.Abs(deltaY));
    }

    private void Fire() {
        float direction = Mathf.Approximately(Input.GetAxis("Fire"), 0) ? joystickDirection.Horizontal : Input.GetAxis("Fire");

        if (!Mathf.Approximately(direction, 0)) {
            transform.localScale = new Vector3(Mathf.Sign(direction) * 0.5f, 0.5f, 1);

            if (_shootingDelay > 0)
                _shootingDelay -= Time.deltaTime;
            else {
                GameObject _bullet;
                _bullet = Instantiate(bulletPrefab) as GameObject;
                _bullet.transform.position = transform.TransformPoint(Vector2.right * 1.5f);
                _bullet.transform.rotation = transform.rotation;
                _bullet.GetComponent<Bullet>().Initialize(new Vector2(Mathf.Sign(direction), 0));
                _shootingDelay = shootingDelay;
            }
        }
    }
}
