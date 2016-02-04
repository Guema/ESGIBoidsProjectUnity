using UnityEngine;
using System.Collections.Generic;
using System.Collections;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Rigidbody))]
public class Boid : Controller
{
    [SerializeField]
    Collider col;
    [SerializeField]
    List<Boid> boids = new List<Boid>();
    Vector3 movement = new Vector3();
    Vector3 perceivedMassCenter = new Vector3();

    void Reset()
    {
        col = GetComponent<Collider>();
    }

    void Start()
    {

    }

    void OnEnable()
    {
        BoidsSquadManager.Instance.Register(this, unit.GetFaction());
    }

    void OnDisable()
    {
        BoidsSquadManager.Instance.UnRegister(this);
    }

    void Update()
    {
        perceivedMassCenter = CalculatePerceivedMassCenter();
        
        Vector3 v1 = DoFirstRule(perceivedMassCenter);
        Vector3 v2 = DoSecondRule(perceivedMassCenter);
        //DoThirdRule(perceivedMassCenter);
        movement = movement + v1 + v2;
        transform.LookAt(transform.position + movement);
        transform.position += movement.normalized * Time.deltaTime * unit.GetSpeed();
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

    #region _RULES_ALGORITHMS_

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
        Vector3 vec = (perceivedMassCenter - transform.position);
        return vec;
    }

    Vector3 DoSecondRule(Vector3 perceivedMassCenter)
    {
        Vector3 vec = new Vector3();
        foreach (Boid item in boids)
        {
            if ((item.transform.position - transform.position).magnitude < 50.0f)
            {
                vec = vec - (item.transform.position - transform.position);
            }
        }
        return vec;
    }

    void DoThirdRule(Vector3 perceivedMassCenter)
    {

    }

    #endregion

    #region _DEBUG_FUNCS_
    void OnDrawGizmosSelected()
    {
        //Always On
        
    }

    void OnDrawGizmos()
    {
        //Gizmos.color = Color.white;
        //Gizmos.DrawSphere(perceivedMassCenter, 4.0f);
        //Gizmos.DrawLine(transform.position, perceivedMassCenter);
    }

    #endregion
}
