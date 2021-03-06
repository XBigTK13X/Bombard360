﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Bombard360.Tiles;
using Bombard360.Management;

namespace Bombard360
{
    class BoardManager
    {
        private static BoardTile[,] s_board = new BoardTile[SpriteSheetManager.Columns, SpriteSheetManager.Rows];

        public static void Initialize()
        {
            s_board = ConvertTilemapToLevelData(SaveManager.Load());
        }

        public static bool AddExplosion(int gridColumn, int gridRow, int power, int range, int xVel, int yVel)
        {
            if(IsCoordValid(gridColumn,gridRow))
            {
                if (s_board[gridColumn,gridRow].CanPlaceBomb())
                {
                    Add(new Explosion(gridColumn, gridRow, power, range, xVel, yVel));
                }
            }
            return false;
        }
        public static bool AddIfUnblocked(GameplayObject component)
        {
            return AddIfUnblocked((int)component.GetPosition().X, (int)component.GetPosition().Y, component);
        }
        public static bool AddIfUnblocked(int gridColumn, int gridRow, GameplayObject componentToAdd)
        {
            if (BoardManager.IsCoordValid(gridColumn, gridRow))
            {
                if (!s_board[gridColumn, gridRow].IsBlocked())
                {
                    s_board[gridColumn, gridRow].Register(componentToAdd);
                    return true;
                }
            }
            return false;
        }
        public static bool Add(GameplayObject componentToAdd)
        {
            return Add((int)componentToAdd.GetPosition().X, (int)componentToAdd.GetPosition().Y, componentToAdd);
        }
        public static bool Add(int gridColumn,int gridRow, GameplayObject componentToAdd)
        {
            if(BoardManager.IsCoordValid(gridColumn,gridRow))
            {
                 return s_board[gridColumn, gridRow].Register(componentToAdd);
            }
            return false;
        }
        public static bool IsCellEmpty(int gridColumn, int gridRow)
        {
            if (gridColumn < 0 || gridRow < 0 || gridColumn >= SpriteSheetManager.Columns || gridRow >= SpriteSheetManager.Rows)
            {
                return false;
            }
            if (s_board[gridColumn, gridRow].IsBlocked())
            {
                return false;
            }            
            return true;
        }
        public static bool HasTileType(int gridColumn,int gridRow, SpriteType assetType)
        {
            return s_board[gridColumn, gridRow].IsTypeRegistered(assetType);
        }

        public static bool HasTileType(Vector2 location,SpriteType assetType)
        {
            return s_board[(int)location.X, (int)location.Y].IsTypeRegistered(assetType);
        }

        public static GameplayObject GetTileType(Vector2 location, SpriteType type)
        {
           return s_board[(int)location.X,(int)location.Y].GetTileOfType(type);
        }

        public static Explosion GetExplosionInstance(Vector2 location)
        {
            return (Explosion)GetTileType(location, SpriteType.EXPLOSION);
        }

        public static void Update()
        {
            foreach (BoardTile tile in s_board)
            {
                tile.Update();
                tile.RemoveGarbage();
            }
        }
        public static void LoadContent(ContentManager assetHandler)
        {
            foreach (BoardTile tile in s_board)
            {
                tile.LoadContent(assetHandler);
            }
        }
        public static void Draw(SpriteBatch target)
        {
            foreach (BoardTile tile in s_board)
            {
                tile.Draw(target);
            }
        }
        public static string DumpDebuggingLog()
        {
            string output = "";
            foreach (BoardTile tile in s_board)
            {
                output += tile.GetPosition().X + "," +tile.GetPosition().Y + " ::: "+tile.GetDebugLog()+"\n";
            }
            return output;
        }
        public static bool IsCoordValid(int gridColumns, int gridRows)
        {
            return (gridRows > -1 && gridRows < SpriteSheetManager.Rows && gridColumns > -1 && gridColumns < SpriteSheetManager.Columns);
        }
        public static BoardTile[,] ConvertTilemapToLevelData(KeyValuePair<SpriteType[,],SpriteType> saveContents)
        {
            GameplayObjectFactory.ResetPlayerCount();
            BoardTile[,] data = new BoardTile[SpriteSheetManager.Columns, SpriteSheetManager.Rows];
            for (int ii = 0; ii < SpriteSheetManager.Columns; ii++)
            {
                for (int jj = 0; jj < SpriteSheetManager.Rows; jj++)
                {
                    data[ii, jj] = new BoardTile(ii, jj);
                    data[ii, jj].Register(GameplayObjectFactory.Create(saveContents.Value, ii, jj));
                    if (saveContents.Key[ii, jj] != SpriteType.EMPTY)
                    {
                        data[ii, jj].Register(GameplayObjectFactory.Create(saveContents.Key[ii, jj], ii, jj));
                    }
                }
            }
            return data;
        }
    }
}
