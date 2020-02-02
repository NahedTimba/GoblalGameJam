﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Burger : MonoBehaviour
{
	
	public List<GameObject> allIngredientsFound = new List<GameObject>();
	public List<Keys> onBurger = new List<Keys>(); 
	public GameObject BunTop, BunBottom;
	//public float sizeOfIngredients = 0.1f;
	public Transform[] positions = new Transform[0];
	
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
	    for(int i = 0; i < onBurger.Count; i++)
	    {
	    	if(onBurger[i].isUsed)
	    	{
	    		onBurger.RemoveAt(i);
	    		i--;
	    		
	    		UpdatePositions();
	    	}
	    }
    }
    
	public void UpdatePositions()
	{
		for(int i = 0; i < onBurger.Count; i++)
		{
			onBurger[i].GetComponent<CopyTransform>().target = this.positions[i];
		}
		//move bun up
		BunTop.transform.localPosition = this.positions[onBurger.Count].localPosition;
	}
	 
    
	private void OnTriggerEnter(Collider col)
	{
		if (col.gameObject.layer == 8)
		{
			//its already in the burger
			if(allIngredientsFound.Contains(col.gameObject))
				return;
				
			//turn on kinematic rigidboard
			col.GetComponent<Rigidbody>().isKinematic = false;
			
			allIngredientsFound.Add(col.gameObject);
			onBurger.Add(col.gameObject.GetComponent<Keys>());
		
			UpdatePositions();
		}
		else 
		if (col.gameObject.layer == 9)//head
		{
			//play eat animation - TO DO
			///play eat sound - TO DO
			/// wait time until finished - TO DO
			/// 
			//reset game
			GameController.Instance.ResetGame();
			ResetBurger();
		}
	}
	
	void ResetBurger()
	{
		onBurger = new List<Keys>();
		
		for(int i = 0; i < allIngredientsFound.Count; i++)
		{
			onBurger.Add(allIngredientsFound[i].GetComponent<Keys>());
		}
		
		UpdatePositions();
	}
	
}