using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public Transform target;
    public Vector3 target_offset;

    //  savas alan�na g�rd�kten sonra kamera'n�n alacag� konum
    private Vector3 sonKonum;
    public bool alanaGirdiMi=false;

    void Start()
    {
        sonKonum = new Vector3(0f, 3.2f, -13f);
        target_offset =  transform.position - target.position ;

    }

    // takip sistemleri LateUpdate i�erisine yaz�l�r
    void LateUpdate()
    {
        if (!alanaGirdiMi)
        {
            transform.position = Vector3.Lerp(transform.position, target_offset + target.position, 0.100f);
        }
        else
        {
            transform.position = Vector3.Lerp(transform.position, sonKonum, 0.125f);
        }
 
    }
}
