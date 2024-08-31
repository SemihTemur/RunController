using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdamLekesi : MonoBehaviour
{
    // Start methodunu Ienumartor yapabýlýrsýn boyle býr kullanýmda var
    // alt karakterim öldügünde çýkan lekeyi belli bir süre sonra gorunurlugunu false yapmak ýstýyorum.
    IEnumerator Start()
    {
        yield return new WaitForSeconds(5f);
        transform.gameObject.SetActive(false);
    }
}
