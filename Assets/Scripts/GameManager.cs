




  
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Semih;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject varýþ_Yeri;
    public static int currentCharacterCount;
    // performans acýsýndan yapýyoruz
    public List<GameObject> karakterHavuzu;
    public List<GameObject> olusmaEfektleri;
    public List<GameObject> yokOlmaEfektleri;
    public List<GameObject> adamLekeleri;

    [Header("Level Verileri")]
    public List<GameObject> dusmanHavuzu;
    public int kacDusmanOlsun = 0;

    // ozelleþtirme için
    [Header("SAPKALAR")]
    public GameObject[] sapkalar;
    // ozelleþtirme için
    [Header("SOPALAR")]
    public GameObject[] sopalar;
    // ozelleþtirme için
    [Header("KARAKTERLER")]
    public Material[] materialKarakter;

    public GameObject AnaKarakter;
    public SkinnedMeshRenderer rendererKarakter;

    bool sonaGeldikMi = false;

    Ýslemler islemler = new Ýslemler();
    BellekYönetimi bellekYönetimi = new BellekYönetimi();

    private UnityEngine.SceneManagement.Scene scene;

    public AudioSource oyunSesi;

    public GameObject[] islemPanelleri;

    public Slider oyunSesiSlider;

    // buda kullancagýmýz dil bilgisi içini Ýslemler kutuphanesýndeký Veri yonetýmý sýnýfýnýn Dili_ListeyeAktar fonksýyonundan dolduruyoruz
    List<Diller> DilBilgileri = new List<Diller>();

    //bu ise DilBilgileri listesinden sahnemde kullanacagým býlgýlerý alýp bu lýsteye yerlestýrmek ýcýn kullandýgým lýste
    // bu sayede ýndexým hep 0 olucak ve ýndexlerle ugrasmýycam
    public List<Diller> kullanýlacakDilBilgileri = new List<Diller>();

    // dosya ýle alakalý seyler var burda
    VeriYonetimi veriYonetimi = new VeriYonetimi();

    // hepsinin textlerini degýstýrýcegýz ýngýlýzce yada türkçeye gore. sahnedeký textlerý alýyorum bu arada onlarýn ýsmý degýscek ya
    public Text[] Texts;

    // sahne yukleme panelýmýn oldugu kýsým
    public GameObject sahneYuklemeEkrani;
    public Slider sahneYuklemeSlider;

    ReklamYonetimi reklamYonetimi = new ReklamYonetimi();


    private void Awake()
    {
        //Destroy(MenuSes.Instance.gameObject);
        //  Destroy(GameObject.FindWithTag("MenuSes")); boylede yok edebýlýrsýn
        KarakteriOzellestir();
        oyunSesi.volume = PlayerPrefs.GetFloat("OyunSes");
        oyunSesiSlider.value = bellekYönetimi.VeriyiGetir_float("OyunSes");
    }

    void Start()
    {
        currentCharacterCount = 1;
        DusmaniOlustur();
        // aktif sahneyý almam lazým cunku ornegýn kullanýcý 5 levelýn kýlýdýný actý. 3.levelý gecmýstý býr daha oynamak ýstedý
        // o zaman o anký aktýf sahneyý alýcam ký 3.levelý býtýrdýgýnde de 6.levelýn yaný son levelýn kýlýdý acýlmasýn
        // sadece son leveldeyse onu gecerse son levelýn kýlýdý acýlsýn
        scene = SceneManager.GetActiveScene();

        veriYonetimi.DilLoad();
        // sonra doldurulan lýsteyý cagýrýp bu lýsteyýde onun sayesýnde dolduruyoruz
        DilBilgileri = veriYonetimi.Dili_ListeyeAktar();
        // bu sayede 4.ncý ýndekse suan level ýle alakalý býlgýler, dýrekt kullanacagým lýsteye geldý rahatým
        kullanýlacakDilBilgileri.Add(DilBilgileri[5]);

        DilTercihiYonetimi();

        // oyun baslatýldýgýnda reklamýmý googledan ýstýyorum.reklamým hazýrlanýyo
        reklamYonetimi.RequestInterstitial();
        reklamYonetimi.RequestRewardedAd();

    }


public void DilTercihiYonetimi()
    {
        if (PlayerPrefs.GetString("Dil") == "TR")
        {
            for (int i = 0; i < Texts.Length; i++)
            {
                Texts[i].text = kullanýlacakDilBilgileri[0].dilBilgileriTR[i].Text;
            }
        }
        else
        {
            for (int i = 0; i < Texts.Length; i++)
            {
                Texts[i].text = kullanýlacakDilBilgileri[0].dilBilgileriEN[i].Text;
            }
        }
    }

    public void DusmaniOlustur()
    {
        for (int i = 0; i < kacDusmanOlsun; i++)
        {
            dusmanHavuzu[i].gameObject.SetActive(true);
        }
    }

    public void DusmaniTetikle()
    {
        foreach (var item in dusmanHavuzu)
        {
            if (item.activeInHierarchy)
            {
                item.GetComponent<Dusman>().AnimasyonuTetikle();
            }
        }
        sonaGeldikMi = true;
        KazanmaDurumu();
    }


    public void Ýþlemler(string iþlem, int sayý, Transform iþlemPozisyonu)
    {
        switch (iþlem)
        {
            case "Toplama":
                islemler.Toplama(sayý, karakterHavuzu, iþlemPozisyonu, olusmaEfektleri);
                Debug.Log("Toplama :" + currentCharacterCount);
                break;

            case "Çýkarma":
                islemler.Çýkarma(sayý, karakterHavuzu, yokOlmaEfektleri);
                Debug.Log("Çýkarma :" + currentCharacterCount);
                break;

            case "Çarpma":
                islemler.Çarpma(sayý, karakterHavuzu, iþlemPozisyonu, olusmaEfektleri);
                Debug.Log("Çarpma :" + currentCharacterCount);
                break;

            case "Bölme":
                islemler.Bölme(sayý, karakterHavuzu, yokOlmaEfektleri);
                Debug.Log("Bölme :" + currentCharacterCount);
                break;
        }
    }

    public void YokOlmaEfektiOluþtur(Vector3 iþlemPozisyonu, bool isAdamLekesi = false, bool azalmaDurumu = false)
    {
        foreach (var item in yokOlmaEfektleri)
        {
            if (!item.activeInHierarchy)
            {
                item.SetActive(true);
                item.transform.position = iþlemPozisyonu;
                item.GetComponent<ParticleSystem>().Play();
                item.GetComponent<AudioSource>().Play();
                // azalmaDurumu alt karakterý mý dusmaný mý azaltýcagýz onun ýcýn var
                if (!azalmaDurumu)
                    currentCharacterCount--;
                else
                    kacDusmanOlsun--;
                break;
            }
        }

        if (isAdamLekesi == true)
        {
            Vector3 iþlemPozisyonu2 = new Vector3(iþlemPozisyonu.x, .0005f, iþlemPozisyonu.z);
            foreach (var item2 in adamLekeleri)
            {
                if (!item2.activeInHierarchy)
                {
                    item2.SetActive(true);
                    item2.transform.position = iþlemPozisyonu2;
                    break;
                }
            }
        }

        KazanmaDurumu();
    }

    public void KazanmaDurumu()
    {
        if (sonaGeldikMi)
        {
            if (currentCharacterCount == 1 || kacDusmanOlsun == 0)
            {
                // 2 3 ya da 3 3
                if (currentCharacterCount < kacDusmanOlsun || currentCharacterCount == kacDusmanOlsun)
                {
                    foreach (var item in dusmanHavuzu)
                    {
                        if (item.activeInHierarchy)
                        {
                            item.GetComponent<Animator>().SetBool("Saldir", false);
                        }
                    }

                    StartCoroutine(PaneliAktifEt(3));
                    // sona gelýnce reklamý goster
                    reklamYonetimi.GecisReklamiGoster();
                }
                else
                {
                    foreach (var item in karakterHavuzu)
                    {
                        if (item.activeInHierarchy)
                        {
                            item.GetComponent<Animator>().SetBool("Saldir", false);
                        }
                    }

                    if (currentCharacterCount > 5)
                    {
                        // eger son leveldeysem artýr gardas degýlse nýe levelý artýrsýn
                        if (scene.buildIndex == bellekYönetimi.VeriyiGetir_int("SonLevel"))
                        {
                            bellekYönetimi.VeriyiKaydet_int("SonLevel", bellekYönetimi.VeriyiGetir_int("SonLevel") + 1);
                            bellekYönetimi.VeriyiKaydet_int("Puan", bellekYönetimi.VeriyiGetir_int("Puan") + 600);
                        }
                    }

                    else
                    {
                        // eger son leveldeysem artýr gardas degýlse nýe levelý artýrsýn
                        if (scene.buildIndex == bellekYönetimi.VeriyiGetir_int("SonLevel"))
                        {
                            bellekYönetimi.VeriyiKaydet_int("SonLevel", bellekYönetimi.VeriyiGetir_int("SonLevel") + 1);
                            bellekYönetimi.VeriyiKaydet_int("Puan", bellekYönetimi.VeriyiGetir_int("Puan") + 100);
                        }
                    }

                    StartCoroutine(PaneliAktifEt(2));
                }

                AnaKarakter.GetComponent<Animator>().SetBool("Saldir", false);
            }
        }
    }

    IEnumerator PaneliAktifEt(int index)
    {
        yield return new WaitForSeconds(1f);
        islemPanelleri[index].gameObject.SetActive(true);
    }

    public void KarakteriOzellestir()
    {
        // -1 olursa karakterýn bý ozellestýrmesý olmaz aktýf olan varsa kapatýlýr sapka sopa falan
        if (bellekYönetimi.VeriyiGetir_int("AktifSapka") == -1)
        {
            foreach (var item in sapkalar)
            {
                item.gameObject.SetActive(false);
            }
        }
        else
        {
            sapkalar[bellekYönetimi.VeriyiGetir_int("AktifSapka")].SetActive(true);
        }

        // -1 olursa karakterýn bý ozellestýrmesý olmaz aktýf olan varsa kapatýlýr sapka sopa falan
        if (bellekYönetimi.VeriyiGetir_int("AktifSopa") == -1)
        {
            foreach (var item in sopalar)
            {
                item.gameObject.SetActive(false);
            }
        }
        else
        {
            sopalar[bellekYönetimi.VeriyiGetir_int("AktifSopalar")].SetActive(true);
        }

        Material[] mats = rendererKarakter.materials;
        mats[0] = materialKarakter[bellekYönetimi.VeriyiGetir_int("AktifKarakter")];
        rendererKarakter.materials = mats;

    }

    // UI Menu Kýsýmlarý

    // butonlara basýnca butonlara gore panel açar
    public void PaneliAç(string panelAdi)
    {
        Time.timeScale = 0f;

        if (panelAdi == "Cikis")
        {
            islemPanelleri[1].SetActive(true);
        }
        else if (panelAdi == "Ayarlar")
        {
            islemPanelleri[0].SetActive(true);
        }
    }

    // sliderýn tetýklenecegý fonksýyon ayarlar panelý acýlýnca
    public void OyunSesiAyarý()
    {
        bellekYönetimi.VeriyiKaydet_float("OyunSes", oyunSesiSlider.value);
        oyunSesi.volume = oyunSesiSlider.value;
    }

    // Cýkýþ butonuna basýnca acýlýcak panel
    public void ÇýkýþEkrani(string durum)
    {
        Time.timeScale = 1f;
        if (durum == "Devam")
        {
            islemPanelleri[1].SetActive(false);
        }
           
        else if(durum=="Tekrar")
        {
            Time.timeScale = 1f;
            // o anký aktýf sahneyý tekrar yukluyo
            SceneManager.LoadScene(scene.buildIndex);
        }
        else
        {
            SceneManager.LoadScene(0);
        }
    }

    public void OyunEkranýnýKapat()
    {
        islemPanelleri[0].SetActive(false);
        Time.timeScale = 1f;
    }

    public void SonrakiLeveliAç()
    {
        StartCoroutine(LoadAsync(scene.buildIndex+1));
    }

    // sahne yukleme ekraný ýcýn kullanýlan fonksýyon sadece level açarken cýkartýyorum.
    IEnumerator LoadAsync(int index)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(index);

        sahneYuklemeEkrani.SetActive(true);

        // deger 1 olursa buraya gýrmez halýyle ve 0.9da kalýr slýder
        while (!operation.isDone)
        {
            // kalmamasý ýcýn bu matematksel ýslemý yapýyoruz bu ýslem deger 0 ise 0a 0.45 ise 0.5e 0.9 ise 1e yuvarlar
            // burdada 0.9a bolmemýzýn nedený deger 0.9da kalýyor ve ben onu 0.9a bolersem sonc 1 olur ve slider tamamlanmýþ tam yuklenmýs olur
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            sahneYuklemeSlider.value = progress;
            yield return null;
        }
    }

    public void OdulluReklamiGoster()
    {
        reklamYonetimi.OdulluReklamiGoster();
    }

}











