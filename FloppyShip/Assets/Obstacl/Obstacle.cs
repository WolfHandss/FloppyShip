using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private ObstacleManager OM;
    private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        OM = GameObject.FindGameObjectWithTag("OM").GetComponent<ObstacleManager>();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        DeSpawn();
    }

    private void DeSpawn()
    {
        if(transform.position.x <= -12)
        {
            gameObject.SetActive(false);
            rb.velocity = Vector3.zero;
            OM.ObstacleSpawner();
        }
    }
}
