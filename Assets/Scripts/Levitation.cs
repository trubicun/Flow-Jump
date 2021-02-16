using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

using static Tweeny.Animation.Transformation;
using static Tweeny.Function;

public class Levitation : MonoBehaviour
{
    [SerializeField] private float _duration = 0.3f;
    [SerializeField] private float _hitDuration = 0.3f;
    [SerializeField] private float _height = 0.1f;
    private IEnumerator _levitation;
    private void Start()
    {
        Invoke("StartLevitaion", Random.Range(0f, 1f));
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (_levitation != null) StopCoroutine(_levitation);
        StartCoroutine(Move(SpikeEaseOut, _hitDuration, gameObject, transform.position + collision.relativeVelocity.normalized));
    }

    private void StartLevitaion()
    {
        _levitation = MoveAxis(SpikeEaseInOut, _duration, gameObject, Vector3.up * _height, true);
        StartCoroutine(_levitation);
    }
}
