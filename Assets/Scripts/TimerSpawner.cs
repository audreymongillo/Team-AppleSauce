using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerSpawner : MonoBehaviour
{




	public GameObject prefab;
	


	public float minSpawnRate = 20f;
	public float maxSpawnRate = 30f;
	
	private void OnEnable(){
		Invoke(nameof(Spawn), Random.Range(minSpawnRate, maxSpawnRate));

	}
	private void OnDisable(){
		CancelInvoke();


	}



	private void Spawn(){
		

		GameObject powerup = Instantiate(prefab);
		powerup.transform.position += transform.position;

		Invoke(nameof(Spawn), Random.Range(minSpawnRate, maxSpawnRate));


	


	}

        
    }

