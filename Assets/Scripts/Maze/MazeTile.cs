using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeTile {
    public List<List<ITile>> Tiles;
    public float width;
    public float length;
    public float floorLevel;

    private TileClear tileClearTemplate;
    private TileObstacle tileObstacleTemplate;
    private TileWall tileWallTemplate;

    public MazeTile(GameObject tileClear, GameObject tileObstacle, GameObject tileWall, List<List<char>> tiles, Vector3 size)
    {

        width = size.x;
        floorLevel = size.y;
        length = size.z;

        tileClearTemplate = new TileClear(tileClear);
        tileObstacleTemplate = new TileObstacle(tileObstacle);
        tileWallTemplate = new TileWall(tileWall);

        //Create maze
        Tiles = new List<List<ITile>>();

        for (int i = 0; i < tiles.Count; i++)
        {
            List<ITile> row = new List<ITile>();
            for (int j = 0; j < tiles[i].Count; j++)
            {
                ITile t;

                switch(tiles[i][j])
                {
                    case ' ':
                        t = tileClearTemplate;
                        break;
                    case 'O':
                        t = tileObstacleTemplate;
                        break;
                    case 'X':
                        t = tileWallTemplate;
                        break;
                    default:
                        t = tileClearTemplate;
                        break;
                }

                row.Add(t);
            }
            Tiles.Add(row);
        }
    }
    public void Fuse(MazeTile mazeTile)
    {

    }

    public void Rotate90()
    {
        List<List<ITile>> newTiles = new List<List<ITile>>();

        for (int i = 0; i < Tiles[0].Count; i++)
        {
            List<ITile> row = new List<ITile>();
            for (int j = 0; j < Tiles.Count; j++)
            {
                row.Add(Tiles[j][i]);
            }
            newTiles.Add(row);
        }
        Tiles = newTiles;
    }

    public void Instantiate()
    {
        for(int i = 0; i < Tiles.Count; i++)
        {
            for (int j = 0; j < Tiles[i].Count; j++)
            {
                Vector3 position = new Vector3(width * (i+0.5f), floorLevel, length * (j+0.5f));
                Tiles[i][j].Instantiate(position);
            }
        }
    }
}
