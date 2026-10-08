# Valley Void victory panel

Original victory art in the same deep-navy, cyan, white, and amber visual language as the laser hotbar. The amber star and wing trail are the primary reward signal; the score and restart action are the next reading targets. The preview uses English labels to match the existing `GAME OVER` UI; gameplay text remains editable in Unity.

## Exported sprites

All sprites are transparent PNGs in `Assets/UI/VictoryPanel/` with Unity Sprite import metadata.

- `victory_backdrop.png` — 1920 × 1080 full-screen dimming layer.
- `victory_frame.png` — 900 × 610 central frame, with no text baked in.
- `victory_emblem.png` — 200 × 150 celebratory star and flight wings.
- `victory_title.png` — 600 × 105 English VICTORY title. Omit this layer and use TextMesh Pro for another language.
- `victory_score_card.png` — 500 × 120 score field, without a baked value.
- `victory_button_primary.png` and `victory_button_secondary.png` — 278 × 76 blank button visuals.
- `victory_layout.png` — 900 × 610 preassembled visual layout; score and button labels are still blank.
- `victory_backplate_flexible.png` — 512 × 256 generic backplate with 32-pixel 9-slice borders for alternate panel sizes.

`Design/VictoryPanel/preview.png` is a mockup with sample score `012450`, button labels, and background. The SVG source files are in `svg/`. All artwork is original to this project.

## Suggested Unity hierarchy

Create a full-screen `VictoryPanel` under `UI Canvas` with a backdrop Image. Center a 900 × 610 container, then add the frame, emblem, title, score card, two Buttons with the supplied images, and TextMesh Pro objects for `MISSION COMPLETE`, `SCORE`, score value, `RESTART`, and `MAIN MENU`. Keep the score and button labels as live text. Use `victory_layout.png` only for a quick visual prototype; use the separate layers for clickable buttons and animated elements.

For responsive sizing, keep the central container's aspect ratio or use `victory_backplate_flexible.png` with Image Type **Sliced**. Keep Rect Transform scale at `(1,1,1)` and change Width/Height. A CanvasGroup on the root can fade the whole result in and out.

The project currently has a `GameState.Victory` state, but this art does not alter the user's victory flow or scene. Connect the panel's visibility and live score when that flow is ready.
