using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class CraftingManager : MonoBehaviour
{
    private Tamga currentTamga;
    public Image customCursor;

    public List<Tamga> tamgaList;
    public string[] recipes;
    public Tamga[] recipeResults;
    public Slot resultSlot;

    public Slot[] craftingSlots;

    private void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            if(currentTamga != null)
            {
                customCursor.gameObject.SetActive(false);
                Slot nearestSlot = null;
                float shortestDistance = float.MaxValue;

                foreach (Slot slot in craftingSlots)
                {
                    float dist = Vector2.Distance(Input.mousePosition, slot.transform.position);
                    if(dist < shortestDistance)
                    {
                        shortestDistance = dist;
                        nearestSlot = slot;
                    }
                }
                nearestSlot.gameObject.SetActive(true);
                nearestSlot.GetComponent<Image>().sprite = currentTamga.GetComponent<Image>().sprite;
                nearestSlot.tamga = currentTamga;
                tamgaList[nearestSlot.index] = currentTamga;
                currentTamga = null;
                CheckForCompleteRecipes();
            }
        }
    }

    void CheckForCompleteRecipes()
    {
        resultSlot.gameObject.SetActive(false);
        resultSlot.tamga = null;
        string currentRecipeString = "";
        foreach(Tamga tamga in tamgaList)
        {
            if(tamga != null)
            {
                currentRecipeString += tamga.tamgaName;
            }
            else
            {
                currentRecipeString += "null";
            }
        }


        for (int i = 0; i < recipes.Length; i++)
        {
            if(recipes[i] == currentRecipeString)
            {
                Debug.Log(recipes[i]);
                Debug.Log(currentRecipeString);
               // Debug.Log(resultSlot.tamga.name);
                resultSlot.gameObject.SetActive(true);
                resultSlot.GetComponent<Image>().sprite = recipeResults[i].GetComponent<Image>().sprite;         
                resultSlot.tamga = recipeResults[i];
            }
        }



    }


    public void OnClickSlot(Slot slot)
    {
        slot.tamga = null;
        tamgaList[slot.index] = null;
        slot.gameObject.SetActive(false);
        CheckForCompleteRecipes();
    }
    public void OnMouseDownTamga(Tamga tamga)
    {
        if(currentTamga == null)
        {
            currentTamga = tamga;
            customCursor.gameObject.SetActive(true);
            customCursor.sprite = currentTamga.GetComponent<Image>().sprite;

        }
    }
}
