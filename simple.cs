
// https://www.51cto.com/article/488588.html
//影响BA的数据有很多，比如移动速度 攻击力 基础属性 等等，影响的入口也有很多：

// 技能

// buff/被动技能

// 装备

// 强化

// 宝石

// 魂

// 等等，而这些实际上从影响结果没什么区别。

// 首先我们先谈区别，对于这些数值影响，其实区别只有入口或者说是作用的方式，

// 技能是BA(castor)对BA(target)释放造成的瞬间数值影响。

// buff是castor对BA(target)安装后造成的持续数值影响，分为按时触发瞬发和持续修改数值。

// 装备是特定容器对BA持续修改数值。

// 所以这里游戏开发者们抽象出了 效果这个概念。

// 对与效果而言，只存在2个行为：

// 对BA产生数值影响
// 对BA撤销数值影响
// 所以效果最终定义为：
interface Effect { 
   void cast(BattleAgent target); 
   default void reverse(){ 
   } 
} 

// 其他实体
interface EffectContainer extends Effect{ 
  List<Effect> getEffects(); 
} 

// 技能
class abstract  Skill implements EffectContainer{ 
    public void spellTo(BattleAgent target){ 
      foreach(Effect effect in getEffects()){ 
         effect.cast(target); 
      }     
   }  
} 

// buff
class abstract Buff implements EffectContainer{ 
    public void update(){ 
       foreach(Effect effect in getEffects()){ 
         effect.cast(target); 
      }     
    } 
} 


// 被动也是buff，只不过可以安装和卸载
class abstract  BuffSkill extends Buff { 
    public void install(){ 
       foreach(Effect effect in getEffects()){ 
         effect.cast(target); 
      }     
    } 
 
    public void unstall(){ 
       foreach(Effect effect in getEffects()){ 
         effect.reverse(target); 
      }     
    } 
} 

// 实现
class DamageEffect implements Effect{ 
   private int damage = 100; 
   public void cast(BattleAgent target){ 
     target.hp -= damage; 
    } 
} 

// 变羊
class ChangSheepEffect implements Effect{ 
    
   public void cast(BattleAgent target){ 
     target.gameObject = GameManager.getAnimeObject("sheep"); 
    } 
} 

// 攻击力和防御力变0
class PropChangeEffect implements Effect{ 
    
   public void cast(BattleAgent target){ 
     target.atk = 0; 
     target.def = 0; 
     target.speed = 50; 
    } 
} 

// 触发效果
class TriggerBuffEffect implements Effect{ 
  BuffSkill buff = new BuffSkill (){ 
    public List<>getEffects(){ 
    return new List().add(new ChangSheepEffect()).add(new PropChangeEffect());  
} 
} 
   public void cast(BattleAgent target){ 
     int time = 3000;//3秒 
     target.addBuff(buff,time); 
    } 
} 