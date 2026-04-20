2.5D 极简弹幕游戏 Demo 

> 一个基于 Unity 开发的极简游戏 熬了3个通宵把Unity基础吃下然后做完了Demo 有点赶时间所以就做的比较简单 但后续会自己慢慢完善 也是给自己留一个纪念


演示

<img width="943" height="484" alt="DemoFinished" src="https://github.com/user-attachments/assets/324f317a-08d6-4e08-b85e-50ccf68f5c93" />

核心

1.对象池高频弹幕与敌人的生成/回收逻辑。支持同屏100物体高频碰撞，运行期间内存零抖动。

2.利用 Unity 粒子系统和一点点的Bloom实现了击打的爆炸效果

3.完整的游戏循环包含动态生成规则、碰撞击杀判定、实时 UI 计分与漏怪扣血逻辑，具备基础的单局游戏逻辑。

4.采用单例模式搭建全局管理器；过程全部在commit里面了，游戏比较简单就没有commit很多。

5.敌人速度会随时间不同而随机产生,甚至在行动过程中也会突然加速增加一点游戏难度。

操作说明

移动：WASD Lshift 加速 
开火：按住 `Space` (空格键)
目标：在倒计时结束前尽可能多地击毁红色几何体。

开发环境

引擎版本： Unity 6 (6000.3.10f1）LTS
编程语言：C#
渲染管线：URP
IDE:Rider

导读

如果您时间有限，推荐优先查看以下核心代码文件：
1.  `Assets/Scripts/BulletPool3D.cs` —— 泛型对象池的实现。
2.  `Assets/Scripts/GameManager.cs` —— 全局状态机与得分以及倒计时。
