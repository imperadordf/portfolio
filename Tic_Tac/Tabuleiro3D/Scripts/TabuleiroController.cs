using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TabuleiroController : MonoBehaviour
{
    public Node prefab;
    public Transform parentNodes;
    public List<Triplet> totalTriplets;


    [HideInInspector] public List<Node> nodes;

    public int MaxNodes
    {
        get
        {
            return nodes.Count;
        }
    }

    private int N;

    #region  Iniciando o Tabuleiro

    //Iniciando o Tabuleiro pelo tipo dele
    public void Initialize(TabuleiroType tabuleiroType)
    {
        N = (int)tabuleiroType;
        CreateTabuleiro(N);
    }

    //Criando o Tabuleiro atraves do Valor N, que seria 3X3X3 = 3 e etc
    private void CreateTabuleiro(int N)
    {
        int contNode = 0;
        for (int z = 0; z < N; z++)
        {
            for (int y = 0; y < N; y++)
            {
                for (int x = 0; x < N; x++)
                {
                    contNode++;
                    Vector3 position = new Vector3(x * 3, y * 3, z * 3) + new Vector3(-3.68f, 4.059679f, 1.94f);
                    Node node = Instantiate(prefab, position, Quaternion.identity);
                    node.transform.SetParent(parentNodes);
                    InitializeNode(node, contNode);
                }
            }
        }

        CreateTriplets();
    }

    private void InitializeNode(Node node, int index)
    {
        nodes.Add(node);
        node.Initialize(index);
    }

    //N é a "dimensão" do Cubo NxNxN (se for um cubo 3x3x3, então N=3)
    //Chama a função FindTriplets pra adicionar tudo no "totalTriplets"
    private void CreateTriplets()
    {
        totalTriplets = new List<Triplet>();
        totalTriplets.AddRange(FindTriplets(1, 1, N * N, N, N, "Right")); // Right to Left
        totalTriplets.AddRange(FindTriplets(N - 1, N, N, N * N, N, "Diagonal left-down"));
        totalTriplets.AddRange(FindTriplets(N, 1, N * N, -1, N, "Down", true));
        totalTriplets.AddRange(FindTriplets(N + 1, 1, N, N * N, N, "Diagonal right-down"));
        totalTriplets.AddRange(FindTriplets((N * (N - 1)) - 1, N * N, 1, 0, N, "Diagonal back-left-up"));
        totalTriplets.AddRange(FindTriplets(N * (N - 1), (N * (N - 1) + 1), N, 1, N, "Diagonal Back-up"));
        totalTriplets.AddRange(FindTriplets((N * (N - 1)) + 1, N * (N - 1) + 1, 1, 0, N, "Diagonal Back-right-up"));
        totalTriplets.AddRange(FindTriplets((N * N) - 1, N, N, N, N, "Diagonal Back-Left"));
        totalTriplets.AddRange(FindTriplets(N * N, 1, N * N, 1, N, "Back"));
        totalTriplets.AddRange(FindTriplets(N * N + 1, 1, N, N, N, "Diagonal back-right"));
        totalTriplets.AddRange(FindTriplets((N * (N + 1)) - 1, N, 1, 0, N, "Diagonal back-left-down"));
        totalTriplets.AddRange(FindTriplets(N * (N + 1), 1, N, 1, N, "Diagonal back-down"));
        totalTriplets.AddRange(FindTriplets((N * (N + 1)) + 1, 1, 1, 0, N, "Diagonal Back-right-down"));
    }


    //resolution = se é 3x3x3 ou 4x4x4 ou etc
    //total é quantos Triplets vai dar no total
    //init é qual inicia
    //progress é o (x+1) ou (x+2) ou etc
    //toNextTrip é pra ir pra "próxima linha"
    public List<Triplet> FindTriplets(int progress, int init, int total, int toNextTrip, int resolution, string nameTriplet = "", bool isDown = false)
    {
        List<Triplet> ts = new List<Triplet>();
        for (int i = 0; i < total; i++)
        {
            Triplet t = new Triplet();
            t.Initialize(nameTriplet, this);
            for (int j = 0; j < resolution; j++)
            {
                int nodeID;
                if (isDown)
                    nodeID = ((init + (N * N) * (i / N) + i % N) + (progress * j));
                else
                    nodeID = (init + (i * toNextTrip)) + (progress * j);
                // Debug.Log("nodes" + nodeID + "Name Triplet: " + nameTriplet);
                Debug.Log(nodeID);
                t.AddNode(nodes[nodeID - 1].SetTriplet(t));
            }
            ts.Add(t);
        }

        return ts;
    }

    #endregion
    //Finalizando o Jogo
    public void FinishGame()
    {
        Debug.Log("Acabou");
    }
}

public enum TabuleiroType
{
    Tabuleiro3x3x3 = 3,
    Tabuleiro4x4x4 = 4,
    Tabuleiro5x5x5 = 5,
}