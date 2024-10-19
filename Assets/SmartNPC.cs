using UnityEngine;

public class SmartNPC : MonoBehaviour
{
    private Animator animator; // Animator bileşeni

    void Start()
    {
        animator = GetComponent<Animator>(); // Animator'u al
        PlayStartAnimation(); // Başlangıç animasyonunu başlat
    }

    void PlayStartAnimation()
    {
        animator.SetTrigger("StartAnimation"); // Başlangıç animasyonunu tetikle
    }

    // Diğer kodlar...
}
