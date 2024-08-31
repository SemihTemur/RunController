using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// diðer sahnelere gectýgýmdede muzýgýn calmasýný ýstýyorum ondan boyle yaptým // bu classýn amacý bu yaný
public class MenuSes : MonoBehaviour
{
    // diðer sahnelere gectýgýmdede muzýgýn calmasýný ýstýyorum ondan boyle yaptým
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
