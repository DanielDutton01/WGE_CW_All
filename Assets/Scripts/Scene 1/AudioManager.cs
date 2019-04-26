using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {
    public AudioClip destroy1, destroy2, destroy3, destroy4;
    public AudioClip placeBlockSound;

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
            GetComponent<AudioSource>().PlayOneShot(destroy1);
        }
        else if (blockType == 1)
        {
            GetComponent<AudioSource>().PlayOneShot(destroy2);
        }
        else if (blockType == 2)
        {
            GetComponent<AudioSource>().PlayOneShot(destroy3);
        }
        else if (blockType == 3)
        {
            GetComponent<AudioSource>().PlayOneShot(destroy4);
        }
        else
        {
            GetComponent<AudioSource>().PlayOneShot(placeBlockSound);
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
