using UnityEngine;

public class AmbulancePathFollower : MonoBehaviour
{
    public Transform[] waypoints; // Yol noktalarının Transform referansları
    public float speed = 2f; // Ambulansın hızı
    public float turnSpeed = 2f; // Dönüş hızı
    public float waypointThreshold = 0.5f; // Yol noktasına ne kadar yaklaştığında bir sonraki yol noktasına geçeceği

    private int currentWaypointIndex = 0; // Mevcut hedef yol noktasının indeksi

    void Update()
    {
        if (waypoints.Length == 0)
        {
            Debug.LogWarning("No waypoints set for the ambulance.");
            return;
        }

        // Mevcut hedef yol noktasına yönel
        Vector3 targetPosition = waypoints[currentWaypointIndex].position;
        Vector3 directionToTarget = (targetPosition - transform.position).normalized;

        // Ambulansı hedef yöne doğru döndür
        Quaternion targetRotation = Quaternion.LookRotation(directionToTarget);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);

        // Ambulansı ileri yönde hareket ettir
        transform.position += transform.forward * speed * Time.deltaTime;

        // Eğer hedef yol noktasına yeterince yaklaştıysa bir sonraki yol noktasına geç
        float distanceToWaypoint = Vector3.Distance(transform.position, targetPosition);
        if (distanceToWaypoint < waypointThreshold)
        {
            // Bir sonraki yol noktasına geç
            currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
        }
    }
}
