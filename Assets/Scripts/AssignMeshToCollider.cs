using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class AssignMeshToCollider : MonoBehaviour
{
#if UNITY_EDITOR
    private void OnValidate()
    {
        MeshFilter[] meshFilters = FindObjectsOfType<MeshFilter>();
        foreach (MeshFilter meshFilter in meshFilters)
        {
            MeshCollider meshCollider = meshFilter.GetComponent<MeshCollider>();
            if (meshCollider)
            {
                meshCollider.sharedMesh = meshFilter.sharedMesh;
            }
        }
    }
#endif
}