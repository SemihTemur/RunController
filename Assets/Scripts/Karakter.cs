using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Karakter : MonoBehaviour
{
    public GameManager gameManager;
    public GameObject _Camera;

    public bool alanaGirdiMi = false;

    // alana girdikten sonra gidece�i son konum
    public GameObject sonKonum;

    public GameObject savasAlani;

    public Slider slider;

    public float fark;

  

    private void Start()
    {
        // burda slider'�n maxvalues'sunu ilk ba�taki mesafe fark� yap�yorum k� ona g�re azalma olsun
        // oyle yapmay�p 500 dersen afaki bir �ekilde arada o kadar mesafe yoksa slider bir zaman sonra dolmaz
        fark = Vector3.Distance(transform.position, savasAlani.transform.position);
        slider.maxValue =fark ;
    }

    // hareket i�lemleri FixedUpdate k�sm�nda yap�l�r
    void FixedUpdate()
    {
        if (!alanaGirdiMi)
        {
            transform.Translate(Vector3.forward * .5f * Time.deltaTime);
        }
      
    }


    void Update()
    {
        if (Time.timeScale != 0f)
        {
            if (!alanaGirdiMi)
            {
                //  sol fare d�gmes�ne bas�l� tutuldugunda cal�s�yo
                if (Input.GetKey(KeyCode.Mouse0))
                {
                    // mouse sola hareket  ettirirsen
                    if (Input.GetAxis("Mouse X") < 0)
                    {
                        transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x - .1f, transform.position.y, transform.position.z), .3f);
                    }

                    if (Input.GetAxis("Mouse X") > 0)
                    {
                        transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x + .1f, transform.position.y, transform.position.z), .3f);
                    }
                }
                // burdada ilk fark� slider�n valuesne koyuyorum oyun �al���r �al��maz slider'�n tamam� dolar
                // daha sonra fark azal�nca slider'da azal�r
                fark = Vector3.Distance(transform.position, savasAlani.transform.position);
                slider.value = fark;
            }
            else
            {
                transform.position = Vector3.Lerp(transform.position, sonKonum.transform.position, 0.015f);
                if (slider.value != 0)
                {
                    slider.value -= .01f;
                }
            }
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        int sayi;

        //Obje'nin ismine bak�yor.E�er say�ya d�n��t�r�lecek bir �ey ise bu if'in i�ine girip d�n��t�rme i�lemi yap�yo
        if (int.TryParse(other.gameObject.name, out sayi))
        {
            sayi = int.Parse(other.gameObject.name);
            Debug.Log(other.gameObject.transform.position);

            gameManager.��lemler(other.gameObject.tag, sayi, other.gameObject.transform);
        }
        
        else if (other.CompareTag("SavasAlani"))
        {
            _Camera.GetComponent<CameraController>().alanaGirdiMi = true;
             gameManager.DusmaniTetikle();
             alanaGirdiMi = true;;
        }

    }


    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag=="i�neliKutuu" || collision.gameObject.tag == "Pervaneigneleri")
        {
            // bu engelleri �arp�nca saga ya da  sola gidiyor otomat�kmen
            if (transform.position.x >= 0f)
            {
                Debug.Log("selam");
                transform.position = new Vector3(transform.position.x - 0.3f, transform.position.y, transform.position.z);
            }
            else
            {
                transform.position = new Vector3(transform.position.x + 0.3f, transform.position.y, transform.position.z);
            }

        }
        else if(collision.gameObject.tag == "Direk")
        {
            // bu engelleri �arp�nca saga ya da  sola gidiyor otomat�kmen
            if (transform.position.x >= 0f)
            {
                transform.position = new Vector3(transform.position.x - 0.35f, transform.position.y, transform.position.z+0.1f);
            }
            else
            {
                transform.position = new Vector3(transform.position.x + 0.35f, transform.position.y, transform.position.z+0.1f);
            }
        }
       

    }


}
