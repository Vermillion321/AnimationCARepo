
using UnityEngine;
using UnityEngine.InputSystem;

public class RagdollControls : MonoBehaviour
{
    [SerializeField] public Animator animator;
    [SerializeField] private KeyCode ragdollKey = KeyCode.R;
    [SerializeField] private MonoBehaviour thirdPersonController;
    [SerializeField] private CharacterController characterController;
    private Rigidbody[] ragdollBodies;

    private bool isRagdoll;

    private void Start()
    {
        ragdollBodies = GetComponentsInChildren<Rigidbody>();

        SetRagdoll(false);
    }

    public void SetRagdoll(bool enabled)
    {
        isRagdoll = enabled;

        Debug.Log("SetRagdoll called: " + enabled);
        Debug.Log("Number of ragdoll bodies: " + ragdollBodies.Length);

        animator.enabled = !enabled;

        foreach (Rigidbody body in ragdollBodies)
        {
            body.isKinematic = !enabled;

            Debug.Log(
                body.name + " | Kinematic: " + body.isKinematic
            );
        }


        // Disable controllers (might be problematic)
        if (thirdPersonController != null)
        {
            thirdPersonController.enabled = !enabled;
        }

        if (characterController != null)
        {
            characterController.enabled = !enabled;
        }
    }
    // works in conjunction with prior method to control ragdoll
    public void ActivateRagdoll(Vector3 force, Vector3 hitPoint)
    {
        if (isRagdoll) return;

        SetRagdoll(true);

        Rigidbody hitBody = GetClosestBody(hitPoint);

        if (hitBody != null)
        {
            hitBody.AddForceAtPosition(
                force,
                hitPoint,
                ForceMode.Impulse
            );
        }
    }

    // Checks for each rigidbody in the ragdoll
    private Rigidbody GetClosestBody(Vector3 point)
    {
        Rigidbody closest = null;
        float closestDistance = float.MaxValue;

        foreach (Rigidbody body in ragdollBodies)
        {
            float distance = Vector3.Distance(
                body.worldCenterOfMass,
                point
            );

            if (distance < closestDistance)
            {
                closestDistance = distance;
                closest = body;
            }
        }

        return closest;
    }

    private void StartSwimming()
    {
        animator.SetTrigger("StartSwim");
        Debug.Log("Swimming animation triggered!");
    }

    // forces ragdoll
    private void Update()
    {
        if (Keyboard.current != null &&
            Keyboard.current.rKey.wasPressedThisFrame)
        {
            StartSwimming();
        }
    }
}
