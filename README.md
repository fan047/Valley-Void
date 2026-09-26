# Valley Void

Valley Void 是一款开发中的 3D 峡谷飞行射击游戏原型。玩家驾驶飞船在场景中移动，用鼠标瞄准并持续发射激光，击毁沿时间轴出现的敌机以获得分数。

## 当前进度

- 玩家飞船支持 `W` `A` `S` `D` 移动，以及随输入变化的俯仰和横滚反馈。
- 鼠标控制准星与激光瞄准；按住鼠标左键持续开火。
- 敌机具有命中判定、生命值、击毁特效和计分逻辑。
- 敌机生成已接入对象池；场景中使用 Timeline 与 Signal 安排敌机波次。
- 已有峡谷地形、天空盒、暂停菜单和分数显示。

当前仍是 Unity 编辑器中的开发原型，仓库没有提供可直接运行的发行版。

## 操作

| 操作 | 按键 |
| --- | --- |
| 移动飞船 | `W` `A` `S` `D` |
| 瞄准 | 移动鼠标 |
| 持续开火 | 按住鼠标左键 |
| 暂停 / 继续 | `Esc` |

## 技术栈

- **引擎与语言：** Unity 6（编辑器版本 `6000.4.3f1`）、C#。
- **渲染：** Universal Render Pipeline（URP）`17.4.0`；项目还安装了 Shader Graph `17.4.0`。
- **输入：** Unity Input System `1.19.0`。
- **关卡编排：** Unity Timeline `1.8.12` 与 Signal。
- **界面：** Unity UI（uGUI）`2.0.0`、TextMesh Pro。
- **敌机复用：** Unity `ObjectPool<GameObject>`。
- **地形工具：** Unity Terrain Tools `5.3.3`。

依赖版本以 [`Packages/manifest.json`](Packages/manifest.json) 为准；项目同时安装了 HDRP 包，但当前图形设置引用的是 URP 资源。

## 在 Unity 中运行

1. 使用 Unity `6000.4.3f1` 打开仓库根目录，等待包和资源导入完成。
2. 打开 [`Assets/Scenes/Main Level.unity`](Assets/Scenes/Main%20Level.unity)。
3. 在编辑器中按 **Play**。

`Main Level` 已列入构建场景。首次打开时，Unity 会根据 `Packages/manifest.json` 还原依赖。

## 项目结构

```text
Assets/
├── Input/               输入动作配置
├── Prefabs/             玩家、敌机、激光与特效预制体
├── Scenes/              主关卡与飞船展示场景
├── Scripts/Player/      移动、武器、碰撞与暂停逻辑
├── Scripts/Enemy/       敌机、命中判定、对象池与生成器
├── Terrain/              地形资源
└── Timelines/            主时间轴、敌机时间轴与 Signal
```

第三方导入资源位于 `Assets/Imported Assets/`；其随附的说明和许可文件保留在各自目录中。
