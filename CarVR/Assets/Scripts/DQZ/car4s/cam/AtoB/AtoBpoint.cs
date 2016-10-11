using UnityEngine;

public class AtoBpoint : MonoBehaviour {

    //public Transform middPoint;
   // public Transform from, to;

    protected float smooth=5;
    private float distanceP = .1f;//两点的距离
    protected float MaxDistance=.2f;

    protected virtual void AtoB(Transform camTarget, Transform target)
    {
        distanceP = Vector3.Distance(camTarget.position, target.position);
        if (isToB)
        {
            camTarget.position = Vector3.Lerp(camTarget.position, target.position, Time.deltaTime * smooth);
            camTarget.rotation = Quaternion.Slerp(camTarget.rotation, target.rotation, Time.deltaTime * smooth);
        }
    }

    protected bool isToB 
    {
        get
        {
            if (distanceP > MaxDistance)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
    
}
