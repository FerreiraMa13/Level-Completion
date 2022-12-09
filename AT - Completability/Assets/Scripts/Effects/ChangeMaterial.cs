using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMaterial : Effect
{
    public Material new_material;
    public GameObject mesh;

    public override void Activate()
    {
        mesh.GetComponent<MeshRenderer>().material = new_material;
    }
}
