using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PodBehavior : MonoBehaviour
{
    public float speed;
    //public Vector2 direction;
    public PodSpawner pod_spawner;
    public GameObject game_area;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    void Move(){
        transform.position += transform.up * (Time.deltaTime * speed);

        float distance = Vector3.Distance(transform.position, game_area.transform.position);
        if(distance > pod_spawner.death_circle_radius){
            RemovePod();
        }
    }

    void RemovePod(){
        Destroy(gameObject);
        pod_spawner.pod_count -=1;
    }
}
