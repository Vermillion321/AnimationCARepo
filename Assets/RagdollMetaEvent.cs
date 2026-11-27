using UnityEngine;

public class RagdollAnimationEvent : MonoBehaviour
{
    [SerializeField] public RagdollControls ragdollControls;

    public void TriggerRagdoll()
    {
        if (ragdollControls == null)
        {
            Debug.LogWarning("RagdollControls not found");
            return;
        }

        Debug.Log("ragdolling");

        ragdollControls.SetRagdoll(true);
    }
}