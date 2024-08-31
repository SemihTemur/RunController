using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// di�er sahnelere gect�g�mdede muz�g�n calmas�n� �st�yorum ondan boyle yapt�m // bu class�n amac� bu yan�
public class MenuSes : MonoBehaviour
{
    // di�er sahnelere gect�g�mdede muz�g�n calmas�n� �st�yorum ondan boyle yapt�m
    public static GameObject Instance;

    public AudioSource ses;

    private void Awake()
    {
        ses.volume = PlayerPrefs.GetFloat("MenuSes");
        if (Instance == null)
        {
            Instance = gameObject;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        ses.volume = PlayerPrefs.GetFloat("MenuSes");
    }
}
