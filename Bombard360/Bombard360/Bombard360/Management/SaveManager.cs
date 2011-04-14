using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bombard360;
using System.IO;

namespace Bombard360.Management
{
    class SaveManager
    {
        static public void Append(string saveContents)
        {
            TextWriter output = new StreamWriter("save.dat");
            output.WriteLine(saveContents);
            output.Close();
        }
        static public KeyValuePair<SpriteType[,],SpriteType> Load()
        {
            SpriteType[,] data = new SpriteType[SpriteSheetManager.Columns, SpriteSheetManager.Rows];
            SpriteType backgroundTile = SpriteType.EMPTY;
            if (File.Exists("save.dat"))
            {
                TextReader input = new StreamReader("save.dat");
                string saveData = input.ReadLine();
                string[] explodedSaveData = saveData.Split(',');
                for (int ii = 0; ii < SpriteSheetManager.Columns; ii++)
                {
                    for (int jj = 0; jj < SpriteSheetManager.Rows; jj++)
                    {
                        data[ii, jj] = SpriteType.EMPTY;
                    }
                }
                for (int ii = 0; ii < explodedSaveData.Count() - 1; ii += 3)
                {
                    foreach (SpriteType item in Enum.GetValues(typeof(SpriteType)))
                    {
                        if (item.ToString() == explodedSaveData[ii + 2])
                        {
                            if (int.Parse(explodedSaveData[ii]) == -1)
                            {
                                backgroundTile = item;
                            }
                            else
                            {

                                data[int.Parse(explodedSaveData[ii]), int.Parse(explodedSaveData[ii + 1])] = item;
                            }
                        }
                    }
                }
            }
            return new KeyValuePair<SpriteType[,],SpriteType>(data,backgroundTile);
        }
    }
}
