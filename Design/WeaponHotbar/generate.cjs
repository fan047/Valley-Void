// Original vector artwork for the Valley Void weapon hotbar.
// Run with Node.js and the sharp package to regenerate PNGs from the SVG sources.
const fs = require('node:fs');
const path = require('node:path');
const sharp = require('sharp');

const root = __dirname;
const svgDir = path.join(root, 'svg');
const pngDir = path.resolve(root, '../../Assets/UI/WeaponHotbar');
fs.mkdirSync(svgDir, { recursive: true });
fs.mkdirSync(pngDir, { recursive: true });

const icon = (body) => `<svg xmlns="http://www.w3.org/2000/svg" width="128" height="128" viewBox="0 0 128 128">
  <g fill="none" stroke="#fff" stroke-width="5" stroke-linecap="round" stroke-linejoin="round">${body}</g>
</svg>\n`;

const slot = (fill, border, inner, rail) => `<svg xmlns="http://www.w3.org/2000/svg" width="128" height="128" viewBox="0 0 128 128">
  <rect x="4" y="4" width="120" height="120" fill="${fill}" stroke="${border}" stroke-width="3"/>
  <rect x="11" y="11" width="106" height="106" fill="none" stroke="${inner}" stroke-width="1.5"/>
  <path d="M43 111H85" fill="none" stroke="${rail}" stroke-width="4" stroke-linecap="round"/>
</svg>\n`;

const panel = (count) => {
  const width = 104 + count * 208;
  const right = width - 8;
  const slots = Array.from({ length: count }, (_, i) => {
    const x = 52 + i * 208;
    return `<path d="M${x + 8} 199H${x + 188}L${x + 196} 207V222H${x}V207Z" fill="#0B2531" stroke="#638FA199" stroke-width="2"/>`;
  }).join('\n');
  return `<svg xmlns="http://www.w3.org/2000/svg" width="${width}" height="244" viewBox="0 0 ${width} 244">
    <path d="M28 72H${width - 28}L${right} 92V211L${width - 30} 236H30L8 211V92Z" fill="#06131DDD" stroke="#7294A69C" stroke-width="2"/>
    <path d="M29 88H${width - 29}" stroke="#83AABB80" stroke-width="2"/>
    <path d="M40 230H${width - 40}" stroke="#4CBECE80" stroke-width="2"/>
    <path d="M17 117V99L33 83M${width - 17} 117V99L${width - 33} 83" fill="none" stroke="#A6D3DD88" stroke-width="2"/>
    ${slots}
  </svg>\n`;
};

const flexiblePanel = `<svg xmlns="http://www.w3.org/2000/svg" width="512" height="192" viewBox="0 0 512 192">
  <path d="M28 2H484L510 28V164L484 190H28L2 164V28Z" fill="#06131DDD" stroke="#7294A6B0" stroke-width="3"/>
  <path d="M34 18H478M34 174H478" stroke="#5DC9D890" stroke-width="2"/>
  <path d="M15 52V31L31 15M497 52V31L481 15" fill="none" stroke="#A6D3DD90" stroke-width="2"/>
</svg>\n`;

const artwork = {
  weapon_dual_laser: icon(`
    <path d="M40 19V70M88 19V70" stroke-width="7"/>
    <path d="M34 13H46M82 13H94" stroke-width="4"/>
    <path d="M31 80H49L53 88V106H27V88ZM79 80H97L101 88V106H75V88Z"/>
    <path d="M40 106V115M88 106V115"/>
  `),
  weapon_beam_laser: icon(`
    <path d="M64 15V78" stroke-width="11"/>
    <path d="M52 17H76M49 37H54M74 37H79M48 58H53M75 58H80" stroke-width="4"/>
    <path d="M52 85H76L83 94L76 108H52L45 94Z"/>
    <path d="M64 108V117"/>
  `),
  weapon_burst_laser: icon(`
    <path d="M63 15L71 31L84 21L84 38L103 34L91 49L106 60L87 64L91 79L73 70L64 83L54 70L36 79L40 63L22 58L38 47L27 34L47 38L48 21L59 31Z" stroke-width="4"/>
    <circle cx="64" cy="51" r="8" fill="#fff" stroke="none"/>
    <path d="M64 84V91M54 92H74L80 101L73 113H55L48 101Z" stroke-width="4"/>
    <path d="M64 113V120M18 22L24 17M106 18L112 13M111 80L117 86" stroke-width="3.5"/>
  `),
  weapon_pulse_laser: icon(`
    <path d="M64 14V29M64 40V55M64 66V81" stroke-width="10"/>
    <path d="M54 34H74M54 60H74" stroke-width="3"/>
    <path d="M50 88H78L82 98L74 110H54L46 98Z"/>
    <path d="M64 110V117"/>
  `),
  weapon_spread_laser: icon(`
    <path d="M64 23V76M31 35L53 78M97 35L75 78" stroke-width="7"/>
    <path d="M25 26L37 32M58 15H70M91 32L103 26" stroke-width="4"/>
    <path d="M50 85H78L84 96L74 110H54L44 96Z"/>
    <path d="M64 110V117"/>
  `),
  slot_base: slot('#071522E8', '#80AABD', '#456B7D', '#6493A5'),
  slot_selected: slot('#0A2632ED', '#6AEAFF', '#3FBDD1', '#B6FAFF'),
  slot_disabled: slot('#09121BD4', '#657985', '#3F5360', '#526773'),
  panel_2slot: panel(2),
  panel_3slot: panel(3),
  panel_4slot: panel(4),
  panel_flexible: flexiblePanel,
};

async function main() {
  for (const [name, svg] of Object.entries(artwork)) {
    fs.writeFileSync(path.join(svgDir, `${name}.svg`), svg);
    const width = name === 'panel_2slot' ? 520 : name === 'panel_3slot' ? 728 : name === 'panel_4slot' ? 936 : name === 'panel_flexible' ? 512 : 256;
    const height = name === 'panel_flexible' ? 192 : name.startsWith('panel_') ? 244 : 256;
    await sharp(Buffer.from(svg), { density: 192 }).resize(width, height).png().toFile(path.join(pngDir, `${name}.png`));
  }

  const labels = [
    ['weapon_dual_laser', 'DUAL LASER', '1'],
    ['weapon_beam_laser', 'BEAM LASER', '2'],
    ['weapon_burst_laser', 'BURST LASER', '3'],
    ['weapon_pulse_laser', 'PULSE LASER', '4'],
    ['weapon_spread_laser', 'SPREAD LASER', '5'],
  ];
  const preview = `<svg xmlns="http://www.w3.org/2000/svg" width="1250" height="470" viewBox="0 0 1250 470">
    <rect width="1250" height="470" fill="#0B1721"/>
    <text x="52" y="64" fill="#DCECF2" font-family="Segoe UI,Arial" font-size="28" font-weight="600">Valley Void  /  Weapon Hotbar</text>
    <text x="52" y="96" fill="#829BA7" font-family="Segoe UI,Arial" font-size="16">WHITE ICONS · SEPARATE SLOT BACKGROUNDS · 256 PX PNG</text>
    ${labels.map(([name, label, key], i) => {
      const x = 54 + i * 240;
      const selected = i === 0;
      const slotSvg = artwork[selected ? 'slot_selected' : 'slot_base'];
      const iconSvg = artwork[name];
      const slotData = Buffer.from(slotSvg).toString('base64');
      const iconData = Buffer.from(iconSvg).toString('base64');
      return `<image href="data:image/svg+xml;base64,${slotData}" x="${x}" y="150" width="174" height="174"/>
        <image href="data:image/svg+xml;base64,${iconData}" x="${x + 31}" y="180" width="112" height="112" opacity="${selected ? 1 : 0.7}"/>
        <text x="${x + 10}" y="343" fill="#EAF5F7" font-family="Segoe UI,Arial" font-size="18">${key}  ${label}</text>`;
    }).join('')}
    <text x="54" y="416" fill="#7C919C" font-family="Segoe UI,Arial" font-size="16">Loadout: 1 Dual Laser  ·  2 Beam Laser  ·  3 Burst Laser</text>
  </svg>`;
  await sharp(Buffer.from(preview)).png().toFile(path.join(root, 'preview.png'));

  const stateData = (name) => Buffer.from(artwork[name]).toString('base64');
  const statePreview = `<svg xmlns="http://www.w3.org/2000/svg" width="760" height="300" viewBox="0 0 760 300">
    <rect width="760" height="300" fill="#0B1721"/>
    <text x="34" y="48" fill="#DCECF2" font-family="Segoe UI,Arial" font-size="23" font-weight="600">Weapon Slot States</text>
    ${[['slot_base', 'NORMAL'], ['slot_disabled', 'DISABLED'], ['slot_selected', 'SELECTED']].map(([name, label], i) => {
      const x = 40 + i * 240;
      return `<image href="data:image/svg+xml;base64,${stateData(name)}" x="${x}" y="70" width="176" height="176"/>
        <text x="${x}" y="274" fill="#9FB8C2" font-family="Segoe UI,Arial" font-size="15">${label}</text>`;
    }).join('')}
  </svg>`;
  await sharp(Buffer.from(statePreview)).png().toFile(path.join(root, 'slot_states_preview.png'));

  const data = (name) => Buffer.from(artwork[name]).toString('base64');
  const panelPreview = `<svg xmlns="http://www.w3.org/2000/svg" width="1060" height="760" viewBox="0 0 1060 760">
    <rect width="1060" height="760" fill="#0B1721"/>
    <text x="52" y="61" fill="#DCECF2" font-family="Segoe UI,Arial" font-size="28" font-weight="600">Weapon Hotbar Panels</text>
    <text x="52" y="92" fill="#829BA7" font-family="Segoe UI,Arial" font-size="16">TRANSPARENT PNG · ICONS AND SLOTS REMAIN SEPARATE</text>
    <text x="52" y="143" fill="#DCECF2" font-family="Segoe UI,Arial" font-size="19">3-SLOT  /  LASER LOADOUT</text>
    <image href="data:image/svg+xml;base64,${data('panel_3slot')}" x="48" y="160" width="728" height="244"/>
    <image href="data:image/svg+xml;base64,${data('slot_base')}" x="108" y="158" width="188" height="188"/>
    <image href="data:image/svg+xml;base64,${data('slot_base')}" x="316" y="158" width="188" height="188"/>
    <image href="data:image/svg+xml;base64,${data('slot_selected')}" x="524" y="158" width="188" height="188"/>
    <image href="data:image/svg+xml;base64,${data('weapon_dual_laser')}" x="143" y="190" width="118" height="118"/>
    <image href="data:image/svg+xml;base64,${data('weapon_beam_laser')}" x="351" y="190" width="118" height="118" opacity="0.72"/>
    <image href="data:image/svg+xml;base64,${data('weapon_burst_laser')}" x="559" y="190" width="118" height="118"/>
    <text x="116" y="379" fill="#E9F4F6" font-family="Segoe UI,Arial" font-size="15">1   DUAL LASER</text>
    <text x="324" y="379" fill="#B6C9D0" font-family="Segoe UI,Arial" font-size="15">2   BEAM LASER</text>
    <text x="532" y="379" fill="#E9F4F6" font-family="Segoe UI,Arial" font-size="15">3   BURST LASER</text>
    <text x="52" y="475" fill="#DCECF2" font-family="Segoe UI,Arial" font-size="19">4-SLOT  /  EXPANSION PANEL</text>
    <image href="data:image/svg+xml;base64,${data('panel_4slot')}" x="48" y="495" width="936" height="244"/>
  </svg>`;
  await sharp(Buffer.from(panelPreview)).png().toFile(path.join(root, 'panel_preview.png'));
}

main().catch((error) => { console.error(error); process.exitCode = 1; });
