# Valley Void

**独立开发的 3D 峡谷飞行射击游戏 · 持续开发中**

驾驶飞船穿越峡谷，用鼠标瞄准并击毁沿关卡路线出现的敌机。游戏包含双武器切换、敌机波次、计分，以及开始、暂停、胜利和失败结算流程。

**[在线试玩（itch.io）](https://fan047.itch.io/valley-void)** · [作品集介绍](https://myportfolio-bdo.pages.dev/#project)

> 在线试玩是当前发布的版本；仓库中的 Unity 项目可能包含尚未发布的改动。

## 玩法与操作

- `W` `A` `S` `D`：控制飞船在画面内移动；飞船会随输入产生俯仰和横滚反馈。
- 移动鼠标：移动准星并调整射击方向。
- 按住鼠标左键：持续开火。
- `1` / `2`：切换双发激光 / 光束激光。
- `Esc`：暂停或继续游戏。

击毁敌机可以获得分数。飞船发生碰撞会进入失败结算；关卡时间轴结束后进入胜利结算。结算界面可重新开始游戏。

## 已实现的功能

- **飞船与射击：** 使用 Unity Input System 处理移动、开火和武器切换；限制飞船活动范围，并用旋转插值表现操控反馈。双发激光使用粒子碰撞判定命中，光束激光使用 `Physics.Raycast` 判定命中并由 `LineRenderer` 绘制光束。
- **武器选择界面：** 将武器选择拆分为 Model、Controller、View。Controller 接收按键，Model 保存当前武器并发出 C# 事件，View 订阅事件更新快捷栏显示。
- **敌机波次：** 使用 Timeline 与 Signal 控制波次生成和关卡结束；按敌机类型建立 `ObjectPool<GameObject>`，在敌机再次出场时重置生命值和飞行时间轴，减少重复实例化。
- **战斗反馈：** 敌机拥有生命值、命中判定、击毁特效和计分逻辑；场景内显示准星、敌机位置标记及分数。
- **游戏流程：** 通过 `Playing`、`Paused`、`GameOver`、`Victory` 状态和状态变更事件，联动暂停菜单、结算界面、音乐与鼠标状态。
- **参数配置：** 使用 `GameBalanceConfig`（ScriptableObject）集中配置飞船操控、敌机生命值与分数、武器伤害及射程。

## 技术栈

- **引擎与语言：** Unity 6（`6000.4.3f1`）、C#。
- **渲染与界面：** URP、uGUI、TextMesh Pro、粒子系统、`LineRenderer`。
- **输入与关卡：** Input System、Timeline、Signal。
- **数据与对象管理：** ScriptableObject、C# 事件、`ObjectPool<GameObject>`。

具体依赖版本见 [`Packages/manifest.json`](Packages/manifest.json)。

## 在 Unity 中运行

1. 使用 Unity Hub 以 **Unity `6000.4.3f1`** 打开仓库根目录，等待资源和 Package 导入完成。
2. 打开 [`Assets/Scenes/Title Screen.unity`](Assets/Scenes/Title%20Screen.unity)，按 **Play**，点击“开始游戏”进入主关卡。
3. 若要直接调试战斗场景，可打开 [`Assets/Scenes/Main Level.unity`](Assets/Scenes/Main%20Level.unity) 并按 **Play**。

`Title Screen` 与 `Main Level` 均已加入构建场景。游戏目前仍在迭代；试玩页面与仓库源码的更新节奏可能不同。

## 项目目录

```text
Assets/
├── Input/            输入动作配置
├── Prefabs/          飞船、敌机、激光和特效预制体
├── Scenes/           标题界面与主关卡
├── Scripts/
│   ├── Config/       游戏参数配置
│   ├── Enemy/        敌机、对象池与波次生成
│   ├── GameState/    游戏状态、菜单和结算
│   └── Player/       飞船移动、武器与碰撞
├── Timelines/        波次、敌机路线和 Signal
└── UI/               游戏界面资源
```

项目包含位于 `Assets/Imported Assets/` 的第三方资源；相关说明和许可文件保留在各资源目录中。
