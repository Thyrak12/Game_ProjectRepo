using UnityEngine;

public class SphereRoller : MonoBehaviour
{
    [Header("Force Settings")]
    public float pushForce = 10f;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = false;
        rb.useGravity = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Check if hero touches the sphere
        if (collision.gameObject.CompareTag("Hero"))
        {
            // Direction from hero → sphere
            Vector3 pushDir = (transform.position - collision.transform.position).normalized;

            // Apply a burst of force
            rb.AddForce(pushDir * pushForce, ForceMode.Impulse);

            Debug.Log("Sphere pushed by hero!");
        }
    }
}
