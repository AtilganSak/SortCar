using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Colorizer : MonoBehaviour
{
    public Team team;

    public int materialIndex;

    private void OnEnable()
    {
        MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
        if (meshRenderer != null)
        {
            meshRenderer.materials[materialIndex].color = ReferenceKeeper.Instance.LevelSettings.GetColorByTeam(team);
        }
        else
        {
            SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
            if (spriteRenderer != null)
            {
                spriteRenderer.color = ReferenceKeeper.Instance.LevelSettings.GetColorByTeam(team);
            }
        }
    }
}
