using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Bombard360
{
    class InputManager
    {
        private static readonly List<string> m_playerInputDevices = new List<string>()
        {
            "KEYBOARD"
        };
        private static readonly Dictionary<string, Keys> m_keyboardMapping = new Dictionary<string, Keys>()
        {
            {"MOVE_UP",Keys.Up},
            {"MOVE_DOWN",Keys.Down},
            {"MOVE_RIGHT",Keys.Right},
            {"MOVE_LEFT",Keys.Left},
            {"PLACE_BOMB",Keys.Space}
        };
        public static bool IsMovingUp(int playerIndex)
        {
            return IsCommandBeingExecuted("MOVE_UP",m_playerInputDevices[playerIndex]);
        }
        public static bool IsMovingDown(int playerIndex)
        {
            return IsCommandBeingExecuted("MOVE_DOWN", m_playerInputDevices[playerIndex]);
        }
        public static bool IsMovingRight(int playerIndex)
        {
            return IsCommandBeingExecuted("MOVE_RIGHT", m_playerInputDevices[playerIndex]);
        }
        public static bool IsMovingLeft(int playerIndex)
        {
            return IsCommandBeingExecuted("MOVE_LEFT", m_playerInputDevices[playerIndex]);
        }
        public static bool IsPlacingBomb(int playerIndex)
        {
            return IsCommandBeingExecuted("PLACE_BOMB", m_playerInputDevices[playerIndex]);
        }
        private static bool IsCommandBeingExecuted(string command, string inputMechanism)
        {
            bool isInputActive = false;
            switch (inputMechanism)
            {
                case "KEYBOARD":
                    isInputActive = Keyboard.GetState().IsKeyDown(m_keyboardMapping[command]);
                    break;
                case "GAMEPAD":
                    throw new NotImplementedException("Still need to program the Controller definitions.");
                default:
                    throw new Exception("What were you smoking that brought up this error?");
            }
            return isInputActive;
        }
    }
}
