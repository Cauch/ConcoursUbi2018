using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyStupidMaze : MonoBehaviour {
    public GameObject tileClearTemplate;
    public GameObject tileObstacleTemplate;
    public GameObject tileWallTemplate;

    MazeTile mazeTile;
	// Use this for initialization
	void Start () {
        List<List<char>> map = new List<List<char>> {
            new List<char> { 'X', 'X', 'E', 'X' },
            new List<char> { 'X', 'O', ' ', 'X' },
            new List<char> { 'X', 'X', 'S', 'X' }
        };

        List<List<char>> map2 = new List<List<char>> {
            new List<char> { 'X', 'X', 'E', 'X' },
            new List<char> { 'X', 'O', ' ', 'X' },
            new List<char> { 'X', 'X', 'S', 'X' }
        };
        mazeTile = new MazeTile(tileClearTemplate, tileObstacleTemplate, tileWallTemplate, map, new Vector3(10,0,10));
        mazeTile.Rotate90();
        mazeTile.Rotate90();
        mazeTile.Instantiate();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
