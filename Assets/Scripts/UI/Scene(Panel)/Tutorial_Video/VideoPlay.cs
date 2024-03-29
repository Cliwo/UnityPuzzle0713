﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
//https://www.youtube.com/watch?v=dWncJP6KMxc


public class VideoPlay : MonoBehaviour {

    public MovieTexture movie;
    private AudioSource audio;
    // Use this for initialization
    void Start () {
        GetComponent<RawImage>().texture = movie as MovieTexture;
        audio = GetComponent<AudioSource>();
        audio.clip = movie.audioClip;
        movie.Play();
        audio.Play();
        Debug.Log("Video Play");
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
