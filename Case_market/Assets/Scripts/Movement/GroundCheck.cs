


using UnityEngine;

public class GroundCheck : MonoBehaviour 
{
    public string groundTag = "Ground";
    public float groundRange = .5f;

    private float _distanceToGround;

    public void OnUpdate()
    {
        if(Physics.Raycast(transform.position, Vector3.down, out RaycastHit hitInfo, 2f))
        {
            if(hitInfo.collider.CompareTag(groundTag))
            {
                _distanceToGround = Vector3.Distance(transform.position, hitInfo.point);
            }
        }
    }

    public bool IsOnGround()
    {
        if(_distanceToGround <= groundRange) return true;
        else return false;
    }
}