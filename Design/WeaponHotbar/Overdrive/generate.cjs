// Original Valley Void "Overdrive Laser" HUD art. PNGs are rendered from these SVGs.
const fs = require('node:fs');
const path = require('node:path');
const sharp = require('sharp');

const sourceDir = path.join(__dirname, 'svg');
const outputDir = path.resolve(__dirname, '../../../Assets/UI/WeaponHotbar/Overdrive');
fs.mkdirSync(sourceDir, { recursive: true });
fs.mkdirSync(outputDir, { recursive: true });

const frame = (width, height, body, defs = '') => `<svg xmlns="http://www.w3.org/2000/svg" width="${width}" height="${height}" viewBox="0 0 ${width} ${height}">
  <defs>${defs}</defs>${body}
</svg>\n`;

const glow = `<filter id="glow" x="-100%" y="-100%" width="300%" height="300%"><feGaussianBlur stdDeviation="5"/></filter>`;
const panel = frame(820, 250, `
  <path d="M24 47H796L817 70V221L786 247H34L3 221V70Z" fill="#06111DE8" stroke="#3B91A8" stroke-width="2"/>
  <path d="M47 60H773L804 83V213L780 235H40L16 213V83Z" fill="#0A1D2BEA" stroke="#19465C" stroke-width="2"/>
  <path d="M31 61H791" stroke="#65E7F6" stroke-width="7" opacity=".25" filter="url(#glow)"/>
  <path d="M31 61H791" stroke="#5EDEE9" stroke-width="2" opacity=".75"/>
  <path d="M40 235H780" stroke="#52D6E5" stroke-width="2" opacity=".66"/>
  <path d="M24 86V210L42 227M796 86V210L778 227" fill="none" stroke="#89D9E4" stroke-width="3" opacity=".6"/>
  <path d="M71 49H259M283 49H471" stroke="#8DEEF3" stroke-width="4" opacity=".72"/>
  <path d="M487 86H776M487 172H776" stroke="#3A8DA3" stroke-width="2" opacity=".9"/>
  <path d="M487 95L503 112V151L487 168M776 95L760 112V151L776 168" fill="none" stroke="#4CAFC2" stroke-width="2"/>
  <path d="M508 104H754M508 160H754" stroke="#6EB2C0" stroke-width="1.5" opacity=".6"/>
  <path d="M501 123H760M501 141H760" stroke="#29566A" stroke-width="2"/>
  <path d="M463 199H767" stroke="#3988A0" stroke-width="2"/>
  <path d="M467 206H760" stroke="#5DE4EE" stroke-width="6" opacity=".18" filter="url(#glow)"/>
  <path d="M467 206H760" stroke="#5DE4EE" stroke-width="2" opacity=".7"/>
  <path d="M57 216H433" stroke="#4CA1B2" stroke-width="2" opacity=".55"/>
  <path d="M112 229H162M320 229H370" stroke="#D7FBFF" stroke-width="2" opacity=".52"/>
  <path d="M479 45L496 31H557M750 31H785L803 48" fill="none" stroke="#57CADD" stroke-width="2.5"/>
  <path d="M575 31H733" stroke="#56BFD1" stroke-width="2" opacity=".62"/>
  <path d="M483 42L492 35M798 42L789 35" stroke="#B6FCFF" stroke-width="3"/>
  <path d="M490 225L499 217M751 217L760 225" stroke="#FFB75E" stroke-width="3" opacity=".8"/>
`, glow);

const panel3 = frame(1028, 250, `
  <path d="M24 47H1004L1025 70V221L994 247H34L3 221V70Z" fill="#06111DE8" stroke="#3B91A8" stroke-width="2"/>
  <path d="M47 60H981L1012 83V213L988 235H40L16 213V83Z" fill="#0A1D2BEA" stroke="#19465C" stroke-width="2"/>
  <path d="M31 61H999" stroke="#65E7F6" stroke-width="7" opacity=".25" filter="url(#glow)"/>
  <path d="M31 61H999M40 235H988" stroke="#5EDEE9" stroke-width="2" opacity=".72"/>
  <path d="M24 86V210L42 227M1004 86V210L986 227" fill="none" stroke="#89D9E4" stroke-width="3" opacity=".6"/>
  <path d="M71 49H259M283 49H471M495 49H683" stroke="#8DEEF3" stroke-width="4" opacity=".72"/>
  <path d="M695 86H984M695 172H984" stroke="#3A8DA3" stroke-width="2"/>
  <path d="M695 95L711 112V151L695 168M984 95L968 112V151L984 168" fill="none" stroke="#4CAFC2" stroke-width="2"/>
  <path d="M716 104H962M716 160H962" stroke="#6EB2C0" stroke-width="1.5" opacity=".6"/>
  <path d="M709 123H968M709 141H968" stroke="#29566A" stroke-width="2"/>
  <path d="M671 199H975M675 206H968" stroke="#3988A0" stroke-width="2"/>
  <path d="M675 206H968" stroke="#5DE4EE" stroke-width="6" opacity=".18" filter="url(#glow)"/>
  <path d="M675 206H968" stroke="#5DE4EE" stroke-width="2" opacity=".7"/>
  <path d="M57 216H641" stroke="#4CA1B2" stroke-width="2" opacity=".55"/>
  <path d="M112 229H162M320 229H370M528 229H578" stroke="#D7FBFF" stroke-width="2" opacity=".52"/>
  <path d="M687 45L704 31H765M958 31H993L1011 48" fill="none" stroke="#57CADD" stroke-width="2.5"/>
  <path d="M783 31H941" stroke="#56BFD1" stroke-width="2" opacity=".62"/>
  <path d="M698 225L707 217M959 217L968 225" stroke="#FFB75E" stroke-width="3" opacity=".8"/>
`, glow);

const slot = (active) => frame(192, 192, `
  <rect x="5" y="5" width="182" height="182" fill="${active ? '#082C3AEC' : '#061927E8'}" stroke="${active ? '#7EF4FF' : '#5D90A4'}" stroke-width="3"/>
  <rect x="19" y="19" width="154" height="154" fill="none" stroke="${active ? '#44C9E0' : '#285B70'}" stroke-width="2" opacity=".85"/>
  <path d="M35 13H158M35 179H158" stroke="${active ? '#CEFFFF' : '#6EA5B4'}" stroke-width="2" opacity="${active ? '.85' : '.48'}"/>
  <path d="M40 164H73M119 164H152" stroke="#6EDDE7" stroke-width="2" opacity="${active ? '.85' : '.45'}"/>
  <path d="M66 184H126" stroke="${active ? '#C8FFFF' : '#6EA5B4'}" stroke-width="5" stroke-linecap="round" opacity="${active ? '1' : '.52'}"/>
  ${active ? `<rect x="5" y="5" width="182" height="182" fill="none" stroke="#66EAFF" stroke-width="12" opacity=".42" filter="url(#glow)"/>
    ` : ''}
`, glow);

const dual = frame(128, 128, `
  <g fill="none" stroke-linecap="round" stroke-linejoin="round">
    <path d="M43 24V77M85 24V77" stroke="#4FE6FF" stroke-width="17" opacity=".8" filter="url(#glow)"/>
    <path d="M43 24V77M85 24V77" stroke="#5AEBFF" stroke-width="10"/>
    <path d="M43 20V76M85 20V76" stroke="#F1FFFF" stroke-width="4.5"/>
    <path d="M36 13H50M78 13H92" stroke="#B7FCFF" stroke-width="4"/>
    <path d="M34 82H52L55 91V110H31V91ZM76 82H94L97 91V110H73V91Z" fill="#0D4356" stroke="#D2FFFF" stroke-width="4"/>
    <path d="M43 109V117M85 109V117" stroke="#6FE8FA" stroke-width="4"/>
  </g>
`, glow);

const beam = frame(128, 128, `
  <g fill="none" stroke-linecap="round" stroke-linejoin="round">
    <path d="M64 11V83" stroke="#48DFF8" stroke-width="26" opacity=".85" filter="url(#glow)"/>
    <path d="M64 11V83" stroke="#4DDDF2" stroke-width="16"/>
    <path d="M64 8V81" stroke="#FFFFFF" stroke-width="6"/>
    <path d="M52 18H76M46 42H53M75 42H82M45 65H52M76 65H83" stroke="#A8F7FF" stroke-width="3"/>
    <path d="M51 88H77L84 98L76 112H52L44 98Z" fill="#10475A" stroke="#D7FFFF" stroke-width="4"/>
    <path d="M64 112V121" stroke="#69E9FA" stroke-width="4"/>
  </g>
`, glow);

const burstLaser = frame(128, 128, `
  <g fill="none" stroke-linecap="round" stroke-linejoin="round">
    <path d="M63 15L71 31L84 21L84 38L103 34L91 49L106 60L87 64L91 79L73 70L64 83L54 70L36 79L40 63L22 58L38 47L27 34L47 38L48 21L59 31Z" stroke="#46E4FC" stroke-width="13" opacity=".8" filter="url(#glow)"/>
    <path d="M63 15L71 31L84 21L84 38L103 34L91 49L106 60L87 64L91 79L73 70L64 83L54 70L36 79L40 63L22 58L38 47L27 34L47 38L48 21L59 31Z" fill="#126078" fill-opacity=".64" stroke="#C9FCFF" stroke-width="3.5"/>
    <circle cx="64" cy="51" r="13" fill="#FF9C42" opacity=".7" filter="url(#glow)"/>
    <circle cx="64" cy="51" r="8" fill="#FFF9E6" stroke="#FFB55E" stroke-width="2"/>
    <path d="M64 84V91M54 92H74L80 101L73 113H55L48 101Z" fill="#10475A" stroke="#D7FFFF" stroke-width="4"/>
    <path d="M64 113V120" stroke="#6DE9FA" stroke-width="4"/>
    <path d="M18 22L24 17M106 18L112 13M111 80L117 86" stroke="#FFB65C" stroke-width="3.5"/>
  </g>
`, glow);

const burst = frame(240, 240, `
  <g fill="none" stroke-linecap="round">
    <circle cx="120" cy="100" r="72" stroke="#57E7F5" stroke-width="12" opacity=".38" filter="url(#glow)"/>
    <path d="M120 4V23M46 25L64 43M199 25L179 43M8 98L34 98M232 98L206 98M29 177L51 158M211 177L188 158" stroke="#56E9FB" stroke-width="15" opacity=".5" filter="url(#glow)"/>
    <path d="M120 4V23M46 25L64 43M199 25L179 43M8 98L34 98M232 98L206 98M29 177L51 158M211 177L188 158" stroke="#A6FAFF" stroke-width="3"/>
    <path d="M73 33A74 74 0 0 1 101 28M141 28A74 74 0 0 1 169 39M190 85A74 74 0 0 1 187 120M60 154A74 74 0 0 1 45 121" stroke="#80F3FA" stroke-width="3" opacity=".84"/>
    <path d="M177 42L189 34L185 49ZM198 137L213 144L195 149ZM45 69L28 63L38 80ZM64 180L55 195L51 177Z" fill="#FFB65C" stroke="#FFF0C2" stroke-width="2"/>
  </g>
`, glow);

const charge = frame(256, 30, `
  <path d="M8 4H247L252 15L247 26H8L3 15Z" fill="#0D3645" stroke="#5AD7E5" stroke-width="2"/>
  <path d="M13 9H226L235 15L226 21H13Z" fill="#3FE3F3" opacity=".8"/>
  <path d="M13 12H226" stroke="#E9FFFF" stroke-width="5" opacity=".9"/>
  <path d="M221 7L242 15L221 23" fill="#FFB960" stroke="#FFD39A" stroke-width="2"/>
  <path d="M26 9V21M56 9V21M86 9V21M116 9V21M146 9V21M176 9V21M206 9V21" stroke="#154A5C" stroke-width="3"/>
`);

const flexibleBackplate = frame(512, 192, `
  <path d="M28 3H484L509 28V164L484 189H28L3 164V28Z" fill="#06111DE8" stroke="#4BAFC4" stroke-width="3"/>
  <path d="M34 18H478M34 174H478" stroke="#5BE6F0" stroke-width="2" opacity=".78"/>
  <path d="M17 54V31L31 17M495 54V31L481 17" fill="none" stroke="#A8E8EF" stroke-width="2"/>
  <path d="M31 183H481" stroke="#6CF5FF" stroke-width="7" opacity=".28" filter="url(#glow)"/>
  <path d="M31 183H481" stroke="#6CF5FF" stroke-width="2" opacity=".74"/>
`, glow);

const assets = {
  overdrive_panel: panel,
  overdrive_panel_3slot: panel3,
  overdrive_slot_idle: slot(false),
  overdrive_slot_active: slot(true),
  overdrive_dual_laser: dual,
  overdrive_beam_laser: beam,
  overdrive_burst_laser: burstLaser,
  overdrive_burst_fx: burst,
  overdrive_charge_fill: charge,
  overdrive_backplate_flexible: flexibleBackplate,
};

async function main() {
  for (const [name, svg] of Object.entries(assets)) {
    fs.writeFileSync(path.join(sourceDir, `${name}.svg`), svg);
    await sharp(Buffer.from(svg)).png().toFile(path.join(outputDir, `${name}.png`));
  }

  for (const state of ['idle', 'active']) {
    await sharp({ create: { width: 192, height: 192, channels: 4, background: '#00000000' } })
      .composite([
        { input: path.join(outputDir, `overdrive_slot_${state}.png`), left: 0, top: 0 },
        { input: path.join(outputDir, 'overdrive_burst_laser.png'), left: 32, top: 32 },
      ]).png().toFile(path.join(outputDir, `overdrive_burst_slot_${state}.png`));
  }

  const composite = [
    ['overdrive_panel', 0, 0],
    ['overdrive_slot_idle', 55, 12],
    ['overdrive_burst_fx', 241, -15],
    ['overdrive_slot_active', 263, 12],
    ['overdrive_dual_laser', 87, 43],
    ['overdrive_beam_laser', 295, 43],
    ['overdrive_charge_fill', 504, 120],
  ];
  const layers = composite.map(([name, left, top]) => ({ input: path.join(outputDir, `${name}.png`), left, top }));
  // The burst extends above the panel, so render it on a padded canvas.
  const padded = layers.map((layer) => ({ ...layer, top: layer.top + 24 }));
  await sharp({ create: { width: 820, height: 290, channels: 4, background: '#00000000' } })
    .composite(padded).png().toFile(path.join(outputDir, 'overdrive_complete.png'));

  const layers3 = [
    ['overdrive_panel_3slot', 0, 24],
    ['overdrive_slot_idle', 55, 36],
    ['overdrive_slot_idle', 263, 36],
    ['overdrive_burst_fx', 449, 9],
    ['overdrive_slot_active', 471, 36],
    ['overdrive_dual_laser', 87, 67],
    ['overdrive_beam_laser', 295, 67],
    ['overdrive_burst_laser', 503, 67],
    ['overdrive_charge_fill', 712, 144],
  ].map(([name, left, top]) => ({ input: path.join(outputDir, `${name}.png`), left, top }));
  await sharp({ create: { width: 1028, height: 290, channels: 4, background: '#00000000' } })
    .composite(layers3).png().toFile(path.join(outputDir, 'overdrive_complete_3slot.png'));

  const previewSvg = frame(1370, 540, `
    <rect width="1370" height="540" fill="#08121D"/>
    <path d="M0 150H1370M0 350H1370" stroke="#173549" stroke-width="1"/>
    <circle cx="1110" cy="140" r="180" fill="#18465A" opacity=".25"/>
    <text x="72" y="80" fill="#E6FCFF" font-family="Segoe UI,Arial" font-weight="700" font-size="30">OVERDRIVE / LASER ARRAY</text>
    <text x="74" y="112" fill="#70ACBB" font-family="Segoe UI,Arial" font-size="15">VALLEY VOID · THREE WEAPONS · BURST LASER SELECTED</text>
    <image href="data:image/png;base64,${fs.readFileSync(path.join(outputDir, 'overdrive_complete_3slot.png')).toString('base64')}" x="80" y="168" width="1200" height="339"/>
    <text x="195" y="466" fill="#BEDDE5" font-family="Segoe UI,Arial" font-size="16">01  DUAL LASER</text>
    <text x="445" y="466" fill="#BEDDE5" font-family="Segoe UI,Arial" font-size="16">02  BEAM LASER</text>
    <text x="688" y="466" fill="#E5FFFF" font-family="Segoe UI,Arial" font-weight="700" font-size="16">03  BURST LASER</text>
    <text x="916" y="300" fill="#84BFC9" font-family="Segoe UI,Arial" font-size="15">OUTPUT</text>
  `);
  await sharp(Buffer.from(previewSvg)).png().toFile(path.join(__dirname, 'preview.png'));
}

main().catch((error) => { console.error(error); process.exitCode = 1; });
