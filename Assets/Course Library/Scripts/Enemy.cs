using UnityEngine;

public class Enemy : MonoBehaviour {
    public float speed = 3.0f;

    private Rigidbody _enemyRb;
    private GameObject _player;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        _enemyRb = GetComponent<Rigidbody>();
        _player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update() {
        MoveEnemy();
        DestroyEnemy();
    }

    private void MoveEnemy() {
        Vector3 lookDirection = (_player.transform.position - transform.position).normalized;
        _enemyRb.AddForce(lookDirection * speed);
    }

    private void DestroyEnemy() {
        if (transform.position.y < -10) {
            Destroy(gameObject);
        }
    }
}