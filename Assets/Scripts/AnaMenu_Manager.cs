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
    // uyar� mesaj� �c�n
    public GameObject cikisPaneli;

    BellekY�netimi bellekYonetimi = new BellekY�netimi();

    VeriYonetimi veriYonetimi = new VeriYonetimi();

    [Header("Default De�erler")]
    // bunlar ad� �st�nde varsay�lan de�erler. dosyay� oyuna �lk kez g�ren�n olu�turaca�� �cer�s�nde varsay�lan b�lg�ler�n oldugu �eyler
    public List<ItemBilgileri> varsay�lan�temBilgileri = new List<ItemBilgileri>();
    public List<Diller> varsay�lanDilBilgileri = new List<Diller>();

    public AudioSource buttonSes;

    // buda kullancag�m�z dil bilgisi i�ini �slemler kutuphanes�ndek� Veri yonet�m� s�n�f�n�n Dili_ListeyeAktar fonks�yonundan dolduruyoruz
    List<Diller> DilBilgileri = new List<Diller>();

    //bu ise DilBilgileri listesinden sahnemde kullanacag�m b�lg�ler� al�p bu l�steye yerlest�rmek �c�n kulland�g�m l�ste
    // bu sayede �ndex�m hep 0 olucak ve �ndexlerle ugrasm�ycam
    public List<Diller> kullan�lacakDilBilgileri = new List<Diller>();

    // hepsinin textlerini deg�st�r�ceg�z �ng�l�zce yada t�rk�eye gore. sahnedek� textler� al�yorum bu arada onlar�n �sm� deg�scek ya
    public Text[] Texts;

    // sahne yukleme panel�m�n oldugu k�s�m
    public GameObject sahneYuklemeEkrani;
    public Slider sahneYuklemeSlider;

    //ReklamYonetimi reklamYonetimi = new ReklamYonetimi();
    void Start()
    {
        // Oyuna ilk defa girecek olan bir kullaniciya ac�l�cak �lk pencere buras� oldugu �c�n eger h�c oynamam�ssa SonLevel'In bos olmamas�
        //  i�in burda kontrol ed�yoruz �c� bos ise o ve puanlama s�stem�n�n deger�n� g�r�yoruz
        bellekYonetimi.KontrolEtveTan�mla();

        //Oyuna g�r�cek b�r�s�ne ac�lan ekran buras� oldugu �c�n burada dosya olusturma �slem�n� yap�yorum b�r kerel�k
        veriYonetimi.ilkKurulumDosyaOlusturma(varsay�lan�temBilgileri, varsay�lanDilBilgileri);

        // daha once kayded�len ses s�ddet� varsa onu al�yo
        buttonSes.volume = PlayerPrefs.GetFloat("MenuFx");
        // kullan�c� taraf�ndan se�ilen d�le gore textler� deg�st�r�cek

        // once dosyaya kayded�len b�lg�ler� dosyadan al�p orada olusturdugumuz l�steler�n �c�ne at�yoruz.
        veriYonetimi.DilLoad();
        // sonra doldurulan l�stey� cag�r�p bu l�stey�de onun sayes�nde dolduruyoruz
        DilBilgileri = veriYonetimi.Dili_ListeyeAktar();
        // bu sayede 0.nc� �ndekse suan anamenu �le alakal� b�lg�ler d�rekt kullanacag�m l�steye geld� rahat�m
        kullan�lacakDilBilgileri.Add(DilBilgileri[0]);

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
                Texts[i].text = kullan�lacakDilBilgileri[0].dilBilgileriTR[i].Text;
            }
        }
        else
        {
            for (i = 0; i < Texts.Length; i++)
            {
                Texts[i].text = kullan�lacakDilBilgileri[0].dilBilgileriEN[i].Text;
            }
        }
    }

    public void SahneyiYukle(int index)
    {
        StartCoroutine(LoadSceneAfterSound(index));
    }

    private IEnumerator LoadSceneAfterSound(int sahne�ndex)
    {
        buttonSes.Play();
        yield return new WaitForSeconds(buttonSes.clip.length);
        SceneManager.LoadScene(sahne�ndex); // Sahne y�kle
    }

    public void Oyna()
    {
        buttonSes.Play();
        StartCoroutine(LoadAsync(bellekYonetimi.VeriyiGetir_int("SonLevel")));
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

    public void ��k��(string durum)
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
