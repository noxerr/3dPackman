﻿using UnityEngine;
using System.Collections;

public class EnemyLogic : MonoBehaviour {
    public bool canBeEaten;
    public float canBeEatenTime = 7.0f;
    public float spawnTime;
    public Transform SpawnLoc;
    public MonoBehaviour regularIA;
    private MonoBehaviour fleeIA;
    private float timeSinceLastEaten;
    private float timeLastDeath = -80.0f;
    private float lastSpeed;
    private bool dead;
	// Use this for initialization
	void Start () {
        canBeEaten = false;
        fleeIA = gameObject.GetComponent<IAFlee>();
        fleeIA.enabled = false;
        timeSinceLastEaten = 0.0f;
        lastSpeed = GetComponent<NavMeshAgent>().speed;
        dead = false;
    }
    public void startEatenTimer() {
        timeSinceLastEaten = Time.time;
    }
    public void startDeathTimer() {
        timeLastDeath = Time.time;
    }
    public void killGhost() {
        GetComponent<NavMeshAgent>().destination = SpawnLoc.position;
        lastSpeed = GetComponent<NavMeshAgent>().speed;
        GetComponent<NavMeshAgent>().speed = 80000;
        gameObject.GetComponent<CapsuleCollider>().enabled = false;
        MeshRenderer rend = gameObject.GetComponentInChildren<MeshRenderer>();
        rend.enabled = false;
        dead = true;
    }
    public void spawnGhost() {
        GetComponent<NavMeshAgent>().speed = lastSpeed;
        gameObject.GetComponent<CapsuleCollider>().enabled = true;
        MeshRenderer rend = gameObject.GetComponentInChildren<MeshRenderer>();
        rend.enabled = true;
        dead = false;
    }
	// Update is called once per frame
	void Update () {
        float d2 = Time.time;
        if (d2 - timeSinceLastEaten > canBeEatenTime) canBeEaten = false;
        if (d2 - timeLastDeath > spawnTime) spawnGhost();
        if (!dead)
        {
            if (canBeEaten)
            {
                regularIA.enabled = false;
                fleeIA.enabled = true;
            }
            else {
                regularIA.enabled = true;
                fleeIA.enabled = false;
            }
        }
    }
}
