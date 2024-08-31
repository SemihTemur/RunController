using System.Collections;
using System.Collections.Generic;
using Semih;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Level_Manager : MonoBehaviour
{
    // playerprefs �le alakal� seyler var burda
    BellekY�netimi bellekYonetimi = new BellekY�netimi();
    // dosya �le alakal� seyler var burda
    VeriYonetimi veriYonetimi = new VeriYonetimi();

    public Button[] buttons;
    public Sprite sprite;

    int Index;

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

        // once dosyaya kayded�len b�lg�ler� dosyadan al�p orada olusturdugumuz l�steler�n �c�ne at�yoruz.
        veriYonetimi.DilLoad();
        // sonra doldurulan l�stey� cag�r�p bu l�stey�de onun sayes�nde dolduruyoruz
        DilBilgileri = veriYonetimi.Dili_ListeyeAktar();
        // bu sayede 2.nc� �ndekse suan level manager �le alakal� b�lg�ler, d�rekt kullanacag�m l�steye geld� rahat�m
        kullan�lacakDilBilgileri.Add(DilBilgileri[2]);

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
                Texts[i].text = kullan�lacakDilBilgileri[0].dilBilgileriTR[i].Text;
            }
        }
        else
        {
            for ( i = 0; i < Texts.Length; i++)
            {
                Texts[i].text = kullan�lacakDilBilgileri[0].dilBilgileriEN[i].Text;
            }
        }
    }

    public void SahneyiYukle(int sahne�ndexi)
     {
        buttonSes.Play();
        StartCoroutine(LoadAsync(sahne�ndexi));
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

    public void AnaMenuyeDon()
    {
        // sahne yukleme un�tyde asenkron b�r surec oldugu �c�n bazen buttonSes'in b�tmes�n� beklemeden sahney� acab�l�r
        //ondan boyle yapt�m
        StartCoroutine(LoadSceneAfterSound());
    }

    private IEnumerator LoadSceneAfterSound()
    {
        buttonSes.Play(); 
        yield return new WaitForSeconds(buttonSes.clip.length); // Sesin s�resi kadar bekle
        SceneManager.LoadScene(0); // Sahne y�kle
    }

}
