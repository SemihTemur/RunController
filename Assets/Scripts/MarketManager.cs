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

    // buda kullancag�m�z dil bilgisi i�ini �slemler kutuphanes�ndek� Veri yonet�m� s�n�f�n�n Dili_ListeyeAktar fonks�yonundan dolduruyoruz
    List<Diller> DilBilgileri = new List<Diller>();

    //bu ise DilBilgileri listesinden sahnemde kullanacag�m b�lg�ler� al�p bu l�steye yerlest�rmek �c�n kulland�g�m l�ste
    // bu sayede �ndex�m hep 0 olucak ve �ndexlerle ugrasm�ycam
    public List<Diller> kullan�lacakDilBilgileri = new List<Diller>();

    public Text text;

    public AudioSource buttonSes;

    private void Awake()
    {
        buttonSes.volume = PlayerPrefs.GetFloat("MenuFx");
    }

    void Start()
    {
        // once dosyaya kayded�len b�lg�ler� dosyadan al�p orada olusturdugumuz l�steler�n �c�ne at�yoruz.
        veriYonetimi.DilLoad();
        // sonra doldurulan l�stey� cag�r�p bu l�stey�de onun sayes�nde dolduruyoruz
        DilBilgileri = veriYonetimi.Dili_ListeyeAktar();
        // bu sayede 0.nc� �ndekse suan anamenu �le alakal� b�lg�ler d�rekt kullanacag�m l�steye geld� rahat�m
        kullan�lacakDilBilgileri.Add(DilBilgileri[3]);

        DilTercihiYonetimi();
    }

    public void DilTercihiYonetimi()
    {
        if (PlayerPrefs.GetString("Dil") == "TR")
            text.text = kullan�lacakDilBilgileri[0].dilBilgileriTR[0].Text;

        else
            text.text = kullan�lacakDilBilgileri[0].dilBilgileriEN[0].Text;
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
