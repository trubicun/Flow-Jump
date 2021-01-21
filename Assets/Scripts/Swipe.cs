using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swipe : MonoBehaviour
{
    [Range(0,1f)]
    [SerializeField] private float _distance = 0.15f;
    [SerializeField] private float _angle = 0.15f;
    private Vector3 _fingerStart, _fingerEnd, _mouse;
    private Camera _camera;

    public static Swipe Instance;

    public delegate void EventHandler(Vector3 direction);
    public event EventHandler Swiped;

    private void Awake() => Instance = this;

    private void Start() => _camera = Camera.main;

    private void Update()
    {
        _mouse = Input.mousePosition;
        _mouse.z = 10;
        if (Input.GetMouseButtonDown(0))
        {
            _fingerStart = _camera.ScreenToViewportPoint(_mouse);
        }
        if (Input.GetMouseButton(0))
        {
            _fingerEnd = _camera.ScreenToViewportPoint(_mouse);

            //Else can be deleted
            float distance = Vector3.Distance(_fingerStart, _fingerEnd);
            if (distance > _distance)
            {
                float angle = Mathf.Atan2(_fingerEnd.y - _fingerStart.y, _fingerEnd.x - _fingerStart.x) * Mathf.Rad2Deg;
                if (angle < 0) angle += 360;
                if (PlusMinusRad(angle, 0)) Swiped?.Invoke(Vector3.right);
                else
                if (PlusMinusRad(angle, 360)) Swiped?.Invoke(Vector3.right);
                else
                if (PlusMinusRad(angle, 90)) Swiped?.Invoke(Vector3.forward);
                else
                if (PlusMinusRad(angle, 180)) Swiped?.Invoke(Vector3.left);
            }
        }

    }

    private bool PlusMinusRad(float angle, float rad)
    {
        if (angle < rad + _angle)
        {
            if (angle > rad - _angle)
            {
                return true;
            }
        }
        return false;
    }
}
