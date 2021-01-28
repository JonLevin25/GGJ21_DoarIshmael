using UnityEngine;

namespace Utils.Events
{
    public class DestroyContainerOnStateExit : StateMachineBehaviour
    {

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo,
            int layerIndex)
        {
            Destroy(animator.gameObject);
        }

    }
}