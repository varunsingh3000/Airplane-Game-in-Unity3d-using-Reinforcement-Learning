using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

namespace Aircraft
{
    public class MainMenuController : MonoBehaviour
    {
        [Tooltip("The list of levels (scene names) that can be loaded")]
        public List<string> levels;

        [Tooltip("The dropdown for selecting the level")]
        public TMP_Dropdown levelDropdown;

        [Tooltip("The dropdown for selecting the game difficulty")]
        public TMP_Dropdown difficultyDropdown;

        private string selectedLevel;
        private GameDifficulty selectedDifficulty;

        /// <summary>
        /// Automatically fill the dropdown lists
        /// </summary>
        private void Start()
        {
            Debug.Assert(levels.Count > 0, "No levels available");
            levelDropdown.ClearOptions();
            levelDropdown.AddOptions(levels);
            selectedLevel = levels[0];

            difficultyDropdown.ClearOptions();
            difficultyDropdown.AddOptions(Enum.GetNames(typeof(GameDifficulty)).ToList());
            selectedDifficulty = GameDifficulty.Easy;
        }

        public void SetLevel(int levelIndex)
        {
            selectedLevel = levels[levelIndex];
        }

        public void SetDifficulty(int difficultyIndex)
        {
            selectedDifficulty = (GameDifficulty)difficultyIndex;
        }

        /// <summary>
        /// Start the chosen level
        /// </summary>
        public void StartButtonClicked()
        {
            // Set game difficulty
            GameManager.Instance.GameDifficulty = selectedDifficulty;

            // Load the level in 'Preparing' mode
            GameManager.Instance.LoadLevel(selectedLevel, GameState.Preparing);
        }

        /// <summary>
        /// Quit the game
        /// </summary>
        public void QuitButtonClicked()
        {
            Application.Quit();
        }
    }
}
