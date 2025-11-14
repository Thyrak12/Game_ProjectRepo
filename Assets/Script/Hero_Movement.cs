using UnityEngine;

public class Hero_Movement : MonoBehaviour
{
    public float walkSpeed = 2f;
    public float runSpeed = 5f;
    public float rotationSpeed = 100f;

    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
        anim.SetBool("isWalking", false);
        anim.SetBool("isRunning", false);
        anim.SetBool("isHitting", false);
    }

    void Update()
    {
        bool isWalking = false;
        bool isRunning = false;
        float currentSpeed = walkSpeed;

        // --- Run ---
        if (Input.GetKey(KeyCode.R))
        {
            currentSpeed = runSpeed;
            isRunning = true;
        }

        // --- Movement ---
        if (!anim.GetBool("isHitting")) // don't move while hitting
        {
            if (Input.GetKey(KeyCode.W))
            {
                transform.Translate(Vector3.forward * currentSpeed * Time.deltaTime);
                isWalking = true;
            }
            if (Input.GetKey(KeyCode.S))
            {
                transform.Translate(Vector3.back * currentSpeed * Time.deltaTime);
                isWalking = true;
            }
            if (Input.GetKey(KeyCode.A))
            {
                transform.Rotate(Vector3.up, -rotationSpeed * Time.deltaTime);
            }
            if (Input.GetKey(KeyCode.D))
            {
                transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
            }
        }

        // --- Update Animations ---
        anim.SetBool("isWalking", isWalking && !isRunning);
        anim.SetBool("isRunning", isRunning);
        anim.SetBool("isHitting", Input.GetKey(KeyCode.E)); // holding E shows hit animation
    }
}
