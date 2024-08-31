using System.Collections;
using System.Collections.Generic;
using Semih;
using TMPro;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Ayarlar_Manager : MonoBehaviour
{
    public AudioSource ses;
    // ayarlar menusundek� 3 slider i�in bunlar
    public Slider[] sliders;

    BellekY�netimi bellekY�netimi = new BellekY�netimi();

    // buda kullancag�m�z dil bilgisi i�ini �slemler kutuphanes�ndek� Veri yonet�m� s�n�f�n�n Dili_ListeyeAktar fonks�yonundan dolduruyoruz
    List<Diller> DilBilgileri = new List<Diller>();

    //bu ise DilBilgileri listesinden sahnemde kullanacag�m b�lg�ler� al�p bu l�steye yerlest�rmek �c�n kulland�g�m l�ste
    // bu sayede �ndex�m hep 0 olucak ve �ndexlerle ugrasm�ycam
    public List<Diller> kullan�lacakDilBilgileri = new List<Diller>();

    // hepsinin textlerini deg�st�r�ceg�z �ng�l�zce yada t�rk�eye gore. sahnedek� textler� al�yorum bu arada onlar�n �sm� deg�scek ya
    public TextMeshProUGUI[] Texts;

    // dosya �le alakal� seyler var burda
    VeriYonetimi veriYonetimi = new VeriYonetimi();

    // dil se�im k�sm�ndak� text varya turkce �ng�l�zce durumunu kontrol eden en altta bu o
    public TextMeshProUGUI dilDurumu;

    private void Start()
    {
        // bunu yapma neden�m sesler� deg�st�r�p kaydett�kten sonra asag�dak� fonks�yonda tekrar buraya g�rerse kayded�len valuey� gorsun
        sliders[0].value = bellekY�netimi.VeriyiGetir_float("MenuSes");
        sliders[1].value = bellekY�netimi.VeriyiGetir_float("MenuFx");
        sliders[2].value= bellekY�netimi.VeriyiGetir_float("OyunSes");

        ses.volume = bellekY�netimi.VeriyiGetir_float("MenuFx");

        // once dosyaya kayded�len b�lg�ler� dosyadan al�p orada olusturdugumuz l�steler�n �c�ne at�yoruz.
        veriYonetimi.DilLoad();
        // sonra doldurulan l�stey� cag�r�p bu l�stey�de onun sayes�nde dolduruyoruz
        DilBilgileri = veriYonetimi.Dili_ListeyeAktar();
        // bu sayede 4.nc� �ndekse suan ayarlar �le alakal� b�lg�ler, d�rekt kullanacag�m l�steye geld� rahat�m
        kullan�lacakDilBilgileri.Add(DilBilgileri[4]);

        DilTercihiYonetimi();
    }

    public void DilTercihiYonetimi()
    {
        if (PlayerPrefs.GetString("Dil") == "TR")
        {
            for (int i = 0; i < Texts.Length; i++)
            {
                Texts[i].text = kullan�lacakDilBilgileri[0].dilBilgileriTR[i].Text;
            }
            dilDurumu.text = "T�rk�e";
        }
        else
        {
            for (int i = 0; i < Texts.Length; i++)
            {
                Texts[i].text = kullan�lacakDilBilgileri[0].dilBilgileriEN[i].Text;
            }
            dilDurumu.text = "English";
        }
    }

    public void AnaMenuyeDon()
    {
        // sahne yukleme un�tyde asenkron b�r surec oldugu �c�n bazen buttonSes'in b�tmes�n� beklemeden sahney� acab�l�r
        //ondan boyle yapt�m
        StartCoroutine(LoadSceneAfterSound());
    }

    private IEnumerator LoadSceneAfterSound()
    {
        ses.Play();
        yield return new WaitForSeconds(ses.clip.length); // Sesin s�resi kadar bekle
        SceneManager.LoadScene(0); // Sahne y�kle
    }

    public void DilDegistir()
    {
        ses.Play();
        if (bellekY�netimi.VeriyiGetir_string("Dil")=="TR")
        {
            bellekY�netimi.VeriyiKaydet_string("Dil", "EN");
            DilTercihiYonetimi();
            dilDurumu.text = "English";
        }
        else
        {
            bellekY�netimi.VeriyiKaydet_string("Dil", "TR");
            DilTercihiYonetimi();
            dilDurumu.text = "T�rk�e";
        }
    }

    // sliderda deg�s�kl�k oldugunda otomat�k tet�klen�cek fonks�yon bu.
    public void SesAyarlaveKaydet(int index)
    {
        switch (index)
        {
            case 0:
                bellekY�netimi.VeriyiKaydet_float("MenuSes", sliders[0].value);
                break;
            case 1:
                bellekY�netimi.VeriyiKaydet_float("MenuFx", sliders[1].value);
                break;
            case 2:
                bellekY�netimi.VeriyiKaydet_float("OyunSes", sliders[2].value);
                break;
        }
    }

    

}
