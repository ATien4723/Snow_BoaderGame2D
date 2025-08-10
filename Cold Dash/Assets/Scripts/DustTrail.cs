using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DustTrail : MonoBehaviour
{
    [SerializeField] ParticleSystem dustParticles;

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Ground")//Yere deðmeye baðlý efekt tetiklenmesi
        {
            dustParticles.Play();
        }
        if (other.gameObject.tag == "Ground2")
        {
            dustParticles.Play();
        }
    }

    void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.tag == "Ground" || other.gameObject.tag == "Ground2")
        {
            dustParticles.Stop();
        }
    }
}
