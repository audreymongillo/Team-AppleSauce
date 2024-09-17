using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class City : MonoBehaviour
{

	private MeshRenderer meshRenderer;




	private void Awake(){
		meshRenderer = GetComponent<MeshRenderer>();


		
	}


	private void Update(){
		if(GameManager.currentScore >= 200){
			GetComponent<MeshRenderer>().enabled = true;
			GetComponent<BoxCollider>().enabled = true;
			

			float speed = GameManager.Instance.gameSpeed / transform.localScale.x;
			meshRenderer.material.mainTextureOffset += Vector2.right * speed * Time.deltaTime;
		}else{

			if(GetComponent<MeshRenderer>().enabled == true){
				GetComponent<MeshRenderer>().enabled = !GetComponent<MeshRenderer>().enabled;
				GetComponent<BoxCollider>().enabled = !GetComponent<BoxCollider>().enabled;

				

			}

		}
		


	
	}


}
