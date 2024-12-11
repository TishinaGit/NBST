using UnityEngine;

public class DetectedStrategy : MonoBehaviour
{
    readonly float detectionAngle;
    readonly float detectionRadius;
    readonly float innerDetectionRadius;

    public DetectedStrategy(float detectionAngle, float detectionRadius, float innerDetectionRadius)
    {
        this.detectionAngle = detectionAngle;
        this.detectionRadius = detectionRadius;
        this.innerDetectionRadius = innerDetectionRadius;
    }

    public bool Execute(Transform player, Transform detector)
    {
        var directionToPlayer = player.position - detector.position;
        var angleToPlayer = Vector3.Angle(directionToPlayer, detector.forward);


        if ((!(angleToPlayer < detectionAngle / 2f) || !(directionToPlayer.magnitude < detectionRadius))
            && !(directionToPlayer.magnitude < innerDetectionRadius))
            return false;

        return true;
    }
}