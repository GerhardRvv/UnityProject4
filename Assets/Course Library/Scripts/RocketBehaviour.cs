using System;
using UnityEngine;

public class RocketBehaviour : MonoBehaviour {

    private Transform _target;
    private float _speed = 15.0f;
    private bool _homing;

    private float _rocketStrength = 15.0f;
    private float _aliveTimer = 5.0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() { }

    // Update is called once per frame
    void Update() {
        if (_homing && _target != null) {
            Vector3 moveDirection = (_target.transform.position - transform.position).normalized;
            transform.position += moveDirection * (_speed * Time.deltaTime);
            transform.LookAt(_target);
        }
    }

    private void OnCollisionEnter(Collision other) {
        if (_target != null) {
            if (other.gameObject.CompareTag(_target.tag)) {
                Rigidbody targetRb = other.gameObject.GetComponent<Rigidbody>();
                Vector3 away = -other.contacts[0].normal;
                targetRb.AddForce(away * _rocketStrength, ForceMode.Impulse);
                Destroy(gameObject);
            }
        }
    }

    public void Fire(Transform newTarget) {
        _target = newTarget;
        _homing = true;
        Destroy(gameObject, _aliveTimer);
    }
}