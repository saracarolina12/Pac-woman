using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostBoxCollider : MonoBehaviour
{
    public Collider boxCollider;
    // Start is called before the first frame update
    void Start()
    {
        boxCollider  = GetComponent<Collider>();
    }

    // Update is called once per frame
    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player")){
            // boxCollider.enabled = true;
            Debug.Log("player");
        }
        else if(other.CompareTag("Enemy")){
            // boxCollider.enabled = false;
            Debug.Log("ghost");
        }
    }
}
