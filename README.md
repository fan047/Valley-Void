# Canyon Fighter

一款使用 Unity 制作的 3D 太空战斗原型。驾驶战机穿梭峡谷地形，以鼠标瞄准并摧毁敌方飞船。

## 功能

- 玩家飞船的平面移动、俯仰与横滚反馈
- 鼠标十字准星与激光瞄准
- 按住射击与粒子特效
- 敌机碰撞检测、爆炸特效与销毁
- 可编辑的飞船预制体、地形、材质与 Timeline 资源

## 操作

| 操作 | 按键 / 输入 |
| --- | --- |
| 移动 | `W` `A` `S` `D` |
| 瞄准 | 移动鼠标 |
| 开火 | 按住鼠标左键 |

## 运行项目

1. 使用 **Unity 6000.4.3f1**（Unity 6）或兼容版本打开此项目。
2. 等待 Unity 完成包与资源导入。
3. 打开 `Assets/Scenes/Main Level.unity`。
4. 在编辑器中按 Play 运行。

`Main Level` 已配置为构建场景。项目使用 Universal Render Pipeline（URP）与 Unity Input System；首次打开时，Unity 会自动还原 Packages 中列出的依赖。

## 项目结构

```text
Assets/
├── Input/       # Input System 操作映射
├── Prefabs/     # 飞船、激光与特效预制体
├── Scenes/      # Main Level 与 Ships 场景
├── Scripts/     # 玩家移动、武器、敌人和碰撞逻辑
├── Terrain/     # 地形及数据资源
└── Timelines/   # Timeline 资源
```

## 技术栈

- Unity 6（6000.4.3f1）
- C#
- Universal Render Pipeline 17.4.0
- Unity Input System 1.19.0
- Unity Timeline 1.8.12

## 版本控制

仓库已包含适用于 Unity 的 `.gitignore`，不会提交 `Library`、构建产物和本地编辑器缓存。

