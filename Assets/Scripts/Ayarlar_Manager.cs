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
    // ayarlar menusundeký 3 slider için bunlar
    public Slider[] sliders;

    BellekYönetimi bellekYönetimi = new BellekYönetimi();

    // buda kullancagýmýz dil bilgisi içini Ýslemler kutuphanesýndeký Veri yonetýmý sýnýfýnýn Dili_ListeyeAktar fonksýyonundan dolduruyoruz
    List<Diller> DilBilgileri = new List<Diller>();

    //bu ise DilBilgileri listesinden sahnemde kullanacagým býlgýlerý alýp bu lýsteye yerlestýrmek ýcýn kullandýgým lýste
    // bu sayede ýndexým hep 0 olucak ve ýndexlerle ugrasmýycam
    public List<Diller> kullanýlacakDilBilgileri = new List<Diller>();

    // hepsinin textlerini degýstýrýcegýz ýngýlýzce yada türkçeye gore. sahnedeký textlerý alýyorum bu arada onlarýn ýsmý degýscek ya
    public TextMeshProUGUI[] Texts;

    // dosya ýle alakalý seyler var burda
    VeriYonetimi veriYonetimi = new VeriYonetimi();

    // dil seçim kýsmýndaký text varya turkce ýngýlýzce durumunu kontrol eden en altta bu o
    public TextMeshProUGUI dilDurumu;

    private void Start()
    {
        // bunu yapma nedeným seslerý degýstýrýp kaydettýkten sonra asagýdaký fonksýyonda tekrar buraya gýrerse kaydedýlen valueyý gorsun
        sliders[0].value = bellekYönetimi.VeriyiGetir_float("MenuSes");
        sliders[1].value = bellekYönetimi.VeriyiGetir_float("MenuFx");
        sliders[2].value= bellekYönetimi.VeriyiGetir_float("OyunSes");

        ses.volume = bellekYönetimi.VeriyiGetir_float("MenuFx");

        // once dosyaya kaydedýlen býlgýlerý dosyadan alýp orada olusturdugumuz lýstelerýn ýcýne atýyoruz.
        veriYonetimi.DilLoad();
        // sonra doldurulan lýsteyý cagýrýp bu lýsteyýde onun sayesýnde dolduruyoruz
        DilBilgileri = veriYonetimi.Dili_ListeyeAktar();
        // bu sayede 4.ncý ýndekse suan ayarlar ýle alakalý býlgýler, dýrekt kullanacagým lýsteye geldý rahatým
        kullanýlacakDilBilgileri.Add(DilBilgileri[4]);

        DilTercihiYonetimi();
    }

    public void DilTercihiYonetimi()
    {
        if (PlayerPrefs.GetString("Dil") == "TR")
        {
            for (int i = 0; i < Texts.Length; i++)
            {
                Texts[i].text = kullanýlacakDilBilgileri[0].dilBilgileriTR[i].Text;
            }
            dilDurumu.text = "Türkçe";
        }
        else
        {
            for (int i = 0; i < Texts.Length; i++)
            {
                Texts[i].text = kullanýlacakDilBilgileri[0].dilBilgileriEN[i].Text;
            }
            dilDurumu.text = "English";
        }
    }

    public void AnaMenuyeDon()
    {
        // sahne yukleme unýtyde asenkron býr surec oldugu ýcýn bazen buttonSes'in býtmesýný beklemeden sahneyý acabýlýr
        //ondan boyle yaptým
        StartCoroutine(LoadSceneAfterSound());
    }

    private IEnumerator LoadSceneAfterSound()
    {
        ses.Play();
        yield return new WaitForSeconds(ses.clip.length); // Sesin süresi kadar bekle
        SceneManager.LoadScene(0); // Sahne yükle
    }

    public void DilDegistir()
    {
        ses.Play();
        if (bellekYönetimi.VeriyiGetir_string("Dil")=="TR")
        {
            bellekYönetimi.VeriyiKaydet_string("Dil", "EN");
            DilTercihiYonetimi();
            dilDurumu.text = "English";
        }
        else
        {
            bellekYönetimi.VeriyiKaydet_string("Dil", "TR");
            DilTercihiYonetimi();
            dilDurumu.text = "Türkçe";
        }
    }

    // sliderda degýsýklýk oldugunda otomatýk tetýklenýcek fonksýyon bu.
    public void SesAyarlaveKaydet(int index)
    {
        switch (index)
        {
            case 0:
                bellekYönetimi.VeriyiKaydet_float("MenuSes", sliders[0].value);
                break;
            case 1:
                bellekYönetimi.VeriyiKaydet_float("MenuFx", sliders[1].value);
                break;
            case 2:
                bellekYönetimi.VeriyiKaydet_float("OyunSes", sliders[2].value);
                break;
        }
    }

    

}
