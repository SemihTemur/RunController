using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Alt_Karakter : MonoBehaviour
{
    // bu scripte sahip objectnin eleman� prefab oldugu i�in 
    //  de�i�kenleri public olamaz
    public GameObject target;
    public NavMeshAgent navMesh;

    Vector3 obstaclePosition;

    public GameManager gameManager;

    // Takip Sistemleri LateUpdate i�erisine yaz�ld�
    void LateUpdate()
    {
        navMesh.SetDestination(target.transform.position);
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("i�neliKutu"))
        {
            obstaclePosition=transform.position + new Vector3(0f, 0.7f, 0f);
            gameManager.YokOlmaEfektiOlu�tur(obstaclePosition);
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
            gameManager.YokOlmaEfektiOlu�tur(obstaclePosition,true);
            transform.gameObject.SetActive(false);
        }

        else if (other.gameObject.CompareTag("Dusman"))
        {
            obstaclePosition = transform.position + new Vector3(0f, 1f, 0f);
            gameManager.YokOlmaEfektiOlu�tur(obstaclePosition, false,false);
            transform.gameObject.SetActive(false);
        }
        else if (other.gameObject.CompareTag("BosKarakter"))
        {
            gameManager.karakterHavuzu.Add(other.gameObject);
        }

    }


   


}
