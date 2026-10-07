# Valley Void weapon hotbar art

Original, minimal laser icons drawn for this project. The intended three-weapon UI uses `weapon_dual_laser`, `weapon_beam_laser`, and `weapon_burst_laser`; `pulse` and `spread` remain optional variants.

- `Assets/UI/WeaponHotbar/weapon_*.png`: transparent white icons, 256 × 256. Tint with the Unity `Image` component.
- `Assets/UI/WeaponHotbar/slot_*.png`: 256 × 256 slot backgrounds for normal, selected, and disabled states. Put the weapon icon in a separate `Image` on top.
- `Assets/UI/WeaponHotbar/panel_2slot.png`: 520 × 244 panel for the original two-weapon layout.
- `Assets/UI/WeaponHotbar/panel_3slot.png`: 728 × 244 panel for Dual Laser, Beam Laser, and Burst Laser.
- `Assets/UI/WeaponHotbar/panel_4slot.png`: 936 × 244 panel for a future four-weapon loadout. Its lower plates are blank so labels can be set with TextMesh Pro.
- `Assets/UI/WeaponHotbar/panel_flexible.png`: 512 × 192 generic background with a 32-pixel 9-slice border. Use this when the panel width or height needs to change.
- `Design/WeaponHotbar/svg/`: editable SVG originals.
- `Design/WeaponHotbar/preview.png`: contact sheet on a dark background.
- `Design/WeaponHotbar/slot_states_preview.png`: normal, disabled, and selected slots shown at the same size.
- `Design/WeaponHotbar/panel_preview.png`: example layer arrangement for the panel.

The checked-in `.meta` files configure every PNG as **Sprite (2D and UI)** with transparency, no mipmaps, and no texture compression. All three `slot_*` states share exactly the same square frame, inner frame, and bottom rail; only palette and brightness change. The `slot_*` sprites and `panel_flexible` also have 32-pixel 9-slice borders. In Unity, set those background `Image` components to **Sliced**, keep Rect Transform scale at `(1,1,1)`, and change Width/Height instead. Keep weapon icon Images as **Simple** with **Preserve Aspect** enabled. Layer the panel at the back, the slot sprite above it, then the weapon icon and TextMesh Pro label. The SVGs and PNGs are original project artwork; no third-party attribution is required.

To regenerate PNGs after editing `generate.cjs`, run `node Design/WeaponHotbar/generate.cjs` with the `sharp` Node package available.
