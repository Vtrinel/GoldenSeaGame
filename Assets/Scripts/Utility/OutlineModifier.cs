using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class OutlineModifier : MonoBehaviour
{
    public Renderer outlineRenderer;

    [Header("First outline values")]
    public Color firstOutlineColor = Color.white;
    public float firstOutlineWidth;

    [Header("Second outline values")]
    public Color secondOutlineColor = Color.white;
    public float secondOutlineWidth;

    [Space(20)]
    [ReadOnly] public int firstOutlineColorID;
    [ReadOnly] public int firstOutlineWidthID;
    [ReadOnly] public int secondOutlineColorID;
    [ReadOnly] public int secondOutlineWidthID;


    void Start()
    {
        ChangeColor();
    }

    [Button]
    public void Configure()
    {
        outlineRenderer = GetComponent<Renderer>();

        firstOutlineColorID = Shader.PropertyToID("_FirstOutlineColor");
        firstOutlineWidthID = Shader.PropertyToID("_FirstOutlineWidth");

        secondOutlineColorID = Shader.PropertyToID("_SecondOutlineColor");
        secondOutlineWidthID = Shader.PropertyToID("_SecondOutlineWidth");

    }

    [Button]
    public void ChangeColor()
    {
        outlineRenderer.material.SetColor(firstOutlineColorID, firstOutlineColor);
        outlineRenderer.material.SetColor(secondOutlineColorID, secondOutlineColor);
    }

    public void ChangeColor(Color _firstOutlineColor, Color _secondOutlineColor)
    {
        outlineRenderer.material.SetColor(firstOutlineColorID, firstOutlineColor);
        outlineRenderer.material.SetColor(secondOutlineColorID, secondOutlineColor);
    }

    [Button]
    public void ChangeWidth()
    {
        outlineRenderer.material.SetFloat(firstOutlineWidthID, firstOutlineWidth);
        outlineRenderer.material.SetFloat(secondOutlineWidthID, secondOutlineWidth);
    }

    public void ChangeWidth(float _firstOutlineWidth, float _secondOutlineWidth)
    {
        outlineRenderer.material.SetFloat(firstOutlineWidthID, _firstOutlineWidth);
        outlineRenderer.material.SetFloat(secondOutlineWidthID, _secondOutlineWidth);
    }
}
