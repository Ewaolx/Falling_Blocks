using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class obstacle_spawn : MonoBehaviour {

    public GameObject obstacle_prefab;
    float screenHalfWidth;
    float screenHalfHeight;
    float time;
    float timeDifficulty;
    public float difficultyIncreaseSec  = 30;
    public float obstacleSpeed = 3;
    public float spawnIntervalSec = 0.8f; //Time interval between two consecutive spawns of obstacle blocks
    public float minObstacleSize = 0.3f;
    public float maxObstacleSize = 0.5f;
    player playerobject;
    // Use this for initialization
    void Start () {
        screenHalfWidth = Camera.main.orthographicSize * Camera.main.aspect;
        screenHalfHeight = Camera.main.orthographicSize;
        time = Time.time;
        timeDifficulty = Time.time;
        playerobject = FindObjectOfType<player>();
    }
	
	// Update is called once per frame
	void Update () {
        Vector2 input = new Vector2(0,-1);
        Vector2 velocity = input.normalized * obstacleSpeed;
        Vector2 position = new Vector2(Random.Range(-screenHalfWidth+1,screenHalfWidth-1), screenHalfHeight - 1);
        Vector3 rotation = new Vector3(0, 0, 0);

        // Setting the angle so that it doesn't go out of screen in middle of falling
        if(position.x>0)
        {
            float angle1 = Mathf.Rad2Deg * Mathf.Atan2((screenHalfWidth) + position.x , screenHalfHeight * 2 );
            float angle2 = Mathf.Rad2Deg * Mathf.Atan2((screenHalfWidth) - position.x , screenHalfHeight * 2);
            rotation.z = Random.Range(-angle1,angle2);
        }
        else if(position.x<0)
        {
            float angle1 = Mathf.Rad2Deg * Mathf.Atan2((screenHalfWidth) - Mathf.Abs(position.x), screenHalfHeight * 2);
            float angle2 = Mathf.Rad2Deg * Mathf.Atan2(Mathf.Abs(position.x) + (screenHalfWidth ),screenHalfHeight * 2);
            rotation.z = Random.Range(-angle1, angle2);
        }


        if(Time.time - timeDifficulty > difficultyIncreaseSec) // After every difficultyIncreaseSec seconds, the difficulty will increase by making following changes to the variables
        {
            print("Lapped");
            timeDifficulty = Time.time;
            obstacleSpeed += 1;
            spawnIntervalSec -= 0.3f;
            maxObstacleSize += 0.7f;
        }

        GameObject obstacle;
        if (Time.time - time > spawnIntervalSec) //spawning obstacle every 1.5 seconds
        {
            obstacle = (GameObject)Instantiate(obstacle_prefab, position, Quaternion.Euler(rotation));
            time = Time.time;
            obstacle.transform.parent = transform;
            obstacle.transform.localScale = new Vector3(Random.Range(minObstacleSize, maxObstacleSize), Random.Range(minObstacleSize, maxObstacleSize), 0);
        }
        foreach (Transform child in transform) //Iterating throught all the child's transform.
        {
            child.transform.Translate(velocity * Time.deltaTime,Space.Self);
            if (child.position.y < -screenHalfHeight)
            {
                Destroy(child.gameObject);
            }
        }
	}
}
