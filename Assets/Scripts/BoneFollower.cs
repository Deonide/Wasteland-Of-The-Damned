using UnityEngine;

public class BoneFollower : MonoBehaviour
{
    [SerializeField]
    private Transform m_target;
    [SerializeField]
    private Vector3 m_positionOffset;
    [SerializeField]
    private Vector3 m_rotationOffset;

    void Update()
    {
        //To make sure that the weapon is at the right place
        gameObject.transform.position = m_positionOffset + m_target.position;
        gameObject.transform.rotation = Quaternion.Euler(m_rotationOffset + m_target.rotation.eulerAngles);
    }
}
