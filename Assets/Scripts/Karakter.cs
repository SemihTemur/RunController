using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Karakter : MonoBehaviour
{
    public GameManager gameManager;
    public GameObject _Camera;

    public bool alanaGirdiMi = false;

    // alana girdikten sonra gideceði son konum
    public GameObject sonKonum;

    public GameObject savasAlani;

    public Slider slider;

    public float fark;

  

    private void Start()
    {
        // burda slider'ýn maxvalues'sunu ilk baþtaki mesafe farký yapýyorum ký ona göre azalma olsun
        // oyle yapmayýp 500 dersen afaki bir þekilde arada o kadar mesafe yoksa slider bir zaman sonra dolmaz
        fark = Vector3.Distance(transform.position, savasAlani.transform.position);
        slider.maxValue =fark ;
    }

    // hareket iþlemleri FixedUpdate kýsmýnda yapýlýr
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
                //  sol fare dügmesýne basýlý tutuldugunda calýsýyo
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
                // burdada ilk farký sliderýn valuesne koyuyorum oyun çalýþýr çalýþmaz slider'ýn tamamý dolar
                // daha sonra fark azalýnca slider'da azalýr
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

        //Obje'nin ismine bakýyor.Eðer sayýya dönüþtürülecek bir þey ise bu if'in içine girip dönüþtürme iþlemi yapýyo
        if (int.TryParse(other.gameObject.name, out sayi))
        {
            sayi = int.Parse(other.gameObject.name);
            Debug.Log(other.gameObject.transform.position);

            gameManager.Ýþlemler(other.gameObject.tag, sayi, other.gameObject.transform);
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
        if (collision.gameObject.tag=="iðneliKutuu" || collision.gameObject.tag == "Pervaneigneleri")
        {
            // bu engelleri çarpýnca saga ya da  sola gidiyor otomatýkmen
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
            // bu engelleri çarpýnca saga ya da  sola gidiyor otomatýkmen
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
