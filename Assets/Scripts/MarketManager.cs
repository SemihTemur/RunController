using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Semih;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MarketManager : MonoBehaviour
{
    VeriYonetimi veriYonetimi = new VeriYonetimi();

    // buda kullancagýmýz dil bilgisi içini Ýslemler kutuphanesýndeký Veri yonetýmý sýnýfýnýn Dili_ListeyeAktar fonksýyonundan dolduruyoruz
    List<Diller> DilBilgileri = new List<Diller>();

    //bu ise DilBilgileri listesinden sahnemde kullanacagým býlgýlerý alýp bu lýsteye yerlestýrmek ýcýn kullandýgým lýste
    // bu sayede ýndexým hep 0 olucak ve ýndexlerle ugrasmýycam
    public List<Diller> kullanýlacakDilBilgileri = new List<Diller>();

    public Text text;

    public AudioSource buttonSes;

    private void Awake()
    {
        buttonSes.volume = PlayerPrefs.GetFloat("MenuFx");
    }

    void Start()
    {
        // once dosyaya kaydedýlen býlgýlerý dosyadan alýp orada olusturdugumuz lýstelerýn ýcýne atýyoruz.
        veriYonetimi.DilLoad();
        // sonra doldurulan lýsteyý cagýrýp bu lýsteyýde onun sayesýnde dolduruyoruz
        DilBilgileri = veriYonetimi.Dili_ListeyeAktar();
        // bu sayede 0.ncý ýndekse suan anamenu ýle alakalý býlgýler dýrekt kullanacagým lýsteye geldý rahatým
        kullanýlacakDilBilgileri.Add(DilBilgileri[3]);

        DilTercihiYonetimi();
    }

    public void DilTercihiYonetimi()
    {
        if (PlayerPrefs.GetString("Dil") == "TR")
            text.text = kullanýlacakDilBilgileri[0].dilBilgileriTR[0].Text;

        else
            text.text = kullanýlacakDilBilgileri[0].dilBilgileriEN[0].Text;
    }

    public void AnaMenuyeDon()
    {
        // sahne yukleme unýtyde asenkron býr surec oldugu ýcýn bazen buttonSes'in býtmesýný beklemeden sahneyý acabýlýr
        //ondan boyle yaptým
        StartCoroutine(LoadSceneAfterSound());
    }

    private IEnumerator LoadSceneAfterSound()
    {
        buttonSes.Play(); 
        yield return new WaitForSeconds(buttonSes.clip.length); // Sesin süresi kadar bekle
        SceneManager.LoadScene(0); // Sahne yükle
    }

}
