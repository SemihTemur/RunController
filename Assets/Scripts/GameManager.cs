




  
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Semih;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject var��_Yeri;
    public static int currentCharacterCount;
    // performans ac�s�ndan yap�yoruz
    public List<GameObject> karakterHavuzu;
    public List<GameObject> olusmaEfektleri;
    public List<GameObject> yokOlmaEfektleri;
    public List<GameObject> adamLekeleri;

    [Header("Level Verileri")]
    public List<GameObject> dusmanHavuzu;
    public int kacDusmanOlsun = 0;

    // ozelle�tirme i�in
    [Header("SAPKALAR")]
    public GameObject[] sapkalar;
    // ozelle�tirme i�in
    [Header("SOPALAR")]
    public GameObject[] sopalar;
    // ozelle�tirme i�in
    [Header("KARAKTERLER")]
    public Material[] materialKarakter;

    public GameObject AnaKarakter;
    public SkinnedMeshRenderer rendererKarakter;

    bool sonaGeldikMi = false;

    �slemler islemler = new �slemler();
    BellekY�netimi bellekY�netimi = new BellekY�netimi();

    private UnityEngine.SceneManagement.Scene scene;

    public AudioSource oyunSesi;

    public GameObject[] islemPanelleri;

    public Slider oyunSesiSlider;

    // buda kullancag�m�z dil bilgisi i�ini �slemler kutuphanes�ndek� Veri yonet�m� s�n�f�n�n Dili_ListeyeAktar fonks�yonundan dolduruyoruz
    List<Diller> DilBilgileri = new List<Diller>();

    //bu ise DilBilgileri listesinden sahnemde kullanacag�m b�lg�ler� al�p bu l�steye yerlest�rmek �c�n kulland�g�m l�ste
    // bu sayede �ndex�m hep 0 olucak ve �ndexlerle ugrasm�ycam
    public List<Diller> kullan�lacakDilBilgileri = new List<Diller>();

    // dosya �le alakal� seyler var burda
    VeriYonetimi veriYonetimi = new VeriYonetimi();

    // hepsinin textlerini deg�st�r�ceg�z �ng�l�zce yada t�rk�eye gore. sahnedek� textler� al�yorum bu arada onlar�n �sm� deg�scek ya
    public Text[] Texts;

    // sahne yukleme panel�m�n oldugu k�s�m
    public GameObject sahneYuklemeEkrani;
    public Slider sahneYuklemeSlider;

    ReklamYonetimi reklamYonetimi = new ReklamYonetimi();


    private void Awake()
    {
        //Destroy(MenuSes.Instance.gameObject);
        //  Destroy(GameObject.FindWithTag("MenuSes")); boylede yok edeb�l�rs�n
        KarakteriOzellestir();
        oyunSesi.volume = PlayerPrefs.GetFloat("OyunSes");
        oyunSesiSlider.value = bellekY�netimi.VeriyiGetir_float("OyunSes");
    }

    void Start()
    {
        currentCharacterCount = 1;
        DusmaniOlustur();
        // aktif sahney� almam laz�m cunku orneg�n kullan�c� 5 level�n k�l�d�n� act�. 3.level� gecm�st� b�r daha oynamak �sted�
        // o zaman o ank� akt�f sahney� al�cam k� 3.level� b�t�rd�g�nde de 6.level�n yan� son level�n k�l�d� ac�lmas�n
        // sadece son leveldeyse onu gecerse son level�n k�l�d� ac�ls�n
        scene = SceneManager.GetActiveScene();

        veriYonetimi.DilLoad();
        // sonra doldurulan l�stey� cag�r�p bu l�stey�de onun sayes�nde dolduruyoruz
        DilBilgileri = veriYonetimi.Dili_ListeyeAktar();
        // bu sayede 4.nc� �ndekse suan level �le alakal� b�lg�ler, d�rekt kullanacag�m l�steye geld� rahat�m
        kullan�lacakDilBilgileri.Add(DilBilgileri[5]);

        DilTercihiYonetimi();

        // oyun baslat�ld�g�nda reklam�m� googledan �st�yorum.reklam�m haz�rlan�yo
        reklamYonetimi.RequestInterstitial();
        reklamYonetimi.RequestRewardedAd();

    }


public void DilTercihiYonetimi()
    {
        if (PlayerPrefs.GetString("Dil") == "TR")
        {
            for (int i = 0; i < Texts.Length; i++)
            {
                Texts[i].text = kullan�lacakDilBilgileri[0].dilBilgileriTR[i].Text;
            }
        }
        else
        {
            for (int i = 0; i < Texts.Length; i++)
            {
                Texts[i].text = kullan�lacakDilBilgileri[0].dilBilgileriEN[i].Text;
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


    public void ��lemler(string i�lem, int say�, Transform i�lemPozisyonu)
    {
        switch (i�lem)
        {
            case "Toplama":
                islemler.Toplama(say�, karakterHavuzu, i�lemPozisyonu, olusmaEfektleri);
                Debug.Log("Toplama :" + currentCharacterCount);
                break;

            case "��karma":
                islemler.��karma(say�, karakterHavuzu, yokOlmaEfektleri);
                Debug.Log("��karma :" + currentCharacterCount);
                break;

            case "�arpma":
                islemler.�arpma(say�, karakterHavuzu, i�lemPozisyonu, olusmaEfektleri);
                Debug.Log("�arpma :" + currentCharacterCount);
                break;

            case "B�lme":
                islemler.B�lme(say�, karakterHavuzu, yokOlmaEfektleri);
                Debug.Log("B�lme :" + currentCharacterCount);
                break;
        }
    }

    public void YokOlmaEfektiOlu�tur(Vector3 i�lemPozisyonu, bool isAdamLekesi = false, bool azalmaDurumu = false)
    {
        foreach (var item in yokOlmaEfektleri)
        {
            if (!item.activeInHierarchy)
            {
                item.SetActive(true);
                item.transform.position = i�lemPozisyonu;
                item.GetComponent<ParticleSystem>().Play();
                item.GetComponent<AudioSource>().Play();
                // azalmaDurumu alt karakter� m� dusman� m� azalt�cag�z onun �c�n var
                if (!azalmaDurumu)
                    currentCharacterCount--;
                else
                    kacDusmanOlsun--;
                break;
            }
        }

        if (isAdamLekesi == true)
        {
            Vector3 i�lemPozisyonu2 = new Vector3(i�lemPozisyonu.x, .0005f, i�lemPozisyonu.z);
            foreach (var item2 in adamLekeleri)
            {
                if (!item2.activeInHierarchy)
                {
                    item2.SetActive(true);
                    item2.transform.position = i�lemPozisyonu2;
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
                    // sona gel�nce reklam� goster
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
                        // eger son leveldeysem art�r gardas deg�lse n�e level� art�rs�n
                        if (scene.buildIndex == bellekY�netimi.VeriyiGetir_int("SonLevel"))
                        {
                            bellekY�netimi.VeriyiKaydet_int("SonLevel", bellekY�netimi.VeriyiGetir_int("SonLevel") + 1);
                            bellekY�netimi.VeriyiKaydet_int("Puan", bellekY�netimi.VeriyiGetir_int("Puan") + 600);
                        }
                    }

                    else
                    {
                        // eger son leveldeysem art�r gardas deg�lse n�e level� art�rs�n
                        if (scene.buildIndex == bellekY�netimi.VeriyiGetir_int("SonLevel"))
                        {
                            bellekY�netimi.VeriyiKaydet_int("SonLevel", bellekY�netimi.VeriyiGetir_int("SonLevel") + 1);
                            bellekY�netimi.VeriyiKaydet_int("Puan", bellekY�netimi.VeriyiGetir_int("Puan") + 100);
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
        // -1 olursa karakter�n b� ozellest�rmes� olmaz akt�f olan varsa kapat�l�r sapka sopa falan
        if (bellekY�netimi.VeriyiGetir_int("AktifSapka") == -1)
        {
            foreach (var item in sapkalar)
            {
                item.gameObject.SetActive(false);
            }
        }
        else
        {
            sapkalar[bellekY�netimi.VeriyiGetir_int("AktifSapka")].SetActive(true);
        }

        // -1 olursa karakter�n b� ozellest�rmes� olmaz akt�f olan varsa kapat�l�r sapka sopa falan
        if (bellekY�netimi.VeriyiGetir_int("AktifSopa") == -1)
        {
            foreach (var item in sopalar)
            {
                item.gameObject.SetActive(false);
            }
        }
        else
        {
            sopalar[bellekY�netimi.VeriyiGetir_int("AktifSopalar")].SetActive(true);
        }

        Material[] mats = rendererKarakter.materials;
        mats[0] = materialKarakter[bellekY�netimi.VeriyiGetir_int("AktifKarakter")];
        rendererKarakter.materials = mats;

    }

    // UI Menu K�s�mlar�

    // butonlara bas�nca butonlara gore panel a�ar
    public void PaneliA�(string panelAdi)
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

    // slider�n tet�kleneceg� fonks�yon ayarlar panel� ac�l�nca
    public void OyunSesiAyar�()
    {
        bellekY�netimi.VeriyiKaydet_float("OyunSes", oyunSesiSlider.value);
        oyunSesi.volume = oyunSesiSlider.value;
    }

    // C�k�� butonuna bas�nca ac�l�cak panel
    public void ��k��Ekrani(string durum)
    {
        Time.timeScale = 1f;
        if (durum == "Devam")
        {
            islemPanelleri[1].SetActive(false);
        }
           
        else if(durum=="Tekrar")
        {
            Time.timeScale = 1f;
            // o ank� akt�f sahney� tekrar yukluyo
            SceneManager.LoadScene(scene.buildIndex);
        }
        else
        {
            SceneManager.LoadScene(0);
        }
    }

    public void OyunEkran�n�Kapat()
    {
        islemPanelleri[0].SetActive(false);
        Time.timeScale = 1f;
    }

    public void SonrakiLeveliA�()
    {
        StartCoroutine(LoadAsync(scene.buildIndex+1));
    }

    // sahne yukleme ekran� �c�n kullan�lan fonks�yon sadece level a�arken c�kart�yorum.
    IEnumerator LoadAsync(int index)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(index);

        sahneYuklemeEkrani.SetActive(true);

        // deger 1 olursa buraya g�rmez hal�yle ve 0.9da kal�r sl�der
        while (!operation.isDone)
        {
            // kalmamas� �c�n bu matematksel �slem� yap�yoruz bu �slem deger 0 ise 0a 0.45 ise 0.5e 0.9 ise 1e yuvarlar
            // burdada 0.9a bolmem�z�n neden� deger 0.9da kal�yor ve ben onu 0.9a bolersem sonc 1 olur ve slider tamamlanm�� tam yuklenm�s olur
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











