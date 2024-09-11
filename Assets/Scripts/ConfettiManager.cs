using UnityEngine;

public class ConfettiManager : MonoBehaviour
{
    public ParticleSystem confettiEffect;  // Assign this in the Inspector

    public void PlayConfetti()
    {
        if (confettiEffect != null)
        {
            if (confettiEffect.isPlaying)
            {
                confettiEffect.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
            }
            Debug.Log("Playing confetti!");
            confettiEffect.Play();  // Start the confetti effect
        }
    }
}

