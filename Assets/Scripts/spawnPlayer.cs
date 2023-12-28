using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnPlayer : MonoBehaviour
{
    public GameObject Player;
    public Transform plan;
    public Vector3 offset=Vector3.zero;
    void Awake()
    {
        Instantiate(Player,plan.position+offset,plan.rotation);
    }

    
}
