using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour {

	private const int mapSize = 8;
	private char[,] map = new int[mapSize,mapSize];
	private int[,] posBE = new int[2, 2];

	private void CreateBorderInMap(){
		for(int i=0;i<=mapSize; i++){
			for (int j = 0; j < mapSize; j++) {
				if (i == 0 || j == 0 || i == mapSize - 1 || j == mapSize - 1) {
					map [i, j] = "W";
				} else {
					map[i,j] = "E";
				}
			}
		}
	}
	private void GenerateBeginningEnding(){
		//PQ PODE GERAR EM CIMA DE BARREIRA
		posBE [0,0] = Random.Range (1, mapSize-1);
		posBE [1, 0] = 1;
		posBE [0, 1] = Random.Range (1, mapSize-1);
		posBE [1, 1] = 6;

		map [posBE [0, 0], posBE [0, 1]] = "B";
		map [posBE [1, 0], posBE [1, 1]] = "G";
	}
	private void GeneratePossiblePath(){//REVER ISSO
		int horizontal = posBE[0,0] - posBE[1,0];
		int vertical = posBE[1,0] - posBE[1,1];
		int i, j;
		i = posBE [0, 0];
		j = posBE [0, 1];

		while (vertical != 0 && horizontal !=0) {
			switch (Random.Range (1, 3)) {
			case 1://horizontal
				if (horizontal != 0) {
					i -= horizontal / Mathf.Abs (horizontal);
					horizontal -= horizontal / Mathf.Abs (horizontal);
				}
				break;
			case 2://vertical
				if (vertical != 0) {
					j += vertical / Mathf.Abs (vertical);
					vertical += vertical / Mathf.Abs (vertical);
				}
				break;
			}
			map [i, j] = "P";
		}
	}
	private void GenerateObstaclesPlacement(){
			
	}
}
