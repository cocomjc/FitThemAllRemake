using UnityEngine;
using Array2DEditor;
using UnityEngine.UI;

public class BlockSetUp : MonoBehaviour
{
    [HideInInspector] public Vector2 initPos;
    [SerializeField] public Array2DBool shape;
    [SerializeField] private GameObject piecePrefab;
    [SerializeField] private Color blockColor;
    [SerializeField] private Transform glowContainer;
    public GameParam gameParam;
    [SerializeField] private Sprite pieceSprite;

    void Start()
    {
        gameParam = FindObjectOfType<GameParamHandler>().gameParam;
        initPos = transform.position;
        GetComponent<Image>().enabled = false;
        GameObject newPiece;

        for (int i = 0; i < shape.GetCells().GetLength(0); i++)
        {
            for (int j = 0; j < shape.GetCells().GetLength(1); j++)
            {
                if (shape.GetCells()[i,j])
                {
                    newPiece = Instantiate(piecePrefab, new Vector3(0, 0, 0), Quaternion.identity);
                    newPiece.transform.SetParent(transform);
                    newPiece.GetComponent<DraggableItem>().gameParam = gameParam;
                    newPiece.GetComponent<DraggableItem>().SetUpDraggable(gameObject, new Vector3(j * gameParam.piecesSize.y, -i * gameParam.piecesSize.x, 0));
                    newPiece.GetComponent<DraggableItem>().mainImage.sprite = pieceSprite;
                    newPiece.GetComponent<DraggableItem>().mainImage.color = blockColor;
                    newPiece.transform.localPosition = new Vector3(j* gameParam.piecesSize.y, -i* gameParam.piecesSize.x, 0);
                    newPiece.transform.localScale = new Vector3(1, 1, 1);
                    foreach (Transform child in newPiece.transform)
                    {
                        if (child.tag == "Glow")
                        {
                            child.SetParent(glowContainer);
                        }
                    }
                }
            }
        }
        transform.localScale = gameParam.blockSmallScale;
    }
}
