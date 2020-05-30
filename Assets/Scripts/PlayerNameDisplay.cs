using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerNameDisplay : MonoBehaviour {

    [SerializeField] Text playerWinText;
    // Use this for initialization
    void Start ()
    {
        playerWinText.text = ("No Winner Yet");
        //playerWinText = GetComponent<Text>();
        //UpdatePlayerName("white");
    }
	
	// Update is called once per frame
	void Update () {

    }
    public void UpdatePlayerName(string playerName)
    {
        Debug.Log(playerName + " wins!");
        playerWinText.text = (playerName + " Wins");//playerName + 
    }
}
