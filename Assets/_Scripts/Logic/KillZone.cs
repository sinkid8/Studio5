using UnityEngine;

public class KillZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ball"))
        {
            // Trigger camera shake when ball hits the kill zone
            CameraShake.Shake(0.3f, 1.5f);
            GameManager.Instance.KillBall();
        }
    }
}
