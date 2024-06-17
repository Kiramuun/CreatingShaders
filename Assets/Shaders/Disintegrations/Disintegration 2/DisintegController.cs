using System.Collections;
using UnityEngine;
using UnityEngine.VFX;

public class DisintegController : MonoBehaviour
{
    public SkinnedMeshRenderer skinnedMesh;
    public GameObject Eyes;
    public VisualEffect effect;
    public float dissolveRate = 0.125f;
    public float refreshRate = 0.025f;
    private Material[] skinnedMaterials;

    private void Start()
    {
        if(skinnedMesh != null)
            skinnedMaterials = skinnedMesh.materials;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Eyes.SetActive(false);
            StartCoroutine(DissolveCo());
        }
    }

    IEnumerator DissolveCo()
    {
        if(effect != null)
        {
            effect.Play();
        }

        if(skinnedMaterials.Length > 0)
        {
            float counter = 0;
            while (skinnedMaterials[0].GetFloat("_DissolveAmount") < 1)
            {
                counter += dissolveRate;
                for(int i = 0; i < skinnedMaterials.Length; i++)
                {
                    skinnedMaterials[i].SetFloat("_DissolveAmount", counter);
                }
                yield return new WaitForSeconds(refreshRate);
            }
        }
    }
}
