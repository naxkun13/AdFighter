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

    private Rigidbody2D _body;
    private Animator _anim;

    void Start() {
        _body = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
    }
    
    void Update() {
        float deltaSpeed = speed * Time.deltaTime;                  // Убираем влияние частоты обновления кадров
        float deltaY = Input.GetAxis("Vertical")   != .0 ? Input.GetAxis("Vertical")   : joystickMovement.Vertical;
        float deltaX = Input.GetAxis("Horizontal") != .0 ? Input.GetAxis("Horizontal") : joystickMovement.Horizontal;
        Vector2 movement = new Vector2(deltaX, deltaY) * deltaSpeed;
        _body.velocity = movement;

        _anim.SetFloat("speed", Mathf.Abs(deltaX) + Mathf.Abs(deltaY));

        float direction = Input.GetAxis("Fire") != .0 ? Input.GetAxis("Fire") : joystickDirection.Horizontal;

        if (!Mathf.Approximately(direction, 0)) {
            transform.localScale = new Vector3(Mathf.Sign(direction) * 0.5f, 0.5f, 1);
        }
    }
}
