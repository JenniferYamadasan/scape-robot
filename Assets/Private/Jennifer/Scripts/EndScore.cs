using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class EndScore : MonoBehaviour
{

    [SerializeField] PlayerDestroyCounter playerDestroyCounter;

    [SerializeField] List<MeshFilter> counter = new List<MeshFilter>();

    [SerializeField] List<Mesh> meshNumber = new List<Mesh>();


}