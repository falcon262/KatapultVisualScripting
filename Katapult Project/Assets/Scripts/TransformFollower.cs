using UnityEngine;
using System.Collections;

public class TransformFollower : MonoBehaviour
{
    //[SerializeField]
    public Transform target;

    //[SerializeField]
    public Vector3 offsetPosition;

    [SerializeField]
    private Space offsetPositionSpace = Space.Self;

    [SerializeField]
    private bool lookAt = true;

    bool isRefresh = false;



    //private bool pitchLock = false;


    private void LateUpdate()
    {
        Refresh();
        
    }

    public void setIsRefresh()
    {
        isRefresh = true;
    }

    public void Refresh()
    {
        if (isRefresh)
        {
            if (target == null)
            {
                Debug.LogWarning("Missing target ref !", this);

                return;
            }

            // compute position
            if (offsetPositionSpace == Space.Self)
            {
                transform.position = target.TransformPoint(offsetPosition);
            }
            else
            {
                transform.position = target.position + offsetPosition;
            }

            // compute rotation
            if (lookAt)
            {
                transform.LookAt(target);
            }
            else
            {
                transform.rotation = target.rotation;
            }
        }
        
    }    
}