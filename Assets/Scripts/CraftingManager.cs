using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System.Linq;
using System.Runtime.CompilerServices;

public class CraftingManager : MonoBehaviour
{

    private Tamga currentTamga;
    public Image customCursor;

    public List<Tamga> tamgaList;
    public List<Tamga> reversedTamgaList;
    //[NamedAttributes(new string[] { "test", "test" })]
    public string[] recipes;
    public string[] reversedRecipes;
    public Tamga[] recipeResults;
    public Slot resultSlot;

    public Slot[] craftingSlots;

    private void Start()
    {
       
        ReverseStringArray();

        //reversedRecipes = recipes.Reverse().ToArray();
    }

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
                reversedTamgaList = tamgaList.AsEnumerable().Reverse().ToList();
                //Debug.Log(tamgaList[0].tamgaName+"+"+tamgaList[1].tamgaName);
                //Debug.Log(reversedTamgaList[0].tamgaName + "+"+ reversedTamgaList[1].tamgaName);
                CheckForCompleteRecipes();
            }
        }
    }

    void CheckForCompleteRecipes()
    {
        int index = 0;
        int lenght = tamgaList.Count;
        resultSlot.gameObject.SetActive(false);
        resultSlot.tamga = null;
        string currentRecipeString = "";
        string reverseCurrentRecipeString = "";
        foreach(Tamga tamga in tamgaList)
        {
            if(tamga != null)
            {
                currentRecipeString += tamga.tamgaName;
                if(index != lenght -1)
                {
                    currentRecipeString += "+";
                }
            }
            else
            {
                currentRecipeString += "null";
            }
            index++;
        }
        index = 0;
        Debug.Log(currentRecipeString);


        foreach(Tamga tamg in reversedTamgaList)
        {
            if(tamg != null)
            {
                reverseCurrentRecipeString += tamg.tamgaName;
                if (index != lenght - 1)
                {
                    reverseCurrentRecipeString += "+";
                }

            }
            else
            {
                reverseCurrentRecipeString += "null";
            }
            index++;
        }
        index = 0;
        Debug.Log(reverseCurrentRecipeString);


        for (int i = 0; i < recipes.Length; i++)
        {
            if(recipes[i] == currentRecipeString)
            {
                resultSlot.gameObject.SetActive(true);
                resultSlot.GetComponent<Image>().sprite = recipeResults[i].GetComponent<Image>().sprite;         
                resultSlot.tamga = recipeResults[i];
            }
            else if(reversedRecipes[i] == currentRecipeString)
            {
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

    void ReverseStringArray()
    {
        reversedRecipes = new string[recipes.Length];
        for (int i = 0; i < recipes.Length; i++)
        {

            string[] parts = recipes[i].Split('+');


            string reversedRecipe = string.Join('+', parts.Reverse());

            reversedRecipes[i] = reversedRecipe;
        }
    }

    void ArrayNames()
    {

    }
}
