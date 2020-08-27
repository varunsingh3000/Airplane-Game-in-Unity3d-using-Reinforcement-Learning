using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Aircraft
{
    public class HUDController : MonoBehaviour
    {
        [Tooltip("The place in the race (e.g. 1st)")]
        public TextMeshProUGUI placeText;

        [Tooltip("Seconds remaining to reach the next checkpoint (e.g. Time 9.3)")]
        public TextMeshProUGUI timeText;

        [Tooltip("The current lap (e.g. Lap 2)")]
        public TextMeshProUGUI lapText;

        [Tooltip("The icon indicating where the next checkpoint is")]
        public Image checkpointIcon;

        [Tooltip("The arrow pointing toward the next checkpoint")]
        public Image checkpointArrow;

        [Tooltip("At what point to show an arrow toward the checkpoint, rather than the icon centered on it")]
        public float indicatorLimit = .7f;

        /// <summary>
        /// The agent this HUD shows info for
        /// </summary>
        public AircraftAgent FollowAgent { get; set; }

        private RaceManager raceManager;

        private void Awake()
        {
            raceManager = FindObjectOfType<RaceManager>();
        }

        private void Update()
        {
            if (FollowAgent != null)
            {
                UpdatePlaceText();
                UpdateTimeText();
                UpdateLapText();
                UpdateArrow();
            }
        }

        private void UpdatePlaceText()
        {
            string place = raceManager.GetAgentPlace(FollowAgent);
            placeText.text = place;
        }

        private void UpdateTimeText()
        {
            float time = raceManager.GetAgentTime(FollowAgent);
            timeText.text = "Time " + time.ToString("0.0");
        }

        private void UpdateLapText()
        {
            int lap = raceManager.GetAgentLap(FollowAgent);
            lapText.text = "Lap " + lap + "/" + raceManager.numLaps;
        }

        private void UpdateArrow()
        {
            // Find the checkpoint within the viewport
            Transform nextCheckpoint = raceManager.GetAgentNextCheckpoint(FollowAgent);
            Vector3 viewportPoint = raceManager.ActiveCamera.WorldToViewportPoint(nextCheckpoint.transform.position);
            bool behindCamera = viewportPoint.z < 0;
            viewportPoint.z = 0f;

            // Do position calculations
            Vector3 viewportCenter = new Vector3(.5f, .5f, 0f);
            Vector3 fromCenter = viewportPoint - viewportCenter;
            float halfLimit = indicatorLimit / 2f;
            bool showArrow = false;

            if (behindCamera)
            {
                // Limit distance from center
                // (Viewport point is flipped when object is behind camera)
                fromCenter = -fromCenter.normalized * halfLimit;
                showArrow = true;
            }
            else
            {
                if (fromCenter.magnitude > halfLimit)
                {
                    // Limit distance from center
                    fromCenter = fromCenter.normalized * halfLimit;
                    showArrow = true;
                }
            }

            // Update the checkpoint icon and arrow
            checkpointArrow.gameObject.SetActive(showArrow);
            checkpointArrow.rectTransform.rotation = Quaternion.FromToRotation(Vector3.up, fromCenter);
            checkpointIcon.rectTransform.position = raceManager.ActiveCamera.ViewportToScreenPoint(fromCenter + viewportCenter);
        }
    }
}
