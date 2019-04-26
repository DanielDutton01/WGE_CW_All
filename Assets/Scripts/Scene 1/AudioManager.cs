using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {
    public AudioClip place1, place2, place3, place4;
    public AudioClip destroy;

    // Use this for initialization
    void Start () {

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void PlayChangeBlockSound(int blockType)
    {
        if (blockType == 0)
        {
            GetComponent<AudioSource>().PlayOneShot(destroy);
        }
        else if (blockType == 1)
        {
            GetComponent<AudioSource>().PlayOneShot(place1);
        }
        else if (blockType == 2)
        {
            GetComponent<AudioSource>().PlayOneShot(place2);
        }
        else if (blockType == 3)
        {
            GetComponent<AudioSource>().PlayOneShot(place3);
        }
        else
        {
            GetComponent<AudioSource>().PlayOneShot(place4);
        }

    }

    // When game object is enabled
    void OnEnable()
    {
        VoxelChunk.OnEventBlockChanged += PlayChangeBlockSound;

    }

    // When game object is disabled
    void OnDisable()
    {
        VoxelChunk.OnEventBlockChanged -= PlayChangeBlockSound;
    }
}
