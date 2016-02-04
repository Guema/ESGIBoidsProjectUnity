using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BoidsSquadManager
{
    private static BoidsSquadManager instance = null;
    Dictionary<string, List<Boid>> Squads = new Dictionary<string, List<Boid>>();
    Dictionary<string, Vector3> CentersOfSquads = new Dictionary<string, Vector3>();
    float LastActualisation = 0.0f;

    private BoidsSquadManager()
    {
        instance = this;
    }

    ~BoidsSquadManager()
    {
        if (instance == this)
        {
            instance = null;
        }
    }

    public static BoidsSquadManager Instance
    {
        get
        {
            if (instance == null)
            {
                return new BoidsSquadManager();
            }
            return instance;
        }
    }

    public void Register(Boid b, string squad)
    {
        List<Boid> l;
        if (!Squads.TryGetValue(squad, out l))
        {
            Squads[squad] = new List<Boid>();
        }
        Squads[squad].Add(b);
    }

    public void UnRegister(Boid b)
    {
        foreach (KeyValuePair<string, List<Boid>> item in Squads)
        {
            item.Value.Remove(b);
        }
    }

    public void UnRegister(Boid b, string squad)
    {
        Squads[squad].Remove(b);
    }

    private void OnActualization()
    {
        LastActualisation = Time.time;
    }

    public Vector3 GetSquadCenter(string squad)
    {
        Vector3 vec;
        if (Time.time == LastActualisation)
        {
            if (CentersOfSquads.TryGetValue(squad, out vec))
            {
                return vec;
            }
        }
        vec = new Vector3();
        foreach (Boid item in Squads[squad])
        {
            vec += item.transform.position;
        }
        vec = vec / Squads[squad].Count;
        return CentersOfSquads[squad] = vec;
    }
}
