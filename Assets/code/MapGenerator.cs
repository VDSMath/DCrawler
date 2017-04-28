using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour {

	private const int mapSize = 16;
	private float blockSize;
	private char[,] map = new char[mapSize,mapSize];
	[SerializeField]private int[] posB = new int[2];
	[SerializeField]private int[] posG = new int[2];

	[SerializeField]private GameObject groundBlock;
	[SerializeField]private GameObject wallBlock;

	private void Start(){
		blockSize = groundBlock.GetComponent<SpriteRenderer> ().bounds.size.x;
		NewMap ();
		InstantiateMap();
	}
	private void CreateBorderInMap(){
		for(int i = 0; i < mapSize; i++){
			for (int j = 0; j < mapSize; j++) {
				if (i == 0 || j == 0 || i == mapSize - 1 || j == mapSize - 1) {
					map [i,j] = 'W';
				} else {
					map[i,j] = 'E';
				}
			}
		}
	}
	private void GenerateBeginningEnding(){
		//PQ PODE GERAR EM CIMA DE BARREIRA
		posB [0] = Random.Range (1, mapSize-1);
		posB [1] = 1;
		posG [0] = Random.Range (1, mapSize-1);
		posG [1] = mapSize-2;

		map [posB [0], posB [1]] = 'B';
		map [posG [0], posG [1]] = 'G';
	}
	private void GeneratePossiblePath(){//REVER ISSO: TROCAR i E j?
		int horizontal = posB[0] - posG[0];
		int vertical = posB[1] - posG[1];
		int i = posB [0];//i e j sao as coord da posicao inicial
		int j = posB [1];

		while (i != posG[0] || j != posG[1]-1) {//Enquanto i e j forem diferentes da posicao final
			
			switch (Random.Range (1, 3)) {
				case 1://horizontal
					if (horizontal != 0) {
						i -= horizontal / Mathf.Abs (horizontal);
						horizontal += -horizontal / Mathf.Abs (horizontal);
					}
					break;
				case 2://vertical
					if (vertical != 0) {
						j -= vertical / Mathf.Abs (vertical);
						vertical += -vertical / Mathf.Abs (vertical);
					}
					break;
			}
			map [i, j] = 'P';

		}
	}
	private void GenerateObstaclesPlacement(){
		int max = 2;
		for (int i = 0; i < max; i++) {
			map[Random.Range(2,mapSize),Random.Range(2,mapSize)] = 'O';
		}
	}
	private void NewMap(){
		CreateBorderInMap ();
		GenerateBeginningEnding ();
		GenerateObstaclesPlacement ();
		GeneratePossiblePath ();
	}
	private void InstantiateMap(){
		for (int linhas = 0; linhas < mapSize; linhas++) {
			for (int colunas = 0; colunas < mapSize; colunas++) {
				if (map [linhas, colunas] == 'W') {
					Instantiate (wallBlock, new Vector3 (linhas, colunas) * blockSize, Quaternion.identity);
				} else if (map [linhas, colunas] == 'E') {
					Instantiate (groundBlock,new Vector3(linhas,colunas)*blockSize,Quaternion.identity);				
				} else if (map [linhas, colunas] == 'P') {
					Instantiate (groundBlock,new Vector3(linhas,colunas)*blockSize,Quaternion.identity).GetComponent<SpriteRenderer>().color = Color.yellow;				
				} else if (map [linhas, colunas] == 'O') {
					Instantiate (groundBlock,new Vector3(linhas,colunas)*blockSize,Quaternion.identity).GetComponent<SpriteRenderer>().color = Color.red;				
				} else if (map [linhas, colunas] == 'B' || map [linhas, colunas] == 'G') {
					Instantiate (groundBlock,new Vector3(linhas,colunas)*blockSize,Quaternion.identity).GetComponent<SpriteRenderer>().color = Color.green;				
				}
			}
		}
	}
}
