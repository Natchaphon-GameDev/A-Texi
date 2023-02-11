using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyboxController : MonoBehaviour
{
    [SerializeField] private Material[] skyBoxes;

    private void Start()
    {
        var rnd = Random.Range(0, skyBoxes.Length);
        RenderSettings.skybox = skyBoxes[rnd];
    }
}
