using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BonecoInteractive : PuzzleInteractive
{
    //Inicia o Puzzle, deixa o Base.Initialize e adicione as novas funções do Codigo
    public override void Initialize(ItemPuzzle item=null)
    {
       base.Initialize(item);
    }

    //Metodo que chama ao Entrar
    public override void InteractionEnter(PointerEventData data)
    {
        //Deixa isso aqui, ele vai chamar o metodo Callback, depois de um tempo 
        
        if(!_interactive)
        return;
        if(ItemManager.instancie.ItemNecessary(enumItemNecessary))
        base.InteractionEnter(data);
    }

    //Metodo que chama depois de um Tempo
    public override void CallBackAction()
    {  
        FinishPuzzle();
    }

    private void FinishPuzzle()
    {
        SpawnItem();
        animatorBoneco.SetTrigger("Finish");
        DisableInteractive();
        bonecosManager.SolutionBoneco();
        ItemManager.instancie.RemoveItemHand();
    }



    public override void DisableInteractive()
    {      
        this.enabled = false;
        this.gameObject.layer = 0;

        if(bonecosManager.isActive)
        GetComponent<MeshRenderer>().material=bonecosManager.ReturnMaterial(enumStateEstatua.Feliz);
        else 
        GetComponent<MeshRenderer>().material=bonecosManager.ReturnMaterial(enumStateEstatua.Normal);
    }

    private void SpawnItem()
    {
        ItemObject itemObject = ItemManager.instancie.itensSlot.GetItemObject();
        itemObject.Initialize();
        GameObject itemSpawn = Instantiate(itemObject.gameObject_Item, spawnPosition);
        
        itemSpawn.transform.localScale=scaleOffSet;
    }

    // Metodo que vc chama ao sair da Intection
    public override void InteractionBack(PointerEventData data)
    {
        base.InteractionBack(data);
    }

    [SerializeField] private Transform spawnPosition;

    [SerializeField] private Animator animatorBoneco;

    [SerializeField] private EnumItens enumItemNecessary;

    [SerializeField] private BonecosManager bonecosManager;

    [SerializeField] private Vector3 scaleOffSet;


    [SerializeField]
    bool _interactive=true;
}
