using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
    public static EffectManager Instance { get; private set; }
    private void Awake()
    {
        #region Singleton
        
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }

        #endregion

    }

    [SerializeField] private AudioSource shootingSFX;

    public void PlayShootingSFX()
    {
        shootingSFX.Play();
    }

}
