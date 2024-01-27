using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
[RequireComponent(typeof(GridLayoutGroup))]

//Główny skrypt do dopasowywania elementu canvasu grida do ekranu
//Zgadza sie, ukradlem i troche zmienilem na potrzeby ale tylko glupi by nie skorzystal
public class AdjustLayoutCellSize : MonoBehaviour
{
    public enum Axis { X, Y };
    public enum RatioMode { Free, Fixed };

    [SerializeField] Axis expand;
    [SerializeField] RatioMode ratioMode;
    [SerializeField] float cellRatio = 1;

    private RectTransform trans;
    [SerializeField]
    private GridLayoutGroup grid;

    void Awake()
    {
        trans = (RectTransform)gameObject.transform;
        grid = gameObject.GetComponent<GridLayoutGroup>();
    }

    void Start()
    {
        UpdateCellSize();
    }

    void Update()
    {
        // UpdateCellSize();
    }

    void OnValidate()
    {
        trans = (RectTransform)base.transform;
        // grid = gameObject.GetComponent<GridLayoutGroup>();
        UpdateCellSize();
    }

    void OnRectTransformDimensionsChange()
    {
        UpdateCellSize();
    }

    void UpdateCellSize()
    {
        if(grid!=null && trans!=null){
        var count = grid.constraintCount;
        if (expand == Axis.X)
        {
            float contentSize = trans.rect.width - grid.padding.left - grid.padding.right;
            float sizePerCell = contentSize / count;

            // maksymalna komorka w obszarze
            float maxWidth = contentSize / count;
            grid.cellSize = new Vector2(Mathf.Min(maxWidth, sizePerCell), ratioMode == RatioMode.Free ? grid.cellSize.y : sizePerCell * cellRatio);
        }
        else
        {
            float contentSize = trans.rect.height - grid.padding.top - grid.padding.bottom;
            float sizePerCell = contentSize / count;

            // maksymanla komurka w obszarze
            float maxHeight = contentSize / count;
            grid.cellSize = new Vector2(ratioMode == RatioMode.Free ? grid.cellSize.x : sizePerCell * cellRatio, Mathf.Min(maxHeight, sizePerCell));
        }
        }
    }
}
