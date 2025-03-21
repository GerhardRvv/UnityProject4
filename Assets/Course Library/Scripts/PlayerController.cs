using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    private Rigidbody _playerRb;
    private GameObject _focalPoint;
    private float _powerUpStrength = 15.0f;

    public bool hasPowerUp = false;
    public GameObject powerUpIndicator;

    public float speed = 10f;

    public PowerUp.PowerUpType currentPowerUp = PowerUp.PowerUpType.None;

    public GameObject rocketPrefab;
    private GameObject tmpRocket;
    private Coroutine powerupCountdown;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        _playerRb = GetComponent<Rigidbody>();
        _focalPoint = GameObject.Find("FocalPoint");
    }

    // Update is called once per frame
    void Update() {
        MovePlayer();
        PowerUpIndicatorPosition();
        ShouldLaunchRockets();
    }
    
    private void ShouldLaunchRockets() {
        if (currentPowerUp == PowerUp.PowerUpType.Rockets && Input.GetKeyDown(KeyCode.F)) {
            LaunchRockets();
        }
    }

    private void MovePlayer() {
        float forwardInput = Input.GetAxis("Vertical");
        _playerRb.AddForce(_focalPoint.transform.forward * (forwardInput * speed));
    }

    private void PowerUpIndicatorPosition() {
        powerUpIndicator.transform.position = transform.position + new Vector3(0, -0.5f, 0);
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("PowerUp")) {
            hasPowerUp = true;

            currentPowerUp = other.gameObject.GetComponent<PowerUp>().powerUpType;
            SetPowerIndicatorState(hasPowerUp);
            Destroy(other.gameObject);

            if (powerupCountdown != null) {
                StopCoroutine(powerupCountdown);
            }
            powerupCountdown = StartCoroutine(PowerUpCountDownRoutine());
        }
    }
    
    IEnumerator PowerUpCountDownRoutine() {
        yield return new WaitForSeconds(7);
        hasPowerUp = false;
        currentPowerUp = PowerUp.PowerUpType.None;
        SetPowerIndicatorState(hasPowerUp);
    }
    
    private void SetPowerIndicatorState(bool isActive) {
        powerUpIndicator.gameObject.SetActive(isActive);
    }

    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.CompareTag("Enemy") && currentPowerUp == PowerUp.PowerUpType.Pushback) {
            Rigidbody enemyRb = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromPlayer = collision.gameObject.transform.position - transform.position;
            
            enemyRb.AddForce(awayFromPlayer * _powerUpStrength, ForceMode.Impulse);
            Debug.Log("Player collided with: " + collision.gameObject.name + " with powerup set to " + currentPowerUp);
        }
    }

    void LaunchRockets() {
        foreach (var enemy in FindObjectsOfType<Enemy>()) {
            tmpRocket = Instantiate(
                rocketPrefab,
                transform.position + Vector3.up,
                Quaternion.identity
            );
            
            tmpRocket.GetComponent<RocketBehaviour>().Fire(enemy.transform);
        }
    }
}