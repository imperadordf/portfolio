using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Triplet
{
    public string nameTriplet;
    [SerializeField] private List<Node> myNodes;

    TabuleiroController tabuleiroController;

    public void Initialize(string nameTriplet, TabuleiroController controller)
    {
        myNodes = new List<Node>();
        this.nameTriplet = nameTriplet;
        tabuleiroController = controller;
    }

    public void AddNode(Node node)
    {
        myNodes.Add(node);
    }

    public void VerificarNodes(EnumNode personageminNode)
    {
        int pontosMax = myNodes.Count;
        int pontos = 0;

        foreach (Node node in myNodes)
        {
            if (node.PersonagemInNodeEqual(personageminNode))
            {
                pontos++;
            }
            else
            {
                break;
            }
        }

        if (pontos == pontosMax)
        {
            tabuleiroController.FinishGame();
        }
    }
}
