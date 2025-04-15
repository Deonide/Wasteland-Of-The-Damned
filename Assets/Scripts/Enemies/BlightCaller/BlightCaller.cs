using NUnit.Framework;
using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;

public class BlightCaller : EnemyBase
{
    //<-- Movement -->
    //radius of the movement sphere
    [SerializeField]
    private float m_range;
    //centre of the area the agent wants to move around in
    [SerializeField]
    private Transform m_centrePoint; 
    //<- End movement _>

    [SerializeField]
    private GameObject m_toxicWaste;
    [SerializeField]
    private List<GameObject> m_toxicWasteList = new List<GameObject>();

    protected override void Start()
    {
        base.Start();
    }

    void Update()
    {
        //If the character is done with the path
        if (m_agent.remainingDistance <= m_agent.stoppingDistance)
        {
            Vector3 point;
            //To pass in the center point and radius of the movement area
            if (RandomPoint(m_centrePoint.position, m_range, out point))
            {
                //Makes it visible with the use of gizmo's
                Debug.DrawRay(point, Vector3.up, Color.blue, 1.0f);
                m_agent.SetDestination(point);
            }
        }

        GameObject waste = Instantiate(m_toxicWaste, new Vector3(transform.position.x, 0.1f, transform.position.z), Quaternion.identity);
        m_toxicWasteList.Add(waste);
        if(m_toxicWasteList.Count > 20)
        {
            Destroy(m_toxicWasteList[0]);
            m_toxicWasteList.Remove(waste);
        }
    }

    protected override void FixedUpdate()
    {
        //Needs to stay so that the FixedUpdate from EnemyBase doesnt get used.
    }

    bool RandomPoint(Vector3 center, float range, out Vector3 result)
    {
        //random position within the sphere
        Vector3 randomPoint = center + Random.insideUnitSphere * range; 
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas)) //documentation: https://docs.unity3d.com/ScriptReference/AI.NavMesh.SamplePosition.html
        {
            //the 1.0f is the max distance from the random point to a point on the navmesh, might want to increase if range is big
            //or add a for loop like in the documentation
            result = hit.position;
            return true;
        }

        result = Vector3.zero;
        return false;
    }
}
