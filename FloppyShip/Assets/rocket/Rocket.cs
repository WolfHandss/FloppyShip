using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    //lookAngles
    private Vector3 upAngle = new Vector3(0, 0, 315);
    private Vector3 downAngle = new Vector3(0, 0, 225);
    //GamePlay
    private bool Dead;
    private bool ContinueGame;
    private PolygonCollider2D RocketCollider;
    //boost
    [SerializeField] private float initialAccelarateUp = 10;
    [SerializeField] private float accelarationRate = 1;
    [SerializeField] private float MaxBoost;
    //gravity
    [SerializeField] private float Gravity;
    [SerializeField] private float GravityRate;
    [SerializeField] private float MaxGravity;
    //Particle
    [SerializeField] private GameObject FireTrail;
    [SerializeField] private GameObject Explosion;
    //rocket
    [SerializeField] private GameObject RocketObj;
    //scriptRef
    private UIManager GameManager;
    //audio
    [SerializeField] private AudioClip ExplosionSound;

    // Start is called before the first frame update
    void Start()
    {
        //set look angle to down at the beginning
        transform.eulerAngles = downAngle;
        //deactivate the fire effect at the beginning
        FireTrail.SetActive(false);

        //audio
        GetComponent<AudioSource>().playOnAwake = false;
        GetComponent<AudioSource>().clip = ExplosionSound;

        Dead = false;
        ContinueGame = false;

        GameManager = GameObject.FindGameObjectWithTag("UI").GetComponent<UIManager>();
        RocketCollider = GetComponent<PolygonCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!Dead)
        {
            InputManager();
        }
    }
    //manages the input of the player
    private void InputManager()
    {
        //KeyDown
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //recet going up acceleration
            initialAccelarateUp = 0f;
            //set fire effect to true
            FireTrail.SetActive(true);
        }
        //KeyHeld
        if (Input.GetKey(KeyCode.Space))
        {
            LookUp();
        }
        else
        {
            LookDown();
        }
        //KeyUp
        if (Input.GetKeyUp(KeyCode.Space))
        {
            //recet gravity
            Gravity = 0f;
            //set fire effect to false
            FireTrail.SetActive(false);
        }
    }
    //function for when rocket is going up
    private void LookUp()
    {
        //set the rotation of the rocket
        transform.eulerAngles = new Vector3(0, 0, Mathf.Lerp(transform.eulerAngles.z, upAngle.z, 10 * Time.deltaTime));
        //move the rocket based on speed
        transform.position += Vector3.up * initialAccelarateUp * Time.deltaTime;
        //if max speed has not been reached keep accelarating
        if(initialAccelarateUp <= MaxBoost)
        {
            initialAccelarateUp += accelarationRate;
        }
    }
    //function for when rocket is going down
    private void LookDown()
    {
        //set the rotation of the rocket
        transform.eulerAngles = new Vector3(0, 0, Mathf.Lerp(transform.eulerAngles.z, downAngle.z, 10 * Time.deltaTime));
        //move the rocket based on gravity
        transform.position += Vector3.up * Time.deltaTime * Gravity;
        //if max gravity has not been reached keep accelarating down
        if (Gravity >= MaxGravity)
        {
            Gravity += -GravityRate;
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Obstacle"))
        {
            GameManager.Dead();
        }
    }

    public void Death()
    {
        RocketObj.SetActive(false);
        Dead = true;
        Destroy(Instantiate(Explosion,new Vector3(transform.position.x, transform.position.y, transform.position.z - 1), transform.rotation), 1);
        GetComponent<AudioSource>().Play();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Score"))
        {
            GameManager.AddToScore();
        }
    }
}

