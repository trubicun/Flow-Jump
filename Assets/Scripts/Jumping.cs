using System.Collections;
using System.Collections.Generic;
using Tweeny;
using UnityEngine;
using static Tweeny.Animation;
using static Tweeny.Animation.Transformation;
using static Tweeny.Function;

public class Jumping : MonoBehaviour
{
    [SerializeField] private float _verticalPower;
    [SerializeField] private float _jumpDuration;
    [SerializeField] private float _horizontalPower;
    [SerializeField] private float _horizontalDuration;
    private Rigidbody _rigidbody;
    private bool _canJump = true;

    private TweenObject _jump;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        Swipe.Instance.Swiped += Swiped;
    }

    private void OnCollisionEnter(Collision collision)
    {
        _rigidbody.velocity += collision.impulse.normalized * _verticalPower;
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
            StartCoroutine(MoveAxis(EaseOut, _horizontalDuration, gameObject, direction));
            _canJump = false;
        }
    }
}
