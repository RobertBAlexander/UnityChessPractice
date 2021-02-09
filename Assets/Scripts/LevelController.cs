using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour {
    
    [SerializeField] GameObject gameEndLabel;
    //[SerializeField] GameObject loseLabel;
    // Use this for initialization


    void Start ()
    {
        //currentPlayer = FindObjectOfType<GameManager>().GetCurrentPlayerName();
        gameEndLabel.SetActive(false);

        
    }
	
	// Update is called once per frame
	void Update () {
		
	}


    public void HandleEndCondition()
    {
        gameEndLabel.SetActive(true);
        //GetComponent<AudioSource>().Play();
    }
    
}
