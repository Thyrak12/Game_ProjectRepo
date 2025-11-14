using UnityEngine;
using TMPro;

public class DynamicCubeBreaker : MonoBehaviour
{
    [Header("Broken Cube Prefab")]
    public GameObject brokenCubePrefab;   // Assign prefab in inspector

    [Header("Optional UI")]
    public TextMeshProUGUI gameText;

    [Header("Game Settings")]
    public int totalCubes = 4;

    [Header("Explosion Settings")]
    public float explosionForce = 300f;
    public float explosionRadius = 2f;
    public float upwardModifier = 0.5f;
    public float torqueForce = 200f;
    public float pieceOffsetRange = 0.2f;
    public float pieceScaleMin = 0.8f;
    public float pieceScaleMax = 1.2f;

    private int cubesHit = 0;
    private bool isHit = false;

    void Update()
    {
        // Press E to break the cube
        if (!isHit && Input.GetKeyDown(KeyCode.E))
        {
            BreakCube();
        }
    }

    private void BreakCube()
    {
        if (brokenCubePrefab == null)
        {
            Debug.LogError("Broken cube prefab not assigned!");
            return;
        }

        // Spawn broken cube prefab at the same position and rotation
        GameObject broken = Instantiate(brokenCubePrefab, transform.position, transform.rotation);

        // Optional: randomly scale the whole prefab
        float randomScale = Random.Range(pieceScaleMin, pieceScaleMax);
        broken.transform.localScale *= randomScale;

        // Get all Rigidbody pieces
        Rigidbody[] pieces = broken.GetComponentsInChildren<Rigidbody>();
        foreach (Rigidbody rb in pieces)
        {
            // Random small position offset for more natural spread
            Vector3 offset = new Vector3(
                Random.Range(-pieceOffsetRange, pieceOffsetRange),
                Random.Range(-pieceOffsetRange, pieceOffsetRange),
                Random.Range(-pieceOffsetRange, pieceOffsetRange)
            );
            rb.transform.position += offset;

            // Enable physics
            rb.isKinematic = false;

            // Apply explosion from prefab center
            rb.AddExplosionForce(explosionForce, broken.transform.position, explosionRadius, upwardModifier, ForceMode.Impulse);

            // Random torque for spinning
            rb.AddTorque(Random.onUnitSphere * torqueForce, ForceMode.Impulse);
        }

        // Hide or destroy the original cube
        MeshRenderer mr = GetComponent<MeshRenderer>();
        if (mr != null) mr.enabled = false;
        Destroy(gameObject, 0.1f);

        isHit = true;

        // Update hit counter
        cubesHit++;
        if (cubesHit >= totalCubes)
        {
            if (gameText != null) gameText.text = "You Win!";
        }
        else
        {
            if (gameText != null) gameText.text = $"Hit {totalCubes - cubesHit} more cubes!";
        }
    }
}
