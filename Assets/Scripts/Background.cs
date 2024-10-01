using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{

	float speed;
	
    // Start is called before the first frame update
    void Start()
    {
        
    }

  
    void Update()
    {
	speed = GameManager.Instance.gameSpeed;
   //     transform.Translate(Vector).left * Time.deltaTime * speed;
    }
}
