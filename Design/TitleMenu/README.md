# Valley Void title menu

Transparent PNG sprites are in `Assets/UI/TitleMenu`. Editable SVG originals are in `Design/TitleMenu/svg`; run `generate.cjs` to render them again.

- `title_valley_void.png`: 1050 × 250, native size; place in the upper left or upper center of the title Canvas.
- `button_start.png`, `button_quit.png`: 400 × 82, same geometry for vertical stacking.
- `button_*_highlighted.png`: assign to the Unity Button `Highlighted Sprite` with `Transition = Sprite Swap`.

Suggested Canvas Scaler: `Scale With Screen Size`, reference resolution 1920 × 1080, `Match` 0.5. Preserve aspect ratio on the title Image. The button words are baked into the sprites; SVG sources are available if wording needs to change.
