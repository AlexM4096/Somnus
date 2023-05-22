using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager main;

    private void Awake()
    {
        main = this;
    }

    public AudioSource error;
    public AudioSource klawa;
    public AudioSource rule;
    public AudioSource win;
}
