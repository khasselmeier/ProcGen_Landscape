using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TerrainUIManager : MonoBehaviour
{
    public TerrainGenerator terrainGenerator;

    // UI Elements
    public Slider sizeXSlider, sizeYSlider, heightScaleSlider, scaleSlider, dampeningSlider, octavesSlider, persistenceSlider, lacunaritySlider, offsetXSlider, offsetYSlider;
    public TMP_Dropdown colorSettingDropdown;
    public Button generateButton, resetButton, defaultTerrainButton;

    // Presets
    public GameObject defaultTerrain;
    public GameObject preset1, preset2, preset3;
    public Button preset1Button, preset2Button, preset3Button;

    // Store original values
    private int originalSizeX, originalSizeY, originalOctaves;
    private float originalHeightScale, originalScale, originalDampening, originalPersistence, originalLacunarity;
    private ColorSetting originalColorSetting;
    private Vector2 originalOffset;


    void Start()
    {
        // Store original values
        originalSizeX = terrainGenerator.sizeX;
        originalSizeY = terrainGenerator.sizeY;
        originalHeightScale = terrainGenerator.heightScale;
        originalScale = terrainGenerator.scale;
        originalDampening = terrainGenerator.dampening;
        originalOctaves = terrainGenerator.octaves;
        originalPersistence = terrainGenerator.persistence;
        originalLacunarity = terrainGenerator.lacunarity;
        originalColorSetting = terrainGenerator.colorSetting;
        originalOffset = terrainGenerator.offset;

        // Set initial UI values
        sizeXSlider.value = originalSizeX;
        sizeYSlider.value = originalSizeY;
        heightScaleSlider.value = originalHeightScale;
        scaleSlider.value = originalScale;
        dampeningSlider.value = originalDampening;
        octavesSlider.value = originalOctaves;
        persistenceSlider.value = originalPersistence;
        lacunaritySlider.value = originalLacunarity;
        colorSettingDropdown.value = (int)originalColorSetting;
        offsetXSlider.value = originalOffset.x;
        offsetYSlider.value = originalOffset.y;

        // Add listeners that update terrain in real time
        sizeXSlider.onValueChanged.AddListener(value => { terrainGenerator.sizeX = (int)value; UpdateTerrain(); });
        sizeYSlider.onValueChanged.AddListener(value => { terrainGenerator.sizeY = (int)value; UpdateTerrain(); });
        heightScaleSlider.onValueChanged.AddListener(value => { terrainGenerator.heightScale = value; UpdateTerrain(); });
        scaleSlider.onValueChanged.AddListener(value => { terrainGenerator.scale = value; UpdateTerrain(); });
        dampeningSlider.onValueChanged.AddListener(value => { terrainGenerator.dampening = value; UpdateTerrain(); });
        octavesSlider.onValueChanged.AddListener(value => { terrainGenerator.octaves = (int)value; UpdateTerrain(); });
        persistenceSlider.onValueChanged.AddListener(value => { terrainGenerator.persistence = value; UpdateTerrain(); });
        lacunaritySlider.onValueChanged.AddListener(value => { terrainGenerator.lacunarity = value; UpdateTerrain(); });
        colorSettingDropdown.onValueChanged.AddListener(value => { terrainGenerator.colorSetting = (ColorSetting)value; UpdateTerrain(); });
        offsetXSlider.onValueChanged.AddListener(value => { terrainGenerator.offset = new Vector2(value, terrainGenerator.offset.y); UpdateTerrain(); });
        offsetYSlider.onValueChanged.AddListener(value => { terrainGenerator.offset = new Vector2(terrainGenerator.offset.x, value); UpdateTerrain(); });

        // Generate button: Assign new seed & regenerate terrain
        generateButton.onClick.RemoveAllListeners();
        generateButton.onClick.AddListener(GenerateNewTerrain);

        // Reset button: Restore original values
        resetButton.onClick.RemoveAllListeners();
        resetButton.onClick.AddListener(ResetToOriginalValues);

        // Preset buttons
        preset1Button.onClick.AddListener(() => ActivatePreset(preset1));
        preset2Button.onClick.AddListener(() => ActivatePreset(preset2));
        preset3Button.onClick.AddListener(() => ActivatePreset(preset3));
        defaultTerrainButton.onClick.AddListener(ActivateDefaultTerrain);

        // Ensure presets are disabled by default
        preset1.SetActive(false);
        preset2.SetActive(false);
        preset3.SetActive(false);
    }

    void UpdateTerrain()
    {
        terrainGenerator.Initiate();
    }

    void GenerateNewTerrain()
    {
        terrainGenerator.seed = Random.Range(0, 1000000);
        terrainGenerator.Initiate();
    }

    void ResetToOriginalValues()
    {
        // Reset values to original
        terrainGenerator.sizeX = originalSizeX;
        terrainGenerator.sizeY = originalSizeY;
        terrainGenerator.heightScale = originalHeightScale;
        terrainGenerator.scale = originalScale;
        terrainGenerator.dampening = originalDampening;
        terrainGenerator.octaves = originalOctaves;
        terrainGenerator.persistence = originalPersistence;
        terrainGenerator.lacunarity = originalLacunarity;
        terrainGenerator.colorSetting = originalColorSetting;

        // Update UI to reflect original values
        sizeXSlider.value = originalSizeX;
        sizeYSlider.value = originalSizeY;
        heightScaleSlider.value = originalHeightScale;
        scaleSlider.value = originalScale;
        dampeningSlider.value = originalDampening;
        octavesSlider.value = originalOctaves;
        persistenceSlider.value = originalPersistence;
        lacunaritySlider.value = originalLacunarity;
        colorSettingDropdown.value = (int)originalColorSetting;

        // Update offset sliders
        offsetXSlider.value = originalOffset.x;
        offsetYSlider.value = originalOffset.y;

        // Regenerate terrain
        UpdateTerrain();
    }

    void ActivatePreset(GameObject selectedPreset)
    {
        // Disable default terrain and all presets
        defaultTerrain.SetActive(false);
        preset1.SetActive(false);
        preset2.SetActive(false);
        preset3.SetActive(false);

        // Activate the selected preset
        selectedPreset.SetActive(true);
    }

    void ActivateDefaultTerrain()
    {
        // Disable all presets
        preset1.SetActive(false);
        preset2.SetActive(false);
        preset3.SetActive(false);

        // Enable default terrain
        defaultTerrain.SetActive(true);
    }
}