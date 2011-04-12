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
        public enum Commands
        {
            MoveUp,
            MoveDown,
            MoveLeft,
            MoveRight,
            Confirm,
            MainMenu,
            SaveFile,
            PlaceBomb
        }
        private static readonly List<string> m_playerInputDevices = new List<string>()
        {
            "KEYBOARD",
            "GAMEPAD"
        };
        private static readonly Dictionary<Commands, Keys> m_keyboardMapping = new Dictionary<Commands, Keys>()
        {
            {Commands.MoveUp,Keys.Up},
            {Commands.MoveDown,Keys.Down},
            {Commands.MoveRight,Keys.Right},
            {Commands.MoveLeft,Keys.Left},
            {Commands.PlaceBomb,Keys.Space},
            {Commands.Confirm,Keys.Q},
            {Commands.MainMenu,Keys.E},
            {Commands.SaveFile,Keys.S}
        };

        private static readonly Dictionary<string, Buttons> m_gamePadMapping = new Dictionary<string, Buttons>()
        {
            {"MOVE_UP",Buttons.DPadUp},
            {"MOVE_DOWN",Buttons.DPadDown},
            {"MOVE_RIGHT",Buttons.DPadRight},
            {"MOVE_LEFT",Buttons.DPadLeft},
            {"PLACE_BOMB",Buttons.RightTrigger},
            {"CONFIRM",Buttons.LeftShoulder}
        };

        private static readonly List<PlayerIndex> m_playerIndex = new List<PlayerIndex>()
        {
            PlayerIndex.One,
            PlayerIndex.Two,
            PlayerIndex.Three,
            PlayerIndex.Four
        };

        public static bool IsPressed(Commands command,int playerIndex)
        {
            string inputMechanism = m_playerInputDevices[playerIndex];
            bool isInputActive = false;
            switch (inputMechanism)
            {
                case "KEYBOARD":
                    isInputActive = Keyboard.GetState().IsKeyDown(m_keyboardMapping[command]);
                    break;
                case "GAMEPAD":
                    //isInputActive = GamePad.GetState(m_playerIndex[playerIndex]).IsButtonDown(m_gamePadMapping[command]);
                    break;
                default:
                    throw new Exception("What were you smoking that brought up this error?");
            }
            return isInputActive;
        }
    }
}
