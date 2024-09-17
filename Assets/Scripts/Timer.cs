using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    private float leftEdge;
	private void Start(){
		leftEdge = Camera.main.ScreenToWorldPoint(Vector3.zero).x - 2f;


	}
	private void Update(){

		transform.position += Vector3.left * GameManager.Instance.gameSpeed * Time.deltaTime;

		if(transform.position.x < leftEdge){

			Destroy(gameObject);
		}

		
		

    	}


	private void OnTriggerEnter(Collider other)
 	  {

		if(other.CompareTag("Obstacle") || other.CompareTag("Player")){
			Destroy(gameObject);
		}
      	

	}

}



