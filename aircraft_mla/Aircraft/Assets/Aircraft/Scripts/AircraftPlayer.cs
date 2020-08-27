using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Aircraft
{
    public class AircraftPlayer : AircraftAgent
    {
        [Header("Input Bindings")]
        public InputAction pitchInput;
        public InputAction yawInput;
        public InputAction boostInput;
        public InputAction pauseInput;

        public override void InitializeAgent()
        {
            base.InitializeAgent();
            pitchInput.Enable();
            yawInput.Enable();
            boostInput.Enable();
            pauseInput.Enable();
        }

        /// <summary>
        /// Reads player input and converts it to a vector action array
        /// </summary>
        /// <returns>An array of floats for AgentAction to use</returns>
        public override float[] Heuristic()
        {
            // Pitch: 1 == up, 0 == none, -1 == down
            float pitchValue = Mathf.Round(pitchInput.ReadValue<float>());

            // Yaw: 1 == turn right, 0 == none, -1 == turn left
            float yawValue = Mathf.Round(yawInput.ReadValue<float>());

            // Boost: 1 == boost, 0 == no boost
            float boostValue = Mathf.Round(boostInput.ReadValue<float>());

            // convert -1 (down) to discrete value 2
            if (pitchValue == -1f) pitchValue = 2f;

            // convert -1 (turn left) to discrete value 2
            if (yawValue == -1f) yawValue = 2f;

            return new float[] { pitchValue, yawValue, boostValue };
        }

        private void OnDestroy()
        {
            pitchInput.Disable();
            yawInput.Disable();
            boostInput.Disable();
            pauseInput.Disable();
        }
    }
}
