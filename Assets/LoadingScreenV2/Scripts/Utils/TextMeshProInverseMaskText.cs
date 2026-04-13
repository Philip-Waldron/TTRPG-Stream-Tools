using UnityEngine;
using UnityEngine.Rendering;
using TMPro;

public class TextMeshProInverseMaskText : TextMeshProUGUI
{
    public override Material materialForRendering
    {
        get
        {
            Material material = new(base.materialForRendering);
            material.SetInt("_StencilComp", (int)CompareFunction.NotEqual);
            return material;
        }
    }
}
