using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AfterAnimationEndsDestroyer : MonoBehaviour
{
    [SerializeField] private bool DestroyParentToo;
    private void OnEnable()
    {
        float clipLength = gameObject.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length;
        if (DestroyParentToo)
            Destroy(gameObject.transform.parent.gameObject, clipLength);
        else
            Destroy(gameObject, clipLength);
    }
}