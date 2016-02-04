using UnityEngine;
using System.Collections.Generic;
using System.Collections;

[RequireComponent(typeof(Collider))]
public class Boid : Controller
{
    [SerializeField]
    Collider col;
    [SerializeField]
    List<Boid> boids = new List<Boid>();
    Vector3 movement;

    void Reset()
    {
        col = GetComponent<Collider>();
    }

    void Start()
    {
        
    }

    void Update()
    {
        Vector3 perceivedMassCenter = CalculatePerceivedMassCenter();
        
        Vector3 v1 = DoFirstRule(perceivedMassCenter);
        Vector3 v2 = DoSecondRule(perceivedMassCenter);
        //DoThirdRule(perceivedMassCenter);
        movement = movement + v1 + v2;
        movement.Normalize();
        transform.LookAt(transform.position + movement);
        transform.position += movement * Time.deltaTime * unit.GetSpeed();
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

    Vector3 CalculatePerceivedMassCenter()
    {
        Vector3 vec = new Vector3();
        foreach (Boid item in boids)
        {
            vec += item.transform.position;
        }
        vec = vec / boids.Count;
        return vec;
    }

    Vector3 DoFirstRule(Vector3 perceivedMassCenter)
    {
        Vector3 vec = new Vector3();
        vec = (perceivedMassCenter - transform.position) / 100;
        return vec;
    }

    Vector3 DoSecondRule(Vector3 perceivedMassCenter)
    {
        Vector3 vec = new Vector3(0.0f,0.0f,0.0f);
        foreach (Boid item in boids)
        {
            if ((item.transform.position.magnitude - transform.position.magnitude) < 1.0f)
            {
                vec = vec - (item.transform.position - transform.position).normalized;
            }
        }
        return vec;
    }

    void DoThirdRule(Vector3 perceivedMassCenter)
    {

    }
}
