using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class creazioneSfondo : MonoBehaviour
{
    [SerializeField] private int altezza = 1;
    [SerializeField] private int larghezza = 1;
    [SerializeField] public GameObject sfondo;
    [SerializeField] public GameObject[] ingredienti_prefab;
    public GameObject[,] mat;
    public GameObject[,] mat_ingredienti;


    void Start()
    {
        if (altezza != 0 && larghezza != 0)
        {
            mat = new GameObject[altezza, larghezza];
            if (sfondo != null && ingredienti_prefab != null)
            {
                mat_ingredienti = new GameObject[altezza, larghezza];
                SetupMatrici();
            }
        }

    }
    private void SetupMatrici()
    {
        for (int i = 0; i < altezza; i++)
        {
            for (int j = 0; j < larghezza; j++)
            {
                Vector3 vettore = new Vector3();
                Vector3 vettore_transform = this.transform.position;
                Vector3 vettore_scala = this.transform.localScale;
                Vector3 vettore_scala_ing = sfondo.transform.localScale;
                vettore.x = vettore_transform.x + i;
                vettore.y = vettore_transform.y + j;
                vettore.z = vettore_transform.z;
                mat[i, j] = Instantiate(sfondo, vettore, Quaternion.identity);
                mat[i, j].name = "(" + i + "," + j + ")";
                mat[i, j].transform.parent = this.transform;
                int num = Random.Range(0, ingredienti_prefab.Length);
                vettore.z = vettore.z - 0.1f;
                mat_ingredienti[i, j] = Instantiate(ingredienti_prefab[num], vettore, Quaternion.identity);
            }
        }

    }

}