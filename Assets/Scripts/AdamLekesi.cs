using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdamLekesi : MonoBehaviour
{
    // Start methodunu Ienumartor yapab�l�rs�n boyle b�r kullan�mda var
    // alt karakterim �ld�g�nde ��kan lekeyi belli bir s�re sonra gorunurlugunu false yapmak �st�yorum.
    IEnumerator Start()
    {
        yield return new WaitForSeconds(5f);
        transform.gameObject.SetActive(false);
    }
}
