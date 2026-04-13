namespace V1
{
    using System.Collections;
    using System.Collections.Generic;
    using TMPro;
    using UnityEngine;
    using UnityEngine.Events;
    using UnityEngine.UI;

    public class CharacterCard : MonoBehaviour
    {
        [Header("Parameters")]
        public Image CardFaded;
        public Image Card;
        public RectMask2D CardMask;
        public Image CharacterCutout;
        public TMP_Text Text;

        [Header("Character Info")]
        public string CharacterName;
        public string CharacterClass;

        [Header("Stats")]
        public int AC;
        public int HP;
        public int Strength;
        public int Dexterity;
        public int Constitution;
        public int Inteligence;
        public int Wisdom;
        public int Charisma;

        [Header("Hints")]
        public List<string> Hints;
        private int _hintIndex = 0;

        [Header("Transition Settings")]
        public Vector3 LocalStartPosition = new Vector3(300, -50, 0);
        public Vector3 LocalEndPosition = new Vector3(265, -50, 0);

        public Vector3 TextLocalStartPosition = new Vector3(55.5f, 0, 0);
        public Vector3 TextLocalEndPosition = new Vector3(30, 0, 0);

        [Header("Transition In Settings")]
        public AnimationCurve TransitionInAnimationCurve;
        public AnimationCurve TextTransitionInAnimationCurve;
        public float TransitionInAnimationDuration = 1.5f;

        [Header("Transition Out Settings")]
        public AnimationCurve TransitionOutAnimationCurve;

        public AnimationCurve TextTransitionOutAnimationCurve;
        public float TransitionOutAnimationDuration = 1.5f;

        [Header("Loading Settings")]
        public Vector2 LoadingTimeRange = new Vector2(0.25f, 1.25f);
        public Vector2 LoadingPercentIncrementRange = new Vector2(0f, 0.1f);

        public UnityEvent LoadFinished = new UnityEvent();
        public int CardIndex;

        public void Start()
        {
            Hints.Shuffle();
        }

        public string GetNextHintText()
        {
            string hintText = Hints[_hintIndex];

            _hintIndex++;
            if (_hintIndex == Hints.Count)
            {
                _hintIndex = 0;
                Hints.Shuffle();
            }

            return hintText;
        }

        public void LoadCharacter()
        {
            CardFaded.color = new Color(CardFaded.color.r, CardFaded.color.g, CardFaded.color.b, 0);
            Card.color = new Color(Card.color.r, Card.color.g, Card.color.b, 0);
            CharacterCutout.color = new Color(CharacterCutout.color.r, CharacterCutout.color.g, CharacterCutout.color.b, 0);
            Text.color = new Color(Text.color.r, Text.color.g, Text.color.b, 0);
            Text.rectTransform.localPosition = TextLocalStartPosition;
            transform.localPosition = LocalStartPosition;
            StartCoroutine(LoadTransitionInAnimtion());
        }

        public void AddToGrid()
        {
            CardFaded.gameObject.SetActive(false);
            CharacterCutout.gameObject.SetActive(false);
            Text.gameObject.SetActive(false);
            CardMask.enabled = false;
            Card.color = new Color(Card.color.r, Card.color.g, Card.color.b, 1);
            transform.localScale = new Vector3(1.15f, 1.15f, 1.15f);

            if (CardIndex < 6)
            {
                StartCoroutine(GridTransitionInAnimtion(new Vector3(-182.965f + (71.07f * CardIndex), 158, 0), new Vector3(-177.675f + (71.07f * CardIndex), 178, 0)));
            }
            else
            {
                StartCoroutine(GridTransitionInAnimtion(new Vector3(-139.15f + (71.07f * (CardIndex - 6)), -5.53f, 0), new Vector3(-133.86f + (71.07f * (CardIndex - 6)), 14.47f, 0)));
            }
        }

        public void RemoveFromGrid()
        {
            if (CardIndex < 6)
            {
                StartCoroutine(GridTransitionOutAnimtion(new Vector3(-177.675f + (71.07f * CardIndex), 178, 0), new Vector3(-182.965f + (71.07f * CardIndex), 158, 0)));
            }
            else
            {
                StartCoroutine(GridTransitionOutAnimtion(new Vector3(-133.86f + (71.07f * (CardIndex - 6)), 14.47f, 0), new Vector3(-139.15f + (71.07f * (CardIndex - 6)), -5.53f, 0)));
            }
        }

        IEnumerator LoadTransitionInAnimtion()
        {
            yield return new WaitForSeconds(2f);
            StartCoroutine(LoadAnimation());

            float currentTime = 0f;
            Color cardFadedColor = CardFaded.color;
            Color cardColor = Card.color;
            Color characterCutoutColor = CharacterCutout.color;
            Color textColor = Text.color;

            while (currentTime <= TransitionInAnimationDuration)
            {
                currentTime += Time.deltaTime;
                float percent = Mathf.Clamp01(currentTime / TransitionInAnimationDuration);

                float curvePercent = TransitionInAnimationCurve.Evaluate(percent);
                cardFadedColor.a = Mathf.Lerp(0, 1, curvePercent);
                CardFaded.color = cardFadedColor;
                cardColor.a = Mathf.Lerp(0, 1, curvePercent);
                Card.color = cardColor;
                characterCutoutColor.a = Mathf.Lerp(0, 1, curvePercent);
                CharacterCutout.color = characterCutoutColor;

                transform.localPosition = Vector3.Lerp(LocalStartPosition, LocalEndPosition, curvePercent);

                float textCurvePercent = TextTransitionInAnimationCurve.Evaluate(percent);
                textColor.a = Mathf.Lerp(0, 1, textCurvePercent);
                Text.color = textColor;
                Text.rectTransform.localPosition = Vector3.Lerp(TextLocalStartPosition, TextLocalEndPosition, textCurvePercent);

                yield return null;
            }
        }

        IEnumerator LoadTransitionOutAnimtion()
        {
            float currentTime = 0f;
            Color cardFadedColor = CardFaded.color;
            Color cardColor = Card.color;
            Color characterCutoutColor = CharacterCutout.color;
            Color textColor = Text.color;

            while (currentTime <= TransitionOutAnimationDuration)
            {
                currentTime += Time.deltaTime;
                float percent = Mathf.Clamp01(currentTime / TransitionOutAnimationDuration);
                float curvePercent = TransitionOutAnimationCurve.Evaluate(percent);

                cardFadedColor.a = Mathf.Lerp(1, 0, curvePercent);
                CardFaded.color = cardFadedColor;
                cardColor.a = Mathf.Lerp(1, 0, curvePercent);
                Card.color = cardColor;
                characterCutoutColor.a = Mathf.Lerp(1, 0, curvePercent);
                CharacterCutout.color = characterCutoutColor;

                transform.localPosition = Vector3.Lerp(LocalEndPosition, LocalStartPosition, curvePercent);

                float textCurvePercent = TextTransitionOutAnimationCurve.Evaluate(percent);
                textColor.a = Mathf.Lerp(1, 0, textCurvePercent);
                Text.color = textColor;
                //Text.rectTransform.localPosition = Vector3.Lerp(TextLocalEndPosition, TextLocalStartPosition, textCurvePercent);

                yield return null;
            }

            yield return new WaitForSeconds(0.25f);
            AddToGrid();
        }

        IEnumerator GridTransitionInAnimtion(Vector3 startPosition, Vector3 EndPosition)
        {
            float currentTime = 0f;
            Color cardColor = Card.color;

            while (currentTime <= 1)
            {
                currentTime += Time.deltaTime;
                float percent = Mathf.Clamp01(currentTime / 1);

                float curvePercent = TransitionInAnimationCurve.Evaluate(percent);
                cardColor.a = Mathf.Lerp(0, 1, curvePercent);
                Card.color = cardColor;

                transform.localPosition = Vector3.Lerp(startPosition, EndPosition, curvePercent);

                yield return null;
            }

            LoadFinished.Invoke();
        }

        IEnumerator GridTransitionOutAnimtion(Vector3 startPosition, Vector3 EndPosition)
        {
            float currentTime = 0f;
            Color cardColor = Card.color;

            while (currentTime <= 1)
            {
                currentTime += Time.deltaTime;
                float percent = Mathf.Clamp01(currentTime / 1);

                float curvePercent = TransitionOutAnimationCurve.Evaluate(percent);
                cardColor.a = Mathf.Lerp(1, 0, curvePercent);
                Card.color = cardColor;

                transform.localPosition = Vector3.Lerp(startPosition, EndPosition, curvePercent);

                yield return null;
            }

            CardFaded.gameObject.SetActive(true);
            CharacterCutout.gameObject.SetActive(true);
            Text.gameObject.SetActive(true);
            CardMask.enabled = true;
            transform.localScale = new Vector3(2.25f, 2.25f, 2.25f);
            transform.localPosition = LocalStartPosition + new Vector3(1000, 0, 0);
        }

        IEnumerator LoadAnimation()
        {
            float initialTopPadding = 319.95f;
            float targetTopPadding = 0;
            float loadPercent = 0f;
            while (loadPercent < 1f)
            {
                float currentTime = 0f;
                float timeToLerp = UnityEngine.Random.Range(LoadingTimeRange.x, LoadingTimeRange.y);
                float previousLoadPercent = loadPercent;
                loadPercent += Mathf.Clamp01(UnityEngine.Random.Range(LoadingPercentIncrementRange.x, LoadingPercentIncrementRange.y));

                while (currentTime <= timeToLerp)
                {
                    currentTime += Time.deltaTime;
                    float t = Mathf.Lerp(previousLoadPercent, loadPercent, currentTime / timeToLerp);
                    CardMask.padding = Vector4.Lerp(new Vector4(CardMask.padding.x, CardMask.padding.y, CardMask.padding.z, initialTopPadding), new Vector4(CardMask.padding.x, CardMask.padding.y, CardMask.padding.z, targetTopPadding), t);
                    yield return null;
                }

                yield return null;
            }

            yield return new WaitForSeconds(2);
            StartCoroutine(LoadTransitionOutAnimtion());
        }
    }
}