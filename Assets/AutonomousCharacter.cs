using UnityEngine;

public class AutonomousCharacter : MonoBehaviour
{
    public float speed = 2f; // Karakterin hızı
    public float detectionRadius = 5f; // Araçları algılama mesafesi
    public float fleeDistance = 3f; // Uzaklaşma mesafesi
    public float fleeDuration = 2f; // Uzaklaşma süresi

    private Vector3 moveDirection; // Karakterin hareket yönü
    private float changeDirectionTime = 2f; // Yön değiştirme süresi
    private float nextChangeTime = 0f; // Sonraki yön değiştirme zamanı
    private bool isFleeing = false; // Kaçma durumu
    private float fleeTimer = 0f; // Uzaklaşma süresi için zamanlayıcı

    void Start()
    {
        // Başlangıçta rastgele bir hareket yönü belirle
        ChangeDirection();
    }

    void Update()
    {
        // Eğer kaçma durumunda değilse, araçları kontrol et
        if (!isFleeing)
        {
            CheckForVehicles();
        }
        else
        {
            // Kaçma süresi dolana kadar hareket et
            fleeTimer += Time.deltaTime;
            if (fleeTimer >= fleeDuration)
            {
                isFleeing = false; // Kaçmayı sona erdir
                ChangeDirection(); // Yeni bir yön belirle
            }
        }

        // Karakteri hareket ettir
        MoveCharacter();
        
        // Yön değiştirme zamanı geldiğinde yeni bir yön belirle
        if (Time.time >= nextChangeTime && !isFleeing)
        {
            ChangeDirection();
        }
    }

    void MoveCharacter()
    {
        // Eğer hareket yönü belirlenmişse karakteri hareket ettir
        if (moveDirection != Vector3.zero) 
        {
            // Karakteri belirtilen yönde hareket ettir
            transform.position += moveDirection * speed * Time.deltaTime;

            // Yön değişikliği
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 5f); // Dönüş hızı
        }
    }

    void ChangeDirection()
    {
        // Rastgele bir yön belirle
        float randomX = Random.Range(-1f, 1f);
        float randomZ = Random.Range(-1f, 1f);
        moveDirection = new Vector3(randomX, 0, randomZ).normalized;

        // Yön değiştirme zamanını güncelle
        nextChangeTime = Time.time + changeDirectionTime;
    }

    void CheckForVehicles()
    {
        // Yakındaki araçları kontrol et
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, detectionRadius);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Vehicle")) // Araçların tag'ini kontrol et
            {
                // Araç yakınsa uzaklaş
                Vector3 fleeDirection = (transform.position - hitCollider.transform.position).normalized;
                transform.position += fleeDirection * speed * Time.deltaTime; // Uzaklaş
                
                // Kaçma durumunu başlat
                isFleeing = true;
                fleeTimer = 0f; // Zamanlayıcıyı sıfırla
                break; // Bir tane araç bulduğunda kaçma işlemi yap
            }
        }
    }
}
