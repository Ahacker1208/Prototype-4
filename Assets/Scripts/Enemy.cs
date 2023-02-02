using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;
    private Rigidbody enemyRb;
    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
       enemyRb = getComponent<Rigidbody>();
       player = GameObject.Find("Playe");
    }

    // Update is called once per frame
    void Update()
    {
      enemyRb.AddForce(lookDirection * speed);  
      vector3 lookDirection = (player.transform.position - transform.position).normalized;
    }
}
