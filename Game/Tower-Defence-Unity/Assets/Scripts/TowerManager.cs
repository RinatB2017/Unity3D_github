using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TowerManager : Singleton<TowerManager> {

    public TowerBtn TowerBtnPressed { get; set; }
    private SpriteRenderer spriteRenderer;

    private List<Tower> towerList = new List<Tower>();
    private List<Collider2D> buildList = new List<Collider2D>();
    private Collider2D buildTile;

	// Use this for initialization
	void Start () {
        spriteRenderer = GetComponent<SpriteRenderer>();
        buildTile = GetComponent<Collider2D>();
        spriteRenderer.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
        if(Input.GetMouseButtonDown(0)) {
            Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero);
            if(hit.collider.tag == "BuildSite") {   //Check if user clicked on build site
                buildTile = hit.collider;
                buildTile.tag = "BuildSiteFull";
                RegisterBuildSite(buildTile);
                PlaceTower(hit);
            }
        }
        if(spriteRenderer.enabled) {
            FollowMouse();
        }
    }

    public void RegisterBuildSite(Collider2D buildTag) {
        buildList.Add(buildTag);
    }
    
    public void RegisterTower(Tower tower) {
        towerList.Add(tower);
    }

    public void RenameTagsBuildSites() {
        foreach(Collider2D collider in buildList) {
            collider.tag = "BuildSite";
        }
        buildList.Clear();
    }

    public void DestroyAllTowers() {
        foreach(Tower tower in towerList) {
            Destroy(tower.gameObject);
        }
        towerList.Clear();
    }

    public void PlaceTower(RaycastHit2D hit) {
        if(!EventSystem.current.IsPointerOverGameObject() && TowerBtnPressed != null) {
            GameManager.Instance.SoundFx.PlayOneShot(SoundManager.Instance.Towerbuilt);
            Tower newTower = Instantiate(TowerBtnPressed.TowerObject);
            newTower.transform.position = hit.transform.position;
            BuyTower(TowerBtnPressed.TowerPrice);
            RegisterTower(newTower);
            DisableDragSprite();
            TowerBtnPressed = null;
        }
    }

    public void BuyTower(int price) {
        GameManager.Instance.SubtractMoney(price);
    }

    public void SelectedTower(TowerBtn towerSelected) {
        if(towerSelected.TowerPrice <= GameManager.Instance.TotalMoney) {
            TowerBtnPressed = towerSelected;
            EnableDragSprite(TowerBtnPressed.DragSprite);
        }
    }

    public void FollowMouse() {
        transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector2(transform.position.x, transform.position.y);
    }

    public void EnableDragSprite(Sprite sprite) {
        spriteRenderer.enabled = true;
        spriteRenderer.sprite = sprite;
    }

    public void DisableDragSprite() {
        spriteRenderer.enabled = false;
    }
}
