using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    private Transform target;
    private readonly Vector3 offsetPosition;

    public CameraFollower()
    {
        offsetPosition = new Vector3(0, 2.0f, -3.5f);
    }

    private void Update()
    {
        if (target == null)
        {
            var robot = GameObject.FindWithTag("Player");
            if (robot == null)
            {
                return;
            }
            target = robot.transform;
        }

        transform.position = target.TransformPoint(offsetPosition);
        transform.LookAt(target);
    }
}
