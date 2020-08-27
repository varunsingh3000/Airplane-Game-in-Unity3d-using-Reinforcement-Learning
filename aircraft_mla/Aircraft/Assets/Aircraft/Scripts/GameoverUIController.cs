using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Aircraft
{
    public class GameoverUIController : MonoBehaviour
    {
        [Tooltip("Text to display finish place (e.g. 2nd place")]
        public TextMeshProUGUI placeText;

        private RaceManager raceManager;

        private void Awake()
        {
            raceManager = FindObjectOfType<RaceManager>();
        }

        private void OnEnable()
        {
            if (GameManager.Instance != null &&
                GameManager.Instance.GameState == GameState.Gameover)
            {
                // Gets the place and updates the text
                string place = raceManager.GetAgentPlace(raceManager.FollowAgent);
                this.placeText.text = place + " Place";
            }
        }

        /// <summary>
        /// Loads the MainMenu scene
        /// </summary>
        public void MainMenuButtonClicked()
        {
            GameManager.Instance.LoadLevel("MainMenu", GameState.MainMenu);
        }
    }
}
