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

    BellekY�netimi bellekY�netimi = new BellekY�netimi();
    VeriYonetimi veriYonetimi = new VeriYonetimi();

    int sapkaIndex=-1;
    int sopaIndex=-1;
    int karakterIndex=0;

    int aktif�slemPaneli�ndex;

    // burda b�r aud�o source olusturup sahnemde olusturmustum zaten onun aud�o cl�p�ne farkl� sesler ver�cem butonlara gore sat�n alma'n�n
    // ses� ayr� kaydetmen�n ayr� v�deodak� adam ise boyle yapmak yer�ne Aud�oSource'dan dizi olusturup 3 tane aud�oSource olusturuyo
    // b�r objen�n �c�nde heps�ne farkl� cl�pler ver�yo
    public AudioSource ses;
    public AudioClip[] sesler;

    [Header("Genel Veriler")]
    public List<ItemBilgileri> itemBilgileri = new List<ItemBilgileri>();

    // buda kullancag�m�z dil bilgisi i�ini �slemler kutuphanes�ndek� Veri yonet�m� s�n�f�n�n Dili_ListeyeAktar fonks�yonundan dolduruyoruz
    List<Diller> DilBilgileri = new List<Diller>();

    //bu ise DilBilgileri listesinden sahnemde kullanacag�m b�lg�ler� al�p bu l�steye yerlest�rmek �c�n kulland�g�m l�ste
    // bu sayede �ndex�m hep 0 olucak ve �ndexlerle ugrasm�ycam
    public List<Diller> kullan�lacakDilBilgileri = new List<Diller>();

    // hepsinin textlerini deg�st�r�ceg�z �ng�l�zce yada t�rk�eye gore. sahnedek� textler� al�yorum bu arada onlar�n �sm� deg�scek ya
    public Text[] Texts;

    // bunu tan�mlamam�n neden� a��ag�da scr�pt taraf�nda ben koda mudahele ed�p sat�n al dem�s�m ama �ng�l�zce olursa sat�n al
    // dememem gerek�yo ondan duzenlemel�y�m.
    private string sat�nAlmaText;
    private string sat�nAlindiText;

    // bunu tan�mlamam�n neden� a��ag�da scr�pt taraf�nda ben koda mudahele ed�p kaydet dem�s�m ama �ng�l�zce olursa kaydet
    // dememem gerek�yo ondan duzenlemel�y�m.
    private string kaydetmeDurumuText;
    private string kaydedilmeDurumuText;

    // bunu tan�mlamam�n neden� a��ag�da scr�pt taraf�nda ben koda mudahele ed�p item yok dem�s�m ama �ng�l�zce olursa item yok
    // dememem gerek�yo ondan duzenlemel�y�m.
    private string itemDurumText;

    void Start()
    {
        //bellekY�netimi.VeriyiKaydet_int("Puan", 1000);

        bellekY�netimi.VeriyiKaydet_int("Puan", 2450);
        puanText.text = bellekY�netimi.VeriyiGetir_int("Puan").ToString();

        // once dosyaya kayded�len b�lg�ler� dosyadan al�p orada olusturdugumuz l�steler�n �c�ne at�yoruz.
        veriYonetimi.Load();
        // sonra doldurulan l�stey� cag�r�p bu l�stey�de onun sayes�nde dolduruyoruz
        itemBilgileri = veriYonetimi.ListeyiAktar();

        // kaydetd�g�m�z seyler�n g�rd�g�m�zde gozukmes�n� �sted�g�m�z �c�n yapt�k bunlar� bunlar� yazmasak starta sadece o 3 butona g�r�p
        // ger� c�kt�g�m�zda gel�rd� ozellest�rmeler
        DurumuKontrolEt(0);
        DurumuKontrolEt(1);
        DurumuKontrolEt(2);

        ses.volume = PlayerPrefs.GetFloat("MenuFx");

        // once dosyaya kayded�len b�lg�ler� dosyadan al�p orada olusturdugumuz l�steler�n �c�ne at�yoruz.
        veriYonetimi.DilLoad();
        // sonra doldurulan l�stey� cag�r�p bu l�stey�de onun sayes�nde dolduruyoruz
        DilBilgileri = veriYonetimi.Dili_ListeyeAktar();
        // bu sayede 0.nc� �ndekse suan anamenu �le alakal� b�lg�ler d�rekt kullanacag�m l�steye geld� rahat�m
        kullan�lacakDilBilgileri.Add(DilBilgileri[1]);

        DilTercihiYonetimi();

    }

    public void DilTercihiYonetimi()
    {
        if (PlayerPrefs.GetString("Dil") == "TR")
        {
            for (int i = 0; i < Texts.Length; i++)
            {
                Debug.Log("selamlar");
                Texts[i].text = kullan�lacakDilBilgileri[0].dilBilgileriTR[i].Text;
            }
            sat�nAlmaText = kullan�lacakDilBilgileri[0].dilBilgileriTR[5].Text;
            sat�nAlindiText = "Sat�n Al�nd�";

            kaydetmeDurumuText = kullan�lacakDilBilgileri[0].dilBilgileriTR[6].Text;
            kaydedilmeDurumuText = "Kaydedildi";

            itemDurumText = kullan�lacakDilBilgileri[0].dilBilgileriTR[4].Text;
        }
        else
        {
            for (int i = 0; i < Texts.Length; i++)
            {
                Texts[i].text = kullan�lacakDilBilgileri[0].dilBilgileriEN[i].Text;
            }
            sat�nAlmaText = kullan�lacakDilBilgileri[0].dilBilgileriEN[5].Text;
            sat�nAlindiText = "Buyed";

            kaydetmeDurumuText = kullan�lacakDilBilgileri[0].dilBilgileriEN[6].Text;
            kaydedilmeDurumuText = "Saved";


            itemDurumText = kullan�lacakDilBilgileri[0].dilBilgileriEN[4].Text;
        }
    }

    // saga sola g�tt�g�m�z yer var ya oyun �lk ac�ld�g�nda c�kan ekranda karakter k�sm�na bas�nca sag sol oklar var yukar�dak� met�nde
    // oras� bu
    public void Sapka_YonButonlari(string islem)
    {
        ses.clip = sesler[0];
        ses.Play();
        if (islem == "ileri")
        {
            // ba�ta ya da sona geldiyse ve sondan sonra -1 olucag� �c�n b�r sey kapatmas�na gerek olmad�g� �c�n �unku -1de sapka yok
            if (sapkaIndex == -1)
            {
                sapkaIndex++;
                sapkalar[sapkaIndex].gameObject.SetActive(true);
                sapkaDurumText.text = itemBilgileri[sapkaIndex].itemAdi;

                //Bunu yapma neden�m sec�len objeler�n sat�l al�n�p al�nmad�g�n� kontrol etmek �c�n
                if (!itemBilgileri[sapkaIndex].satildiAlmaDurumu)
                {
                    satinAlmaText.text = itemBilgileri[sapkaIndex].puan + "- "+ sat�nAlmaText;
                    islemButtonlar[1].interactable = false;
                    // bunu yazmasam b�r tane kaydetme text� oldugu �c�n kayded�len b�r sey olursa kayded�lmeyen seyede kayded�ld� yazar
                    kaydetmeText.text = kaydetmeDurumuText;

                    // ben�m e�er puan�m sat�n al�cag�m seyden kucuk �se butonu �nakt�f ed�yorum
                    if (itemBilgileri[sapkaIndex].puan > bellekY�netimi.VeriyiGetir_int("Puan"))
                        islemButtonlar[0].interactable = false;

                    else
                        islemButtonlar[0].interactable = true;
                }
                else
                {
                    satinAlmaText.text = sat�nAlindiText;
                    islemButtonlar[0].interactable = false;
                    // e�er kaydedilen obje ise kaydetme butonunun gorunurlugunu �nakt�f et
                    if (sapkaIndex == bellekY�netimi.VeriyiGetir_int("AktifSapka"))
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
            // son gelmi� ise en ba�a donmesini istiyorum
            else if (sapkaIndex == sapkalar.Length - 1)
            {
                sapkalar[sapkaIndex].gameObject.SetActive(false);
                sapkaIndex = -1;
                sapkaDurumText.text = itemDurumText;

                //Bunu yapma neden�m sec�len objeler�n sat�l al�n�p al�nmad�g�n� kontrol etmek �c�n burda �f else gerek yok cunku
                // -1e gel�rse default seyler gel�cek onlar zaten oyunun beles verd�g� seyler sat�n al�nmaz ama kayded�l�r
                satinAlmaText.text = sat�nAlindiText;
                islemButtonlar[0].interactable = false;
                // e�er kaydedilen obje ise kaydetme butonunun gorunurlugunu �nakt�f et
                if (sapkaIndex == bellekY�netimi.VeriyiGetir_int("AktifSapka"))
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
            // ortalardaysa orneg�n 4 sapka varsa 2 deyse 3e gecmek �c�n once 2 kapat�l�r sonra 3e gec�l�r.
            else
            {
                sapkalar[sapkaIndex].gameObject.SetActive(false);
                sapkaIndex++;
                sapkalar[sapkaIndex].gameObject.SetActive(true);
                sapkaDurumText.text = itemBilgileri[sapkaIndex].itemAdi;

                //Bunu yapma neden�m sec�len objeler�n sat�l al�n�p al�nmad�g�n� kontrol etmek �c�n
                if (!itemBilgileri[sapkaIndex].satildiAlmaDurumu)
                {
                    satinAlmaText.text = itemBilgileri[sapkaIndex].puan + "- " + sat�nAlmaText;
                    kaydetmeText.text = kaydetmeDurumuText;
                    islemButtonlar[1].interactable = false;
                    // ben�m e�er puan�m sat�n al�cag�m seyden kucuk �se butonu �nakt�f ed�yorum
                    if (itemBilgileri[sapkaIndex].puan > bellekY�netimi.VeriyiGetir_int("Puan"))
                        islemButtonlar[0].interactable = false;

                    else
                        islemButtonlar[0].interactable = true;
                }
                else
                {
                    satinAlmaText.text = sat�nAlindiText;
                    islemButtonlar[0].interactable = false;
                    // e�er kaydedilen obje ise kaydetme butonunun gorunurlugunu �nakt�f et
                    if (sapkaIndex == bellekY�netimi.VeriyiGetir_int("AktifSapka"))
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
            // Ba�taysa ya da oyuna hi� girmemi� ise
            if (sapkaIndex == -1)
            {
                sapkaIndex = sapkalar.Length - 1;
                sapkalar[sapkaIndex].gameObject.SetActive(true);
                sapkaDurumText.text = itemBilgileri[sapkaIndex].itemAdi;

                //Bunu yapma neden�m sec�len objeler�n sat�l al�n�p al�nmad�g�n� kontrol etmek �c�n
                if (!itemBilgileri[sapkaIndex].satildiAlmaDurumu)
                {
                    satinAlmaText.text = itemBilgileri[sapkaIndex].puan + "- " + sat�nAlmaText;
                    kaydetmeText.text = kaydetmeDurumuText;
                    islemButtonlar[1].interactable = false;
                    // ben�m e�er puan�m sat�n al�cag�m seyden kucuk �se butonu �nakt�f ed�yorum
                    if (itemBilgileri[sapkaIndex].puan > bellekY�netimi.VeriyiGetir_int("Puan"))
                        islemButtonlar[0].interactable = false;

                    else
                        islemButtonlar[0].interactable = true;
                }
                else
                {
                    satinAlmaText.text = sat�nAlindiText;
                    islemButtonlar[0].interactable = false;
                    // e�er kaydedilen obje ise kaydetme butonunun gorunurlugunu �nakt�f et
                    if (sapkaIndex == bellekY�netimi.VeriyiGetir_int("AktifSapka"))
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
            // ortalardaysa orneg�n 4 sapka varsa 2 deyse 3e gecmek �c�n once 2 kapat�l�r sonra 3e gec�l�r.
            else
            {
                sapkalar[sapkaIndex].gameObject.SetActive(false);
                sapkaIndex--;
                // mesela �ndex 1 se buraya g�rerse -1 olur ve -1de ac�l�cak b�r sey yok
                if (sapkaIndex != -1)
                {
                    sapkalar[sapkaIndex].gameObject.SetActive(true);
                    sapkaDurumText.text = itemBilgileri[sapkaIndex].itemAdi;

                    //Bunu yapma neden�m sec�len objeler�n sat�l al�n�p al�nmad�g�n� kontrol etmek �c�n
                    if (!itemBilgileri[sapkaIndex].satildiAlmaDurumu)
                    {
                        satinAlmaText.text = itemBilgileri[sapkaIndex].puan + "- " + sat�nAlmaText;
                        kaydetmeText.text = kaydetmeDurumuText;
                        islemButtonlar[1].interactable = false;
                        // ben�m e�er puan�m sat�n al�cag�m seyden kucuk �se butonu �nakt�f ed�yorum
                        if (itemBilgileri[sapkaIndex].puan > bellekY�netimi.VeriyiGetir_int("Puan"))
                            islemButtonlar[0].interactable = false;

                        else
                            islemButtonlar[0].interactable = true;
                    }
                    else
                    {
                        satinAlmaText.text = sat�nAlindiText;
                        islemButtonlar[0].interactable = false;
                        // e�er kaydedilen obje ise kaydetme butonunun gorunurlugunu �nakt�f et
                        if (sapkaIndex == bellekY�netimi.VeriyiGetir_int("AktifSapka"))
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

                    //Bunu yapma neden�m sec�len objeler�n sat�l al�n�p al�nmad�g�n� kontrol etmek �c�n burda �f else gerek yok cunku
                    // -1e gel�rse default seyler gel�cek onlar zaten oyunun beles verd�g� seyler sat�n al�nmaz ama kayded�l�r
                    satinAlmaText.text = sat�nAlindiText;
                    islemButtonlar[0].interactable = false;
                    // e�er kaydedilen obje ise kaydetme butonunun gorunurlugunu �nakt�f et
                    if (sapkaIndex == bellekY�netimi.VeriyiGetir_int("AktifSapka"))
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

    // saga sola g�tt�g�m�z yer var ya oyun �lk ac�ld�g�nda c�kan ekranda karakter k�sm�na bas�nca sag sol oklar var yukar�dak� met�nde
    // oras� bu
    public void Sopa_YonButonlari(string islem)
    {
        ses.clip = sesler[0];
        ses.Play();
        if (islem == "ileri")
        {
            // ba�ta ya da sona geldiyse ve sondan sonra -1 olucag� �c�n b�r sey kapatmas�na gerek olmad�g� �c�n �unku -1de sapka yok
            if (sopaIndex == -1)
            {
                sopaIndex++;
                sopalar[sopaIndex].gameObject.SetActive(true);
                sopaDurumText.text = itemBilgileri[sopaIndex + 3].itemAdi;

                //Bunu yapma neden�m sec�len objeler�n sat�l al�n�p al�nmad�g�n� kontrol etmek �c�n
                if (!itemBilgileri[sopaIndex + 3].satildiAlmaDurumu)
                {
                    satinAlmaText.text = itemBilgileri[sopaIndex + 3].puan + "- " + sat�nAlmaText;
                    kaydetmeText.text = kaydetmeDurumuText;
                    islemButtonlar[1].interactable = false;

                    // ben�m e�er puan�m sat�n al�cag�m seyden kucuk �se butonu �nakt�f ed�yorum
                    if (itemBilgileri[sopaIndex + 3].puan > bellekY�netimi.VeriyiGetir_int("Puan"))
                        islemButtonlar[0].interactable = false;

                    else
                        islemButtonlar[0].interactable = true;
                }
                else
                {
                    satinAlmaText.text = sat�nAlindiText;
                    islemButtonlar[0].interactable = false;
                    if (sopaIndex == bellekY�netimi.VeriyiGetir_int("AktifSopa"))
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
            // son gelmi� ise en ba�a donmesini istiyorum
            else if (sopaIndex == sapkalar.Length - 1)
            {
                sopalar[sopaIndex].gameObject.SetActive(false);
                sopaIndex = -1;
                sopaDurumText.text = itemDurumText;
                kaydetmeText.text = kaydetmeDurumuText;
                //Bunu yapma neden�m sec�len objeler�n sat�l al�n�p al�nmad�g�n� kontrol etmek �c�n burda �f else gerek yok cunku
                // -1e gel�rse default seyler gel�cek onlar zaten oyunun beles verd�g� seyler sat�n al�nmaz ama kayded�l�r
                satinAlmaText.text = sat�nAlindiText;
                islemButtonlar[0].interactable = false;
                if (sopaIndex == bellekY�netimi.VeriyiGetir_int("AktifSopa"))
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
            // ortalardaysa orneg�n 4 sapka varsa 2 deyse 3e gecmek �c�n once 2 kapat�l�r sonra 3e gec�l�r.
            else
            {
                sopalar[sopaIndex].gameObject.SetActive(false);
                sopaIndex++;
                sopalar[sopaIndex].gameObject.SetActive(true);
                sopaDurumText.text = itemBilgileri[sopaIndex + 3].itemAdi;

                //Bunu yapma neden�m sec�len objeler�n sat�l al�n�p al�nmad�g�n� kontrol etmek �c�n
                if (!itemBilgileri[sopaIndex + 3].satildiAlmaDurumu)
                {
                    satinAlmaText.text = itemBilgileri[sopaIndex + 3].puan + "- " + sat�nAlmaText;
                    kaydetmeText.text = kaydetmeDurumuText;
                    islemButtonlar[1].interactable = false;
                    // ben�m e�er puan�m sat�n al�cag�m seyden kucuk �se butonu �nakt�f ed�yorum
                    if (itemBilgileri[sopaIndex + 3].puan > bellekY�netimi.VeriyiGetir_int("Puan"))
                        islemButtonlar[0].interactable = false;

                    else
                        islemButtonlar[0].interactable = true;
                }
                else
                {
                    satinAlmaText.text = sat�nAlindiText;
                    islemButtonlar[0].interactable = false;
                    if (sopaIndex == bellekY�netimi.VeriyiGetir_int("AktifSopa"))
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
            // Ba�taysa ya da oyuna hi� girmemi� ise
            if (sopaIndex == -1)
            {
                sopaIndex = sapkalar.Length - 1;
                sopalar[sopaIndex].gameObject.SetActive(true);
                sopaDurumText.text = itemBilgileri[sopaIndex + 3].itemAdi;

                //Bunu yapma neden�m sec�len objeler�n sat�l al�n�p al�nmad�g�n� kontrol etmek �c�n
                if (!itemBilgileri[sopaIndex + 3].satildiAlmaDurumu)
                {
                    satinAlmaText.text = itemBilgileri[sopaIndex + 3].puan + "- " + sat�nAlmaText;
                    kaydetmeText.text = kaydetmeDurumuText;
                    islemButtonlar[1].interactable = false;
                    // ben�m e�er puan�m sat�n al�cag�m seyden kucuk �se butonu �nakt�f ed�yorum
                    if (itemBilgileri[sopaIndex + 3].puan > bellekY�netimi.VeriyiGetir_int("Puan"))
                        islemButtonlar[0].interactable = false;

                    else
                        islemButtonlar[0].interactable = true;
                }
                else
                {
                    satinAlmaText.text = sat�nAlindiText;
                    islemButtonlar[0].interactable = false;
                    if (sopaIndex == bellekY�netimi.VeriyiGetir_int("AktifSopa"))
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
            // ortalardaysa orneg�n 4 sapka varsa 2 deyse 1e gecmek �c�n once 2 kapat�l�r sonra 1e gec�l�r.
            else
            {
                sopalar[sopaIndex].gameObject.SetActive(false);
                sopaIndex--;
                // mesela �ndex 1 se buraya g�rerse -1 olur ve -1de ac�l�cak b�r sey yok ondan else yazd�m
                if (sopaIndex != -1)
                {
                    sopalar[sopaIndex].gameObject.SetActive(true);
                    sopaDurumText.text = itemBilgileri[sopaIndex + 3].itemAdi;

                    //Bunu yapma neden�m sec�len objeler�n sat�l al�n�p al�nmad�g�n� kontrol etmek �c�n
                    if (!itemBilgileri[sopaIndex + 3].satildiAlmaDurumu)
                    {
                        satinAlmaText.text = itemBilgileri[sopaIndex + 3].puan + "- " + sat�nAlmaText;
                        kaydetmeText.text = kaydetmeDurumuText;
                        islemButtonlar[1].interactable = false;
                        // ben�m e�er puan�m sat�n al�cag�m seyden kucuk �se butonu �nakt�f ed�yorum
                        if (itemBilgileri[sopaIndex + 3].puan > bellekY�netimi.VeriyiGetir_int("Puan"))
                            islemButtonlar[0].interactable = false;

                        else
                            islemButtonlar[0].interactable = true;
                    }
                    else
                    {
                        satinAlmaText.text = sat�nAlindiText;
                        islemButtonlar[0].interactable = false;
                        if (sopaIndex == bellekY�netimi.VeriyiGetir_int("AktifSopa"))
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

                    //Bunu yapma neden�m sec�len objeler�n sat�l al�n�p al�nmad�g�n� kontrol etmek �c�n burda �f else gerek yok cunku
                    // -1e gel�rse default seyler gel�cek onlar zaten oyunun beles verd�g� seyler sat�n al�nmaz ama kayded�l
                    satinAlmaText.text = sat�nAlindiText;
                    islemButtonlar[0].interactable = false;
                    if (sopaIndex == bellekY�netimi.VeriyiGetir_int("AktifSopa"))
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

    // saga sola g�tt�g�m�z yer var ya oyun �lk ac�ld�g�nda c�kan ekranda karakter k�sm�na bas�nca sag sol oklar var yukar�dak� met�nde
    // oras� bu
    public void MaterialKarakter_YonButonlari(string islem)
    {
        ses.clip = sesler[0];
        ses.Play();
        if (islem == "ileri")
        {
            // ba�ta ya da sona geldiyse ve sondan sonra -1 olucag� �c�n b�r sey kapatmas�na gerek olmad�g� �c�n �unku -1de sapka yok
            if (karakterIndex == 0)
            {
                karakterIndex++;
                Material[] mats = rendererKarakter.materials;
                // varsay�lan materyal� ekled�m
                mats[0] = materialKarakter[karakterIndex];
                rendererKarakter.materials = mats;
                karakterDurumText.text = itemBilgileri[karakterIndex + 6].itemAdi;

                //Bunu yapma neden�m sec�len objeler�n sat�l al�n�p al�nmad�g�n� kontrol etmek �c�n
                if (!itemBilgileri[karakterIndex + 6].satildiAlmaDurumu)
                {
                    satinAlmaText.text = itemBilgileri[karakterIndex + 6].puan + "- " + sat�nAlmaText;
                    kaydetmeText.text = kaydetmeDurumuText;
                    islemButtonlar[1].interactable = false;
                    // ben�m e�er puan�m sat�n al�cag�m seyden kucuk �se butonu �nakt�f ed�yorum
                    if (itemBilgileri[karakterIndex + 6].puan > bellekY�netimi.VeriyiGetir_int("Puan"))
                        islemButtonlar[0].interactable = false;

                    else
                        islemButtonlar[0].interactable = true;
                }
                else
                {
                    satinAlmaText.text = sat�nAlindiText;
                    islemButtonlar[0].interactable = false;
                    if (karakterIndex == bellekY�netimi.VeriyiGetir_int("AktifKarakter"))
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
            // son gelmi� ise en ba�a donmesini istiyorum
            else if (karakterIndex == materialKarakter.Length - 1)
            {
                karakterIndex = 0;
                Material[] mats = rendererKarakter.materials;
                // varsay�lan materyal� ekled�m
                mats[0] = materialKarakter[0];
                rendererKarakter.materials = mats;
                karakterDurumText.text = itemBilgileri[karakterIndex + 6].itemAdi;

                //Bunu yapma neden�m sec�len objeler�n sat�l al�n�p al�nmad�g�n� kontrol etmek �c�n burda �f else gerek yok cunku
                // -1e gel�rse default seyler gel�cek onlar zaten oyunun beles verd�g� seyler sat�n al�nmaz ama kayded�l�r
                satinAlmaText.text = sat�nAlindiText;
                islemButtonlar[0].interactable = false;
                if (karakterIndex == bellekY�netimi.VeriyiGetir_int("AktifKarakter"))
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
            // ortalardaysa orneg�n 4 sapka varsa 2 deyse 3e gecmek �c�n once 2 kapat�l�r sonra 3e gec�l�r.
            else
            {
                karakterIndex++;
                Material[] mats = rendererKarakter.materials;
                // varsay�lan materyal� ekled�m
                mats[0] = materialKarakter[karakterIndex];
                rendererKarakter.materials = mats;
                karakterDurumText.text = itemBilgileri[karakterIndex + 6].itemAdi;

                //Bunu yapma neden�m sec�len objeler�n sat�l al�n�p al�nmad�g�n� kontrol etmek �c�n
                if (!itemBilgileri[karakterIndex + 6].satildiAlmaDurumu)
                {
                    satinAlmaText.text = itemBilgileri[karakterIndex + 6].puan + "- " + sat�nAlmaText;
                    kaydetmeText.text = kaydetmeDurumuText;
                    islemButtonlar[1].interactable = false;
                    // ben�m e�er puan�m sat�n al�cag�m seyden kucuk �se butonu �nakt�f ed�yorum
                    if (itemBilgileri[karakterIndex + 6].puan > bellekY�netimi.VeriyiGetir_int("Puan"))
                        islemButtonlar[0].interactable = false;

                    else
                        islemButtonlar[0].interactable = true; ;
                }
                else
                {
                    satinAlmaText.text = sat�nAlindiText;
                    islemButtonlar[0].interactable = false;
                    if (karakterIndex == bellekY�netimi.VeriyiGetir_int("AktifKarakter"))
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
            // Ba�taysa ya da oyuna hi� girmemi� ise
            if (karakterIndex == 0)
            {
                karakterIndex = materialKarakter.Length - 1;

                Material[] mats = rendererKarakter.materials;
                // varsay�lan materyal� ekled�m
                mats[0] = materialKarakter[karakterIndex];
                rendererKarakter.materials = mats;

                karakterDurumText.text = itemBilgileri[karakterIndex + 6].itemAdi;

                //Bunu yapma neden�m sec�len objeler�n sat�l al�n�p al�nmad�g�n� kontrol etmek �c�n
                if (!itemBilgileri[karakterIndex + 6].satildiAlmaDurumu)
                {
                    satinAlmaText.text = itemBilgileri[karakterIndex + 6].puan + "- " + sat�nAlmaText;
                    kaydetmeText.text = kaydetmeDurumuText;
                    islemButtonlar[1].interactable = false;
                    // ben�m e�er puan�m sat�n al�cag�m seyden kucuk �se butonu �nakt�f ed�yorum
                    if (itemBilgileri[karakterIndex + 6].puan > bellekY�netimi.VeriyiGetir_int("Puan"))
                        islemButtonlar[0].interactable = false;

                    else
                        islemButtonlar[0].interactable = true;
                }
                else
                {
                    satinAlmaText.text = sat�nAlindiText;
                    islemButtonlar[0].interactable = false;
                    if (karakterIndex == bellekY�netimi.VeriyiGetir_int("AktifKarakter"))
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
            // ortalardaysa orneg�n 4 sapka varsa 2 deyse 1e gecmek �c�n once 2 kapat�l�r sonra 1e gec�l�r.
            else
            {
                karakterIndex--;
                // mesela �ndex 1 se buraya g�rerse -1 olur ve -1de ac�l�cak b�r sey yok ondan else yazd�m onun �c�n var yan�
                if (karakterIndex != 0)
                {
                    Material[] mats = rendererKarakter.materials;
                    // varsay�lan materyal� ekled�m
                    mats[0] = materialKarakter[karakterIndex];
                    rendererKarakter.materials = mats;

                    karakterDurumText.text = itemBilgileri[karakterIndex + 6].itemAdi;

                    //Bunu yapma neden�m sec�len objeler�n sat�l al�n�p al�nmad�g�n� kontrol etmek �c�n
                    if (!itemBilgileri[karakterIndex + 6].satildiAlmaDurumu)
                    {
                        satinAlmaText.text = itemBilgileri[karakterIndex + 6].puan + "- " + sat�nAlmaText;
                        kaydetmeText.text = kaydetmeDurumuText;
                        islemButtonlar[1].interactable = false;
                        // ben�m e�er puan�m sat�n al�cag�m seyden kucuk �se butonu �nakt�f ed�yorum
                        if (itemBilgileri[karakterIndex + 6].puan > bellekY�netimi.VeriyiGetir_int("Puan"))
                            islemButtonlar[0].interactable = false;

                        else
                            islemButtonlar[0].interactable = true;
                    }
                    else
                    {
                        satinAlmaText.text = sat�nAlindiText;
                        islemButtonlar[0].interactable = false;
                        if (karakterIndex == bellekY�netimi.VeriyiGetir_int("AktifKarakter"))
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
                    // varsay�lan materyal� ekled�m
                    mats[0] = materialKarakter[karakterIndex];
                    rendererKarakter.materials = mats;

                    karakterDurumText.text =itemDurumText;

                    //Bunu yapma neden�m sec�len objeler�n sat�l al�n�p al�nmad�g�n� kontrol etmek �c�n burda �f else gerek yok cunku
                    // -1e gel�rse default seyler gel�cek onlar zaten oyunun beles verd�g� seyler sat�n al�nmaz ama kayded�l
                    satinAlmaText.text = sat�nAlindiText;
                    islemButtonlar[0].interactable = false;
                    if (karakterIndex == bellekY�netimi.VeriyiGetir_int("AktifKarakter"))
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


    // Kaydedilen bir �ey var m� yok mu onun kontrol ed�ld�g� yer
    public void DurumuKontrolEt(int bolum, bool islem = false)
    {
        // Sapkaya t�klarsa buras�
        if (bolum == 0)
        {
            if (bellekY�netimi.VeriyiGetir_int("AktifSapka") == -1)
            {
                // e�er daha �nce hi� girilmemi� yada girip sonra -1 olarak kalm�� ise t�m aktif objeleri kapat
                foreach (var item in sapkalar)
                {
                    item.gameObject.SetActive(false);
                }
                if (!islem)
                {
                    sapkaDurumText.text = itemDurumText;

                    //Bunu yapma neden�m sec�len objeler�n sat�l al�n�p al�nmad�g�n� kontrol etmek �c�n burda �f else gerek yok cunku
                    // -1e gel�rse default seyler gel�cek onlar zaten oyunun beles verd�g� seyler sat�n al�nmaz ama kayded�l�r
                    satinAlmaText.text = sat�nAlindiText;
                    islemButtonlar[0].interactable = false;
                    // e�er kaydedilen obje ise kaydetme butonunun gorunurlugunu �nakt�f et
                    if (sapkaIndex == bellekY�netimi.VeriyiGetir_int("AktifSapka"))
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
                Oncekileri�naktifEt(sapkalar);
                sapkaIndex = bellekY�netimi.VeriyiGetir_int("AktifSapka");
                sapkalar[sapkaIndex].gameObject.SetActive(true);
                satinAlmaText.text = sat�nAlindiText;
                sapkaDurumText.text = itemBilgileri[sapkaIndex].itemAdi;
                islemButtonlar[0].interactable = false;
                // e�er kaydedilen obje ise kaydetme butonunun gorunurlugunu �nakt�f et
                if (sapkaIndex == bellekY�netimi.VeriyiGetir_int("AktifSapka"))
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

        //Sopaya t�klarsa buras�
        else if (bolum == 1)
        {
            if (bellekY�netimi.VeriyiGetir_int("AktifSopa") == -1)
            {
                // e�er daha �nce hi� girilmemi� yada girip sonra -1 olarak kalm�� ise t�m aktif objeleri kapat
                foreach (var item in sopalar)
                {
                    item.gameObject.SetActive(false);
                }
                if (!islem)
                {
                    sopaDurumText.text = itemDurumText;

                    //Bunu yapma neden�m sec�len objeler�n sat�l al�n�p al�nmad�g�n� kontrol etmek �c�n burda �f else gerek yok cunku
                    // -1e gel�rse default seyler gel�cek onlar zaten oyunun beles verd�g� seyler sat�n al�nmaz ama kayded�l�r
                    satinAlmaText.text = sat�nAlindiText;
                    islemButtonlar[0].interactable = false;
                    // e�er kaydedilen obje ise kaydetme butonunun gorunurlugunu �nakt�f et
                    if (sopaIndex == bellekY�netimi.VeriyiGetir_int("AktifSopa"))
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
                Oncekileri�naktifEt(sapkalar);
                sopaIndex = bellekY�netimi.VeriyiGetir_int("AktifSopa");
                sopalar[sopaIndex].gameObject.SetActive(true);
                satinAlmaText.text =sat�nAlindiText;
                sopaDurumText.text = itemBilgileri[sopaIndex + 3].itemAdi;
                islemButtonlar[0].interactable = false;
                // e�er kaydedilen obje ise kaydetme butonunun gorunurlugunu �nakt�f et
                if (sopaIndex == bellekY�netimi.VeriyiGetir_int("AktifSopa"))
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
        // karaktere t�klarsa buras�
        else
        {
            if (bellekY�netimi.VeriyiGetir_int("AktifKarakter") == 0)
            {
                Material[] mats = rendererKarakter.materials;
                // varsay�lan materyal� ekled�m
                mats[0] = materialKarakter[0];
                rendererKarakter.materials = mats;
                if (!islem)
                {
                    karakterDurumText.text =itemDurumText;

                    //Bunu yapma neden�m sec�len objeler�n sat�l al�n�p al�nmad�g�n� kontrol etmek �c�n burda �f else gerek yok cunku
                    // -1e gel�rse default seyler gel�cek onlar zaten oyunun beles verd�g� seyler sat�n al�nmaz ama kayded�l�r
                    satinAlmaText.text = sat�nAlindiText;
                    islemButtonlar[0].interactable = false;
                    // e�er kaydedilen obje ise kaydetme butonunun gorunurlugunu �nakt�f et
                    if (karakterIndex == bellekY�netimi.VeriyiGetir_int("AktifKarakter"))
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
                karakterIndex = bellekY�netimi.VeriyiGetir_int("AktifKarakter");
                Material[] mats = rendererKarakter.materials;
                // varsay�lan materyal� ekled�m
                mats[0] = materialKarakter[karakterIndex];
                rendererKarakter.materials = mats;
                satinAlmaText.text =sat�nAlindiText;
                karakterDurumText.text = itemBilgileri[karakterIndex + 6].itemAdi;
                islemButtonlar[0].interactable = false;
                // e�er kaydedilen obje ise kaydetme butonunun gorunurlugunu �nakt�f et
                if (karakterIndex == bellekY�netimi.VeriyiGetir_int("AktifKarakter"))
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

    public void Sat�nAl()
    {
        ses.clip = sesler[2];
        ses.Play();
        // e�er hi�bir k�sma g�r�lmem�s �se sapka sopa falan if'e girmez -1 oldugu i�in
        if (aktif�slemPaneli�ndex != -1)
        {
            switch (aktif�slemPaneli�ndex)
            {
                case 0:
                    Sat�nAlmaDurumu(sapkaIndex);
                    break;
                case 1:
                    int index = sopaIndex +3;
                    Sat�nAlmaDurumu(index);
                    break;
                case 2:
                    int index2 = karakterIndex + 6;
                    Sat�nAlmaDurumu(index2);
                    break;
            }
        }
    }

    public void Kaydet()
    {
        ses.clip = sesler[1];
        ses.Play();
        if (aktif�slemPaneli�ndex != -1)
        {
            switch (aktif�slemPaneli�ndex)
            {
                case 0:
                    KaydetmeDurumu("AktifSapka", sapkaIndex);
                    break;
                case 1:
                    KaydetmeDurumu("AktifSopa", sopaIndex);
                    break;
                case 2:
                    // materialde onceki karakter� d�sakt�f ed�p yen�s�n� akt�f etme boyle
                    Material[] mats = rendererKarakter.materials;
                    // varsay�lan materyal� ekled�m
                    mats[0] = materialKarakter[karakterIndex];
                    rendererKarakter.materials = mats;
                    //kaydete bast�ktan sonra kayded�len sapkan�n �ndeks�n� kayded�yorumk� ger� c�k�p g�rd�g�mde ayn� yer c�ks�n
                    KaydetmeDurumu("AktifKarakter", karakterIndex);
                    break;
            }
           
        }
    }

    // sahne ac�ld�g�nda c�kan 3 k�s�m sapka sopa karakter varya o butonlar bu fonks�yonda cal�s�yo
    public void �slemPaneliAktifEt(int index)
    {
        ses.clip = sesler[0];
        ses.Play();
        // ortaalan objes�n� akt�f etme
        genelPaneller[0].SetActive(true);
        aktif�slemPaneli�ndex = index;
        // hang� �ndex�n ne oldugunu b�ld�g�m �c�n kolay
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
        islemPanelleri[aktif�slemPaneli�ndex].SetActive(false);
        genelPaneller[0].SetActive(false);
        genelPaneller[1].SetActive(false);
        // e�er kaydet butonuna basmas �se esk� deger neyse onu ac�ga c�kart�yor buras� kaydete bas�p ger� c�karsa once kaydet fonks�yonu sonra buras� cal�s�yo.
        DurumuKontrolEt(aktif�slemPaneli�ndex, true);
        aktif�slemPaneli�ndex = -1;
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
        yield return new WaitForSeconds(ses.clip.length); // Sesin s�resi kadar bekle
        SceneManager.LoadScene(0); // Sahne y�kle
    }



    public void Oncekileri�naktifEt(GameObject[] gameObjects)
    {
        foreach (var item in gameObjects)
        {
            item.gameObject.SetActive(false);
        }
    }

    //--------------------Yard�mc� Fonks�yonlar -----------------
    public void Sat�nAlmaDurumu(int index)
    {
        // sat�n al�nan objey� true yap�yorum
        itemBilgileri[index].satildiAlmaDurumu = true;
        bellekY�netimi.VeriyiKaydet_int("Puan", bellekY�netimi.VeriyiGetir_int("Puan") - itemBilgileri[index].puan);
        satinAlmaText.text = sat�nAlindiText;
        islemButtonlar[0].interactable = false;
        islemButtonlar[1].interactable = true;
        puanText.text = bellekY�netimi.VeriyiGetir_int("Puan").ToString();
    }

    public void KaydetmeDurumu(string adi, int indexi)
    {
        //kaydete bast�ktan sonra kayded�len sapkan�n �ndeks�n� kayded�yorumk� ger� c�k�p g�rd�g�mde ayn� yer c�ks�n
        bellekY�netimi.VeriyiKaydet_int(adi, indexi);
        islemButtonlar[1].interactable = false;
        kaydetmeText.text = kaydedilmeDurumuText;
    }

}
