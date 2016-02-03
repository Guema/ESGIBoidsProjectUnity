using UnityEngine;
using System.Collections.Generic;
using System.Collections;

[RequireComponent(typeof(Collider))]
public class Boid : Controller
{
    [SerializeField]
    Collider col;
    List<Boid> boids = new List<Boid>();
    Vector3 direction;

    void Reset()
    {
        col = GetComponent<Collider>();
    }

    void Start()
    {
        
    }

    void Update()
    {
        Vector3 perceivedMassCenter = new Vector3();
        foreach (Boid item in boids)
        {
            perceivedMassCenter += item.transform.position;
        }
        perceivedMassCenter = perceivedMassCenter / boids.Count;

        //DoFirstRule(perceivedMassCenter);
        DoSecondRule(perceivedMassCenter);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other != col)
        {
            boids.Add(other.GetComponent<Boid>());
        }
    }

    void OnTriggerExit(Collider other)
    {
        Boid b;
        if (boids.Contains(b = other.GetComponent<Boid>()))
        {
            boids.Remove(b);
        }
    }

    void DoFirstRule(Vector3 perceivedMassCenter)
    {
        if ((transform.position - perceivedMassCenter).magnitude > 0.5f)
        {
            
            direction = (perceivedMassCenter - transform.position).normalized;
            direction *= unit.GetSpeed();
            transform.position += direction * Time.deltaTime;
        }
    }

    void DoSecondRule(Vector3 perceivedMassCenter)
    {
        if ((transform.position - perceivedMassCenter).magnitude > 0.5f)
        {
            direction = (perceivedMassCenter - transform.position).normalized;
        }
    }
}
