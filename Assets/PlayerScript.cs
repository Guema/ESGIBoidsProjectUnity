using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour
{
	void Start ()
    {
	
	}
	
	void Update ()
    {
        Vector3 movement = (BoidsSquadManager.Instance.GetSquadCenter("Empire") - transform.position);
        transform.position += movement * Time.deltaTime * 5.0f;
	}

    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(BoidsSquadManager.Instance.GetSquadCenter("Empire"), 5.0f);
    }
}
