using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour {
    [SerializeField]
    private Vector3 trgPos;
    [SerializeField]
    private NavMeshAgent agent;

    [SerializeField]
    private AudioClip crowdClip;
    private AudioSource source; 


    public Vector3 TrgPos
    {
        get
        {
            return trgPos;
        }

        set
        {
            trgPos = value;
        }
    }

    private void Awake()
    {
        source = GetComponent<AudioSource>();
    }

    // Use this for initialization
    void Start () {
        agent = GetComponent<NavMeshAgent>();
    }
	
	// Update is called once per frame
	void Update () {
        agent.SetDestination(trgPos);
    }
}
