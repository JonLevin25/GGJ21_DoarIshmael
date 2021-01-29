using System;
using System.Collections;
using UnityEngine;
using Utils.ScriptableObjects.Audio;

public static class Helper
{
    public static IEnumerator RepeatRoutine(float secs, Action action, Func<bool> condition = null,
        bool startWithAction = false)
    {
        condition ??= () => true;
        if (action == null) throw new ArgumentNullException();
        if (startWithAction && condition()) action();
        while (true)
        {
            yield return new WaitForSeconds(secs);
            if (condition()) action();
        }
    }
}

public class PackageTarget : MonoBehaviour
{
    [SerializeField] private Animator animator;

    [Header("Hit Anim")] [SerializeField] private AnimAction[] onHitAnims;

    [Header("Random Idle Anim")] [SerializeField]
    private bool randomIdleOnEnable;

    [MinMaxRange(0.2f, 20.0f)] [SerializeField] private RangedFloat randomIdleEvery;
    [SerializeField] private AnimAction idleAnim;

    protected virtual void Reset()
    {
        animator = GetComponent<Animator>();
    }
    
    protected virtual void OnEnable()
    {
        StartCoroutine(
            Helper.RepeatRoutine(randomIdleEvery.Random(), RandomIdle, startWithAction: randomIdleOnEnable)
        );
    }

    protected void OnDisable()
    {
        StopAllCoroutines();
    }

    private void RandomIdle() => idleAnim.Invoke(animator);

    public virtual void OnPackageHit(PackageBox package)
    {
        Debug.Log($"{name} ({GetType().Name} PackageHit");
        var hitAnim = onHitAnims.RandomItem();
        hitAnim.Invoke(animator);
    }
}