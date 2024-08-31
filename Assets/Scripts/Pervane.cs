using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pervane : MonoBehaviour
{
    public Animator _animator;
    public float beklemeSüresi;
    public BoxCollider rüzgarCollider;

    // anýmatýon kýsmýnda bu obje'nýn en son kýsmýna bir false deger gonderýyorum.oyun çalýþýr çalýþmaz anýmasyon çalýsýyor zaten 
    //animasyonunun en son kýsmýnda yaný býttýgýnde anýmasyon bu fonksýyon çalýþýyor boylece ýstedýgýmýz olayý yakalýyoruz
    public void AnimasyonDurumu(string durum)
    {
        // anýmasyonu çalýstýr
        if (durum == "true")
        {
            _animator.SetBool("Calistir", true);
            // animasyon çalýþýrken rüzgar colliderýný aktýf et ki sadece panel anýmasyonu calýsýrken alt karakterler ölsün
            rüzgarCollider.enabled = true;
        }
        // anýmasyonu durdur
        else
        {
            _animator.SetBool("Calistir", false);
            StartCoroutine(AnimasyonTetikle());
            rüzgarCollider.enabled = false;
        }

    
    }

    //  belli saniyeler arasýnda anýmasyonumun çalýþmasýný ýstýyorum bu  yuzden bunu yazdým

     IEnumerator AnimasyonTetikle()
    {
        yield return new WaitForSeconds(beklemeSüresi);
        AnimasyonDurumu("true");
    }

    
}
