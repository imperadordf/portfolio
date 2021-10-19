using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SO_Personagem", menuName = "Personagem/InformationPersonagem", order = 0)]
public class SO_Personagem : ScriptableObject
{
    public EnumNode nomePersonagem;
    public Sprite sprite_Personagem;

    public byte Id { get; set; }

    public static object Deserialize(byte[] data)
    {
        var result = new SO_Personagem();
        result.Id = data[0];
        return result;
    }

    public static byte[] Serialize(object customType)
    {
        var c = (SO_Personagem)customType;
        return new byte[] { c.Id };
    }

}

public enum EnumNode
{
    Player1,
    Player2,
    Nenhum
}

