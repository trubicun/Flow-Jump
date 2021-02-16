using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static Tweeny.Animation.Transformation;
using static Tweeny.Function;
public class HitScale : MonoBehaviour
{
    [SerializeField] private float _duration;
    [SerializeField] private float _scale;
    private void OnCollisionEnter(Collision collision)
    {
        StartCoroutine(Scale(SpikeEaseInOut, _duration, gameObject, transform.localScale * _scale));
    }
}
