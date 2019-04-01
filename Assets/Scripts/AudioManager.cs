using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {
    public AudioClip destroyBlockSound;
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
            GetComponent<AudioSource>().PlayOneShot(destroyBlockSound);
        }
        else
        {
            GetComponent<AudioSource>().PlayOneShot(placeBlockSound);
        }

    }
    // play the destroy block sound
    void PlayDestroyBlockSound()
    {
        GetComponent<AudioSource>().PlayOneShot(destroyBlockSound);
    }

    // play the place block sound
    void PlayPlaceBlockSound()
    {
        GetComponent<AudioSource>().PlayOneShot(placeBlockSound);
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
