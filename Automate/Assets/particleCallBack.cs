using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class particleCallBack : MonoBehaviour
{
    public AudioSource bruh;
    public void OnParticleSystemStopped()
    {
        bruh = GetComponent<AudioSource>();
        bruh.Play();
        StartCoroutine(Check());
    }

    IEnumerator Check()
    {
        while(true)
        {
            if (!bruh.isPlaying)
            {
                automate asd = FindObjectOfType<automate>();
                StartCoroutine(asd.HealthBar());
                break;
            }
            yield return null;
        }

    }
}
