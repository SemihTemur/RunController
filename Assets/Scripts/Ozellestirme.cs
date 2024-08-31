using System;
using System.Collections;
using System.Collections.Generic;
using Semih;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Ozellestirme : MonoBehaviour
{
    public Text puanText;
    public GameObject islemCanvas;
    public GameObject[] islemPanelleri;
    public GameObject[] genelPaneller;
    public Button[] islemButtonlar;

    public Text satinAlmaText;
    public Text kaydetmeText;

    [Header("SAPKALAR")]
    public GameObject[] sapkalar;
    public Text sapkaDurumText;

    [Header("SOPALAR")]
    public GameObject[] sopalar;
    public Text sopaDurumText;

    [Header("KARAKTERLER")]
    public Material[] materialKarakter;
    public Text karakterDurumText;
    public SkinnedMeshRenderer rendererKarakter;

    BellekYönetimi bellekYönetimi = new BellekYönetimi();
    VeriYonetimi veriYonetimi = new VeriYonetimi();

    int sapkaIndex=-1;
    int sopaIndex=-1;
    int karakterIndex=0;

    int aktifÝslemPaneliÝndex;

    // burda býr audýo source olusturup sahnemde olusturmustum zaten onun audýo clýpýne farklý sesler verýcem butonlara gore satýn alma'nýn
    // sesý ayrý kaydetmenýn ayrý výdeodaký adam ise boyle yapmak yerýne AudýoSource'dan dizi olusturup 3 tane audýoSource olusturuyo
    // býr objenýn ýcýnde hepsýne farklý clýpler verýyo
    public AudioSource ses;
    public AudioClip[] sesler;

    [Header("Genel Veriler")]
    public List<ItemBilgileri> itemBilgileri = new List<ItemBilgileri>();

    // buda kullancagýmýz dil bilgisi içini Ýslemler kutuphanesýndeký Veri yonetýmý sýnýfýnýn Dili_ListeyeAktar fonksýyonundan dolduruyoruz
    List<Diller> DilBilgileri = new List<Diller>();

    //bu ise DilBilgileri listesinden sahnemde kullanacagým býlgýlerý alýp bu lýsteye yerlestýrmek ýcýn kullandýgým lýste
    // bu sayede ýndexým hep 0 olucak ve ýndexlerle ugrasmýycam
    public List<Diller> kullanýlacakDilBilgileri = new List<Diller>();

    // hepsinin textlerini degýstýrýcegýz ýngýlýzce yada türkçeye gore. sahnedeký textlerý alýyorum bu arada onlarýn ýsmý degýscek ya
    public Text[] Texts;

    // bunu tanýmlamamýn nedený aþþagýda scrýpt tarafýnda ben koda mudahele edýp satýn al demýsým ama ýngýlýzce olursa satýn al
    // dememem gerekýyo ondan duzenlemelýyým.
    private string satýnAlmaText;
    private string satýnAlindiText;

    // bunu tanýmlamamýn nedený aþþagýda scrýpt tarafýnda ben koda mudahele edýp kaydet demýsým ama ýngýlýzce olursa kaydet
    // dememem gerekýyo ondan duzenlemelýyým.
    private string kaydetmeDurumuText;
    private string kaydedilmeDurumuText;

    // bunu tanýmlamamýn nedený aþþagýda scrýpt tarafýnda ben koda mudahele edýp item yok demýsým ama ýngýlýzce olursa item yok
    // dememem gerekýyo ondan duzenlemelýyým.
    private string itemDurumText;

    void Start()
    {
        //bellekYönetimi.VeriyiKaydet_int("Puan", 1000);

        bellekYönetimi.VeriyiKaydet_int("Puan", 2450);
        puanText.text = bellekYönetimi.VeriyiGetir_int("Puan").ToString();

        // once dosyaya kaydedýlen býlgýlerý dosyadan alýp orada olusturdugumuz lýstelerýn ýcýne atýyoruz.
        veriYonetimi.Load();
        // sonra doldurulan lýsteyý cagýrýp bu lýsteyýde onun sayesýnde dolduruyoruz
        itemBilgileri = veriYonetimi.ListeyiAktar();

        // kaydetdýgýmýz seylerýn gýrdýgýmýzde gozukmesýný ýstedýgýmýz ýcýn yaptýk bunlarý bunlarý yazmasak starta sadece o 3 butona gýrýp
        // gerý cýktýgýmýzda gelýrdý ozellestýrmeler
        DurumuKontrolEt(0);
        DurumuKontrolEt(1);
        DurumuKontrolEt(2);

        ses.volume = PlayerPrefs.GetFloat("MenuFx");

        // once dosyaya kaydedýlen býlgýlerý dosyadan alýp orada olusturdugumuz lýstelerýn ýcýne atýyoruz.
        veriYonetimi.DilLoad();
        // sonra doldurulan lýsteyý cagýrýp bu lýsteyýde onun sayesýnde dolduruyoruz
        DilBilgileri = veriYonetimi.Dili_ListeyeAktar();
        // bu sayede 0.ncý ýndekse suan anamenu ýle alakalý býlgýler dýrekt kullanacagým lýsteye geldý rahatým
        kullanýlacakDilBilgileri.Add(DilBilgileri[1]);

        DilTercihiYonetimi();

    }

    public void DilTercihiYonetimi()
    {
        if (PlayerPrefs.GetString("Dil") == "TR")
        {
            for (int i = 0; i < Texts.Length; i++)
            {
                Debug.Log("selamlar");
                Texts[i].text = kullanýlacakDilBilgileri[0].dilBilgileriTR[i].Text;
            }
            satýnAlmaText = kullanýlacakDilBilgileri[0].dilBilgileriTR[5].Text;
            satýnAlindiText = "Satýn Alýndý";

            kaydetmeDurumuText = kullanýlacakDilBilgileri[0].dilBilgileriTR[6].Text;
            kaydedilmeDurumuText = "Kaydedildi";

            itemDurumText = kullanýlacakDilBilgileri[0].dilBilgileriTR[4].Text;
        }
        else
        {
            for (int i = 0; i < Texts.Length; i++)
            {
                Texts[i].text = kullanýlacakDilBilgileri[0].dilBilgileriEN[i].Text;
            }
            satýnAlmaText = kullanýlacakDilBilgileri[0].dilBilgileriEN[5].Text;
            satýnAlindiText = "Buyed";

            kaydetmeDurumuText = kullanýlacakDilBilgileri[0].dilBilgileriEN[6].Text;
            kaydedilmeDurumuText = "Saved";


            itemDurumText = kullanýlacakDilBilgileri[0].dilBilgileriEN[4].Text;
        }
    }

    // saga sola gýttýgýmýz yer var ya oyun ýlk acýldýgýnda cýkan ekranda karakter kýsmýna basýnca sag sol oklar var yukarýdaký metýnde
    // orasý bu
    public void Sapka_YonButonlari(string islem)
    {
        ses.clip = sesler[0];
        ses.Play();
        if (islem == "ileri")
        {
            // baþta ya da sona geldiyse ve sondan sonra -1 olucagý ýcýn býr sey kapatmasýna gerek olmadýgý ýcýn çunku -1de sapka yok
            if (sapkaIndex == -1)
            {
                sapkaIndex++;
                sapkalar[sapkaIndex].gameObject.SetActive(true);
                sapkaDurumText.text = itemBilgileri[sapkaIndex].itemAdi;

                //Bunu yapma nedeným secýlen objelerýn satýl alýnýp alýnmadýgýný kontrol etmek ýcýn
                if (!itemBilgileri[sapkaIndex].satildiAlmaDurumu)
                {
                    satinAlmaText.text = itemBilgileri[sapkaIndex].puan + "- "+ satýnAlmaText;
                    islemButtonlar[1].interactable = false;
                    // bunu yazmasam býr tane kaydetme textý oldugu ýcýn kaydedýlen býr sey olursa kaydedýlmeyen seyede kaydedýldý yazar
                    kaydetmeText.text = kaydetmeDurumuText;

                    // beným eðer puaným satýn alýcagým seyden kucuk ýse butonu ýnaktýf edýyorum
                    if (itemBilgileri[sapkaIndex].puan > bellekYönetimi.VeriyiGetir_int("Puan"))
                        islemButtonlar[0].interactable = false;

                    else
                        islemButtonlar[0].interactable = true;
                }
                else
                {
                    satinAlmaText.text = satýnAlindiText;
                    islemButtonlar[0].interactable = false;
                    // eðer kaydedilen obje ise kaydetme butonunun gorunurlugunu ýnaktýf et
                    if (sapkaIndex == bellekYönetimi.VeriyiGetir_int("AktifSapka"))
                    {
                        islemButtonlar[1].interactable = false;
                        kaydetmeText.text = kaydedilmeDurumuText;
                    }
                    else
                    {
                        islemButtonlar[1].interactable = true;
                        kaydetmeText.text = kaydetmeDurumuText;
                    }
                }
            }
            // son gelmiþ ise en baþa donmesini istiyorum
            else if (sapkaIndex == sapkalar.Length - 1)
            {
                sapkalar[sapkaIndex].gameObject.SetActive(false);
                sapkaIndex = -1;
                sapkaDurumText.text = itemDurumText;

                //Bunu yapma nedeným secýlen objelerýn satýl alýnýp alýnmadýgýný kontrol etmek ýcýn burda ýf else gerek yok cunku
                // -1e gelýrse default seyler gelýcek onlar zaten oyunun beles verdýgý seyler satýn alýnmaz ama kaydedýlýr
                satinAlmaText.text = satýnAlindiText;
                islemButtonlar[0].interactable = false;
                // eðer kaydedilen obje ise kaydetme butonunun gorunurlugunu ýnaktýf et
                if (sapkaIndex == bellekYönetimi.VeriyiGetir_int("AktifSapka"))
                {
                    islemButtonlar[1].interactable = false;
                    kaydetmeText.text = kaydedilmeDurumuText;
                }
                else
                {
                    islemButtonlar[1].interactable = true;
                    kaydetmeText.text = kaydetmeDurumuText;
                }
            }
            // ortalardaysa ornegýn 4 sapka varsa 2 deyse 3e gecmek ýcýn once 2 kapatýlýr sonra 3e gecýlýr.
            else
            {
                sapkalar[sapkaIndex].gameObject.SetActive(false);
                sapkaIndex++;
                sapkalar[sapkaIndex].gameObject.SetActive(true);
                sapkaDurumText.text = itemBilgileri[sapkaIndex].itemAdi;

                //Bunu yapma nedeným secýlen objelerýn satýl alýnýp alýnmadýgýný kontrol etmek ýcýn
                if (!itemBilgileri[sapkaIndex].satildiAlmaDurumu)
                {
                    satinAlmaText.text = itemBilgileri[sapkaIndex].puan + "- " + satýnAlmaText;
                    kaydetmeText.text = kaydetmeDurumuText;
                    islemButtonlar[1].interactable = false;
                    // beným eðer puaným satýn alýcagým seyden kucuk ýse butonu ýnaktýf edýyorum
                    if (itemBilgileri[sapkaIndex].puan > bellekYönetimi.VeriyiGetir_int("Puan"))
                        islemButtonlar[0].interactable = false;

                    else
                        islemButtonlar[0].interactable = true;
                }
                else
                {
                    satinAlmaText.text = satýnAlindiText;
                    islemButtonlar[0].interactable = false;
                    // eðer kaydedilen obje ise kaydetme butonunun gorunurlugunu ýnaktýf et
                    if (sapkaIndex == bellekYönetimi.VeriyiGetir_int("AktifSapka"))
                    {
                        islemButtonlar[1].interactable = false;
                        kaydetmeText.text = kaydedilmeDurumuText;
                    }
                    else
                    {
                        islemButtonlar[1].interactable = true;
                        kaydetmeText.text = kaydetmeDurumuText;
                    }
                }
            }
        }

        else if (islem == "geri")
        {
            // Baþtaysa ya da oyuna hiç girmemiþ ise
            if (sapkaIndex == -1)
            {
                sapkaIndex = sapkalar.Length - 1;
                sapkalar[sapkaIndex].gameObject.SetActive(true);
                sapkaDurumText.text = itemBilgileri[sapkaIndex].itemAdi;

                //Bunu yapma nedeným secýlen objelerýn satýl alýnýp alýnmadýgýný kontrol etmek ýcýn
                if (!itemBilgileri[sapkaIndex].satildiAlmaDurumu)
                {
                    satinAlmaText.text = itemBilgileri[sapkaIndex].puan + "- " + satýnAlmaText;
                    kaydetmeText.text = kaydetmeDurumuText;
                    islemButtonlar[1].interactable = false;
                    // beným eðer puaným satýn alýcagým seyden kucuk ýse butonu ýnaktýf edýyorum
                    if (itemBilgileri[sapkaIndex].puan > bellekYönetimi.VeriyiGetir_int("Puan"))
                        islemButtonlar[0].interactable = false;

                    else
                        islemButtonlar[0].interactable = true;
                }
                else
                {
                    satinAlmaText.text = satýnAlindiText;
                    islemButtonlar[0].interactable = false;
                    // eðer kaydedilen obje ise kaydetme butonunun gorunurlugunu ýnaktýf et
                    if (sapkaIndex == bellekYönetimi.VeriyiGetir_int("AktifSapka"))
                    {
                        islemButtonlar[1].interactable = false;
                        kaydetmeText.text = kaydedilmeDurumuText;
                    }
                    else
                    {
                        islemButtonlar[1].interactable = true;
                        kaydetmeText.text = kaydetmeDurumuText;
                    }
                }
            }
            // ortalardaysa ornegýn 4 sapka varsa 2 deyse 3e gecmek ýcýn once 2 kapatýlýr sonra 3e gecýlýr.
            else
            {
                sapkalar[sapkaIndex].gameObject.SetActive(false);
                sapkaIndex--;
                // mesela ýndex 1 se buraya gýrerse -1 olur ve -1de acýlýcak býr sey yok
                if (sapkaIndex != -1)
                {
                    sapkalar[sapkaIndex].gameObject.SetActive(true);
                    sapkaDurumText.text = itemBilgileri[sapkaIndex].itemAdi;

                    //Bunu yapma nedeným secýlen objelerýn satýl alýnýp alýnmadýgýný kontrol etmek ýcýn
                    if (!itemBilgileri[sapkaIndex].satildiAlmaDurumu)
                    {
                        satinAlmaText.text = itemBilgileri[sapkaIndex].puan + "- " + satýnAlmaText;
                        kaydetmeText.text = kaydetmeDurumuText;
                        islemButtonlar[1].interactable = false;
                        // beným eðer puaným satýn alýcagým seyden kucuk ýse butonu ýnaktýf edýyorum
                        if (itemBilgileri[sapkaIndex].puan > bellekYönetimi.VeriyiGetir_int("Puan"))
                            islemButtonlar[0].interactable = false;

                        else
                            islemButtonlar[0].interactable = true;
                    }
                    else
                    {
                        satinAlmaText.text = satýnAlindiText;
                        islemButtonlar[0].interactable = false;
                        // eðer kaydedilen obje ise kaydetme butonunun gorunurlugunu ýnaktýf et
                        if (sapkaIndex == bellekYönetimi.VeriyiGetir_int("AktifSapka"))
                        {
                            islemButtonlar[1].interactable = false;
                            kaydetmeText.text = kaydedilmeDurumuText;
                        }
                        else
                        {
                            islemButtonlar[1].interactable = true;
                            kaydetmeText.text = kaydetmeDurumuText;
                        }
                    }
                }
                else
                {
                    sapkaDurumText.text = itemDurumText;

                    //Bunu yapma nedeným secýlen objelerýn satýl alýnýp alýnmadýgýný kontrol etmek ýcýn burda ýf else gerek yok cunku
                    // -1e gelýrse default seyler gelýcek onlar zaten oyunun beles verdýgý seyler satýn alýnmaz ama kaydedýlýr
                    satinAlmaText.text = satýnAlindiText;
                    islemButtonlar[0].interactable = false;
                    // eðer kaydedilen obje ise kaydetme butonunun gorunurlugunu ýnaktýf et
                    if (sapkaIndex == bellekYönetimi.VeriyiGetir_int("AktifSapka"))
                    {
                        islemButtonlar[1].interactable = false;
                        kaydetmeText.text = kaydedilmeDurumuText;
                    }
                    else
                    {
                        islemButtonlar[1].interactable = true;
                        kaydetmeText.text = kaydetmeDurumuText;
                    }
                }

            }
        }
    }

    // saga sola gýttýgýmýz yer var ya oyun ýlk acýldýgýnda cýkan ekranda karakter kýsmýna basýnca sag sol oklar var yukarýdaký metýnde
    // orasý bu
    public void Sopa_YonButonlari(string islem)
    {
        ses.clip = sesler[0];
        ses.Play();
        if (islem == "ileri")
        {
            // baþta ya da sona geldiyse ve sondan sonra -1 olucagý ýcýn býr sey kapatmasýna gerek olmadýgý ýcýn çunku -1de sapka yok
            if (sopaIndex == -1)
            {
                sopaIndex++;
                sopalar[sopaIndex].gameObject.SetActive(true);
                sopaDurumText.text = itemBilgileri[sopaIndex + 3].itemAdi;

                //Bunu yapma nedeným secýlen objelerýn satýl alýnýp alýnmadýgýný kontrol etmek ýcýn
                if (!itemBilgileri[sopaIndex + 3].satildiAlmaDurumu)
                {
                    satinAlmaText.text = itemBilgileri[sopaIndex + 3].puan + "- " + satýnAlmaText;
                    kaydetmeText.text = kaydetmeDurumuText;
                    islemButtonlar[1].interactable = false;

                    // beným eðer puaným satýn alýcagým seyden kucuk ýse butonu ýnaktýf edýyorum
                    if (itemBilgileri[sopaIndex + 3].puan > bellekYönetimi.VeriyiGetir_int("Puan"))
                        islemButtonlar[0].interactable = false;

                    else
                        islemButtonlar[0].interactable = true;
                }
                else
                {
                    satinAlmaText.text = satýnAlindiText;
                    islemButtonlar[0].interactable = false;
                    if (sopaIndex == bellekYönetimi.VeriyiGetir_int("AktifSopa"))
                    {
                        islemButtonlar[1].interactable = false;
                        kaydetmeText.text = kaydedilmeDurumuText;
                    }
                    else
                    {
                        islemButtonlar[1].interactable = true;
                        kaydetmeText.text = kaydetmeDurumuText;
                    }
                }
            }
            // son gelmiþ ise en baþa donmesini istiyorum
            else if (sopaIndex == sapkalar.Length - 1)
            {
                sopalar[sopaIndex].gameObject.SetActive(false);
                sopaIndex = -1;
                sopaDurumText.text = itemDurumText;
                kaydetmeText.text = kaydetmeDurumuText;
                //Bunu yapma nedeným secýlen objelerýn satýl alýnýp alýnmadýgýný kontrol etmek ýcýn burda ýf else gerek yok cunku
                // -1e gelýrse default seyler gelýcek onlar zaten oyunun beles verdýgý seyler satýn alýnmaz ama kaydedýlýr
                satinAlmaText.text = satýnAlindiText;
                islemButtonlar[0].interactable = false;
                if (sopaIndex == bellekYönetimi.VeriyiGetir_int("AktifSopa"))
                {
                    Debug.Log("selamlar");
                    islemButtonlar[1].interactable = false;
                    kaydetmeText.text = kaydedilmeDurumuText;
                }
                else
                {
                    islemButtonlar[1].interactable = true;
                    kaydetmeText.text = kaydetmeDurumuText;
                }
            }
            // ortalardaysa ornegýn 4 sapka varsa 2 deyse 3e gecmek ýcýn once 2 kapatýlýr sonra 3e gecýlýr.
            else
            {
                sopalar[sopaIndex].gameObject.SetActive(false);
                sopaIndex++;
                sopalar[sopaIndex].gameObject.SetActive(true);
                sopaDurumText.text = itemBilgileri[sopaIndex + 3].itemAdi;

                //Bunu yapma nedeným secýlen objelerýn satýl alýnýp alýnmadýgýný kontrol etmek ýcýn
                if (!itemBilgileri[sopaIndex + 3].satildiAlmaDurumu)
                {
                    satinAlmaText.text = itemBilgileri[sopaIndex + 3].puan + "- " + satýnAlmaText;
                    kaydetmeText.text = kaydetmeDurumuText;
                    islemButtonlar[1].interactable = false;
                    // beným eðer puaným satýn alýcagým seyden kucuk ýse butonu ýnaktýf edýyorum
                    if (itemBilgileri[sopaIndex + 3].puan > bellekYönetimi.VeriyiGetir_int("Puan"))
                        islemButtonlar[0].interactable = false;

                    else
                        islemButtonlar[0].interactable = true;
                }
                else
                {
                    satinAlmaText.text = satýnAlindiText;
                    islemButtonlar[0].interactable = false;
                    if (sopaIndex == bellekYönetimi.VeriyiGetir_int("AktifSopa"))
                    {
                        islemButtonlar[1].interactable = false;
                        kaydetmeText.text = kaydedilmeDurumuText;
                    }
                    else
                    {
                        islemButtonlar[1].interactable = true;
                        kaydetmeText.text = kaydetmeDurumuText;
                    }
                }
            }
        }

        else if (islem == "geri")
        {
            // Baþtaysa ya da oyuna hiç girmemiþ ise
            if (sopaIndex == -1)
            {
                sopaIndex = sapkalar.Length - 1;
                sopalar[sopaIndex].gameObject.SetActive(true);
                sopaDurumText.text = itemBilgileri[sopaIndex + 3].itemAdi;

                //Bunu yapma nedeným secýlen objelerýn satýl alýnýp alýnmadýgýný kontrol etmek ýcýn
                if (!itemBilgileri[sopaIndex + 3].satildiAlmaDurumu)
                {
                    satinAlmaText.text = itemBilgileri[sopaIndex + 3].puan + "- " + satýnAlmaText;
                    kaydetmeText.text = kaydetmeDurumuText;
                    islemButtonlar[1].interactable = false;
                    // beným eðer puaným satýn alýcagým seyden kucuk ýse butonu ýnaktýf edýyorum
                    if (itemBilgileri[sopaIndex + 3].puan > bellekYönetimi.VeriyiGetir_int("Puan"))
                        islemButtonlar[0].interactable = false;

                    else
                        islemButtonlar[0].interactable = true;
                }
                else
                {
                    satinAlmaText.text = satýnAlindiText;
                    islemButtonlar[0].interactable = false;
                    if (sopaIndex == bellekYönetimi.VeriyiGetir_int("AktifSopa"))
                    {
                        islemButtonlar[1].interactable = false;
                        kaydetmeText.text = kaydedilmeDurumuText;
                    }
                    else
                    {
                        islemButtonlar[1].interactable = true;
                        kaydetmeText.text = kaydetmeDurumuText;
                    }
                }
            }
            // ortalardaysa ornegýn 4 sapka varsa 2 deyse 1e gecmek ýcýn once 2 kapatýlýr sonra 1e gecýlýr.
            else
            {
                sopalar[sopaIndex].gameObject.SetActive(false);
                sopaIndex--;
                // mesela ýndex 1 se buraya gýrerse -1 olur ve -1de acýlýcak býr sey yok ondan else yazdým
                if (sopaIndex != -1)
                {
                    sopalar[sopaIndex].gameObject.SetActive(true);
                    sopaDurumText.text = itemBilgileri[sopaIndex + 3].itemAdi;

                    //Bunu yapma nedeným secýlen objelerýn satýl alýnýp alýnmadýgýný kontrol etmek ýcýn
                    if (!itemBilgileri[sopaIndex + 3].satildiAlmaDurumu)
                    {
                        satinAlmaText.text = itemBilgileri[sopaIndex + 3].puan + "- " + satýnAlmaText;
                        kaydetmeText.text = kaydetmeDurumuText;
                        islemButtonlar[1].interactable = false;
                        // beným eðer puaným satýn alýcagým seyden kucuk ýse butonu ýnaktýf edýyorum
                        if (itemBilgileri[sopaIndex + 3].puan > bellekYönetimi.VeriyiGetir_int("Puan"))
                            islemButtonlar[0].interactable = false;

                        else
                            islemButtonlar[0].interactable = true;
                    }
                    else
                    {
                        satinAlmaText.text = satýnAlindiText;
                        islemButtonlar[0].interactable = false;
                        if (sopaIndex == bellekYönetimi.VeriyiGetir_int("AktifSopa"))
                        {
                            islemButtonlar[1].interactable = false;
                            kaydetmeText.text = kaydedilmeDurumuText;
                        }
                        else
                        {
                            islemButtonlar[1].interactable = true;
                            kaydetmeText.text = kaydetmeDurumuText;
                        }
                    }
                }
                else
                {
                    sopaDurumText.text = itemDurumText;

                    //Bunu yapma nedeným secýlen objelerýn satýl alýnýp alýnmadýgýný kontrol etmek ýcýn burda ýf else gerek yok cunku
                    // -1e gelýrse default seyler gelýcek onlar zaten oyunun beles verdýgý seyler satýn alýnmaz ama kaydedýl
                    satinAlmaText.text = satýnAlindiText;
                    islemButtonlar[0].interactable = false;
                    if (sopaIndex == bellekYönetimi.VeriyiGetir_int("AktifSopa"))
                    {
                        islemButtonlar[1].interactable = false;
                        kaydetmeText.text = kaydedilmeDurumuText;
                    }
                    else
                    {
                        islemButtonlar[1].interactable = true;
                        kaydetmeText.text = kaydetmeDurumuText;
                    }
                }

            }
        }
    }

    // saga sola gýttýgýmýz yer var ya oyun ýlk acýldýgýnda cýkan ekranda karakter kýsmýna basýnca sag sol oklar var yukarýdaký metýnde
    // orasý bu
    public void MaterialKarakter_YonButonlari(string islem)
    {
        ses.clip = sesler[0];
        ses.Play();
        if (islem == "ileri")
        {
            // baþta ya da sona geldiyse ve sondan sonra -1 olucagý ýcýn býr sey kapatmasýna gerek olmadýgý ýcýn çunku -1de sapka yok
            if (karakterIndex == 0)
            {
                karakterIndex++;
                Material[] mats = rendererKarakter.materials;
                // varsayýlan materyalý ekledým
                mats[0] = materialKarakter[karakterIndex];
                rendererKarakter.materials = mats;
                karakterDurumText.text = itemBilgileri[karakterIndex + 6].itemAdi;

                //Bunu yapma nedeným secýlen objelerýn satýl alýnýp alýnmadýgýný kontrol etmek ýcýn
                if (!itemBilgileri[karakterIndex + 6].satildiAlmaDurumu)
                {
                    satinAlmaText.text = itemBilgileri[karakterIndex + 6].puan + "- " + satýnAlmaText;
                    kaydetmeText.text = kaydetmeDurumuText;
                    islemButtonlar[1].interactable = false;
                    // beným eðer puaným satýn alýcagým seyden kucuk ýse butonu ýnaktýf edýyorum
                    if (itemBilgileri[karakterIndex + 6].puan > bellekYönetimi.VeriyiGetir_int("Puan"))
                        islemButtonlar[0].interactable = false;

                    else
                        islemButtonlar[0].interactable = true;
                }
                else
                {
                    satinAlmaText.text = satýnAlindiText;
                    islemButtonlar[0].interactable = false;
                    if (karakterIndex == bellekYönetimi.VeriyiGetir_int("AktifKarakter"))
                    {
                        islemButtonlar[1].interactable = false;
                        kaydetmeText.text = kaydedilmeDurumuText;
                    }
                    else
                    {
                        islemButtonlar[1].interactable = true;
                        kaydetmeText.text = kaydetmeDurumuText;
                    }
                }
            }
            // son gelmiþ ise en baþa donmesini istiyorum
            else if (karakterIndex == materialKarakter.Length - 1)
            {
                karakterIndex = 0;
                Material[] mats = rendererKarakter.materials;
                // varsayýlan materyalý ekledým
                mats[0] = materialKarakter[0];
                rendererKarakter.materials = mats;
                karakterDurumText.text = itemBilgileri[karakterIndex + 6].itemAdi;

                //Bunu yapma nedeným secýlen objelerýn satýl alýnýp alýnmadýgýný kontrol etmek ýcýn burda ýf else gerek yok cunku
                // -1e gelýrse default seyler gelýcek onlar zaten oyunun beles verdýgý seyler satýn alýnmaz ama kaydedýlýr
                satinAlmaText.text = satýnAlindiText;
                islemButtonlar[0].interactable = false;
                if (karakterIndex == bellekYönetimi.VeriyiGetir_int("AktifKarakter"))
                {
                    islemButtonlar[1].interactable = false;
                    kaydetmeText.text = kaydedilmeDurumuText;
                }
                else
                {
                    islemButtonlar[1].interactable = true;
                    kaydetmeText.text = kaydetmeDurumuText;
                }
            }
            // ortalardaysa ornegýn 4 sapka varsa 2 deyse 3e gecmek ýcýn once 2 kapatýlýr sonra 3e gecýlýr.
            else
            {
                karakterIndex++;
                Material[] mats = rendererKarakter.materials;
                // varsayýlan materyalý ekledým
                mats[0] = materialKarakter[karakterIndex];
                rendererKarakter.materials = mats;
                karakterDurumText.text = itemBilgileri[karakterIndex + 6].itemAdi;

                //Bunu yapma nedeným secýlen objelerýn satýl alýnýp alýnmadýgýný kontrol etmek ýcýn
                if (!itemBilgileri[karakterIndex + 6].satildiAlmaDurumu)
                {
                    satinAlmaText.text = itemBilgileri[karakterIndex + 6].puan + "- " + satýnAlmaText;
                    kaydetmeText.text = kaydetmeDurumuText;
                    islemButtonlar[1].interactable = false;
                    // beným eðer puaným satýn alýcagým seyden kucuk ýse butonu ýnaktýf edýyorum
                    if (itemBilgileri[karakterIndex + 6].puan > bellekYönetimi.VeriyiGetir_int("Puan"))
                        islemButtonlar[0].interactable = false;

                    else
                        islemButtonlar[0].interactable = true; ;
                }
                else
                {
                    satinAlmaText.text = satýnAlindiText;
                    islemButtonlar[0].interactable = false;
                    if (karakterIndex == bellekYönetimi.VeriyiGetir_int("AktifKarakter"))
                    {
                        islemButtonlar[1].interactable = false;
                        kaydetmeText.text = kaydedilmeDurumuText;
                    }
                    else
                    {
                        islemButtonlar[1].interactable = true;
                        kaydetmeText.text = kaydetmeDurumuText;
                    }
                }
            }
        }

        else if (islem == "geri")
        {
            // Baþtaysa ya da oyuna hiç girmemiþ ise
            if (karakterIndex == 0)
            {
                karakterIndex = materialKarakter.Length - 1;

                Material[] mats = rendererKarakter.materials;
                // varsayýlan materyalý ekledým
                mats[0] = materialKarakter[karakterIndex];
                rendererKarakter.materials = mats;

                karakterDurumText.text = itemBilgileri[karakterIndex + 6].itemAdi;

                //Bunu yapma nedeným secýlen objelerýn satýl alýnýp alýnmadýgýný kontrol etmek ýcýn
                if (!itemBilgileri[karakterIndex + 6].satildiAlmaDurumu)
                {
                    satinAlmaText.text = itemBilgileri[karakterIndex + 6].puan + "- " + satýnAlmaText;
                    kaydetmeText.text = kaydetmeDurumuText;
                    islemButtonlar[1].interactable = false;
                    // beným eðer puaným satýn alýcagým seyden kucuk ýse butonu ýnaktýf edýyorum
                    if (itemBilgileri[karakterIndex + 6].puan > bellekYönetimi.VeriyiGetir_int("Puan"))
                        islemButtonlar[0].interactable = false;

                    else
                        islemButtonlar[0].interactable = true;
                }
                else
                {
                    satinAlmaText.text = satýnAlindiText;
                    islemButtonlar[0].interactable = false;
                    if (karakterIndex == bellekYönetimi.VeriyiGetir_int("AktifKarakter"))
                    {
                        islemButtonlar[1].interactable = false;
                        kaydetmeText.text =kaydedilmeDurumuText;
                    }
                    else
                    {
                        islemButtonlar[1].interactable = true;
                        kaydetmeText.text = kaydetmeDurumuText;
                    }
                }
            }
            // ortalardaysa ornegýn 4 sapka varsa 2 deyse 1e gecmek ýcýn once 2 kapatýlýr sonra 1e gecýlýr.
            else
            {
                karakterIndex--;
                // mesela ýndex 1 se buraya gýrerse -1 olur ve -1de acýlýcak býr sey yok ondan else yazdým onun ýcýn var yaný
                if (karakterIndex != 0)
                {
                    Material[] mats = rendererKarakter.materials;
                    // varsayýlan materyalý ekledým
                    mats[0] = materialKarakter[karakterIndex];
                    rendererKarakter.materials = mats;

                    karakterDurumText.text = itemBilgileri[karakterIndex + 6].itemAdi;

                    //Bunu yapma nedeným secýlen objelerýn satýl alýnýp alýnmadýgýný kontrol etmek ýcýn
                    if (!itemBilgileri[karakterIndex + 6].satildiAlmaDurumu)
                    {
                        satinAlmaText.text = itemBilgileri[karakterIndex + 6].puan + "- " + satýnAlmaText;
                        kaydetmeText.text = kaydetmeDurumuText;
                        islemButtonlar[1].interactable = false;
                        // beným eðer puaným satýn alýcagým seyden kucuk ýse butonu ýnaktýf edýyorum
                        if (itemBilgileri[karakterIndex + 6].puan > bellekYönetimi.VeriyiGetir_int("Puan"))
                            islemButtonlar[0].interactable = false;

                        else
                            islemButtonlar[0].interactable = true;
                    }
                    else
                    {
                        satinAlmaText.text = satýnAlindiText;
                        islemButtonlar[0].interactable = false;
                        if (karakterIndex == bellekYönetimi.VeriyiGetir_int("AktifKarakter"))
                        {
                            islemButtonlar[1].interactable = false;
                            kaydetmeText.text = kaydedilmeDurumuText;
                        }
                        else
                        {
                            islemButtonlar[1].interactable = true;
                            kaydetmeText.text = kaydetmeDurumuText;
                        }
                    }
                }
                else
                {
                    Material[] mats = rendererKarakter.materials;
                    // varsayýlan materyalý ekledým
                    mats[0] = materialKarakter[karakterIndex];
                    rendererKarakter.materials = mats;

                    karakterDurumText.text =itemDurumText;

                    //Bunu yapma nedeným secýlen objelerýn satýl alýnýp alýnmadýgýný kontrol etmek ýcýn burda ýf else gerek yok cunku
                    // -1e gelýrse default seyler gelýcek onlar zaten oyunun beles verdýgý seyler satýn alýnmaz ama kaydedýl
                    satinAlmaText.text = satýnAlindiText;
                    islemButtonlar[0].interactable = false;
                    if (karakterIndex == bellekYönetimi.VeriyiGetir_int("AktifKarakter"))
                    {
                        islemButtonlar[1].interactable = false;
                        kaydetmeText.text = kaydedilmeDurumuText;
                    }
                    else
                    {
                        islemButtonlar[1].interactable = true;
                        kaydetmeText.text = kaydetmeDurumuText;
                    }
                }
            }
        }
    }


    // Kaydedilen bir þey var mý yok mu onun kontrol edýldýgý yer
    public void DurumuKontrolEt(int bolum, bool islem = false)
    {
        // Sapkaya týklarsa burasý
        if (bolum == 0)
        {
            if (bellekYönetimi.VeriyiGetir_int("AktifSapka") == -1)
            {
                // eðer daha önce hiç girilmemiþ yada girip sonra -1 olarak kalmýþ ise tüm aktif objeleri kapat
                foreach (var item in sapkalar)
                {
                    item.gameObject.SetActive(false);
                }
                if (!islem)
                {
                    sapkaDurumText.text = itemDurumText;

                    //Bunu yapma nedeným secýlen objelerýn satýl alýnýp alýnmadýgýný kontrol etmek ýcýn burda ýf else gerek yok cunku
                    // -1e gelýrse default seyler gelýcek onlar zaten oyunun beles verdýgý seyler satýn alýnmaz ama kaydedýlýr
                    satinAlmaText.text = satýnAlindiText;
                    islemButtonlar[0].interactable = false;
                    // eðer kaydedilen obje ise kaydetme butonunun gorunurlugunu ýnaktýf et
                    if (sapkaIndex == bellekYönetimi.VeriyiGetir_int("AktifSapka"))
                    {
                        islemButtonlar[1].interactable = false;
                        kaydetmeText.text = kaydedilmeDurumuText;
                    }
                    else
                    {
                        islemButtonlar[1].interactable = true;
                        kaydetmeText.text = kaydetmeDurumuText;
                    }

                }
            }

            else
            {
                OncekileriÝnaktifEt(sapkalar);
                sapkaIndex = bellekYönetimi.VeriyiGetir_int("AktifSapka");
                sapkalar[sapkaIndex].gameObject.SetActive(true);
                satinAlmaText.text = satýnAlindiText;
                sapkaDurumText.text = itemBilgileri[sapkaIndex].itemAdi;
                islemButtonlar[0].interactable = false;
                // eðer kaydedilen obje ise kaydetme butonunun gorunurlugunu ýnaktýf et
                if (sapkaIndex == bellekYönetimi.VeriyiGetir_int("AktifSapka"))
                {
                    islemButtonlar[1].interactable = false;
                    kaydetmeText.text = kaydedilmeDurumuText;
                }
                else
                {
                    islemButtonlar[1].interactable = true;
                    kaydetmeText.text = kaydetmeDurumuText;
                }
            }
        }

        //Sopaya týklarsa burasý
        else if (bolum == 1)
        {
            if (bellekYönetimi.VeriyiGetir_int("AktifSopa") == -1)
            {
                // eðer daha önce hiç girilmemiþ yada girip sonra -1 olarak kalmýþ ise tüm aktif objeleri kapat
                foreach (var item in sopalar)
                {
                    item.gameObject.SetActive(false);
                }
                if (!islem)
                {
                    sopaDurumText.text = itemDurumText;

                    //Bunu yapma nedeným secýlen objelerýn satýl alýnýp alýnmadýgýný kontrol etmek ýcýn burda ýf else gerek yok cunku
                    // -1e gelýrse default seyler gelýcek onlar zaten oyunun beles verdýgý seyler satýn alýnmaz ama kaydedýlýr
                    satinAlmaText.text = satýnAlindiText;
                    islemButtonlar[0].interactable = false;
                    // eðer kaydedilen obje ise kaydetme butonunun gorunurlugunu ýnaktýf et
                    if (sopaIndex == bellekYönetimi.VeriyiGetir_int("AktifSopa"))
                    {
                        islemButtonlar[1].interactable = false;
                        kaydetmeText.text = kaydedilmeDurumuText;
                    }
                    else
                    {
                        islemButtonlar[1].interactable = true;
                        kaydetmeText.text = kaydetmeDurumuText;
                    }
                }
            }

            else
            {
                OncekileriÝnaktifEt(sapkalar);
                sopaIndex = bellekYönetimi.VeriyiGetir_int("AktifSopa");
                sopalar[sopaIndex].gameObject.SetActive(true);
                satinAlmaText.text =satýnAlindiText;
                sopaDurumText.text = itemBilgileri[sopaIndex + 3].itemAdi;
                islemButtonlar[0].interactable = false;
                // eðer kaydedilen obje ise kaydetme butonunun gorunurlugunu ýnaktýf et
                if (sopaIndex == bellekYönetimi.VeriyiGetir_int("AktifSopa"))
                {
                    islemButtonlar[1].interactable = false;
                    kaydetmeText.text = kaydedilmeDurumuText;
                }
                else
                {
                    islemButtonlar[1].interactable = true;
                    kaydetmeText.text = kaydetmeDurumuText;
                }
            }
        }
        // karaktere týklarsa burasý
        else
        {
            if (bellekYönetimi.VeriyiGetir_int("AktifKarakter") == 0)
            {
                Material[] mats = rendererKarakter.materials;
                // varsayýlan materyalý ekledým
                mats[0] = materialKarakter[0];
                rendererKarakter.materials = mats;
                if (!islem)
                {
                    karakterDurumText.text =itemDurumText;

                    //Bunu yapma nedeným secýlen objelerýn satýl alýnýp alýnmadýgýný kontrol etmek ýcýn burda ýf else gerek yok cunku
                    // -1e gelýrse default seyler gelýcek onlar zaten oyunun beles verdýgý seyler satýn alýnmaz ama kaydedýlýr
                    satinAlmaText.text = satýnAlindiText;
                    islemButtonlar[0].interactable = false;
                    // eðer kaydedilen obje ise kaydetme butonunun gorunurlugunu ýnaktýf et
                    if (karakterIndex == bellekYönetimi.VeriyiGetir_int("AktifKarakter"))
                    {
                        islemButtonlar[1].interactable = false;
                        kaydetmeText.text = kaydedilmeDurumuText;
                    }
                    else
                    {
                        islemButtonlar[1].interactable = true;
                        kaydetmeText.text = kaydetmeDurumuText;
                    }
                }
            }

            else
            {
                karakterIndex = bellekYönetimi.VeriyiGetir_int("AktifKarakter");
                Material[] mats = rendererKarakter.materials;
                // varsayýlan materyalý ekledým
                mats[0] = materialKarakter[karakterIndex];
                rendererKarakter.materials = mats;
                satinAlmaText.text =satýnAlindiText;
                karakterDurumText.text = itemBilgileri[karakterIndex + 6].itemAdi;
                islemButtonlar[0].interactable = false;
                // eðer kaydedilen obje ise kaydetme butonunun gorunurlugunu ýnaktýf et
                if (karakterIndex == bellekYönetimi.VeriyiGetir_int("AktifKarakter"))
                {
                    islemButtonlar[1].interactable = false;
                    kaydetmeText.text = kaydedilmeDurumuText;
                }
                else
                {
                    islemButtonlar[1].interactable = true;
                    kaydetmeText.text = kaydetmeDurumuText;
                };
            }
        }

    }

    public void SatýnAl()
    {
        ses.clip = sesler[2];
        ses.Play();
        // eðer hiçbir kýsma gýrýlmemýs ýse sapka sopa falan if'e girmez -1 oldugu için
        if (aktifÝslemPaneliÝndex != -1)
        {
            switch (aktifÝslemPaneliÝndex)
            {
                case 0:
                    SatýnAlmaDurumu(sapkaIndex);
                    break;
                case 1:
                    int index = sopaIndex +3;
                    SatýnAlmaDurumu(index);
                    break;
                case 2:
                    int index2 = karakterIndex + 6;
                    SatýnAlmaDurumu(index2);
                    break;
            }
        }
    }

    public void Kaydet()
    {
        ses.clip = sesler[1];
        ses.Play();
        if (aktifÝslemPaneliÝndex != -1)
        {
            switch (aktifÝslemPaneliÝndex)
            {
                case 0:
                    KaydetmeDurumu("AktifSapka", sapkaIndex);
                    break;
                case 1:
                    KaydetmeDurumu("AktifSopa", sopaIndex);
                    break;
                case 2:
                    // materialde onceki karakterý dýsaktýf edýp yenýsýný aktýf etme boyle
                    Material[] mats = rendererKarakter.materials;
                    // varsayýlan materyalý ekledým
                    mats[0] = materialKarakter[karakterIndex];
                    rendererKarakter.materials = mats;
                    //kaydete bastýktan sonra kaydedýlen sapkanýn ýndeksýný kaydedýyorumký gerý cýkýp gýrdýgýmde ayný yer cýksýn
                    KaydetmeDurumu("AktifKarakter", karakterIndex);
                    break;
            }
           
        }
    }

    // sahne acýldýgýnda cýkan 3 kýsým sapka sopa karakter varya o butonlar bu fonksýyonda calýsýyo
    public void ÝslemPaneliAktifEt(int index)
    {
        ses.clip = sesler[0];
        ses.Play();
        // ortaalan objesýný aktýf etme
        genelPaneller[0].SetActive(true);
        aktifÝslemPaneliÝndex = index;
        // hangý ýndexýn ne oldugunu býldýgým ýcýn kolay
        DurumuKontrolEt(index);
        islemPanelleri[index].SetActive(true);
        genelPaneller[1].SetActive(true);
        islemCanvas.SetActive(false);
    }

    public void GeriButton()
    {
        ses.clip = sesler[0];
        ses.Play();
        islemCanvas.SetActive(true);
        islemPanelleri[aktifÝslemPaneliÝndex].SetActive(false);
        genelPaneller[0].SetActive(false);
        genelPaneller[1].SetActive(false);
        // eðer kaydet butonuna basmas ýse eský deger neyse onu acýga cýkartýyor burasý kaydete basýp gerý cýkarsa once kaydet fonksýyonu sonra burasý calýsýyo.
        DurumuKontrolEt(aktifÝslemPaneliÝndex, true);
        aktifÝslemPaneliÝndex = -1;
    }

    public void AnaMenuyeDon()
    {
        veriYonetimi.Save(itemBilgileri);
        ses.clip = sesler[0];
        StartCoroutine(LoadSceneAfterSound());
    }

    private IEnumerator LoadSceneAfterSound()
    {
        ses.Play();
        yield return new WaitForSeconds(ses.clip.length); // Sesin süresi kadar bekle
        SceneManager.LoadScene(0); // Sahne yükle
    }



    public void OncekileriÝnaktifEt(GameObject[] gameObjects)
    {
        foreach (var item in gameObjects)
        {
            item.gameObject.SetActive(false);
        }
    }

    //--------------------Yardýmcý Fonksýyonlar -----------------
    public void SatýnAlmaDurumu(int index)
    {
        // satýn alýnan objeyý true yapýyorum
        itemBilgileri[index].satildiAlmaDurumu = true;
        bellekYönetimi.VeriyiKaydet_int("Puan", bellekYönetimi.VeriyiGetir_int("Puan") - itemBilgileri[index].puan);
        satinAlmaText.text = satýnAlindiText;
        islemButtonlar[0].interactable = false;
        islemButtonlar[1].interactable = true;
        puanText.text = bellekYönetimi.VeriyiGetir_int("Puan").ToString();
    }

    public void KaydetmeDurumu(string adi, int indexi)
    {
        //kaydete bastýktan sonra kaydedýlen sapkanýn ýndeksýný kaydedýyorumký gerý cýkýp gýrdýgýmde ayný yer cýksýn
        bellekYönetimi.VeriyiKaydet_int(adi, indexi);
        islemButtonlar[1].interactable = false;
        kaydetmeText.text = kaydedilmeDurumuText;
    }

}
