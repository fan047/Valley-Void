# Overdrive Laser hotbar

An original high-energy weapon UI for the Valley Void spaceship. Concept-derived visual tags: chamfered hull, beam rupture, restrained glow. Deep navy is the background, cyan identifies powered laser hardware, white marks the beam core, and amber appears only at overdrive peaks. The third slot is Burst Laser: a jagged blast ring, hot impact core, and flying fragments distinguish it from Dual Laser's paired rays and Beam Laser's continuous ray.

## Files

All gameplay sprites are transparent PNGs in `Assets/UI/WeaponHotbar/Overdrive/` with Unity Sprite import metadata.

- `overdrive_complete.png` (820 × 290): ready-made static composition, with Beam Laser selected and the charge bar full.
- `overdrive_complete_3slot.png` (1028 × 290): static three-weapon composition, with Burst Laser selected.
- `overdrive_panel.png` (820 × 250): detailed fixed two-slot frame.
- `overdrive_panel_3slot.png` (1028 × 250): detailed three-slot frame.
- `overdrive_backplate_flexible.png` (512 × 192): simple 9-slice backplate for responsive widths. Its Sprite Border is 32 pixels on all sides.
- `overdrive_slot_idle.png` and `overdrive_slot_active.png` (192 × 192): separate slot states with identical square geometry, inner frame, and bottom rail. Active state adds brightness and glow.
- `overdrive_dual_laser.png`, `overdrive_beam_laser.png`, and `overdrive_burst_laser.png` (128 × 128): separate weapon icons.
- `overdrive_burst_slot_idle.png` and `overdrive_burst_slot_active.png` (192 × 192): ready-to-use third-slot sprites with the Burst Laser icon already placed.
- `overdrive_burst_fx.png` (240 × 240): optional selected-weapon energy corona.
- `overdrive_charge_fill.png` (256 × 30): use on a Unity Image with Type **Filled**, Fill Method **Horizontal** to drive charge amount.

The SVG source files are in `svg/`; `preview.png` shows the intended assembled look. The art is original to this project.

## Unity assembly

For a quick static prototype, drag `overdrive_complete_3slot.png` into a UI Image and enable Preserve Aspect. For a functional hotbar, layer the panel or flexible backplate, slot sprites, burst FX, icons, and charge fill in that order. Keep each icon square with Preserve Aspect; leave Rect Transform scale at `(1,1,1)` and resize using Width/Height. Set the flexible backplate Image Type to **Sliced**. Use TextMesh Pro for weapon labels so localization and selected states stay editable. This pack supplies the third weapon's visual assets; its gameplay selection and firing logic are separate.
