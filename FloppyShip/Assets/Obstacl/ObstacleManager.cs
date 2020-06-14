using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleManager : MonoBehaviour
{
    //gameojects
    [SerializeField] private GameObject Obstacle;
    public List<GameObject> Manager = new List<GameObject>();
    //obstacle Spawn
    private float Spawnx;
    private float ObstacleLength;
    private int amnObstclesOnScreen;
    private int ObstacleMag = 7;
    private bool Beginning;

    [SerializeField] private float PlatformSpeed = 5;
    [SerializeField] private float speedMultiplyer = 0.2f;
    [SerializeField] private float MaxSpeed = 10;

    // Start is called before the first frame update
    void Start()
    {
        Spawnx = 12;
        ObstacleLength = 5f;
        amnObstclesOnScreen = 5;
        Beginning = true;

        //create number off obstacles for object pool
        for (int i = 0; i < ObstacleMag; i++)
        {
            GameObject clone = Instantiate(Obstacle, transform.position, transform.rotation);
            clone.SetActive(false);
            clone.transform.SetParent(transform);
            Manager.Add(clone);
        }
        //spawn obstacles on screen
        for (int i = 0; i < amnObstclesOnScreen; i++)
        {
            ObstacleSpawner();
        }
        Beginning = false;
        Spawnx = 12;
    }

    public void PlayerDied()
    {
        for (int i = 0; i < Manager.Count; i++)
        {
            if (Manager[i].activeSelf)
            {
                Manager[i].GetComponent<Rigidbody>().velocity = Vector3.zero;
            }
        }
    }

    //function to spawn an obstacle at the end of the row
    public void ObstacleSpawner()
    {
        GameObject O_clone = null;
        //find an obstacle thats deactve 
        for(int i = 0; i < Manager.Count; i++)
        {
            if(Manager[i].activeSelf == false)
            {
                O_clone = Manager[i];
                break;
            }
        }

        if (O_clone != null)
        {
            //set to active
            O_clone.SetActive(true);

            //set the y position
            O_clone.transform.position = Vector3.right * Spawnx;
            //set the x position
            O_clone.transform.position += Vector3.up * Random.Range(-3, 3);
            //set speed
            O_clone.GetComponent<Rigidbody>().velocity = -transform.right * PlatformSpeed;
            //for setting up obstacles in the beginning
            if (Beginning)
            {
                Spawnx += ObstacleLength;
            }
            else
            {
                //if current speed is less than max speed
                if (PlatformSpeed < MaxSpeed)
                {
                    //increase speed
                    PlatformSpeed += speedMultiplyer;
                }
            }
        }
    }
}
