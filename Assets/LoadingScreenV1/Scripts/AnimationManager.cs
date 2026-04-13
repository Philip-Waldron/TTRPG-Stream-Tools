using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    [Header("Character Card Animation Settings")]
    public AnimationCurve TransitionInAnimationCurve;
    public AnimationCurve TextTransitionInAnimationCurve;
    public float TransitionInAnimationDuration = 1.5f;
    public AnimationCurve TransitionOutAnimationCurve;
    public AnimationCurve TextTransitionOutAnimationCurve;
    public float TransitionOutAnimationDuration = 1.5f;

    [Header("Character Card Loading Settings")]
    public Vector2 LoadingIncrementDurationRange = new Vector2(0.25f, 1.25f);
    public Vector2 LoadingPercentIncrementRange = new Vector2(0f, 0.1f);

    [Header("Hint Text Animation Settings")]
    public AnimationCurve HintTextTransitionAnimationCurve;
    public float HintTextFadeDuration = 1f;

    [Header("Loading Text Animation Settings")]
    public AnimationCurve TextTransitionAnimationCurve;
    public float LoadingTextFadeDuration = 0.5f;
}
