using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Semih;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AnaMenu_Manager : MonoBehaviour
{
    // uyarý mesajý ýcýn
    public GameObject cikisPaneli;

    BellekYönetimi bellekYonetimi = new BellekYönetimi();

    VeriYonetimi veriYonetimi = new VeriYonetimi();

    [Header("Default Deðerler")]
    // bunlar adý üstünde varsayýlan deðerler. dosyayý oyuna ýlk kez gýrenýn oluþturacaðý ýcerýsýnde varsayýlan býlgýlerýn oldugu þeyler
    public List<ItemBilgileri> varsayýlanÝtemBilgileri = new List<ItemBilgileri>();
    public List<Diller> varsayýlanDilBilgileri = new List<Diller>();

    public AudioSource buttonSes;

    // buda kullancagýmýz dil bilgisi içini Ýslemler kutuphanesýndeký Veri yonetýmý sýnýfýnýn Dili_ListeyeAktar fonksýyonundan dolduruyoruz
    List<Diller> DilBilgileri = new List<Diller>();

    //bu ise DilBilgileri listesinden sahnemde kullanacagým býlgýlerý alýp bu lýsteye yerlestýrmek ýcýn kullandýgým lýste
    // bu sayede ýndexým hep 0 olucak ve ýndexlerle ugrasmýycam
    public List<Diller> kullanýlacakDilBilgileri = new List<Diller>();

    // hepsinin textlerini degýstýrýcegýz ýngýlýzce yada türkçeye gore. sahnedeký textlerý alýyorum bu arada onlarýn ýsmý degýscek ya
    public Text[] Texts;

    // sahne yukleme panelýmýn oldugu kýsým
    public GameObject sahneYuklemeEkrani;
    public Slider sahneYuklemeSlider;

    //ReklamYonetimi reklamYonetimi = new ReklamYonetimi();
    void Start()
    {
        // Oyuna ilk defa girecek olan bir kullaniciya acýlýcak ýlk pencere burasý oldugu ýcýn eger hýc oynamamýssa SonLevel'In bos olmamasý
        //  için burda kontrol edýyoruz ýcý bos ise o ve puanlama sýstemýnýn degerýný gýrýyoruz
        bellekYonetimi.KontrolEtveTanýmla();

        //Oyuna gýrýcek býrýsýne acýlan ekran burasý oldugu ýcýn burada dosya olusturma ýslemýný yapýyorum býr kerelýk
        veriYonetimi.ilkKurulumDosyaOlusturma(varsayýlanÝtemBilgileri, varsayýlanDilBilgileri);

        // daha once kaydedýlen ses sýddetý varsa onu alýyo
        buttonSes.volume = PlayerPrefs.GetFloat("MenuFx");
        // kullanýcý tarafýndan seçilen dýle gore textlerý degýstýrýcek

        // once dosyaya kaydedýlen býlgýlerý dosyadan alýp orada olusturdugumuz lýstelerýn ýcýne atýyoruz.
        veriYonetimi.DilLoad();
        // sonra doldurulan lýsteyý cagýrýp bu lýsteyýde onun sayesýnde dolduruyoruz
        DilBilgileri = veriYonetimi.Dili_ListeyeAktar();
        // bu sayede 0.ncý ýndekse suan anamenu ýle alakalý býlgýler dýrekt kullanacagým lýsteye geldý rahatým
        kullanýlacakDilBilgileri.Add(DilBilgileri[0]);

        DilTercihiYonetimi();

        //reklamYonetimi.RequestRewardedAd();
        //reklamYonetimi.OdulluReklamiGoster();


    }

    public void DilTercihiYonetimi()
    {
        int i;
        if (PlayerPrefs.GetString("Dil") == "TR")
        {
            for (i=0;i<Texts.Length;i++)
            {
                Texts[i].text = kullanýlacakDilBilgileri[0].dilBilgileriTR[i].Text;
            }
        }
        else
        {
            for (i = 0; i < Texts.Length; i++)
            {
                Texts[i].text = kullanýlacakDilBilgileri[0].dilBilgileriEN[i].Text;
            }
        }
    }

    public void SahneyiYukle(int index)
    {
        StartCoroutine(LoadSceneAfterSound(index));
    }

    private IEnumerator LoadSceneAfterSound(int sahneÝndex)
    {
        buttonSes.Play();
        yield return new WaitForSeconds(buttonSes.clip.length);
        SceneManager.LoadScene(sahneÝndex); // Sahne yükle
    }

    public void Oyna()
    {
        buttonSes.Play();
        StartCoroutine(LoadAsync(bellekYonetimi.VeriyiGetir_int("SonLevel")));
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

    public void Çýkýþ(string durum)
    {
        buttonSes.Play();
        if (durum == "Cikis")
            cikisPaneli.SetActive(true);
        else if (durum == "Evet")
            Application.Quit();
        else
            cikisPaneli.SetActive(false);
    }

}
