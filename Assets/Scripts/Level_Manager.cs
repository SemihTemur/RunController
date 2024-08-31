using System.Collections;
using System.Collections.Generic;
using Semih;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Level_Manager : MonoBehaviour
{
    // playerprefs ýle alakalý seyler var burda
    BellekYönetimi bellekYonetimi = new BellekYönetimi();
    // dosya ýle alakalý seyler var burda
    VeriYonetimi veriYonetimi = new VeriYonetimi();

    public Button[] buttons;
    public Sprite sprite;

    int Index;

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

    public void Start()
    {
        buttonSes.volume = PlayerPrefs.GetFloat("MenuFx");
        int meevcutLevel = bellekYonetimi.VeriyiGetir_int("SonLevel")-4;
        Index = 1;

        for (int i = 0; i < buttons.Length; i++)
        {
            if (Index <= meevcutLevel)
            {
                buttons[i].GetComponentInChildren<Text>().text = (i + 1).ToString();
                int sahneyiYukle = Index + 4;
                buttons[i].onClick.AddListener( delegate{SahneyiYukle(sahneyiYukle); });
            }
            else
            {
                buttons[i].GetComponent<Image>().sprite = sprite;
                buttons[i].enabled = false;
            }
            Index++;
        }

        // once dosyaya kaydedýlen býlgýlerý dosyadan alýp orada olusturdugumuz lýstelerýn ýcýne atýyoruz.
        veriYonetimi.DilLoad();
        // sonra doldurulan lýsteyý cagýrýp bu lýsteyýde onun sayesýnde dolduruyoruz
        DilBilgileri = veriYonetimi.Dili_ListeyeAktar();
        // bu sayede 2.ncý ýndekse suan level manager ýle alakalý býlgýler, dýrekt kullanacagým lýsteye geldý rahatým
        kullanýlacakDilBilgileri.Add(DilBilgileri[2]);

        //PlayerPrefs.SetString("Dil", "TR");

        DilTercihiYonetimi();

    }

    public void DilTercihiYonetimi()
    {
        int i;
        if (PlayerPrefs.GetString("Dil") == "TR")
        {
            for (i = 0; i < Texts.Length; i++)
            {
                Texts[i].text = kullanýlacakDilBilgileri[0].dilBilgileriTR[i].Text;
            }
        }
        else
        {
            for ( i = 0; i < Texts.Length; i++)
            {
                Texts[i].text = kullanýlacakDilBilgileri[0].dilBilgileriEN[i].Text;
            }
        }
    }

    public void SahneyiYukle(int sahneÝndexi)
     {
        buttonSes.Play();
        StartCoroutine(LoadAsync(sahneÝndexi));
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
