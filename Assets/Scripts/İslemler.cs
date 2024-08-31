using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using GoogleMobileAds.Api;


namespace Semih
{
    public class İslemler
    {
        public int yedek = 1;

        public void Toplama(int sayı, List<GameObject> karakterHavuzu, Transform işlemPozisyonu, List<GameObject> oluşmaEfektleri)
        {
            Debug.Log("toplamada" + GameManager.currentCharacterCount);
            foreach (var item in karakterHavuzu)
            {

                if (GameManager.currentCharacterCount + sayı > karakterHavuzu.Count)
                {
                    if (!item.activeInHierarchy)
                    {
                        foreach (var item2 in oluşmaEfektleri)
                        {
                            if (!item2.activeInHierarchy)
                            {
                                item2.gameObject.SetActive(true);
                                item2.transform.position = işlemPozisyonu.transform.position - new Vector3(0, 0.4f, 0);
                                item2.gameObject.transform.GetComponent<ParticleSystem>().Play();
                                item2.GetComponent<AudioSource>().Play();
                                break;
                            }
                        }
                        Debug.Log(işlemPozisyonu.position);
                        item.transform.position = işlemPozisyonu.position - new Vector3(0, 0, 0.1f);
                        item.gameObject.SetActive(true);
                    }

                    GameManager.currentCharacterCount = karakterHavuzu.Count + 1;
                }

                else
                {

                    if (yedek <= sayı)
                    {
                        if (!item.activeInHierarchy)
                        {
                            foreach (var item2 in oluşmaEfektleri)
                            {
                                if (!item2.activeInHierarchy)
                                {
                                    item2.gameObject.SetActive(true);
                                    item2.transform.position = işlemPozisyonu.transform.position - new Vector3(0, 0.4f, 0);
                                    item2.gameObject.transform.GetComponent<ParticleSystem>().Play();
                                    item2.GetComponent<AudioSource>().Play();
                                    break;
                                }
                            }
                            Debug.Log(işlemPozisyonu.position);
                            item.transform.position = işlemPozisyonu.position;
                            item.gameObject.SetActive(true);
                            yedek++;
                        }

                    }

                    else
                    {
                        GameManager.currentCharacterCount += yedek - 1;
                        break;
                    }
                }

            }

            yedek = 1;


        }

        public void Çıkarma(int sayı, List<GameObject> karakterHavuzu, List<GameObject> yokOlmaEfektleri)
        {
            Debug.Log("toplamada" + GameManager.currentCharacterCount);
            foreach (var item in karakterHavuzu)
            {
                // 10 15, 20 25 
                if (GameManager.currentCharacterCount <= sayı)
                {
                    if (yedek < GameManager.currentCharacterCount)
                    {

                        if (item.activeInHierarchy)
                        {
                            foreach (var item2 in yokOlmaEfektleri)
                            {
                                if (!item2.activeInHierarchy)
                                {
                                    item2.gameObject.SetActive(true);
                                    item2.transform.position = item.transform.position + new Vector3(0, 0.6f, 0);
                                    item2.gameObject.transform.GetComponent<ParticleSystem>().Play();
                                    item2.gameObject.transform.GetComponent<AudioSource>().Play();
                                    break;
                                }
                            }
                            item.transform.position = Vector3.zero;
                            item.gameObject.SetActive(false);
                            yedek++;
                        }
                    }

                    else
                    {
                        GameManager.currentCharacterCount = 1;
                        break;
                    }

                }

                //20karakterım var 10 tane silmem lazım
                else
                {
                    Debug.Log("yedek" + yedek);
                    if (yedek <= sayı)
                    {
                        if (item.activeInHierarchy)
                        {
                            foreach (var item2 in yokOlmaEfektleri)
                            {
                                if (!item2.activeInHierarchy)
                                {
                                    item2.gameObject.SetActive(true);
                                    item2.transform.position = item.transform.position + new Vector3(0, 0.6f, 0);
                                    item2.gameObject.transform.GetComponent<ParticleSystem>().Play();
                                    item2.gameObject.transform.GetComponent<AudioSource>().Play();
                                    break;
                                }
                            }
                            item.transform.position = Vector3.zero;
                            item.gameObject.SetActive(false);
                            yedek++;
                        }
                    }

                    else
                    {
                        yedek--;
                        GameManager.currentCharacterCount = GameManager.currentCharacterCount - yedek;
                        break;
                    }
                }


            }

            yedek = 1;

        }

        public void Çarpma(int sayı, List<GameObject> karakterHavuzu, Transform işlemPozisyonu, List<GameObject> oluşmaEfektleri)
        {
            Debug.Log("toplamada" + GameManager.currentCharacterCount);
            foreach (var item in karakterHavuzu)
            {

                int hesap = (GameManager.currentCharacterCount * sayı) - GameManager.currentCharacterCount;

                if (karakterHavuzu.Count <= hesap)
                {
                    if (!item.activeInHierarchy)
                    {
                        foreach (var item2 in oluşmaEfektleri)
                        {
                            if (!item2.activeInHierarchy)
                            {
                                item2.gameObject.SetActive(true);
                                item2.transform.position = işlemPozisyonu.transform.position - new Vector3(0, 0.4f, 0);
                                item2.gameObject.transform.GetComponent<ParticleSystem>().Play();
                                item2.GetComponent<AudioSource>().Play();
                                break;
                            }
                        }
                        item.transform.position = işlemPozisyonu.position - new Vector3(0, 0, 0.1f);
                        item.gameObject.SetActive(true);
                    }
                    GameManager.currentCharacterCount = karakterHavuzu.Count + 1;
                }
                else
                {
                    if (yedek <= hesap)
                    {
                        if (!item.activeInHierarchy)
                        {
                            foreach (var item2 in oluşmaEfektleri)
                            {
                                if (!item2.activeInHierarchy)
                                {
                                    item2.gameObject.SetActive(true);
                                    item2.transform.position = işlemPozisyonu.transform.position - new Vector3(0, 0.4f, 0);
                                    item2.gameObject.transform.GetComponent<ParticleSystem>().Play();
                                    item2.GetComponent<AudioSource>().Play();
                                    break;
                                }
                            }
                            item.transform.position = işlemPozisyonu.position - new Vector3(0, 0, 0.1f);
                            item.gameObject.SetActive(true);
                            yedek++;
                        }

                    }

                    else
                    {
                        GameManager.currentCharacterCount += yedek - 1;

                        break;
                    }
                }

            }


            yedek = 1;

        }


        public void Bölme(int sayı, List<GameObject> karakterHavuzu, List<GameObject> yokOlmaEfektleri)
        {
            Debug.Log("toplamada" + GameManager.currentCharacterCount);
            foreach (var item in karakterHavuzu)
            {
                // en fazla karakterhavuzum 23 olabılır ya sayımda ondan buyuk olursa  kontrolu bu 23/30 gibi
                if (sayı >= karakterHavuzu.Count)
                {
                    if (item.activeInHierarchy)
                    {
                        foreach (var item2 in yokOlmaEfektleri)
                        {
                            if (!item2.activeInHierarchy)
                            {
                                item2.gameObject.SetActive(true);
                                item2.transform.position = item.transform.position + new Vector3(0, 0.6f, 0);
                                item2.gameObject.transform.GetComponent<ParticleSystem>().transform.position = new Vector3(item2.gameObject.transform.GetComponent<ParticleSystem>().transform.position.x, 1.2f, item2.gameObject.transform.GetComponent<ParticleSystem>().transform.position.z);
                                item2.gameObject.transform.GetComponent<ParticleSystem>().Play();
                                item2.GetComponent<AudioSource>().Play();
                                break;
                            }
                        }
                        item.transform.position = Vector3.zero;
                        item.gameObject.SetActive(false);
                    }
                    GameManager.currentCharacterCount = 1;
                }

                else
                {
                    // bu da sahnemdekı karakter sayım işlem sayımdan kucukse yanı 10/20 gibi
                    if (GameManager.currentCharacterCount <= sayı)
                    {
                        if (item.activeInHierarchy)
                        {
                            foreach (var item2 in yokOlmaEfektleri)
                            {
                                if (!item2.activeInHierarchy)
                                {
                                    item2.gameObject.SetActive(true);
                                    item2.transform.position = item.transform.position + new Vector3(0, 0.6f, 0);
                                    item2.gameObject.transform.GetComponent<ParticleSystem>().transform.position = new Vector3(item2.gameObject.transform.GetComponent<ParticleSystem>().transform.position.x, 1.2f, item2.gameObject.transform.GetComponent<ParticleSystem>().transform.position.z);
                                    item2.gameObject.transform.GetComponent<ParticleSystem>().Play();
                                    item2.gameObject.transform.GetComponent<AudioSource>().Play();
                                    break;
                                }
                            }
                            item.transform.position = Vector3.zero;
                            item.gameObject.SetActive(false);
                        }
                        GameManager.currentCharacterCount = 1;
                    }
                    // sahdemdeki  karakter sayım islem sayımdan buyukse 10 / 5 gibi
                    else
                    {
                        int hesap;
                        hesap = (GameManager.currentCharacterCount) - (GameManager.currentCharacterCount / sayı);
                        Debug.Log(hesap);
                        if (yedek <= hesap)
                        {
                            if (item.activeInHierarchy)
                            {
                                foreach (var item2 in yokOlmaEfektleri)
                                {
                                    if (!item2.activeInHierarchy)
                                    {
                                        item2.gameObject.SetActive(true);
                                        item2.transform.position = item.transform.position + new Vector3(0, 0.6f, 0);
                                        item2.gameObject.transform.GetComponent<ParticleSystem>().transform.position = new Vector3(item2.gameObject.transform.GetComponent<ParticleSystem>().transform.position.x, 1.2f, item2.gameObject.transform.GetComponent<ParticleSystem>().transform.position.z);
                                        item2.gameObject.transform.GetComponent<ParticleSystem>().Play();
                                        item2.gameObject.transform.GetComponent<AudioSource>().Play();
                                        break;
                                    }
                                }
                                item.transform.position = Vector3.zero;
                                item.gameObject.SetActive(false);
                                yedek++;
                            }
                        }
                        else
                        {
                            GameManager.currentCharacterCount = GameManager.currentCharacterCount / sayı;
                            break;
                        }

                    }



                }



            }

            yedek = 1;

        }

    }

    public class BellekYönetimi
    {

        public void VeriyiKaydet_string(string veriAdi, string veri)
        {
            PlayerPrefs.SetString(veriAdi, veri);
            PlayerPrefs.Save();
        }

        public void VeriyiKaydet_int(string veriAdi, int veri)
        {
            PlayerPrefs.SetInt(veriAdi, veri);
            PlayerPrefs.Save();
        }

        public void VeriyiKaydet_float(string veriAdi, float veri)
        {
            PlayerPrefs.SetFloat(veriAdi, veri);
            PlayerPrefs.Save();
        }

        public string VeriyiGetir_string(string key)
        {
            return PlayerPrefs.GetString(key);
        }

        public int VeriyiGetir_int(string key)
        {
            return PlayerPrefs.GetInt(key);
        }

        public float VeriyiGetir_float(string key)
        {
            return PlayerPrefs.GetFloat(key);
        }

        // oyuna ılk gıren bırısı ıcın tanımlanıcak seyler
        public void KontrolEtveTanımla()
        {
            if (!PlayerPrefs.HasKey("SonLevel"))
            {
                PlayerPrefs.SetInt("SonLevel", 5);
                PlayerPrefs.SetInt("Puan", 100);
                PlayerPrefs.SetInt(("AktifSapka"), -1);
                PlayerPrefs.SetInt(("AktifSopa"), -1);
                PlayerPrefs.SetInt(("AktifKarakter"), 0);
                PlayerPrefs.SetFloat(("MenuSes"), 1);
                PlayerPrefs.SetFloat(("MenuFx"), 1);
                PlayerPrefs.SetFloat(("OyunSes"), 1);
                PlayerPrefs.SetString("Dil", "TR");
                PlayerPrefs.SetInt(("GecisReklami"), 1);
            }
        }

    }


    [Serializable]
    public class ItemBilgileri
    {
        public int grupIndex;
        public int itemIndex;
        public string itemAdi;
        public int puan;
        public bool satildiAlmaDurumu;
    }

    [Serializable]
    public class Diller
    {
        public List<DilBilgileri> dilBilgileriTR = new List<DilBilgileri>();
        public List<DilBilgileri> dilBilgileriEN = new List<DilBilgileri>();
    }

    [Serializable]
    public class DilBilgileri
    {
        public string Text;
    }

    public class VeriYonetimi
    {
        List<ItemBilgileri> kullanılacakİtemBilgileri;
        List<Diller> kullanılacakDilVerileri;

        public void Save(List<ItemBilgileri> itemBilgileri)
        {
            //verilierimiz serileştirmemiz ya da serileştirilen veriyi geri eskı degerıne almamız için gereken sınıf
            BinaryFormatter bf = new BinaryFormatter();
            // bu konumda bir dosya olusturuyoruz
            FileStream file = File.OpenWrite(Application.persistentDataPath + "/ItemVerileri.gd");
            // Verilerimiz.puan verisini serileştir ve file dosyasına kaydet
            bf.Serialize(file, itemBilgileri);
            file.Close();
        }

        // kullanacagımızİtemBilgilerini yukluyo bu
        public void Load()
        {
            //bu konumda bu dosya var ise
            if (File.Exists(Application.persistentDataPath + "/ItemVerileri.gd"))
            {
                //verilierimiz serileştirmemiz için gereken sınıf
                BinaryFormatter bf = new BinaryFormatter();
                // dosyanın ıcını acmamızı saglıyo
                FileStream file = File.Open(Application.persistentDataPath + "/ItemVerileri.gd", FileMode.Open);
                // daha once olusturdugumuz dosyayı kı dosyalar serilestırılebılır haldeydı onu deserılestırılebılır yap yanı oncekı degerıne gerı dondur oncenden ıkılı tabanda yanı serılestırılebılırdı
                // tabi hangı degısken tıpı seklınde kaydettıysen gerı dondururken onu yazdırman lazım
                kullanılacakİtemBilgileri = (List<ItemBilgileri>)bf.Deserialize(file);
                file.Close();

            }
        }

        public List<ItemBilgileri> ListeyiAktar()
        {
            return kullanılacakİtemBilgileri;
        }

        // kullanacagımızDilVerilerini yukluyo bu
        public void DilLoad()
        {
            //bu konumda bu dosya var ise
            if (File.Exists(Application.persistentDataPath + "/DilVerileri.gd"))
            {
                //verilierimiz serileştirmemiz için gereken sınıf
                BinaryFormatter bf = new BinaryFormatter();
                // dosyanın ıcını acmamızı saglıyo
                FileStream file = File.Open(Application.persistentDataPath + "/DilVerileri.gd", FileMode.Open);
                // daha once olusturdugumuz dosyayı kı dosyalar serilestırılebılır haldeydı onu deserılestırılebılır yap yanı oncekı degerıne gerı dondur oncenden ıkılı tabanda yanı serılestırılebılırdı
                // tabi hangı degısken tıpı seklınde kaydettıysen gerı dondururken onu yazdırman lazım
                kullanılacakDilVerileri = (List<Diller>)bf.Deserialize(file);
                file.Close();

            }
        }

        public List<Diller> Dili_ListeyeAktar()
        {
            return kullanılacakDilVerileri;
        }

        public void ilkKurulumDosyaOlusturma(List<ItemBilgileri> itemBilgileri, List<Diller> varsayılanDilVerileri)
        {
            // boyle bır dosya ısmı yoksa olustur demek bu
            if (!File.Exists(Application.persistentDataPath + "/ItemVerileri.gd"))
            {
                //verilierimiz serileştirmemiz ya da serileştirilen veriyi geri eskı degerıne almamız için gereken sınıf
                BinaryFormatter bf = new BinaryFormatter();
                // bu konumda bir dosya olusturuyoruz
                FileStream file = File.Create(Application.persistentDataPath + "/ItemVerileri.gd");
                // itemBilgilerini serileştir ve file dosyasına kaydet
                bf.Serialize(file, itemBilgileri);
                file.Close();
            }

            if (!File.Exists(Application.persistentDataPath + "/DilVerileri.gd"))
            {
                //verilierimiz serileştirmemiz ya da serileştirilen veriyi geri eskı degerıne almamız için gereken sınıf
                BinaryFormatter bf = new BinaryFormatter();
                // bu konumda bir dosya olusturuyoruz
                FileStream file = File.Create(Application.persistentDataPath + "/DilVerileri.gd");
                // Verilerimiz.puan verisini serileştir ve file dosyasına kaydet
                bf.Serialize(file, varsayılanDilVerileri);
                file.Close();
            }
        }

    }

    // REKLAM YONETIMI
    public class ReklamYonetimi
    {
        // geçiş reklamini bunun üzerinden yoneticeğim.
        private InterstitialAd interstitial;
        // odullu reklamınıda bunun uzerınden yonetıyoruz
        private RewardedAd rewardedAd;

        // Gecıs reklamı

        // bu fonksıyon ıle googledan bir geçiş reklami istiyoruz.//GECIS REKLAMI
        public void RequestInterstitial()
        {
            // Reklam istemek için gereken kimlik kodları
            string adUnitId;
#if UNITY_ANDROID
            adUnitId = "ca-app-pub-3940256099942544/1033173712";
#elif UNITY_IPHONE
            adUnitId = "ca-app-pub-3940256099942544/4411468910";
#else
            adUnitId = "unexpected-platform";
#endif

            // Bu kimliği vererek geçiş reklamı istiyoruz.
            interstitial = new InterstitialAd(adUnitId);
            // reklam ıstıyoruz
            AdRequest request = new AdRequest.Builder().Build();
            // gelen reklamı yukluyoruz şimdi gelen reklam gosterıme hazır.
            interstitial.LoadAd(request);

            // reklamı kapatma kısmına basınca bu fonksıyon calısıyor dınleyıcı gıbı bır sey
            interstitial.OnAdClosed += GecisReklamiKapatildi;
        }

        public void GecisReklamiKapatildi(object sender,EventArgs args)
        {
            interstitial.Destroy();
            RequestInterstitial();
        }

        public void GecisReklamiGoster()
        {
            // eğer 2 levelde 1 reklam gostermek ıstıyorsan bunu yap
            if (PlayerPrefs.GetInt("GecisReklami")==2)
            {
                // reklam yuklenmısse gosterılmeye hazır ıse
                if (interstitial.IsLoaded())
                {
                    PlayerPrefs.SetInt("GecisReklami", 0);
                    // reklami goster
                    interstitial.Show();
                }
                // eğer yuklenemedıyse teknık arıza falan cıktıysa 
                else
                {
                    // reklamı yok et ve tekrar goster
                    interstitial.Destroy();
                    RequestInterstitial();
                }
            }
            else
            {
                PlayerPrefs.SetInt("GecisReklami", PlayerPrefs.GetInt("GecisReklami")+1);
            }
         
        }



        // ODULLU REKLAM

        // googledan odullu reklam ıstıyorum
        public void RequestRewardedAd()
        {
            // Reklam istemek için gereken kimlik kodları
            string adUnitId;
#if UNITY_ANDROID
            adUnitId = "ca-app-pub-3940256099942544/5224354917";
#elif UNITY_IPHONE
            adUnitId = "ca-app-pub-3940256099942544/1712485313";
#else
            adUnitId = "unexpected-platform";
#endif

            // Bu kimliği vererek odullu reklamı istiyoruz.
            rewardedAd = new RewardedAd(adUnitId);
            //odullu reklam ıstıyoruz
            AdRequest request = new AdRequest.Builder().Build();
            // gelen reklamı yukluyoruz şimdi gelen odullu reklam gosterıme hazır.
            rewardedAd.LoadAd(request);

            rewardedAd.OnUserEarnedReward += OdulluReklamTamamlandi;
            // reklamı kapatma kısmına basınca bu fonksıyon calısıyor dınleyıcı gıbı bır sey
            rewardedAd.OnAdClosed += OdulluReklamKapatildi;
            rewardedAd.OnAdLoaded += OdulluReklamYuklendi;
        }

        public void OdulluReklamTamamlandi(object sender, Reward e)
        {
            string type = e.Type;
            double amount = e.Amount;

            Debug.Log("Odul Alınsın :" + type + " -- " + amount);
        }

        public void OdulluReklamKapatildi(object sender, EventArgs args)
        {
            Debug.Log("reklam Kapatıldı");
            rewardedAd.Destroy();
            // yenı bır reklam ıstıyorum
            RequestRewardedAd();
        } 

        public void OdulluReklamYuklendi(object sender, EventArgs args)
        {
            Debug.Log("reklam Yüklendi");
        }

        public void OdulluReklamiGoster()
        {
            if (rewardedAd.IsLoaded())
            {
                rewardedAd.Show();
            }
            else
            {
                rewardedAd.Destroy();
                RequestRewardedAd();
            }
        }

    }

}
