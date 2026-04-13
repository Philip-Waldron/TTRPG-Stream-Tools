namespace V1
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class GameManager : MonoBehaviour
    {
        [Header("Parameters")]
        public List<CharacterCard> CharacterCards;
        public LoadingText LoadingText;
        public HintText HintText;
        public HintTextPanel HintTextPanel;
        //public GreenScreen GreenScreen;
        public StatsManager StatsManager;
        public SelectionOutline SelectionOutline;

        private int characterIndex = 0;
        private bool _playing = false;
        private bool _animatingUI = false;
        private bool _uiVisible = false;
        private bool _queuedStop = false;
        private bool _manualCharacterSelect = false;

        void Start()
        {
            foreach (var characterCard in CharacterCards)
            {
                characterCard.LoadFinished.AddListener(LoadNextCharacter);
            }

            CharacterCards.Shuffle();
        }

        void Update()
        {
            // Show/Hide UI. There must be no animations in progress.
            if (!_playing && Input.GetKeyDown(KeyCode.Space))
            {
                _playing = true;
                _animatingUI = true;
                if (_uiVisible)
                {
                    _uiVisible = false;
                    StartCoroutine(StaggeredUIHideAnimation());
                }
                else
                {
                    _uiVisible = true;
                    StartCoroutine(StaggeredUIShowAnimation());
                }
            }

            // Play loading animation. UI must be visible.
            if (!_playing && _uiVisible && Input.GetKeyDown(KeyCode.Return))
            {
                _playing = true;
                StartCoroutine(StaggeredStart());
            }

            // Play Full animation sequence. UI must not be visible.
            if (!_playing && !_uiVisible && Input.GetKeyDown(KeyCode.Return))
            {
                _playing = true;
                _uiVisible = true;
                StartCoroutine(StaggeredFullStart());
            }

            // Queue animation stop.
            if (_playing && !_animatingUI && !_queuedStop && Input.GetKeyDown(KeyCode.Escape))
            {
                _queuedStop = true;
            }

            if (_playing && !_manualCharacterSelect && (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow)))
            {
                _manualCharacterSelect = true;
            }
        }

        private void LoadNextHintText()
        {

        }

        void LoadNextCharacter()
        {
            if (_queuedStop)
            {
                if (characterIndex == 0)
                {
                    StartCoroutine(StaggeredUIHideAnimation());
                }
                else
                {
                    StartCoroutine(CharacterSelectAnimation(true));
                }
            }
            else if (characterIndex == CharacterCards.Count)
            {
                StartCoroutine(CharacterSelectAnimation(false));
            }
            else
            {
                CharacterCards[characterIndex].CardIndex = characterIndex;
                CharacterCards[characterIndex].LoadCharacter();
                characterIndex++;
            }
        }

        IEnumerator StaggeredStart()
        {
            HintText.StartCarousel();
            yield return new WaitForSeconds(3);
            LoadingText.StartCarousel();
            yield return new WaitForSeconds(3);
            LoadNextCharacter();
        }

        IEnumerator StaggeredFullStart()
        {
            HintText.Text.text = "";
            //GreenScreen.Show();
            HintTextPanel.Show();
            yield return new WaitForSeconds(0.5f);
            LoadingText.StartCarousel();
            LoadingText.Show();
            yield return new WaitForSeconds(1.5f);
            HintText.StartCarousel();
            yield return new WaitForSeconds(3);
            LoadNextCharacter();
        }

        IEnumerator StaggeredUIShowAnimation()
        {
            //GreenScreen.Show();
            HintTextPanel.Show();
            yield return new WaitForSeconds(0.5f);
            LoadingText.Show();
            yield return new WaitForSeconds(1.5f);
            HintText.Show();
            yield return new WaitForSeconds(2);
            _playing = false;
            _animatingUI = false;
            _uiVisible = true;
        }

        IEnumerator StaggeredUIHideAnimation()
        {
            LoadingText.StopCarousel();
            if (HintText.SwappingText)
            {
                while (HintText.SwappingText)
                {
                    yield return null;
                }

                HintText.StopCarousel();
            }
            else
            {
                HintText.StopCarousel();
            }

            //GreenScreen.Hide();
            HintText.Hide();
            yield return new WaitForSeconds(0.5f);
            LoadingText.Hide();
            HintTextPanel.Hide();
            yield return new WaitForSeconds(2);
            _playing = false;
            _animatingUI = false;
            _uiVisible = false;
            _queuedStop = false;
        }

        IEnumerator CharacterSelectAnimation(bool close)
        {
            LoadingText.StopCarousel();
            while (HintText.SwappingText)
            {
                yield return null;
            }

            HintText.StopCarousel();
            yield return new WaitForSeconds(5f);


            HintText.Hide();
            yield return new WaitForSeconds(0.5f);
            LoadingText.Hide();
            yield return new WaitForSeconds(1.5f);
            int currentIndex = 0;
            StatsManager.SetStats(CharacterCards[currentIndex].CharacterName, CharacterCards[currentIndex].CharacterClass, CharacterCards[currentIndex].AC, CharacterCards[currentIndex].HP, CharacterCards[currentIndex].Strength, CharacterCards[currentIndex].Dexterity, CharacterCards[currentIndex].Constitution, CharacterCards[currentIndex].Inteligence, CharacterCards[currentIndex].Wisdom, CharacterCards[currentIndex].Charisma);
            SelectionOutline.SetPosition(currentIndex);
            SelectionOutline.Show();
            StatsManager.Show();
            yield return new WaitForSeconds(3f);

            int targetIndex = Random.Range(0, characterIndex);
            bool selectedCharacter = false;
            while ((!_manualCharacterSelect && currentIndex < targetIndex) || (_manualCharacterSelect && !selectedCharacter))
            {
                if (!_manualCharacterSelect)
                {
                    if (currentIndex != 0 && Random.Range(1, 6) == 1)
                    {
                        currentIndex--;
                    }
                    else
                    {
                        currentIndex++;
                    }

                    SelectionOutline.SetPosition(currentIndex);
                    StatsManager.SetStats(CharacterCards[currentIndex].CharacterName, CharacterCards[currentIndex].CharacterClass, CharacterCards[currentIndex].AC, CharacterCards[currentIndex].HP, CharacterCards[currentIndex].Strength, CharacterCards[currentIndex].Dexterity, CharacterCards[currentIndex].Constitution, CharacterCards[currentIndex].Inteligence, CharacterCards[currentIndex].Wisdom, CharacterCards[currentIndex].Charisma);
                    yield return new WaitForSeconds(Random.Range(1f, 3f));
                }
                else
                {
                    if (Input.GetKeyDown(KeyCode.Return))
                    {
                        selectedCharacter = true;
                    }
                    else if (Input.GetKeyDown(KeyCode.LeftArrow))
                    {
                        currentIndex--;
                        if (currentIndex < 0)
                        {
                            currentIndex = characterIndex - 1;
                        }
                    }
                    else if (Input.GetKeyDown(KeyCode.RightArrow))
                    {
                        currentIndex++;
                        if (currentIndex == characterIndex)
                        {
                            currentIndex = 0;
                        }
                    }

                    SelectionOutline.SetPosition(currentIndex);
                    StatsManager.SetStats(CharacterCards[currentIndex].CharacterName, CharacterCards[currentIndex].CharacterClass, CharacterCards[currentIndex].AC, CharacterCards[currentIndex].HP, CharacterCards[currentIndex].Strength, CharacterCards[currentIndex].Dexterity, CharacterCards[currentIndex].Constitution, CharacterCards[currentIndex].Inteligence, CharacterCards[currentIndex].Wisdom, CharacterCards[currentIndex].Charisma);
                    yield return null;
                }
            }

            SelectionOutline.Select();
            yield return new WaitForSeconds(0.5f);

            currentIndex = characterIndex - 1;
            bool statsHidden = false;
            if (currentIndex < 3)
            {
                statsHidden = true;
                StatsManager.Hide();
            }

            while (currentIndex >= 0)
            {
                CharacterCards[currentIndex].RemoveFromGrid();
                currentIndex--;
                yield return new WaitForSeconds(0.5f);

                if (!statsHidden && currentIndex < 3)
                {
                    statsHidden = true;
                    StatsManager.Hide();
                }
            }

            characterIndex = 0;
            CharacterCards.Shuffle();

            if (!close)
            {
                LoadingText.StartCarousel();
                LoadingText.Show();
                yield return new WaitForSeconds(1.5f);
                HintText.Text.text = "";
                HintText.StartCarousel();
                yield return new WaitForSeconds(3);
                LoadNextCharacter();
            }
            else
            {
                //GreenScreen.Hide();
                yield return new WaitForSeconds(0.5f);
                HintTextPanel.Hide();
                yield return new WaitForSeconds(2);
                _playing = false;
                _animatingUI = false;
                _uiVisible = false;
                _queuedStop = false;
            }

            _manualCharacterSelect = false;
        }
    }
}
