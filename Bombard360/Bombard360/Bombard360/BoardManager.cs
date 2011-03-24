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
        //private static Dictionary<KeyValuePair<int, int>, BoardTile> s_board = new Dictionary<KeyValuePair<int, int>, BoardTile>();

        private static BoardTile[,] s_board = new BoardTile[SpriteSheetManager.Columns, SpriteSheetManager.Rows];

        public static void Initialize()
        {
            for (int ii = 0; ii < SpriteSheetManager.Columns; ii++)
            {
                for (int jj = 0; jj < SpriteSheetManager.Rows; jj++)
                {
                    s_board.Add(new KeyValuePair<int, int>(ii, jj), new BoardTile(ii,jj));
                }
            }
        }

        public static bool AddIfUnblocked(XnaDrawable component)
        {
            return AddIfUnblocked((int)component.GetPosition().X, (int)component.GetPosition().Y, component);
        }
        public static bool AddIfUnblocked(int gridColumn, int gridRow, XnaDrawable componentToAdd)
        {
            bool elementWasAdded = false;
            KeyValuePair<int, int> targetSpace = new KeyValuePair<int, int>(gridColumn, gridRow);
            if (!s_board[targetSpace].IsBlocked())
            {
                s_board[targetSpace].Register(componentToAdd);
                elementWasAdded = true;
            }
            return elementWasAdded;
        }
        public static bool Add(XnaDrawable componentToAdd)
        {
            return Add((int)componentToAdd.GetPosition().X, (int)componentToAdd.GetPosition().Y, componentToAdd);
        }
        public static bool Add(int gridColumn,int gridRow, XnaDrawable componentToAdd)
        {
            KeyValuePair<int, int> targetSpace = new KeyValuePair<int, int>(gridColumn, gridRow);
            return s_board[targetSpace].Register(componentToAdd);
        }
        public static bool IsCellEmpty(int gridColumn, int gridRow)
        {
            var targetCell = new KeyValuePair<int, int>(gridColumn, gridRow);
            if (gridColumn < 0 || gridRow < 0 || gridColumn >= SpriteSheetManager.Columns || gridRow >= SpriteSheetManager.Rows)
            {
                return false;
            }
            if (s_board[targetCell].IsBlocked())
            {
                return false;
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
            return s_board[targetCell].IsTypeRegistered(assetType);
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
        }
        public static string DumpDebuggingLog()
        {
            string output = "";
            foreach (KeyValuePair<int, int> key in s_board.Keys)
            {
                output += key.Key + "," +key.Value + " ::: "+s_board[key].GetDebugLog()+"\n";
            }
            return output;
        }
    }
}
