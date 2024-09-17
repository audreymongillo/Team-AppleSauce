using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buildings : MonoBehaviour
{
   

	private SpriteRenderer spriteRenderer;




	private void Awake(){
		spriteRenderer = GetComponent<SpriteRenderer>();


		
	}


	private void Update(){
		if(GameManager.currentScore >= 200){
			GetComponent<SpriteRenderer>().enabled = true;
			
			

			float speed = GameManager.Instance.gameSpeed / transform.localScale.x;
			spriteRenderer.material.mainTextureOffset += Vector2.right * speed * Time.deltaTime;
		}else{

			if(GetComponent<SpriteRenderer>().enabled == true){
				GetComponent<SpriteRenderer>().enabled = !GetComponent<SpriteRenderer>().enabled;
				

				

			}

		}
		


	
	}
		
}
