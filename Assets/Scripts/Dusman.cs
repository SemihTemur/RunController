using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Dusman : MonoBehaviour
{
    public GameObject target;
    public NavMeshAgent navMashAgent;
    public bool isRun;
    public GameManager gameManager;

    void Start()
    {
        isRun = false;
    }


    void LateUpdate()
    {
        if (isRun)
        {
            navMashAgent.SetDestination(target.transform.position);
        }
    }

    public void AnimasyonuTetikle()
    {
        GetComponent<Animator>().SetBool("Saldir", true);
        isRun = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("AltKarakterler"))
        {
            Vector3 obstaclePosition = transform.position + new Vector3(0f, 1f, 0f);
            gameManager.YokOlmaEfektiOluþtur(obstaclePosition, false,true);
            transform.gameObject.SetActive(false);
        }
    }


}
