using System;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class ScoreUpdater : MonoBehaviour
{
    [SerializeField] private TMP_Text tmp;
    [SerializeField] private float animTime;
    [SerializeField] private Ease animEase;
    [SerializeField] private Vector3 animScale = new Vector3(2f, 2f, 1f);

    private int _score;
    private Vector3 startScale;

    private Sequence _animSequence;

    private void OnValidate()
    {
        UpdateTween();
    }

    private void Awake()
    {
        startScale = tmp.rectTransform.localScale;
        tmp.text = Customer.SatisfiedCustomers.ToString();
        UpdateTween();
    }

    private void UpdateTween()
    {
        _animSequence?.Kill();

        var halfAnimTime = 0.5f * animTime;
        _animSequence = DOTween.Sequence()
            .Append(tmp.rectTransform.DOScale(animScale, halfAnimTime))
            .Append(tmp.rectTransform.DOScale(startScale, halfAnimTime))
            .SetEase(animEase)
            .SetAutoKill(false)
            .Pause();
    }

    private void OnDestroy() => _animSequence.Kill();

    void Update()
    {
        if (_score != Customer.SatisfiedCustomers)
        {
            SetScore(Customer.SatisfiedCustomers);
        }
    }

    private void SetScore(int satisfiedCustomers)
    {
        _score = satisfiedCustomers;
        tmp.text = _score.ToString();
        AnimateSize();
    }

    [NaughtyAttributes.Button("Test Animate")]
    private void AnimateSize()
    {
        _animSequence.Restart();
        _animSequence.Play();
    }
}
