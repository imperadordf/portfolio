using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Node : MonoBehaviour
{
    [HideInInspector] public int myindex;
    [HideInInspector] public Player playerAtualInNode;

    [HideInInspector] private GameObject GameObjectPadrao;
    [SerializeField] private TextMeshPro textMesh;
    [SerializeField] private MeshRenderer meshRenderer;

    [SerializeField] private List<Triplet> triplets = new List<Triplet>();

    private EnumNode personagemInNode = EnumNode.Nenhum;

    public void Initialize(int index)
    {
        triplets.Clear();
        myindex = index;
        textMesh.text = myindex.ToString();
        gameObject.name = "Node " + myindex;
    }

    public void SetNode(SO_Personagem playerInformation)
    {
        personagemInNode = playerInformation.nomePersonagem;
        Debug.Log("Alterou meu Material" + myindex);
        meshRenderer.materials[0].color = new Color(0, 5, 255);

        foreach (Triplet triplet in triplets)
        {
            triplet.VerificarNodes(personagemInNode);
        }
    }

    public Node SetTriplet(Triplet triplet)
    {
        triplets.Add(triplet);
        return this;
    }

    public bool PersonagemInNodeEqual(EnumNode personagemNome)
    {
        return personagemNome == personagemInNode;
    }
}

public struct StructPlayerInformation
{
    public Color myColor;

    public EnumNode enumPersonagem;
}
