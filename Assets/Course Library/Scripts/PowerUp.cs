using UnityEngine;

public class PowerUp : MonoBehaviour {

    public enum PowerUpType {
        None,
        Pushback,
        Rockets
    }

    public PowerUpType powerUpType;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() { }

    // Update is called once per frame
    void Update() { }
}