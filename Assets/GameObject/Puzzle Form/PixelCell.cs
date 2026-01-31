using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PixelCell : MonoBehaviour
{
    [SerializeField] private EColor _requestedColor;
    private List<EColor> _formColorInCell = new List<EColor>();
    private bool isCellComplete = false;



    public EColor RequestedColor { get => _requestedColor; }
    public bool IsCellComplete { get => isCellComplete; set => isCellComplete = value; }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        FormController form = collision.gameObject.GetComponent<FormController>();

        if (form != null)
            _formColorInCell.Add(form.ActualColor);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        FormController form = collision.gameObject.GetComponent<FormController>();
        if (form != null)
            _formColorInCell.Remove(form.ActualColor);
    }


    public bool CheckColorCondition()
    {
        int colorValue = 0;
        foreach (var color in _formColorInCell)
        {
            colorValue += GameManager.Instance.ColorValueDict[color];
        }
        if (colorValue == GameManager.Instance.ColorValueDict[_requestedColor])
            return true;
        return false;



    }

}
