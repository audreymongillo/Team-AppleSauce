using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buildings : MonoBehaviour
{
   

	private MeshRenderer meshRenderer;
	private float offset;




	private void Awake(){
		meshRenderer = GetComponent<MeshRenderer>();
		//offset = 0;
	
	}


	private void Update(){

           if(MainMenu.altGame){
		if(GameManager.currentScore >= 200){
			GetComponent<MeshRenderer>().enabled = true;
		
	
			float speed = GameManager.Instance.gameSpeed / transform.localScale.x;
			meshRenderer.material.mainTextureOffset += Vector2.right * speed * Time.deltaTime;
			offset += GameManager.Instance.gameSpeed * Time.deltaTime;
			if(offset>=40){
		
				meshRenderer.material.mainTextureOffset = Vector2.zero;
			//	meshRenderer.material.mainTextureOffset *= Vector2.right*speed*0;
				offset = 0;
			}

			
			
		}else{

			if(GetComponent<MeshRenderer>().enabled == true){
				GetComponent<MeshRenderer>().enabled = !GetComponent<MeshRenderer>().enabled;
			

		
			}

		}
	   }


	
	}
		
}
