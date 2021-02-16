using System.Collections;
using System.Collections.Generic;
using Tweeny;
using UnityEngine;

using static Tweeny.Animation.Transformation;
using static Tweeny.Function;

public class Jumping : MonoBehaviour
{
    [SerializeField] private float _verticalPower;
    [SerializeField] private float _verticalScale = 0.5f;
    [SerializeField] private float _speed = 1f;
    [SerializeField] private float _horizontalPower;
    private Rigidbody _rigidbody;
    private bool _canJump = true;
    private TweenObject _jump;

    public const float GlobalGravity = -9.81f;

    public delegate void EventHandler(Vector3 direction);
    public event EventHandler Jumped;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        Swipe.Instance.Swiped += Swiped;
    }

    private void FixedUpdate()
    {
        _rigidbody.AddForce(GlobalGravity / _speed * Vector3.up);
    }

    private void OnCollisionEnter(Collision collision)
    {
        _rigidbody.velocity += collision.impulse.normalized * (_verticalPower + _speed * _verticalScale);
        _canJump = true;
    }

    private void Swiped(Vector3 direction)
    {
        Jump(direction * _horizontalPower);
    }

    private void Jump(Vector3 direction)
    {
        if (_canJump)
        {
            Jumped?.Invoke(direction);
            StartCoroutine(MoveAxis(EaseOut, _speed, gameObject, direction));
            _canJump = false;
        }
    }
}
