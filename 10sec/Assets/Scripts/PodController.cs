using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PodController : MonoBehaviour
{
    
  private void OnTriggerEnter2D(Collider2D collision){
    if (collision is CapsuleCollider2D){
            Destroy(gameObject);
    }
  }
}
