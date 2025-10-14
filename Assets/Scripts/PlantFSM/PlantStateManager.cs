using UnityEngine;
using UnityEngine.EventSystems;

public class PlantStateManager : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler, IPointerClickHandler, IDropHandler
{
    [SerializeField] private GameObject InventoryUI; //asign in inspector
    private Vector3 _InventoryRightPos = new Vector3(540f, 0f, 0f);
    private Vector3 _InventoryLeftPos = new Vector3(-540f, 0f, 0f);

    public PlantBaseState CurrentState;
    public PlantInitialState InitialState = new PlantInitialState();
    public PlantGrowingState GrowingState = new PlantGrowingState();
    public PlantReadyState ReadyState = new PlantReadyState();

    private CanvasGroup _canvasGroup;
    private Canvas _canvas;
    private RectTransform _rectTransform;

    public Seed SeedData;
    public bool IsWatered = false;
    public bool CanBeWatered = true;
    public int GrowthCountdown;

    private void Start()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
        _canvas = GetComponentInParent<Canvas>();
        _rectTransform = GetComponent<RectTransform>();

        if(CurrentState == null)
        {
            CurrentState = InitialState;
            CurrentState.EnterState(this);
        }
    }

    private void Update()
    {
        CurrentState.UpdateState(this);
    }

    public void SwitchState(PlantBaseState state)
    {
        CurrentState = state;
        CurrentState.EnterState(this);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        _canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if(CurrentState.ID == "Ready")
        {
            _rectTransform.anchoredPosition += eventData.delta / _canvas.scaleFactor;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        _canvasGroup.blocksRaycasts = true;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (CurrentState.ID == "Initial")
        {
            if (!InventoryUI.activeInHierarchy)
            {
                InventoryUI.SetActive(true);
                if(transform.position.x > Screen.width / 2)
                {
                    InventoryUI.GetComponent<RectTransform>().anchoredPosition = _InventoryLeftPos;
                }
                else
                {
                    InventoryUI.GetComponent<RectTransform>().anchoredPosition = _InventoryRightPos;
                }
            }
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        if(CurrentState.ID == "Initial" && SeedData == null)
        {
            GameObject droppedObject = eventData.pointerDrag;
            InventorySeedIcon inventorySeedIcon = droppedObject.GetComponent<InventorySeedIcon>();
            if (inventorySeedIcon != null && !inventorySeedIcon.OutOfStock)
            {
                Seed seed = inventorySeedIcon.GetSeedType();
                if (seed != null)
                {
                    SeedData = seed;
                }
            }
        }
    }
}
