// Original Valley Void victory UI. Requires Node.js and sharp to regenerate PNGs.
const fs = require('node:fs');
const path = require('node:path');
const sharp = require('sharp');

const sourceDir = path.join(__dirname, 'svg');
const outputDir = path.resolve(__dirname, '../../Assets/UI/VictoryPanel');
fs.mkdirSync(sourceDir, { recursive: true });
fs.mkdirSync(outputDir, { recursive: true });

const glow = `<filter id="glow" x="-100%" y="-100%" width="300%" height="300%"><feGaussianBlur stdDeviation="8"/></filter>`;
const wrap = (w, h, body, defs = '') => `<svg xmlns="http://www.w3.org/2000/svg" width="${w}" height="${h}" viewBox="0 0 ${w} ${h}"><defs>${defs}</defs>${body}</svg>\n`;

const backdrop = wrap(1920, 1080, `
  <rect width="1920" height="1080" fill="#03101B" opacity=".76"/>
  <ellipse cx="960" cy="510" rx="630" ry="420" fill="#0A5F70" opacity=".18" filter="url(#glow)"/>
  <path d="M0 180H480M1440 180H1920M0 904H480M1440 904H1920" stroke="#66C9D4" stroke-width="2" opacity=".18"/>
`, glow);

const frame = wrap(900, 610, `
  <path d="M66 14H834L885 65V551L834 596H66L15 551V65Z" fill="#071622F2" stroke="#55AFC0" stroke-width="3"/>
  <path d="M82 33H818L862 77V535L816 575H84L38 535V77Z" fill="none" stroke="#2D6174" stroke-width="2"/>
  <path d="M95 37H805" stroke="#5DE8F4" stroke-width="14" opacity=".26" filter="url(#glow)"/>
  <path d="M95 37H805" stroke="#75EAF2" stroke-width="3" opacity=".9"/>
  <path d="M109 571H791" stroke="#62CED8" stroke-width="2" opacity=".7"/>
  <path d="M68 85V534L91 556M832 85V534L809 556" fill="none" stroke="#6BCEDE" stroke-width="3" opacity=".7"/>
  <path d="M70 83L91 62H176M830 83L809 62H724" fill="none" stroke="#C3F7FC" stroke-width="3" opacity=".85"/>
  <path d="M190 62H333M567 62H710" stroke="#F0B962" stroke-width="3" opacity=".8"/>
  <path d="M103 257H797M103 445H797" stroke="#285468" stroke-width="2"/>
  <path d="M100 464L121 484V536M800 464L779 484V536" fill="none" stroke="#3D7890" stroke-width="2"/>
  <path d="M132 550H270M630 550H768" stroke="#67D8E2" stroke-width="2" opacity=".5"/>
  <path d="M107 274H169M731 274H793" stroke="#F3B968" stroke-width="3" opacity=".72"/>
  <path d="M104 430H162M738 430H796" stroke="#F3B968" stroke-width="3" opacity=".55"/>
  <path d="M88 354H116M784 354H812" stroke="#8AE4E8" stroke-width="4"/>
  <path d="M80 98L104 72M820 72L844 98M81 525L106 548M819 548L844 525" stroke="#F6BD6D" stroke-width="3"/>
`, glow);

const emblem = wrap(200, 150, `
  <path d="M100 12V37M46 27L61 47M154 27L139 47M25 73H51M175 73H149" stroke="#FBCB78" stroke-width="12" opacity=".5" filter="url(#glow)"/>
  <path d="M100 12V37M46 27L61 47M154 27L139 47M25 73H51M175 73H149" stroke="#FFE0A4" stroke-width="3" stroke-linecap="round"/>
  <path d="M100 37L108 59L132 60L114 74L120 97L100 84L80 97L86 74L68 60L92 59Z" fill="#F7BC64" stroke="#FFF4D7" stroke-width="3"/>
  <path d="M24 100L74 113L94 136L58 125ZM176 100L126 113L106 136L142 125Z" fill="#12495C" stroke="#84EDF4" stroke-width="4" stroke-linejoin="round"/>
  <path d="M65 118H135" stroke="#F6C36D" stroke-width="3"/>
`, glow);

const title = wrap(600, 105, `
  <text x="300" y="72" text-anchor="middle" fill="#69EAF6" opacity=".5" filter="url(#glow)" font-family="Segoe UI,Arial" font-size="74" font-weight="700" letter-spacing="10">VICTORY</text>
  <text x="300" y="72" text-anchor="middle" fill="#F1FCFF" font-family="Segoe UI,Arial" font-size="74" font-weight="700" letter-spacing="10">VICTORY</text>
  <path d="M161 90H439" stroke="#F3BF70" stroke-width="4" stroke-linecap="round"/>
`, glow);

const score = wrap(500, 120, `
  <path d="M18 4H482L497 19V101L482 116H18L3 101V19Z" fill="#0A2735EB" stroke="#5AA6B9" stroke-width="2.5"/>
  <path d="M23 18H477M23 102H477" stroke="#438DA2" stroke-width="2"/>
  <path d="M123 23V97" stroke="#5CBAC9" stroke-width="2" opacity=".75"/>
  <path d="M33 60H88M413 60H468" stroke="#F4BD69" stroke-width="3" opacity=".7"/>
`);

const button = (primary) => wrap(278, 76, `
  <path d="M20 4H258L274 20V56L258 72H20L4 56V20Z" fill="${primary ? '#0F485B' : '#0A2735'}" stroke="${primary ? '#78F3F7' : '#5A9AAF'}" stroke-width="3"/>
  <path d="M27 14H251M27 62H251" stroke="${primary ? '#C0FFFF' : '#70B8C5'}" stroke-width="2" opacity=".72"/>
  ${primary ? '<path d="M31 69H247" stroke="#F4BD67" stroke-width="3"/>' : ''}
`, glow);

const flexible = wrap(512, 256, `
  <path d="M30 3H482L509 30V226L482 253H30L3 226V30Z" fill="#071622F2" stroke="#5CBACA" stroke-width="3"/>
  <path d="M36 18H476M36 238H476" stroke="#72E5EB" stroke-width="2" opacity=".8"/>
  <path d="M18 55V32L32 18M494 55V32L480 18" fill="none" stroke="#B0EDF2" stroke-width="2"/>
`, glow);

const artwork = {
  victory_backdrop: backdrop,
  victory_frame: frame,
  victory_emblem: emblem,
  victory_title: title,
  victory_score_card: score,
  victory_button_primary: button(true),
  victory_button_secondary: button(false),
  victory_backplate_flexible: flexible,
};

async function main() {
  for (const [name, svg] of Object.entries(artwork)) {
    fs.writeFileSync(path.join(sourceDir, `${name}.svg`), svg);
    await sharp(Buffer.from(svg)).png().toFile(path.join(outputDir, `${name}.png`));
  }

  const layer = (name, left, top) => ({ input: path.join(outputDir, `${name}.png`), left, top });
  const composition = [
    layer('victory_frame', 0, 0),
    layer('victory_emblem', 350, 40),
    layer('victory_title', 150, 170),
    layer('victory_score_card', 200, 287),
    layer('victory_button_primary', 160, 469),
    layer('victory_button_secondary', 462, 469),
  ];
  await sharp({ create: { width: 900, height: 610, channels: 4, background: '#00000000' } })
    .composite(composition).png().toFile(path.join(outputDir, 'victory_layout.png'));

  const layoutBase64 = fs.readFileSync(path.join(outputDir, 'victory_layout.png')).toString('base64');
  const preview = wrap(1200, 780, `
    <rect width="1200" height="780" fill="#09131E"/>
    <path d="M0 230H1200M0 570H1200" stroke="#173D4B" stroke-width="2"/>
    <ellipse cx="600" cy="350" rx="450" ry="310" fill="#12475A" opacity=".2"/>
    <image href="data:image/png;base64,${layoutBase64}" x="150" y="84" width="900" height="610"/>
    <text x="600" y="362" text-anchor="middle" fill="#9CD5DF" font-family="Segoe UI,Arial" font-size="22" letter-spacing="5">MISSION COMPLETE</text>
    <text x="410" y="446" text-anchor="middle" fill="#87C5D1" font-family="Segoe UI,Arial" font-size="19" letter-spacing="2">SCORE</text>
    <text x="660" y="456" text-anchor="middle" fill="#F2FFFF" font-family="Segoe UI,Arial" font-size="48" font-weight="700" letter-spacing="4">012450</text>
    <text x="449" y="605" text-anchor="middle" fill="#E8FFFF" font-family="Segoe UI,Arial" font-size="22" font-weight="700" letter-spacing="2">RESTART</text>
    <text x="751" y="605" text-anchor="middle" fill="#C5DFE5" font-family="Segoe UI,Arial" font-size="22" letter-spacing="2">MAIN MENU</text>
  `);
  await sharp(Buffer.from(preview)).png().toFile(path.join(__dirname, 'preview.png'));
}

main().catch((error) => { console.error(error); process.exitCode = 1; });
