using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;
    public float speed = 2.0f;
    private GameObject focalpoint;
    public bool hasPowerup;
    private float powerupStrength = 15.0f;
    public GameObject powerupIndicator;
    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        focalpoint = GameObject.Find("Focal Point");
    }

    // Update is called once per frame
    void Update()
    {
       float forwardInput = Input.GetAxis("Vertical");
       playerRb.AddForce(focalpoint.transform.forward * speed * forwardInput); 
       powerupIndicator.transform.position = transform.position
    + new Vector3(0,-0.5f, 0);
    }
    private void onTriggerEnter(Collider other)
    {
        if (other.CompareTag("PowerUp"))
        {
            hasPowerup = true;
            Destroy(other.gameObject);
            StartCoroutine(PowerupCountdownRoutine());
            powerupIndicator.gameObject.SetActive(true);
        }
        IEnumerator PowerupCountdownRoutine() 
        {
            powerupIndicator.gameObject.SetActive(false);
            yield return new WaitForSeconds(7);
            hasPowerup = false;
        }
    }
    private void OnCollisionEnter(Collision collision) 
    {
        if (collision.gameObject.CompareTag("Enemy") && hasPowerup)
        {
            Rigidbody enemyRigidbody = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromPlayer = (collision.gameObject.transform.position
            - transform.position);

            Debug.Log("Collided with" + collision.gameObject.name + " with powerup set to " 
            + hasPowerup);
            enemyRigidbody.AddForce(awayFromPlayer * powerupStrength, ForceMode.Impulse);
        }
    }

}
