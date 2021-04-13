using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SonidoFragmento : MonoBehaviour
{
    public AudioClip vidrioRoto;
    

    AudioSource fuenteAudio;
    // Start is called before the first frame update
    void Start()
    {
        fuenteAudio = GetComponent<AudioSource>();
        
    }

    // Update is called once per frame
    void Update()
    {
             
    }

  

    private void OnCollisionEnter(Collision collision)
    {
        
        if (collision.gameObject.CompareTag("piso"))
        {
            fuenteAudio.clip = vidrioRoto;
            fuenteAudio.Play();
            
            Debug.Log("Suena vidrio roto");
        }
        
    }
}
