using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
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
    private Vector3 vettore = new Vector3();
    private GameObject ingredienti;
    private int count = 1;

    private GameObject ingrediente_da_scambiare;
    private GameObject ingrediente_da_scambiare2;
    private int i1, i2, j1, j2;
    private SpriteRenderer ingre1, ingre2, ingre3;
    private bool controllo;

    


    void Start()
    {
        if (altezza != 0 && larghezza != 0)
        {
            mat = new GameObject[altezza, larghezza];
            if (sfondo != null && ingredienti_prefab != null)
            {
                mat_ingredienti = new GameObject[altezza, larghezza];
                ingredienti = new GameObject("Ingredienti");
                SetupMatrici();
                //controllo = ControlloMatch();
                //while(controllo) 
                //{
                 //   controllo=ControlloMatch();
                //}
            }
        }

    }
    private void Update()
    {
        if (ScambiaIngredienti())
        {
            //controllo=ControlloMatch();
            //while (controllo)
            //{
            //    controllo = ControlloMatch();
            //}
        }
    }

    private void SetupMatrici()
    {
        for (int i = 0; i < altezza; i++)
        {
            for (int j = 0; j < larghezza; j++)
            {

                Vector3 vettore_transform = this.transform.position;
                Vector3 vettore_scala = this.transform.localScale;
                Vector3 vettore_scala_ing = sfondo.transform.localScale;
                vettore.x = vettore_transform.x + i;
                vettore.y = vettore_transform.y + j;
                vettore.z = vettore_transform.z;
                mat[i, j] = Instantiate(sfondo, vettore, Quaternion.identity);
                mat[i, j].name = "(" + i + "," + j + ")";
                mat[i, j].transform.parent = this.transform;
                int num = UnityEngine.Random.Range(0, ingredienti_prefab.Length);
                vettore.z = vettore.z - 0.1f;
                mat_ingredienti[i,j] = Instantiate(ingredienti_prefab[num], vettore, Quaternion.identity);
                mat_ingredienti[i,j].name = "Ingrediente #" + count;
                mat_ingredienti[i, j].transform.parent = ingredienti.transform;
                count++;
            }
        }


    }

    public bool ScambiaIngredienti()
    {
        bool Scambiati = false;
        ingrediente_da_scambiare = null;
        ingrediente_da_scambiare2 = null;
        for (int i = 0; i < mat_ingredienti.GetLength(0); i++)
        {
            for (int j = 0; j < mat_ingredienti.GetLength(1); j++)
            {
                if (mat_ingredienti[i, j].GetComponent<MoveIngrediente>().getIsSelected() && ingrediente_da_scambiare == null)
                {
                    ingrediente_da_scambiare = mat_ingredienti[i, j];
                    i1 = i;
                    j1 = j;
                }
                else if (mat_ingredienti[i, j].GetComponent<MoveIngrediente>().getIsSelected() && ingrediente_da_scambiare2 == null)
                {
                    ingrediente_da_scambiare2 = mat_ingredienti[i, j];
                    i2 = i;
                    j2 = j;
                    break;
                }
            }

        }


        if (ingrediente_da_scambiare != null && ingrediente_da_scambiare2 != null)
        {
            ingrediente_da_scambiare.GetComponent<MoveIngrediente>().setIsSelected();
            ingrediente_da_scambiare2.GetComponent<MoveIngrediente>().setIsSelected();

            if (i1 == i2 - 1 || i1 == i2 + 1)
            {
                if (j1 == j2)
                {
                    ingrediente_da_scambiare.GetComponent<MoveIngrediente>().setPosizioneDaCambiare(ingrediente_da_scambiare2.transform.position);
                    ingrediente_da_scambiare2.GetComponent<MoveIngrediente>().setPosizioneDaCambiare(ingrediente_da_scambiare.transform.position);
                    mat_ingredienti[i1, j1] = ingrediente_da_scambiare2;
                    mat_ingredienti[i2, j2] = ingrediente_da_scambiare;
                    Scambiati = true;
                }

            }
            else if (j1 == j2 - 1 || j1 == j2 + 1)
            {
                if (i1 == i2)
                {
                    ingrediente_da_scambiare.GetComponent<MoveIngrediente>().setPosizioneDaCambiare(ingrediente_da_scambiare2.transform.position);
                    ingrediente_da_scambiare2.GetComponent<MoveIngrediente>().setPosizioneDaCambiare(ingrediente_da_scambiare.transform.position);
                    mat_ingredienti[i1, j1] = ingrediente_da_scambiare2;
                    mat_ingredienti[i2, j2] = ingrediente_da_scambiare;
                    Scambiati = true;
                }
            }
        }
        return Scambiati;
    }
    
    public bool ControlloMatch()
    {
        bool Match=false;
        int num;

        for( int i = 0; i < mat_ingredienti.GetLength(0); i++)
        {
            for (int j=0; j+2<mat_ingredienti.GetLength(1); j++)
            {   
                ingre1 = mat_ingredienti[i, j].GetComponent<SpriteRenderer>();
                ingre2= mat_ingredienti[i, j+1].GetComponent<SpriteRenderer>();
                if (ingre2.sprite == ingre1.sprite)
                {
                    ingre3 = mat_ingredienti[i, j+2].GetComponent<SpriteRenderer>();
                    if (ingre3.sprite == ingre2.sprite)
                    {
                        num = UnityEngine.Random.Range(0, ingredienti_prefab.Length);
                        mat_ingredienti[i, j].GetComponent<SpriteRenderer>().sprite= ingredienti_prefab[num].GetComponent<SpriteRenderer>().sprite;

                        num = UnityEngine.Random.Range(0, ingredienti_prefab.Length);
                        mat_ingredienti[i, j+1].GetComponent<SpriteRenderer>().sprite = ingredienti_prefab[num].GetComponent<SpriteRenderer>().sprite;

                        num = UnityEngine.Random.Range(0, ingredienti_prefab.Length);
                        mat_ingredienti[i, j+2].GetComponent<SpriteRenderer>().sprite = ingredienti_prefab[num].GetComponent<SpriteRenderer>().sprite;

                        Match = true;
                        Debug.Log("Match 3 !!");
                        break;
                    }
                    else
                        j++;
                }
            }
        }

        for (int j = 0; j < mat_ingredienti.GetLength(1); j++)
        {
            for (int i = 0; i + 2 < mat_ingredienti.GetLength(0); i++)
            {
                ingre1 = mat_ingredienti[i, j].GetComponent<SpriteRenderer>();
                ingre2 = mat_ingredienti[i + 1, j].GetComponent<SpriteRenderer>();
                if (ingre2.sprite == ingre1.sprite)
                {
                    ingre3 = mat_ingredienti[i + 2, j].GetComponent<SpriteRenderer>();
                    if (ingre3.sprite == ingre2.sprite)
                    {
                        num = UnityEngine.Random.Range(0, ingredienti_prefab.Length);
                        mat_ingredienti[i, j].GetComponent<SpriteRenderer>().sprite = ingredienti_prefab[num].GetComponent<SpriteRenderer>().sprite;

                        num = UnityEngine.Random.Range(0, ingredienti_prefab.Length);
                        mat_ingredienti[i + 1, j].GetComponent<SpriteRenderer>().sprite = ingredienti_prefab[num].GetComponent<SpriteRenderer>().sprite;

                        num = UnityEngine.Random.Range(0, ingredienti_prefab.Length);
                        mat_ingredienti[i + 2, j].GetComponent<SpriteRenderer>().sprite = ingredienti_prefab[num].GetComponent<SpriteRenderer>().sprite;

                        Match = true;
                        Debug.Log("Match 3 !!");
                        break;
                    }
                    else
                        i++;
                }
            }
        }


        return Match;
    }


}