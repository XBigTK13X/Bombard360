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

        private static readonly Dictionary<string, Buttons> m_gamePadMapping = new Dictionary<string, Buttons>()
        {
            {"MOVE_UP",Buttons.DPadUp},
            {"MOVE_DOWN",Buttons.DPadDown},
            {"MOVE_RIGHT",Buttons.DPadRight},
            {"MOVE_LEFT",Buttons.DPadLeft},
            {"PLACE_BOMB",Buttons.RightTrigger}
        };

        private static readonly List<PlayerIndex> m_playerIndex = new List<PlayerIndex>()
        {
            PlayerIndex.One,
            PlayerIndex.Two,
            PlayerIndex.Three,
            PlayerIndex.Four
        };

        public static bool IsMovingUp(int playerIndex)
        {
            return IsCommandBeingExecuted("MOVE_UP",playerIndex);
        }
        public static bool IsMovingDown(int playerIndex)
        {
            return IsCommandBeingExecuted("MOVE_DOWN",playerIndex);
        }
        public static bool IsMovingRight(int playerIndex)
        {
            return IsCommandBeingExecuted("MOVE_RIGHT",playerIndex);
        }
        public static bool IsMovingLeft(int playerIndex)
        {
            return IsCommandBeingExecuted("MOVE_LEFT", playerIndex);
        }
        public static bool IsPlacingBomb(int playerIndex)
        {
            return IsCommandBeingExecuted("PLACE_BOMB", playerIndex);
        }
        private static bool IsCommandBeingExecuted(string command,int playerIndex)
        {
            string inputMechanism = m_playerInputDevices[playerIndex];
            bool isInputActive = false;
            switch (inputMechanism)
            {
                case "KEYBOARD":
                    isInputActive = Keyboard.GetState().IsKeyDown(m_keyboardMapping[command]);
                    break;
                case "GAMEPAD":
                    isInputActive = GamePad.GetState(m_playerIndex[playerIndex]).IsButtonDown(m_gamePadMapping[command]);
                    break;
                default:
                    throw new Exception("What were you smoking that brought up this error?");
            }
            return isInputActive;
        }
    }
}
