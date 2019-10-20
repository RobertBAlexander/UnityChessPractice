using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour {

    AudioSource audioSource;

	// Use this for initialization
	void Start () {
        //prevent the instance of this being destroyed when a new scene loads.
        DontDestroyOnLoad(this);
        audioSource = GetComponent<AudioSource>();
        //this will be changed once I get player preferences, 
        //which will have to be added to a play file which players can sign in with,
        //and hopefully this can be done with minimal server storage
        //else I need ot have this as a local save thing.
        audioSource.volume = 1;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
