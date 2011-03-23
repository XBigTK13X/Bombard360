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
        
        private static bool CreateNewEntry(KeyValuePair<int,int> targetSpace, XnaDrawable componentToAdd)
        {
            if (!s_board.Keys.Contains(targetSpace))
            {
                s_board.Add(targetSpace, new List<XnaDrawable>());
                s_board[targetSpace].Add(componentToAdd);
                return true;
            }
            return false;
        }
        public static bool AddIfUnblocked(int gridColumn, int gridRow, XnaDrawable componentToAdd)
        {
            bool elementWasAdded = false;
            KeyValuePair<int, int> targetSpace = new KeyValuePair<int, int>(gridColumn, gridRow);
            if(!(elementWasAdded = CreateNewEntry(targetSpace,componentToAdd)))
            {
                bool cellIsBlocked = false;
                bool typeIsUnique = true;
                foreach(XnaDrawable component in s_board[targetSpace])
                {
                    if (component.IsBlocking())
                    {
                        cellIsBlocked = true;
                    }
                    if (component.GetAssetType() == componentToAdd.GetAssetType())
                    {
                        typeIsUnique = false;
                    }
                }
                if (!cellIsBlocked&&!s_board[targetSpace].Contains(componentToAdd)&&typeIsUnique)
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
            if (!CreateNewEntry(targetSpace,componentToAdd))
            {
                bool typeIsUnique = true;
                foreach (XnaDrawable component in s_board[targetSpace])
                {
                    if (component.GetAssetType() == componentToAdd.GetAssetType())
                    {
                        typeIsUnique = false;
                    }
                }
                if (!s_board[targetSpace].Contains(componentToAdd)&&typeIsUnique)
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
            KeyValuePair<int, int> targetCell = new KeyValuePair<int, int>((int)location.X, (int)location.Y);
            if (s_board.Keys.Contains(targetCell))
            {
                foreach (XnaDrawable component in s_board[targetCell])
                {
                    if (component.GetAssetType() == assetType)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        public static XnaDrawable GetTileType(Vector2 location, string type)
        {
            foreach (XnaDrawable component in s_board[new KeyValuePair<int, int>((int)location.X, (int)location.Y)])
            {
                if (component.GetAssetType() == type)
                {
                    return component;
                }
            }
            return null;
        }
        public static Explosion GetExplosionInstance(Vector2 location)
        {
            return (Explosion)GetTileType(location, "explosion");
        }
    }
}
