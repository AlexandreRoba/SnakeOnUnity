using UnityEngine;
using System.Collections;

public class FruitSpawner : MonoBehaviour 
{
	
	public int FruitLastingTimeInSeconds;
	public GameObject Fruit;
	
	int _NumberOfRows = 7;
	int _NumberOfColumns = 9;
	private GameObject _Fruit;
	
	// Use this for initialization
	void Start () {
		InvokeRepeating("SpawnNewFruit",0,FruitLastingTimeInSeconds);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void SpawnNewFruit()
	{
		if(_Fruit!=null){
			//Destroy the old fruit
			Destroy(_Fruit);
		}
		//Spawn a new Fruit
		int fruitRaw = Random.Range(-_NumberOfRows,_NumberOfRows);
		int fruitColumn = Random.Range(-_NumberOfColumns,_NumberOfColumns);		
		_Fruit = Instantiate(Fruit) as GameObject;
		_Fruit.transform.position = new Vector3(fruitColumn,fruitRaw,0);
		_Fruit.SetActive(true);
		
	}
}
