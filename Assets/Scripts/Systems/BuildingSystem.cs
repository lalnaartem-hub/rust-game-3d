using UnityEngine;
using System.Collections.Generic;

namespace GameSystems {

    public class BuildingSystem : MonoBehaviour {
        private List<GameObject> buildings = new List<GameObject>();
        private float integrityThreshold = 0.5f;

        public GameObject buildingPrefab;

        // Place a building at the specified position
        public void PlaceBuilding(Vector3 position) {
            GameObject newBuilding = Instantiate(buildingPrefab, position, Quaternion.identity);
            buildings.Add(newBuilding);
        }

        // Demolish a building at the specified index
        public void DemolishBuilding(int index) {
            if (index >= 0 && index < buildings.Count) {
                Destroy(buildings[index]);
                buildings.RemoveAt(index);
            } else {
                Debug.LogWarning("Building index out of range.");
            }
        }

        // Check the structural integrity of buildings
        public void CheckBuildingIntegrity() {
            foreach (GameObject building in buildings) {
                float integrity = CalculateIntegrity(building);
                if (integrity < integrityThreshold) {
                    DemolishBuilding(buildings.IndexOf(building));
                    Debug.Log("Building demolished due to low integrity.");
                }
            }
        }

        // Placeholder for actual integrity calculation logic
        private float CalculateIntegrity(GameObject building) {
            // Assuming some logic to calculate the integrity of the building
            return Random.value; // Placeholder: this should be replaced with actual logic
        }
    }
}