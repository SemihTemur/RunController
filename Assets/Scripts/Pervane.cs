using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pervane : MonoBehaviour
{
    public Animator _animator;
    public float beklemeS�resi;
    public BoxCollider r�zgarCollider;

    // an�mat�on k�sm�nda bu obje'n�n en son k�sm�na bir false deger gonder�yorum.oyun �al���r �al��maz an�masyon �al�s�yor zaten 
    //animasyonunun en son k�sm�nda yan� b�tt�g�nde an�masyon bu fonks�yon �al���yor boylece �sted�g�m�z olay� yakal�yoruz
    public void AnimasyonDurumu(string durum)
    {
        // an�masyonu �al�st�r
        if (durum == "true")
        {
            _animator.SetBool("Calistir", true);
            // animasyon �al���rken r�zgar collider�n� akt�f et ki sadece panel an�masyonu cal�s�rken alt karakterler �ls�n
            r�zgarCollider.enabled = true;
        }
        // an�masyonu durdur
        else
        {
            _animator.SetBool("Calistir", false);
            StartCoroutine(AnimasyonTetikle());
            r�zgarCollider.enabled = false;
        }

    
    }

    //  belli saniyeler aras�nda an�masyonumun �al��mas�n� �st�yorum bu  yuzden bunu yazd�m

     IEnumerator AnimasyonTetikle()
    {
        yield return new WaitForSeconds(beklemeS�resi);
        AnimasyonDurumu("true");
    }

    
}
