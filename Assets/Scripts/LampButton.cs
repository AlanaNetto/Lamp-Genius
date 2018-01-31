using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class LampButton : MonoBehaviour, IPointerClickHandler
{
    public bool broken = false;
    
    public void OnPointerClick(PointerEventData eventData)
    {
        if (GameController.instante.isUserTurn() && !broken)
        {
            GameController.instante.CheckUserInput(int.Parse(this.gameObject.name));
        }
        else
            Debug.Log("Not user turn");
    }
}
