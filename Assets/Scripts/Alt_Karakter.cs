using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Alt_Karakter : MonoBehaviour
{
    // bu scripte sahip objectnin elemaný prefab oldugu için 
    //  deðiþkenleri public olamaz
    public GameObject target;
    public NavMeshAgent navMesh;

    Vector3 obstaclePosition;

    public GameManager gameManager;

    // Takip Sistemleri LateUpdate içerisine yazýldý
    void LateUpdate()
    {
        navMesh.SetDestination(target.transform.position);
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("iðneliKutu"))
        {
            obstaclePosition=transform.position + new Vector3(0f, 0.7f, 0f);
            gameManager.YokOlmaEfektiOluþtur(obstaclePosition);
            transform.gameObject.SetActive(false);
            Debug.Log(GameManager.currentCharacterCount);
        }
        else if (other.gameObject.CompareTag("iðne"))
        {
            obstaclePosition = transform.position + new Vector3(0f, 1f, 0f);
            gameManager.YokOlmaEfektiOluþtur(obstaclePosition);
            transform.gameObject.SetActive(false);
            Debug.Log(GameManager.currentCharacterCount);

        }

        else if (other.gameObject.CompareTag("Testere"))
        {
            obstaclePosition = transform.position + new Vector3(0f, 1.5f, 0f);
            gameManager.YokOlmaEfektiOluþtur(obstaclePosition);
            transform.gameObject.SetActive(false);
        }

        else if (other.gameObject.CompareTag("Pervaneigneleri"))
        {
            obstaclePosition = transform.position + new Vector3(0f, 1f, 0f);
            gameManager.YokOlmaEfektiOluþtur(obstaclePosition);
            transform.gameObject.SetActive(false);
        }

        else if (other.gameObject.CompareTag("Balyoz"))
        {
            Debug.Log("Balyozx");
            obstaclePosition = transform.position + new Vector3(0f, 1f, 0f);
            gameManager.YokOlmaEfektiOluþtur(obstaclePosition,true);
            transform.gameObject.SetActive(false);
        }

        else if (other.gameObject.CompareTag("Dusman"))
        {
            obstaclePosition = transform.position + new Vector3(0f, 1f, 0f);
            gameManager.YokOlmaEfektiOluþtur(obstaclePosition, false,false);
            transform.gameObject.SetActive(false);
        }
        else if (other.gameObject.CompareTag("BosKarakter"))
        {
            gameManager.karakterHavuzu.Add(other.gameObject);
        }

    }


   


}
