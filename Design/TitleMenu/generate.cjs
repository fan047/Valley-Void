const fs = require('fs');
const path = require('path');
const sharp = require('sharp');

const root = path.resolve(__dirname, '../..');
const source = path.join(__dirname, 'svg');
const output = path.join(root, 'Assets/UI/TitleMenu');
fs.mkdirSync(source, { recursive: true });
fs.mkdirSync(output, { recursive: true });

const wrap = (w, h, body) => `<svg xmlns="http://www.w3.org/2000/svg" width="${w}" height="${h}" viewBox="0 0 ${w} ${h}">${body}</svg>`;

// Large type carries the composition. The three short lines recall a ship's
// flight path without turning the title into another HUD frame.
const title = wrap(1050, 250, `
  <g font-family="Bahnschrift, Segoe UI, sans-serif">
    <rect x="12" y="28" width="45" height="4" fill="#67DDED"/>
    <text x="76" y="47" fill="#8ABAC7" font-size="39" font-weight="600" letter-spacing="16">VALLEY</text>
    <text x="4" y="184" fill="#F1FAFC" font-size="157" font-weight="700" letter-spacing="11">VOID</text>
  </g>
  <path d="M10 222 H230 M245 222 H312 M325 222 H355" fill="none" stroke="#6ADBEA" stroke-width="3" opacity=".86"/>
  <path d="M375 222 L391 214 L407 222 L391 230 Z" fill="#D8A95B"/>
`);

function button(label, primary, hover) {
  const stroke = primary ? (hover ? '#B0F7FF' : '#66DDEB') : (hover ? '#8CCCD7' : '#527582');
  const fill = primary ? (hover ? '#16566A' : '#103744') : (hover ? '#132C38' : '#0C202B');
  const color = primary ? '#F2FCFD' : (hover ? '#E3F5F7' : '#A4BBC4');
  const accent = primary ? (hover ? '#D9FCFF' : '#74E5F0') : (hover ? '#9AC9D2' : '#658995');
  return wrap(400, 82, `
    <rect x="2" y="2" width="396" height="78" rx="2" fill="${fill}" stroke="${stroke}" stroke-width="2"/>
    <rect x="2" y="2" width="5" height="78" fill="${accent}"/>
    <text x="41" y="54" fill="${color}" font-family="Bahnschrift, Segoe UI, sans-serif" font-size="31" font-weight="600" letter-spacing="5">${label}</text>
    <path d="M351 31 L364 41 L351 51" fill="none" stroke="${accent}" stroke-width="3" stroke-linejoin="miter"/>
  `);
}

const assets = {
  title_valley_void: title,
  button_start: button('START', true, false),
  button_start_highlighted: button('START', true, true),
  button_quit: button('QUIT', false, false),
  button_quit_highlighted: button('QUIT', false, true),
};

async function main() {
  for (const [name, svg] of Object.entries(assets)) {
    fs.writeFileSync(path.join(source, `${name}.svg`), svg);
    await sharp(Buffer.from(svg)).png().toFile(path.join(output, `${name}.png`));
  }

  const preview = wrap(1280, 720, `
    <defs>
      <linearGradient id="sky" x2="1" y2="1"><stop stop-color="#050D15"/><stop offset="1" stop-color="#0B1E2A"/></linearGradient>
      <linearGradient id="fade" x2="1" y2="0"><stop stop-color="#07111B" stop-opacity=".94"/><stop offset=".66" stop-color="#07111B" stop-opacity=".58"/><stop offset="1" stop-color="#07111B" stop-opacity=".05"/></linearGradient>
    </defs>
    <rect width="1280" height="720" fill="url(#sky)"/>
    <circle cx="1040" cy="152" r="93" fill="#B9CCD3" opacity=".12"/>
    <path d="M580 460 L770 352 L915 370 L1018 276 L1280 400 V720 H580Z" fill="#12232D"/>
    <path d="M710 720 L870 512 L982 550 L1098 375 L1280 433 V720Z" fill="#1B2B34"/>
    <path d="M1015 720 L1105 587 L1178 615 L1280 508 V720Z" fill="#28353A"/>
    <path d="M784 720 L943 568 L1002 583 L857 720Z" fill="#42606A" opacity=".22"/>
    <path d="M848 720 L996 602" stroke="#74D9EA" stroke-width="2" opacity=".42"/>
    <path d="M1024 280 L1048 286 L1033 289 L1024 304 L1018 291 L999 285 Z" fill="#D8AD67" opacity=".92"/>
    <rect width="1280" height="720" fill="url(#fade)"/>
  `);
  await sharp(Buffer.from(preview)).png().composite([
    { input: await sharp(path.join(output, 'title_valley_void.png')).resize({ width: 700 }).toBuffer(), left: 76, top: 128 },
    { input: await sharp(path.join(output, 'button_start.png')).toBuffer(), left: 83, top: 427 },
    { input: await sharp(path.join(output, 'button_quit.png')).toBuffer(), left: 83, top: 526 },
  ]).toFile(path.join(__dirname, 'preview.png'));
}

main().catch(error => { console.error(error); process.exitCode = 1; });
