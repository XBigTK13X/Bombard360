using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Bombard360
{
    class BoardManager
    {
        private static Dictionary<KeyValuePair<int, int>, List<XnaDrawable>> s_board = new Dictionary<KeyValuePair<int, int>, List<XnaDrawable>>();
        public static bool AddIfUnblocked(int gridColumn, int gridRow, XnaDrawable componentToAdd)
        {
            bool elementWasAdded = false;
            KeyValuePair<int, int> targetSpace = new KeyValuePair<int, int>(gridColumn, gridRow);
            if (!s_board.Keys.Contains(targetSpace))
            {
                s_board.Add(targetSpace, new List<XnaDrawable>());
                s_board[targetSpace].Add(componentToAdd);
                elementWasAdded = true;
            }
            else
            {
                bool cellIsBlocked = false;
                foreach(XnaDrawable component in s_board[targetSpace])
                {
                    if (component.IsBlocking())
                    {
                        cellIsBlocked = true;
                    }
                }
                if (!cellIsBlocked&&!s_board[targetSpace].Contains(componentToAdd))
                {
                    s_board[targetSpace].Add(componentToAdd);
                    elementWasAdded = true;
                }
            }
            return elementWasAdded;
        }
        public static void Add(int gridColumn,int gridRow, XnaDrawable componentToAdd)
        {
            KeyValuePair<int, int> targetSpace = new KeyValuePair<int, int>(gridColumn, gridRow);
            if (!s_board.Keys.Contains(targetSpace))
            {
                s_board.Add(targetSpace, new List<XnaDrawable>());
                s_board[targetSpace].Add(componentToAdd);
            }
            else
            {
                if (!s_board[targetSpace].Contains(componentToAdd))
                {
                    s_board[targetSpace].Add(componentToAdd);
                }
            }
        }
        public static bool IsCellEmpty(int gridColumn, int gridRow)
        {
            if (gridColumn < 0 || gridRow < 0 || gridColumn > SpriteSheetManager.Columns || gridRow > SpriteSheetManager.Rows)
            {
                return false;
            }
            if (s_board.Keys.Contains(new KeyValuePair<int, int>(gridColumn, gridRow)))
            {
                foreach (XnaDrawable component in s_board[new KeyValuePair<int, int>(gridColumn, gridRow)])
                {
                    if (component.IsBlocking())
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        public static void CollectGarbage()
        {
            foreach (KeyValuePair<int, int> key in s_board.Keys)
            {
                List<XnaDrawable> deadComponents = new List<XnaDrawable>();
                foreach (XnaDrawable component in s_board[key])
                {
                    if (!component.IsActive())
                    {
                        deadComponents.Add(component);
                    }
                }
                foreach (XnaDrawable component in deadComponents)
                {
                    s_board[key].Remove(component);
                }
            }
        }
        public static bool HasTileType(Vector2 location,string assetType)
        {
            foreach (XnaDrawable component in s_board[new KeyValuePair<int, int>((int)location.X, (int)location.Y)])
            {
                if (component.GetAssetType() == assetType)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
