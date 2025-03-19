using System.Collections.Generic;
using UnityEngine;

public class TerrainChunk {
	
	const float colliderGenerationDistanceThreshold = 5;
	public event System.Action<TerrainChunk, bool> onVisibilityChanged;
	public Vector2 coord;
	 
	GameObject meshObject;
	Vector2 sampleCentre;
	Bounds bounds;

	MeshRenderer meshRenderer;
	MeshFilter meshFilter;
	MeshCollider meshCollider;

	LODInfo[] detailLevels;
	LODMesh[] lodMeshes;
	int colliderLODIndex;

	HeightMap heightMap;
	bool heightMapReceived;
	int previousLODIndex = -1;
	bool hasSetCollider;
	float maxViewDst;

	HeightMapSettings heightMapSettings;
	MeshSettings meshSettings;
	Transform viewer;

    // 건물 배치용 변수
    public GameObject buildingPrefab; // 배치할 건물 프리팹
    private List<GameObject> spawnedBuildings = new List<GameObject>(); // 생성된 건물 목록
    private bool buildingsPlaced = false;
    const int maxBuildingsPerChunk = 3; // 청크당 최대 건물 수
    const float minBuildingDistance = 10f; // 건물 간 최소 거리
    const float flatnessThreshold = 0.1f; // 평탄도 임계값

    public TerrainChunk(Vector2 coord, HeightMapSettings heightMapSettings, MeshSettings meshSettings, LODInfo[] detailLevels, int colliderLODIndex, Transform parent, Transform viewer, Material material, GameObject buildingPrefab = null) {
		this.coord = coord;
		this.detailLevels = detailLevels;
		this.colliderLODIndex = colliderLODIndex;
		this.heightMapSettings = heightMapSettings;
		this.meshSettings = meshSettings;
		this.viewer = viewer;
        this.buildingPrefab = buildingPrefab; // 건물 프리팹 설정

        sampleCentre = coord * meshSettings.meshWorldSize / meshSettings.meshScale;
		Vector2 position = coord * meshSettings.meshWorldSize ;
		bounds = new Bounds(position,Vector2.one * meshSettings.meshWorldSize );


		meshObject = new GameObject("Terrain Chunk");
		meshRenderer = meshObject.AddComponent<MeshRenderer>();
		meshFilter = meshObject.AddComponent<MeshFilter>();
		meshCollider = meshObject.AddComponent<MeshCollider>();
		meshRenderer.material = material;

		meshObject.transform.position = new Vector3(position.x,0,position.y);
		meshObject.transform.parent = parent;
		SetVisible(false);

		lodMeshes = new LODMesh[detailLevels.Length];
		for (int i = 0; i < detailLevels.Length; i++) {
			lodMeshes[i] = new LODMesh(detailLevels[i].lod);
			lodMeshes[i].updateCallback += UpdateTerrainChunk;
			if (i == colliderLODIndex) {
				lodMeshes[i].updateCallback += UpdateCollisionMesh;
			}
		}

		maxViewDst = detailLevels [detailLevels.Length - 1].visibleDstThreshold;

	}

	public void Load() {
		ThreadedDataRequester.RequestData(() => HeightMapGenerator.GenerateHeightMap (meshSettings.numVertsPerLine, meshSettings.numVertsPerLine, heightMapSettings, sampleCentre), OnHeightMapReceived);
	}


    void OnHeightMapReceived(object heightMapObject)
    {
        this.heightMap = (HeightMap)heightMapObject;
        heightMapReceived = true;

        UpdateTerrainChunk();
        PlaceBuildings(); // 높이맵 수신 후 건물 배치
    }

    Vector2 viewerPosition {
		get {
			return new Vector2 (viewer.position.x, viewer.position.z);
		}
	}


	public void UpdateTerrainChunk() {
		if (heightMapReceived) {
			float viewerDstFromNearestEdge = Mathf.Sqrt (bounds.SqrDistance (viewerPosition));

			bool wasVisible = IsVisible ();
			bool visible = viewerDstFromNearestEdge <= maxViewDst;

			if (visible) {
				int lodIndex = 0;

				for (int i = 0; i < detailLevels.Length - 1; i++) {
					if (viewerDstFromNearestEdge > detailLevels [i].visibleDstThreshold) {
						lodIndex = i + 1;
					} else {
						break;
					}
				}

				if (lodIndex != previousLODIndex) {
					LODMesh lodMesh = lodMeshes [lodIndex];
					if (lodMesh.hasMesh) {
						previousLODIndex = lodIndex;
						meshFilter.mesh = lodMesh.mesh;
					} else if (!lodMesh.hasRequestedMesh) {
						lodMesh.RequestMesh (heightMap, meshSettings);
					}
				}


			}

			if (wasVisible != visible) {
				
				SetVisible (visible);
				if (onVisibilityChanged != null) {
					onVisibilityChanged (this, visible);
				}
			}
		}
	}

	public void UpdateCollisionMesh() {
		if (!hasSetCollider) {
			float sqrDstFromViewerToEdge = bounds.SqrDistance (viewerPosition);

			if (sqrDstFromViewerToEdge < detailLevels [colliderLODIndex].sqrVisibleDstThreshold) {
				if (!lodMeshes [colliderLODIndex].hasRequestedMesh) {
					lodMeshes [colliderLODIndex].RequestMesh (heightMap, meshSettings);
				}
			}

			if (sqrDstFromViewerToEdge < colliderGenerationDistanceThreshold * colliderGenerationDistanceThreshold) {
				if (lodMeshes [colliderLODIndex].hasMesh) {
					meshCollider.sharedMesh = lodMeshes [colliderLODIndex].mesh;
					hasSetCollider = true;
				}
			}
		}
	}

	public void SetVisible(bool visible) {
		meshObject.SetActive (visible);
	}

	public bool IsVisible() {
		return meshObject.activeSelf;
	}

    void PlaceBuildings()
    {
        if (buildingsPlaced || buildingPrefab == null || !heightMapReceived) return;

        // 시드 초기화 (NoiseSettings에서 가져옴)
        Random.InitState(heightMapSettings.noiseSettings.seed + (int)(coord.x * 1000 + coord.y)); // 좌표별 고유 시드

        List<Vector2> candidates = FindFlatAreas();
        List<Vector2> buildingPositions = SelectBuildingPositions(candidates);
        SpawnBuildings(buildingPositions);

        buildingsPlaced = true;
    }

    List<Vector2> FindFlatAreas()
    {
        List<Vector2> candidates = new List<Vector2>();
        int width = heightMap.values.GetLength(0);
        int height = heightMap.values.GetLength(1);

        for (int x = 2; x < width - 2; x++)
        {
            for (int y = 2; y < height - 2; y++)
            {
                if (IsFlatEnough(x, y))
                {
                    Vector2 worldPos = new Vector2(
                        bounds.center.x - meshSettings.meshWorldSize / 2f + (x / (float)(width - 1)) * meshSettings.meshWorldSize,
                        bounds.center.y - meshSettings.meshWorldSize / 2f + (y / (float)(height - 1)) * meshSettings.meshWorldSize
                    );
                    candidates.Add(worldPos);
                }
            }
        }
        return candidates;
    }

    bool IsFlatEnough(int x, int y)
    {
        float centerHeight = heightMap.values[x, y];
        for (int i = -2; i <= 2; i++)
        {
            for (int j = -2; j <= 2; j++)
            {
                if (x + i < 0 || x + i >= heightMap.values.GetLength(0) || y + j < 0 || y + j >= heightMap.values.GetLength(1))
                    continue;
                float height = heightMap.values[x + i, y + j];
                if (Mathf.Abs(height - centerHeight) > flatnessThreshold)
                {
                    return false;
                }
            }
        }
        return true;
    }

    List<Vector2> SelectBuildingPositions(List<Vector2> candidates)
    {
        List<Vector2> positions = new List<Vector2>();
        for (int i = 0; i < maxBuildingsPerChunk && candidates.Count > 0; i++)
        {
            int index = Random.Range(0, candidates.Count);
            Vector2 pos = candidates[index];
            if (IsValidPosition(pos, positions))
            {
                positions.Add(pos);
            }
            candidates.RemoveAt(index);
        }
        return positions;
    }

    bool IsValidPosition(Vector2 newPos, List<Vector2> existingPositions)
    {
        foreach (Vector2 pos in existingPositions)
        {
            if (Vector2.Distance(newPos, pos) < minBuildingDistance)
            {
                return false;
            }
        }
        return true;
    }

    void SpawnBuildings(List<Vector2> positions)
    {
        foreach (Vector2 pos in positions)
        {
            int xIndex = Mathf.RoundToInt((pos.x - (bounds.center.x - meshSettings.meshWorldSize / 2f)) / meshSettings.meshWorldSize * (meshSettings.numVertsPerLine - 1));
            int yIndex = Mathf.RoundToInt((pos.y - (bounds.center.y - meshSettings.meshWorldSize / 2f)) / meshSettings.meshWorldSize * (meshSettings.numVertsPerLine - 1));
            float height = heightMap.values[xIndex, yIndex];
            Vector3 spawnPos = new Vector3(pos.x, height, pos.y);
            GameObject building = Object.Instantiate(buildingPrefab, spawnPos, Quaternion.identity, meshObject.transform);
            spawnedBuildings.Add(building);
        }
    }
}

class LODMesh {

	public Mesh mesh;
	public bool hasRequestedMesh;
	public bool hasMesh;
	int lod;
	public event System.Action updateCallback;

	public LODMesh(int lod) {
		this.lod = lod;
	}

	void OnMeshDataReceived(object meshDataObject) {
		mesh = ((MeshData)meshDataObject).CreateMesh ();
		hasMesh = true;

		updateCallback ();
	}

	public void RequestMesh(HeightMap heightMap, MeshSettings meshSettings) {
		hasRequestedMesh = true;
		ThreadedDataRequester.RequestData (() => MeshGenerator.GenerateTerrainMesh (heightMap.values, meshSettings, lod), OnMeshDataReceived);
	}

}