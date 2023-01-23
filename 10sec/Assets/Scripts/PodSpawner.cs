using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PodSpawner : MonoBehaviour
{
    public GameObject game_area;
    public GameObject pod_prefab;

    public int pod_count = 0;
    public int pod_limit = 30;
    public int pod_per_frame = 1;

    public float spawn_circle_radius = 150.0f;
    public float death_circle_radius = 160.0f;

    public float fastest_speed = 10.0f;
    public float slowest_speed = 2.0f;

    // Start is called before the first frame update
    void Start()
    {
        InitialPopulation();
    }

    // Update is called once per frame
    void Update()
    {
        MaintainPopulation();
    }


    void InitialPopulation()
    {
    for (int i=0; i<pod_limit; i++){
        Vector3 position = GetRandomPosition(true);
        PodBehavior pod_behavior = AddPod(position);
        pod_behavior.transform.Rotate(Vector3.forward * Random.Range(0.0f, 360.0f));
    }
}


void MaintainPopulation()
{
    if(pod_count < pod_limit)
    {
        for(int i=0; i<pod_per_frame; i++)
        {
        Vector3 position = GetRandomPosition(false);
        PodBehavior pod_behavior = AddPod(position);
        pod_behavior.transform.Rotate(Vector3.forward * Random.Range(-45.0f,45.0f));
        }
    }

}
    Vector3 GetRandomPosition(bool within_camera)
    {
        Vector3 position = Random.insideUnitCircle;

            if(within_camera == false)
            {
                position = position.normalized;
            }
        position *= spawn_circle_radius;
        position += game_area.transform.position;

        return position;
    }


    PodBehavior AddPod(Vector3 position)
    {
    pod_count += 1;
    GameObject new_pod = Instantiate(
        pod_prefab,
        position,
        Quaternion.FromToRotation(Vector3.up, (game_area.transform.position-position)),
        gameObject.transform
    );


    PodBehavior pod_behavior = new_pod.GetComponent<PodBehavior>();
    pod_behavior.pod_spawner = this;
    pod_behavior.game_area = game_area;
    pod_behavior.speed = Random.Range(slowest_speed, fastest_speed);

    return pod_behavior;
    }
}
