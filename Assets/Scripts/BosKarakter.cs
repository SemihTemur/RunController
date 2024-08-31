using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static UnityEditor.Progress;

public class BosKarakter : MonoBehaviour
{
    public SkinnedMeshRenderer _Renderer;
    public Material karakter_Material;
    public Animator animator;
    public NavMeshAgent navMashAgent;
    public GameObject target;
    public GameManager gameManager;

    public bool carpistiMi = false;

    Vector3 obstaclePosition;

    void LateUpdate()
    {
        if (carpistiMi)
            navMashAgent.SetDestination(target.transform.position);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Karakter") || other.CompareTag("AltKarakterler"))
        {
            if (gameObject.CompareTag("BosKarakter"))
            {
                MateryaliDe�istir();
                carpistiMi = true;
                gameManager.karakterHavuzu.Add(gameObject);
                Debug.Log(gameManager.karakterHavuzu.Count);
                GetComponent<AudioSource>().Play();
            }

        }

        else if (other.gameObject.CompareTag("i�neliKutu"))
        {
            obstaclePosition = transform.position + new Vector3(0f, 0.7f, 0f);
            gameManager.YokOlmaEfektiOlu�tur(obstaclePosition,false,false);
            transform.gameObject.SetActive(false);
            Debug.Log(GameManager.currentCharacterCount);
        }
        else if (other.gameObject.CompareTag("i�ne"))
        {
            obstaclePosition = transform.position + new Vector3(0f, 1f, 0f);
            gameManager.YokOlmaEfektiOlu�tur(obstaclePosition);
            transform.gameObject.SetActive(false);
            Debug.Log(GameManager.currentCharacterCount);

        }

        else if (other.gameObject.CompareTag("Testere"))
        {
            obstaclePosition = transform.position + new Vector3(0f, 1.5f, 0f);
            gameManager.YokOlmaEfektiOlu�tur(obstaclePosition);
            transform.gameObject.SetActive(false);
        }

        else if (other.gameObject.CompareTag("Pervaneigneleri"))
        {
            obstaclePosition = transform.position + new Vector3(0f, 1f, 0f);
            gameManager.YokOlmaEfektiOlu�tur(obstaclePosition);
            transform.gameObject.SetActive(false);
        }

        else if (other.gameObject.CompareTag("Balyoz"))
        {
            Debug.Log("Balyozx");
            obstaclePosition = transform.position + new Vector3(0f, 1f, 0f);
            gameManager.YokOlmaEfektiOlu�tur(obstaclePosition, true);
            transform.gameObject.SetActive(false);
        }

        else if (other.gameObject.CompareTag("Dusman"))
        {
            obstaclePosition = transform.position + new Vector3(0f, 1f, 0f);
            gameManager.YokOlmaEfektiOlu�tur(obstaclePosition, false, false);
            transform.gameObject.SetActive(false);
        }

    }


    public void MateryaliDe�istir()
    {
        // _Renderer.materials[0] = karakter_Material b�yle �al��maz nedeni a�a��da

        // Unity'nin i� mekanizmalar� gere�i, materials dizisi bir kopya d�nd�r�r ve do�rudan bu diziyi de�i�tirirseniz,
        // de�i�iklikler Unity'nin dahili sistemine yans�maz. Ancak, kopyay� de�i�tirip geri atad���n�zda, Unity bu yeni diziyi dahili sistemine uygular."
        Material[] m = _Renderer.materials;
        m[0] = karakter_Material;
        _Renderer.materials = m;

        gameObject.tag = "AltKarakterler";
        GameManager.currentCharacterCount++;
        Debug.Log(GameManager.currentCharacterCount);

        AnimasyonuTetikle();
    }

    public void AnimasyonuTetikle()
    {
        animator.SetBool("Saldir", true);
    }


}
