using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Bombard360
{
    class BoardManager
    {
        private static Dictionary<KeyValuePair<int, int>, BoardTile> s_board = new Dictionary<KeyValuePair<int, int>, BoardTile>();

        private static bool CreateNewEntry(KeyValuePair<int,int> targetSpace, XnaDrawable componentToAdd)
        {
            if (!s_board.Keys.Contains(targetSpace))
            {
                s_board.Add(targetSpace, new BoardTile());
                s_board[targetSpace].Register(componentToAdd);
                return true;
            }
            return false;
        }
        public static bool AddIfUnblocked(XnaDrawable component)
        {
            return AddIfUnblocked((int)component.GetPosition().X, (int)component.GetPosition().Y, component);
        }
        public static bool AddIfUnblocked(int gridColumn, int gridRow, XnaDrawable componentToAdd)
        {
            bool elementWasAdded = false;
            KeyValuePair<int, int> targetSpace = new KeyValuePair<int, int>(gridColumn, gridRow);
            if(!(elementWasAdded = CreateNewEntry(targetSpace,componentToAdd)))
            {
                if (!s_board[targetSpace].IsBlocked())
                {
                    s_board[targetSpace].Register(componentToAdd);
                    elementWasAdded = true;
                }
            }
            return elementWasAdded;
        }
        public static void Add(XnaDrawable componenetToAdd)
        {
            Add((int)componenetToAdd.GetPosition().X, (int)componenetToAdd.GetPosition().Y, componenetToAdd);
        }
        public static void Add(int gridColumn,int gridRow, XnaDrawable componentToAdd)
        {
            KeyValuePair<int, int> targetSpace = new KeyValuePair<int, int>(gridColumn, gridRow);
            if (!CreateNewEntry(targetSpace,componentToAdd))
            {
                s_board[targetSpace].Register(componentToAdd);
            }
        }
        public static bool IsCellEmpty(int gridColumn, int gridRow)
        {
            var targetCell = new KeyValuePair<int, int>(gridColumn, gridRow);
            if (gridColumn < 0 || gridRow < 0 || gridColumn > SpriteSheetManager.Columns || gridRow > SpriteSheetManager.Rows)
            {
                return false;
            }
            if (s_board.Keys.Contains(targetCell))
            {
                if (s_board[targetCell].IsBlocked())
                {
                    return false;
                }
            }
            return true;
        }
        public static void CollectGarbage()
        {
            foreach (KeyValuePair<int, int> key in s_board.Keys)
            {
                s_board[key].RemoveGarbage();
            }
        }
        public static bool HasTileType(Vector2 location,string assetType)
        {
            KeyValuePair<int, int> targetCell = new KeyValuePair<int, int>((int)location.X, (int)location.Y);
            if (s_board.Keys.Contains(targetCell))
            {
                return s_board[targetCell].IsTypeRegistered(assetType);
            }
            return false;
        }
        public static XnaDrawable GetTileType(Vector2 location, string type)
        {
           return s_board[new KeyValuePair<int, int>((int)location.X, (int)location.Y)].GetTileOfType(type);
        }
        public static Explosion GetExplosionInstance(Vector2 location)
        {
            return (Explosion)GetTileType(location, "explosion");
        }
        public static void Update()
        {
            foreach (KeyValuePair<int, int> key in s_board.Keys)
            {
                s_board[key].Update();
                s_board[key].RemoveGarbage();
            }
        }
        public static void LoadContent(ContentManager assetHandler)
        {
            foreach (KeyValuePair<int, int> key in s_board.Keys)
            {
                s_board[key].LoadContent(assetHandler);
            } 
        }
        public static void Draw(SpriteBatch target)
        {
            foreach (KeyValuePair<int, int> key in s_board.Keys)
            {
                s_board[key].Draw(target);
            }
            Console.WriteLine();
        }
        public static string DumpDebuggingLog()
        {
            string output = "";
            foreach (KeyValuePair<int, int> key in s_board.Keys)
            {
                List<XnaDrawable> deadComponents = new List<XnaDrawable>();
                output+="\n";
            }
            return output;
        }
    }
}
